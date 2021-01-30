namespace FourmBuilder.Common.Options
{
    public class JwtSetting
    {
        public string SecretKey { get; set; }

        public int ExpireDays { get; set; }

        public string ValidIssuer { get; set; }

        public string ValidAudience { get; set; }
    }
}