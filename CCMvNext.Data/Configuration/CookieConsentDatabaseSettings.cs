namespace CCMvNext.Data.Configuration
{
    public class CookieConsentDatabaseSettings : ICookieConsentDatabaseSettings
    {
        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }

        public string CollectionName { get; set; }
    }
}
