 public class DosyaIslem{
        /// <summary>
        /// PDF, DOC, DOCX, CVS, XLS ve XLSX formatlarında dosyaların kayıt işlemlerini yapabilir
        /// </summary>
        /// <param name="dosyaFileUpload">FileUpload u alır</param>
        /// <param name="dosyaAdi">Dosyanın adını alır</param>
        /// <param name="dosyaSanalYolu">Dosyanın kayıt edileceği yer Public/???</param>
        /// <returns>Dosya adını döner</returns>
        public static String EvrakKaydet(FileUpload dosyaFileUpload, string dosyaAdi = "", string dosyaSanalYolu = "") {
            HttpServerUtility Server = HttpContext.Current.Server;

            // geri dönüş : boş dönüş ise hatalı dönüş, değilse dosya adıdır

            //dosya adı boş girilirse, orjinal dosya adı verilir.
            if (string.IsNullOrEmpty(dosyaAdi)) dosyaAdi = dosyaFileUpload.FileName;

            //dosyaSanalYolu boş girilirse, resim klasörüne yönlendirilir
            if (string.IsNullOrEmpty(dosyaSanalYolu)) dosyaSanalYolu = "Evraklar";


            //dosyaFizikselYol'nun fiziksel verilme durumuna karşı alınan önlem. : Hata alınmış ise sanal yol değildir. Büyük ihtimal fiziksel yoldur.
            string dosyaFizikselYol;
            try {
                dosyaFizikselYol = Server.MapPath("/Public/" + dosyaSanalYolu + "\\");
            }
            catch (Exception) {
                dosyaFizikselYol = dosyaSanalYolu + "\\";
            }


            //dosyaAdi'na dosya uzantısı adı verilerek girilmesine karşı önlem
            string dosyaUzanti = "pdf";
            if (dosyaAdi.IndexOf('.') > -1) {
                dosyaUzanti = Path.GetExtension(dosyaAdi);
                dosyaUzanti = dosyaUzanti.Replace(".", "");
                dosyaAdi = dosyaAdi.Substring(0, dosyaAdi.LastIndexOf('.'));
                dosyaAdi = dosyaAdi.Replace(".", "");
            }

            if (dosyaFileUpload.FileName != string.Empty) {
                if (!System.IO.Directory.Exists(dosyaFizikselYol)) System.IO.Directory.CreateDirectory(dosyaFizikselYol);

                switch (dosyaUzanti) {
                    case "pdf":
                        dosyaUzanti = "pdf";
                        break;
                    case "doc":
                        dosyaUzanti = "doc";
                        break;
                    case "docx":
                        dosyaUzanti = "docx";
                        break;
                    case "cvs":
                        dosyaUzanti = "cvs";
                        break;
                    case "xls":
                        dosyaUzanti = "xls";
                        break;
                    case "xlsx":
                        dosyaUzanti = "xlsx";
                        break;
                    default:
                        dosyaUzanti = "pdf";
                        break;
                }

                string yeniDosyaAdi = KarakterIslem.Kisalt(KarakterIslem.TarayiciUrl(dosyaAdi), 96) + "." + dosyaUzanti; //toplam max 100 karakter olacak şekilde

                int sayac = 1;
                while (File.Exists(dosyaFizikselYol + yeniDosyaAdi)) {
                    yeniDosyaAdi = KarakterIslem.Kisalt(KarakterIslem.TarayiciUrl(dosyaAdi), 90) + "_" + sayac + "." + dosyaUzanti;
                    sayac++;
                }

                dosyaFileUpload.SaveAs(dosyaFizikselYol + yeniDosyaAdi);
                return yeniDosyaAdi;
            }
            return string.Empty;
        }
    }
