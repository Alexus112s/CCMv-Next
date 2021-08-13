using System;

namespace CCMvNext.Models.CookieConsent
{
    /// <summary>
    /// Represents a sample Cookie Consent record.
    /// </summary>
    public class CookieConsentRecord : Entity
    {
        public bool IsAccepted { get; set; }

        public DateTime Date { get; set; }

        public string Ip { get; set; }

        public string ClientId { get; set; }
    }
}
