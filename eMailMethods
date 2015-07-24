public class eMailMethods
    {
        public static bool HesapAktivasyonMailGonder(string tckn, string aktivasyonKodu, string mailAdresi)
        {
            try
            {
                String mailBody;
                mailBody = HesapAktivasyonMailBody(tckn, aktivasyonKodu);
                SendMail(new MailAddress("no-reply@domain.org.tr"), new MailAddress(mailAdresi), "20#gosca#13", "Hesap Aktivasyon", mailBody);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool yp_HesapSifresiniMailGonder(string sifre, string mailAdresi)
        {
            try
            {
                String mailBody;
                mailBody = yp_HesapSifresiniMailGonderBody(sifre);
                SendMail(new MailAddress("no-reply@domain.org.tr"), new MailAddress(mailAdresi), "20#gosca#13", "domainnet - Hesap Şifresi", mailBody);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool SifreAktivasyonMailGonder(string tckn, string aktivasyonKodu, string mailAdresi)
        {
            try
            {
                String mailBody;
                mailBody = SifremiAktivasyonMailBody(tckn, aktivasyonKodu);
                SendMail(new MailAddress("no-reply@domain.org.tr"), new MailAddress(mailAdresi), "20#gosca#13", "domainnet - Şifremi Unuttum", mailBody);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void IletisimMailGonder(string adSoyad, string ePosta, string baslik, string icerik)
        {
            try
            {
                String mailBody;
                mailBody = IletisimMailBody(adSoyad, baslik, ePosta, icerik);
                IletisimSendMail(new MailAddress("domainnet@domain.org.tr"), new MailAddress("domainnet@domain.org.tr"), "domain!123456!domain", "domainnet - İletişim Formu", mailBody);
            }
            catch (Exception ex)
            {
                throw;
            }
        }



        public static IslemSonuc MailGonder(string gonderilecekEposta, string gonderilecekBaslik, string gonderilecekIcerik, string gonderenEposta, string displayName, string gonderenEpostaParola)
        {
            IslemSonuc sonuc = new IslemSonuc() { Mesaj = "Eposta başarılı bir şekilde gönderilmiştir", Sonuc = false };
            try
            {
                string[] epostalar = Utils.SplitStrings(gonderilecekEposta, ';');
                foreach (var item in epostalar) IletisimSendMail(new MailAddress(gonderenEposta, displayName), new MailAddress(item), gonderenEpostaParola, gonderilecekBaslik, gonderilecekIcerik);
            }
            catch (Exception ex)
            {
                sonuc = new IslemSonuc() { Mesaj = "Eposta göndermede hata oluştu bilgileriniz kontrol ediniz", Sonuc = false };
            }
            return sonuc;
        }


        private static void IletisimSendMail(MailAddress from, MailAddress to, String pwd, String subject, String body, Action<Object> completeCallback = null)
        {
            try
            {
                SmtpClient smtp = new SmtpClient
                {
                    Host = "smtp.domain.org.tr",
                    Port = 587,
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(from.Address, pwd)
                };
                MailMessage mm;
                mm = new MailMessage(from, to)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true,
                    Priority = MailPriority.Normal
                };
                smtp.SendCompleted += (s, e) => { if (completeCallback != null) completeCallback(e.UserState); };
                ServicePointManager.ServerCertificateValidationCallback
                    += (sender, certificate, chain, sslPolicyErrors) => true;
                smtp.Send(mm);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private static void SendMail(MailAddress from, MailAddress to, String pwd, String Subject, String Body, Action<Object> completeCallback = null)
        {
            try
            {
                SmtpClient smtp = new SmtpClient
                {
                    Host = "smtp.domain.org.tr",
                    Port = 587,
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(from.Address, pwd)
                };
                MailMessage mm;
                mm = new MailMessage(from, to)
                {
                    Subject = Subject,
                    Body = Body,
                    IsBodyHtml = true,
                    Priority = MailPriority.Normal
                };
                smtp.SendCompleted += (s, e) => { if (completeCallback != null) completeCallback(e.UserState); };
                ServicePointManager.ServerCertificateValidationCallback
                    += (sender, certificate, chain, sslPolicyErrors) => true;
                smtp.Send(mm);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private static String IletisimMailBody(string adSoyad, string baslik, string ePosta, string icerik)
        {
            return String.Format(@"<center>
            <b>Gönderen:</b> {0}<br/>
            <b>E-Posta:</b> {1}<br/>
            <b>Başlık:</b> {2}<br/>
            <b>İçerik:</b> {3}<br/>
            </center>", adSoyad, ePosta, baslik, icerik);
        }

        private static String yp_HesapSifresiniMailGonderBody(string sifre)
        {
            return String.Format(@"<center>
            Tebrikler, domainnet Üyelik Şifreniz.<br/>
            {0}", sifre);
        }

        private static String HesapAktivasyonMailBody(string tckn, string aktivasyonKodu)
        {
            string _sifreliTckn = Utils.Encrypt(tckn.ToString());
            string _sifreliAktivasyon = Utils.Encrypt(aktivasyonKodu);

            return String.Format(@"<center>
            Tebrikler, domainnet Üyeliğinizi Aktive Etmek İçin Lütfen Link'e Tıklayınız.<br/>
            <a href='{0}AktivasyonTamamlandi.aspx?t={1}&a={2}'>domainnet Hesabı Aktivasyon Linki</a><br />İyi Günler Dileriz.<br /></center>", ConfigurationManager.AppSettings["aktivasyonLinki"].ToString(), _sifreliTckn, _sifreliAktivasyon);
        }

        private static String SifremiAktivasyonMailBody(string tckn, string aktivasyonKodu)
        {
            string _sifreliTckn = Utils.Encrypt(tckn.ToString());
            string _sifreliAktivasyon = Utils.Encrypt(aktivasyonKodu);

            return String.Format(@"<center>
            Sayın Kullanıcı, Şifre değiştirme talebinde bulundunuz.<br/>Şifrenizi değiştirmek için aşağıdaki linke tıklayarak açılan sayfada gerekli bilgileri doldurunuz.<br/>
            <a href='{0}SifreDegistir.aspx?t={1}&a={2}'>Şifremi Unuttum Aktivasyon Linki</a><br />Eğer böyle bir talepte bulunmadıysanız bu e-postayı dikkate almayınız.<br/>İyi Günler Dileriz.<br /></center>", ConfigurationManager.AppSettings["aktivasyonLinki"].ToString(), _sifreliTckn, _sifreliAktivasyon);
        }
    }
