public static class ExtensionMethods
    {
        /// <summary>
        /// TCKN Geçerlilik Kontrolu Yapar
        /// </summary>
        /// <param name="tckn"></param>
        /// <returns></returns>
        public static bool TcValidate(this string tckn)
        {
            bool returnvalue = false;
            if (tckn.Length == 11)
            {
                Int64 ATCNO, BTCNO, TcNo;
                long C1, C2, C3, C4, C5, C6, C7, C8, C9, Q1, Q2;

                TcNo = Int64.Parse(tckn);

                ATCNO = TcNo / 100;
                BTCNO = TcNo / 100;

                C1 = ATCNO % 10;
                ATCNO = ATCNO / 10;
                C2 = ATCNO % 10;
                ATCNO = ATCNO / 10;
                C3 = ATCNO % 10;
                ATCNO = ATCNO / 10;
                C4 = ATCNO % 10;
                ATCNO = ATCNO / 10;
                C5 = ATCNO % 10;
                ATCNO = ATCNO / 10;
                C6 = ATCNO % 10;
                ATCNO = ATCNO / 10;
                C7 = ATCNO % 10;
                ATCNO = ATCNO / 10;
                C8 = ATCNO % 10;
                ATCNO = ATCNO / 10;
                C9 = ATCNO % 10;
                ATCNO = ATCNO / 10;
                Q1 = ((10 - ((((C1 + C3 + C5 + C7 + C9) * 3) + (C2 + C4 + C6 + C8)) % 10)) % 10);
                Q2 = ((10 - (((((C2 + C4 + C6 + C8) + Q1) * 3) + (C1 + C3 + C5 + C7 + C9)) % 10)) % 10);

                returnvalue = ((BTCNO * 100) + (Q1 * 10) + Q2 == TcNo);
            }
            return returnvalue;
        }

        /// <summary>
        /// String içindeki html kodlarını temizler
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string StripHtml(this string input)
        {
            var tagsExpression = new Regex(@"</?.+?>");
            return tagsExpression.Replace(input, " ");
        }

        /// <summary>
        /// Bir koleksiyon boş olup olmadığını belirler veya sayımını almak için tüm koleksiyonu numaralandırmak zorunda kalmadan herhangi bir öğeyi kontrol eder. LINQ kullanır.
        /// </summary>
        /// <typeparam name="T">The item type.</typeparam>
        /// <param name="items">The items.</param>
        /// <returns>
        /// <c>true</c> if this list is null or empty; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNullOrEmpty<T>(this IList<T> items)
        {
            return items == null || !items.Any();

            //Örnek Kullanım
            //var list = new List<string>();
            //list.Add("Test1");
            //list.Add("Test2");
            //Assert.IsFalse(list.IsNullOrEmpty());
            //list.Clear();
            //Assert.IsTrue(list.IsNullOrEmpty());
            //list = null;
            //Assert.IsTrue(list.IsNullOrEmpty());
        }

        /// <summary>
        /// Girilen objecti xml e çevirir
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerializeToXml(this object obj)
        {
            XDocument doc = new XDocument();
            using (XmlWriter xmlWriter = doc.CreateWriter())
            {
                XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());
                xmlSerializer.Serialize(xmlWriter, obj);
                xmlWriter.Close();
            }
            return doc.ToString();

            //Örnek Kullanım
            //string hi = "hellow world";
            //string xml = hi.SerializeToXml();
            //Console.WriteLine(xml);

            //Output: 
            //<string>hellow world</string>
        }

        public static int YasHesap(this DateTime dateOfBirth)
        {
            if (DateTime.Today.Month < dateOfBirth.Month || DateTime.Today.Month == dateOfBirth.Month && DateTime.Today.Day < dateOfBirth.Day) return DateTime.Today.Year - dateOfBirth.Year - 1;
            else
                return DateTime.Today.Year - dateOfBirth.Year;

            //Örnek Uygulama
            //DateTime henrybirthdate = new DateTime(1998, 10, 12);
            //int age = henrybirthdate.Age();
        }

        /// <summary>
        /// DateDiff in SQL style. 
        /// Datepart implemented: 
        ///     "year" (abbr. "yy", "yyyy"), 
        ///     "quarter" (abbr. "qq", "q"), 
        ///     "month" (abbr. "mm", "m"), 
        ///     "day" (abbr. "dd", "d"), 
        ///     "week" (abbr. "wk", "ww"), 
        ///     "hour" (abbr. "hh"), 
        ///     "minute" (abbr. "mi", "n"), 
        ///     "second" (abbr. "ss", "s"), 
        ///     "millisecond" (abbr. "ms").
        /// </summary>
        /// <param name="DatePart"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public static Int64 DateDiff(this DateTime StartDate, String DatePart, DateTime EndDate)
        {
            Int64 dateDiffVal;
            System.Globalization.Calendar cal = System.Threading.Thread.CurrentThread.CurrentCulture.Calendar;
            TimeSpan ts = new TimeSpan(EndDate.Ticks - StartDate.Ticks);
            switch (DatePart.ToLower().Trim())
            {
                #region year

                case "year":
                case "yy":
                case "yyyy":
                    dateDiffVal = (Int64)(cal.GetYear(EndDate) - cal.GetYear(StartDate));
                    break;

                #endregion

                #region quarter

                case "quarter":
                case "qq":
                case "q":
                    dateDiffVal = (Int64)((((cal.GetYear(EndDate)
                                              - cal.GetYear(StartDate)) * 4)
                                            + ((cal.GetMonth(EndDate) - 1) / 3))
                                           - ((cal.GetMonth(StartDate) - 1) / 3));
                    break;

                #endregion

                #region month

                case "month":
                case "mm":
                case "m":
                    dateDiffVal = (Int64)(((cal.GetYear(EndDate)
                                             - cal.GetYear(StartDate)) * 12
                                            + cal.GetMonth(EndDate))
                                           - cal.GetMonth(StartDate));
                    break;

                #endregion

                #region day

                case "day":
                case "d":
                case "dd":
                    dateDiffVal = (Int64)ts.TotalDays;
                    break;

                #endregion

                #region week

                case "week":
                case "wk":
                case "ww":
                    dateDiffVal = (Int64)(ts.TotalDays / 7);
                    break;

                #endregion

                #region hour

                case "hour":
                case "hh":
                    dateDiffVal = (Int64)ts.TotalHours;
                    break;

                #endregion

                #region minute

                case "minute":
                case "mi":
                case "n":
                    dateDiffVal = (Int64)ts.TotalMinutes;
                    break;

                #endregion

                #region second

                case "second":
                case "ss":
                case "s":
                    dateDiffVal = (Int64)ts.TotalSeconds;
                    break;

                #endregion

                #region millisecond

                case "millisecond":
                case "ms":
                    dateDiffVal = (Int64)ts.TotalMilliseconds;
                    break;

                #endregion

                default:
                    throw new Exception(String.Format("DatePart \"{0}\" is unknown", DatePart));
            }
            return dateDiffVal;

            //Örnek Kullanım            
            //DateTime dt = new DateTime(2000, 01, 01);
            //Int64 days = dt.DateDiff("day", DateTime.Now);            
            //Int64 hours = dt.DateDiff("hour", DateTime.Now);
        }

        public static bool IsValidEmail(this string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }

            //Örnek Kullanım
            //string ToAddress = "check@test.com";
            //ToAddress.IsValidEmail()
        }

        public static long GetSize(this DirectoryInfo dir)
        {
            long length = 0;

            // Loop through files and keep adding their size
            foreach (FileInfo nextfile in dir.GetFiles())
                length += nextfile.Exists ? nextfile.Length : 0;

            // Loop through subdirectories and keep adding their size
            foreach (DirectoryInfo nextdir in dir.GetDirectories())
                length += nextdir.Exists ? nextdir.GetSize() : 0;

            return length;

            //Örnek Kullanım
            //DirectoryInfo WindowsDir = new DirectoryInfo(@"C:\WINDOWS");
            //long WindowsSize = WindowsDir.GetSize();
        }

        public static long FileSize(this string filePath)
        {
            long bytes = 0;

            try
            {
                var oFileInfo = new FileInfo(filePath);
                bytes = oFileInfo.Length;
            }
            catch { }
            return bytes;

            //Örnek Kullanım
            //string path = @"D:\WWW\Proj\web.config";
            //Console.WriteLine("File Size is: {0} bytes.", path.FileSize());
        }

        /// <summary>
        /// Parametre olarak verilen collection'nun null durumunu ve item sayısını kontrol eder.
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static bool HasElements(this ICollection items)
        {
            return items != null && items.Count > 0;
        }
    }
