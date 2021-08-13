namespace CCMvNext.Data.Configuration
{
    public interface ICookieConsentDatabaseSettings
    {
        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }

        public string CollectionName { get; set; }
    }
}
