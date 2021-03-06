    /// <summary>
    /// Author: sertunc.selen
    /// Tarih: 17.12.2014 14:20
    /// Net Single Sign On Yapısı için Cookie Sınıfıdır... 
    /// </summary>
    public class SSOCookieIslem
    {
        private const string cookieName = "SSOKullanici";
        private const string domainName = ".domain.org.tr";

        /// <summary>
        /// Kullanıcı sisteme login olduktan sonra diğer projelerede login olabilmesi için cookie oluştur.
        /// </summary>
        /// <param name="token">Kullanıcı Token</param>
        public static void CookieCreate(string token)
        {
            HttpCookie ssoCookie = new HttpCookie(cookieName);
            ssoCookie["token"] = token;
            ssoCookie.Expires = DateTime.Now.AddYears(2);
            ssoCookie.Domain = domainName;
            HttpContext.Current.Response.Cookies.Add(ssoCookie);
        }

        /// <summary>
        /// SSO cookiesinin varlığını kontrol eder.
        /// </summary>        
        /// <returns>bool</returns>
        [Obsolete]
        public static bool CookieCheck()
        {
            var httpCookie = HttpContext.Current.Request.Cookies[cookieName];
            return httpCookie != null && (HttpContext.Current.Response.Cookies[cookieName] != null && httpCookie["token"] != null);
        }

        /// <summary>
        /// Var olan SSO cookie sinin içerisinde bulunan alanları okur ve referans type olarak geri verir.
        /// </summary>
        /// <param name="token">out Kullanıcı token</param>
        public static string CookieRead()
        {
            string retVal = "";

            if (HttpContext.Current.Request.Cookies[cookieName] != null)
                retVal = HttpContext.Current.Request.Cookies[cookieName]["token"];

            return retVal;
        }

        /// <summary>
        /// SSO varolan cookie'nin statu durumunu döner
        /// </summary>
        /// <returns>True,False</returns>
        [Obsolete]
        public static bool CookieStatuKontrol()
        {
            bool retVal = false;
            if (HttpContext.Current.Request.Cookies[cookieName] != null)
            {
                string durum;
                if (HttpContext.Current.Request.Cookies[cookieName]["durum"] != null)
                {
                    durum = HttpContext.Current.Request.Cookies[cookieName]["durum"];
                    retVal = durum == "1" ? true : false;
                }
            }
            return retVal;
        }

        /// <summary>
        /// Tarayıcıdaki cookieleri temizler
        /// </summary>
        public static void CookieKill()
        {
            HttpCookie c = HttpContext.Current.Request.Cookies[cookieName];
            if (c != null)
            {
                c.Domain = domainName;
                c.Expires = DateTime.Now.AddYears(-2);
                HttpContext.Current.Response.Cookies.Add(c);
            }
        }

        public static string GenerateToken()
        {
            byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            byte[] key = Guid.NewGuid().ToByteArray();
            return Convert.ToBase64String(time.Concat(key).ToArray());
        }
    }
