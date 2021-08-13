using CCMvNext.Data.Configuration;
using CCMvNext.Data.Core;
using CCMvNext.Models.CookieConsent;

namespace CCMvNext.Data.CookieConsents
{
    public class CookieConsentsRepository : MongoRepository<CookieConsentRecord>
    {
        public CookieConsentsRepository(CookieConsentDatabaseSettings settings) : base(settings)
        {
        }
    }
}
