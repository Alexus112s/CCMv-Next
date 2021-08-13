using CCMvNext.BusinessLogic.CookieConsents;
using CCMvNext.BusinessLogic.DTO;
using CCMvNext.Data.Core;
using CCMvNext.Infrastructure.ReinforcedTypings;
using CCMvNext.Models.CookieConsent;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CCMvNext.Controllers
{
    /// <summary>
    /// Handles the Reporting requests.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CookieConsentsReportingController : ControllerBase
    {
        private readonly IRepository<CookieConsentRecord> _repository;

        public CookieConsentsReportingController(IRepository<CookieConsentRecord> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Gets the chart data for the Consent By Day chart.
        /// <para>GET api/CookieConsentsReporting/GetConsentRate</para>
        /// </summary>
        /// <param name="sinceDate"></param>
        /// <param name="beforeDate"></param>
        /// <returns></returns>
        [HttpGet("GetConsentRate"), InvokedFromAngular]
        public IEnumerable<ConsentByDay> GetConsentRate(DateTime sinceDate, DateTime beforeDate)
        {
            return _repository.AsQueryable()
                .GetConsentRateByDay(sinceDate, beforeDate)
                .ToArray();
        }
    }
}
