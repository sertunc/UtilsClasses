public class KarakterIslem{
        /// <summary>
        /// Karakter dizisi, istenilen uzunlukta kısaltılır. Eğer isteniyorsa yazının sonuna ... konulabilir.
        /// </summary>
        /// <param name="gelenVeri">Kısaltılacak karakter dizisidir</param>
        /// <param name="istenilenUzunluk">İstenilen karakter uzunluğunu belirtir</param>
        /// <param name="ucNoktaOlsun">Kısaldığında 3nokta(...) istenip istenmediğini belirtir</param>
        /// <returns></returns>
        public static string Kisalt(string gelenVeri, int istenilenUzunluk, Boolean ucNoktaOlsun = false) {
            if (!String.IsNullOrEmpty(gelenVeri)) {
                HttpServerUtility server = HttpContext.Current.Server;
                if (gelenVeri.Length > istenilenUzunluk) {
                    if (ucNoktaOlsun) {
                        istenilenUzunluk = istenilenUzunluk - 3;
                        if (istenilenUzunluk < 1) istenilenUzunluk = 1;
                        gelenVeri = gelenVeri.Substring(0, istenilenUzunluk) + "...";
                    }
                    else gelenVeri = gelenVeri.Substring(0, istenilenUzunluk);
                }
            }
            return gelenVeri;
        }

        /// <summary>
        /// Girilen metin, tarayıcının url adresinde düzgün görünebilecek şekilde çevrilir. 
        /// </summary>
        /// <param name="gelenString"></param>
        /// <returns></returns>
        /// <example>Merhaba Dünya. Nasılsınız? => merhaba-dunya-nasilsiniz</example>
        public static string TarayiciUrl(string gelenString) {
            if (String.IsNullOrEmpty(gelenString)) return "-";
            gelenString = gelenString.Trim();
            gelenString = gelenString.ToLower();
            gelenString = gelenString.Replace("ı", "i");
            gelenString = gelenString.Replace("ğ", "g");
            gelenString = gelenString.Replace("ü", "u");
            gelenString = gelenString.Replace("ş", "s");
            gelenString = gelenString.Replace("ö", "o");
            gelenString = gelenString.Replace("ç", "c");

            char[] gelenKarakterler = gelenString.ToCharArray();

            int charSayi = 0;
            string gidenVeri = String.Empty;
            for (int i = 0; i < gelenKarakterler.Length; i++) {
                charSayi = Convert.ToInt32(gelenKarakterler[i]);
                //48-57  : 0....9
                //97-122 : a...z
                //32 : space
                //45 : -
                //95 : _

                if ((charSayi >= 48 && charSayi <= 57) || (charSayi >= 97 && charSayi <= 122)) gidenVeri += gelenKarakterler[i];
                else if (charSayi == 32) gidenVeri += "-";
                else if (charSayi == 45 || charSayi == 95) gidenVeri += gelenKarakterler[i];
            }

            while (gidenVeri.IndexOf("--") > -1) gidenVeri = gidenVeri.Replace("--", "-");

            gidenVeri = Kisalt(gidenVeri, 2000, false); //IE nin url sinde gözükebilecek mak karakter sayısı 2.048 dir.

            return gidenVeri;
        }

        /// <summary>
        /// Verilen tarihin, bulunulan tarihe göre yakın tarih formatı şeklinde dönüştürmektedir.
        /// </summary>
        /// <param name="tarih"></param>
        /// <returns></returns>
        /// <example>12:20 veya Çarşamba 12:20 veya 12 Ekim Çarşamba 12:20</example>
        public static string YakinTarih(Object tarih) {
            if (tarih == null) return String.Empty;
            try {
                DateTime tarihTmp = Convert.ToDateTime(tarih);
                string donusTxt = String.Empty;
                DateTime simdi = DateTime.Now;

                TimeSpan kalan = (simdi - tarihTmp);
                double kalanGun = Math.Floor(kalan.TotalDays);
                if (kalanGun >= 60) donusTxt = String.Format("{0:dd MMMM yyyy HH:mm}", tarih);
                else if (kalanGun >= 6) donusTxt = String.Format("{0:dd MMMM dddd HH:mm}", tarih);
                else if (kalanGun > 0) donusTxt = String.Format("{0:dddd HH:mm}", tarih);
                else if (kalanGun == 0) donusTxt = String.Format("{0:HH:mm}", tarih);
                return donusTxt;
            }
            catch {
                return String.Empty;
            }
        }

        /// <summary>
        /// karakterSayisi kadar karakterDizisi dizisi içersinden random dizi üretir
        /// Eğer dizi boş gönderilirse A-Z, 0-9 dizisini kullanır.
        /// </summary>
        /// <param name="karakterDizisi">
        ///     "1,2,3,4" şekline benzer virgulle ayrılmış değerlerdir. Oluşturulacak kod bu karakterler içinden oluşturulur. Boş ise default değerler a-z|A-Z|0-9 arası kullanılır
        /// </param>
        /// <param name="karakterSayisi">
        ///     Kodun kaç karakter olacağını belirler.
        /// </param>
        public static string Rastgele(int karakterSayisi, string karakterDizisi = "") {
            if (String.IsNullOrEmpty(karakterDizisi))
                karakterDizisi = "1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,J,K,M,N,P,R,S,T,U,V,Y,Z,Q,X,W";
            string[] Characters = karakterDizisi.Split(',');
            string returnString = String.Empty;

            Random rnd = new Random();
            int number = 0;
            for (int i = 0; i < karakterSayisi; i++) {
                number = rnd.Next(0, Characters.Length);
                returnString += Characters[number].ToString();
            }

            return returnString;
        }

        /// <summary>
        /// Girilen Adresin başına HTTP:// tagı eklenmesini sağlar.
        /// </summary>
        /// <param name="adres"></param>
        public static string HttpDonustur(string adres) {
            if (String.IsNullOrEmpty(adres)) return String.Empty;
            string yeniAdres = adres;
            if (adres.ToLower().IndexOf("http://") != 0) yeniAdres = "http://" + adres; //.Replace("http://", "").Replace("HTTP://", "");
            return yeniAdres;
        }

        /// <summary>
        /// GelenKarakter'i küçük harf dizisine çevirir
        /// </summary>
        /// <param name="GelenKarakter"></param>
        /// <param name="TurkceKarakterleriDonustur">Türkçe karakterleri Ingilizce karakterlere çevirir</param>
        public static string Kucult(string GelenKarakter, bool TurkceKarakterleriDonustur) {
            GelenKarakter = GelenKarakter.Trim();
            if (TurkceKarakterleriDonustur) {
                GelenKarakter = GelenKarakter.Replace("I", "i");
                GelenKarakter = GelenKarakter.Replace("İ", "i");
                GelenKarakter = GelenKarakter.Replace("Ğ", "g");
                GelenKarakter = GelenKarakter.Replace("Ü", "u");
                GelenKarakter = GelenKarakter.Replace("Ş", "s");
                GelenKarakter = GelenKarakter.Replace("Ç", "c");
                GelenKarakter = GelenKarakter.Replace("Ö", "o");
                GelenKarakter = GelenKarakter.Replace("ı", "i");
                GelenKarakter = GelenKarakter.Replace("ğ", "g");
                GelenKarakter = GelenKarakter.Replace("ü", "u");
                GelenKarakter = GelenKarakter.Replace("ş", "s");
                GelenKarakter = GelenKarakter.Replace("ç", "c");
                GelenKarakter = GelenKarakter.Replace("ö", "o");
            }
            GelenKarakter = GelenKarakter.ToLower();
            return GelenKarakter;
        }

        /// <summary>
        /// GelenKarakter'i büyük harf dizisine çevirir
        /// </summary>
        /// <param name="GelenKarakter"></param>
        /// <param name="TurkceKarakterleriDonustur">Türkçe karakterleri Ingilizce karakterlere çevirir</param>
        public static string Buyut(string GelenKarakter, bool TurkceKarakterleriDonustur) {
            GelenKarakter = GelenKarakter.Trim();
            if (TurkceKarakterleriDonustur) {
                GelenKarakter = GelenKarakter.Replace("i", "I");
                GelenKarakter = GelenKarakter.Replace("ı", "I");
                GelenKarakter = GelenKarakter.Replace("ğ", "G");
                GelenKarakter = GelenKarakter.Replace("ü", "U");
                GelenKarakter = GelenKarakter.Replace("ş", "S");
                GelenKarakter = GelenKarakter.Replace("ç", "C");
                GelenKarakter = GelenKarakter.Replace("ö", "O");
                GelenKarakter = GelenKarakter.Replace("İ", "I");
            }
            GelenKarakter = GelenKarakter.ToUpper();
            return GelenKarakter;
        }
    }
