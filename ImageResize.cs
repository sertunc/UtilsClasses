public class ImageResize{
        private double _mWidth, _mHeight;
        private bool _mUseAspect = true;
        private bool _mUsePercentage;
        private string _mImagePath;
        private Image _mSrcImage, _mDstImage;
        private ImageResize _mCache;
        private Graphics _mGraphics;

        public string File {
            get { return _mImagePath; }
            set { _mImagePath = value; }
        }

        public Image Image {
            get { return _mSrcImage; }
            set { _mSrcImage = value; }
        }

        public bool PreserveAspectRatio {
            get { return _mUseAspect; }
            set { _mUseAspect = value; }
        }

        public bool UsePercentages {
            get { return _mUsePercentage; }
            set { _mUsePercentage = value; }
        }

        public double Width {
            get { return _mWidth; }
            set { _mWidth = value; }
        }

        public double Height {
            get { return _mHeight; }
            set { _mHeight = value; }
        }

        protected virtual bool IsSameSrcImage(ImageResize other) {
            if (other != null)
                return (this.File == other.File && this.Image == other.Image);

            return false;
        }

        protected virtual bool IsSameDstImage(ImageResize other) {
            if (other != null)
                return (this.Width == other.Width && this.Height == other.Height && this.UsePercentages == other.UsePercentages && this.PreserveAspectRatio == other.PreserveAspectRatio);

            return false;
        }

        public virtual Image Rotate(int angle) {
            if (!IsSameSrcImage(_mCache)) {
                if (_mImagePath.Length > 0) {
                    // Load via stream rather than Image.FromFile to release the file
                    // handle immediately
                    if (_mSrcImage != null)
                        _mSrcImage.Dispose();

                    // Wrap the FileStream in a "using" directive, to ensure the handle
                    // gets closed when the object goes out of scope
                    using (var stream = new FileStream(_mImagePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                        _mSrcImage = Image.FromStream(stream);
                }
            }
            RotateFlipType rf = RotateFlipType.RotateNoneFlipNone;
            if (angle > 0 && angle <= 90)
                rf = RotateFlipType.Rotate90FlipNone;
            else if (angle > 90 && angle <= 180)
                rf = RotateFlipType.Rotate180FlipNone;
            else if (angle > 180)
                rf = RotateFlipType.Rotate270FlipNone;
            if (_mDstImage != null) {
                _mDstImage.Dispose();
                _mGraphics.Dispose();
            }
            Bitmap bitmap = new Bitmap((int) _mSrcImage.Width, (int) _mSrcImage.Height, PixelFormat.Format24bppRgb);
            _mGraphics = Graphics.FromImage(bitmap);
            _mGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            _mGraphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            _mGraphics.DrawImage(_mSrcImage, 0, 0, bitmap.Width, bitmap.Height);
            _mDstImage = bitmap;

            _mDstImage.RotateFlip(rf);
            return _mDstImage;
        }

        public virtual Image GetThumbnail() {
            // Flag whether a new image is required
            bool recalculate = false;
            double newWidth = Width;
            double newHeight = Height;

            // Is there a cached source image available? If not,
            // load the image if a filename was specified; otherwise
            // use the image in the Image property
            if (!IsSameSrcImage(_mCache)) {
                if (_mImagePath.Length > 0) {
                    // Load via stream rather than Image.FromFile to release the file
                    // handle immediately
                    if (_mSrcImage != null)
                        _mSrcImage.Dispose();

                    // Wrap the FileStream in a "using" directive, to ensure the handle
                    // gets closed when the object goes out of scope
                    using (Stream stream = new FileStream(_mImagePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                        _mSrcImage = Image.FromStream(stream);

                    recalculate = true;
                }
            }

            // Check whether the required destination image properties have
            // changed
            if (!IsSameDstImage(_mCache)) {
                // Yes, so we need to recalculate.
                // If you opted to specify width and height as percentages of the original
                // image's width and height, compute these now
                if (UsePercentages) {
                    if (Width != 0) {
                        newWidth = (double) _mSrcImage.Width*Width/100;

                        if (PreserveAspectRatio)
                            newHeight = newWidth*_mSrcImage.Height/(double) _mSrcImage.Width;
                    }

                    if (Height != 0) {
                        newHeight = (double) _mSrcImage.Height*Height/100;

                        if (PreserveAspectRatio)
                            newWidth = newHeight*_mSrcImage.Width/(double) _mSrcImage.Height;
                    }
                }
                else
                    // If you specified an aspect ratio and absolute width or height, then calculate this 
                    // now; if you accidentally specified both a width and height, 
                    // the specified values are used as maximum allowed for each dimension
                    if (PreserveAspectRatio) {
                        if (Width != 0 && Height == 0)
                            newHeight = (Width/(double) _mSrcImage.Width)*_mSrcImage.Height;
                        else if (Height != 0 && Width == 0)
                            newWidth = (Height/(double) _mSrcImage.Height)*_mSrcImage.Width;
                        else if (Height != 0 && Width != 0) {
                            newHeight = (Width/(double) _mSrcImage.Width)*_mSrcImage.Height;
                            newWidth = Width;
                            if (newHeight > Height) {
                                newWidth = (Height/(double) _mSrcImage.Height)*_mSrcImage.Width;
                                newHeight = Height;
                            }
                        }
                    }

                recalculate = true;
            }

            if (recalculate) {
                // Calculate the new image
                if (_mDstImage != null) {
                    _mDstImage.Dispose();
                    _mGraphics.Dispose();
                }

                Bitmap bitmap = new Bitmap((int) newWidth, (int) newHeight, PixelFormat.Format24bppRgb);
                bitmap.SetResolution(72, 72);
                _mGraphics = Graphics.FromImage(bitmap);

                _mGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                _mGraphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;


                // m_graphics.DrawImage(m_src_image, 0, 0, bitmap.Width, bitmap.Height);
                _mGraphics.DrawImage(_mSrcImage, -1, -1, bitmap.Width + 2, bitmap.Height + 2);
                _mDstImage = bitmap;


                // Cache the image and its associated settings
                _mCache = this.MemberwiseClone() as ImageResize;
                bitmap = null;
            }

            return _mDstImage;
        }

        ~ImageResize() {
            // Free resources
            if (_mDstImage != null) {
                _mDstImage.Dispose();
                _mGraphics.Dispose();
            }

            if (_mSrcImage != null)
                _mSrcImage.Dispose();
        }


        public void ResimBoyutlandır(string resimYoluVeAdi, string yeniResimYoluVeAdi, int genislik, int yukseklik)
        {
            Bitmap myBitmap = new Bitmap(resimYoluVeAdi);
            Bitmap yeniResim = new Bitmap(myBitmap, genislik, yukseklik);

            yeniResim.Save(yeniResimYoluVeAdi, ImageFormat.Jpeg);
            yeniResim.Dispose();
            myBitmap.Dispose();
        }
    }
