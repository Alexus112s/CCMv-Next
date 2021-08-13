using CCMvNext.BusinessLogic.DTO;
using CCMvNext.BusinessLogic.Helpers;
using CCMvNext.Models.CookieConsent;
using System;
using System.Linq;

namespace CCMvNext.BusinessLogic.CookieConsents
{
    /// <summary>
    /// Static queries that don't need any context except the input. 
    /// Must not contain any side-efects.
    /// </summary>
    public static class Queries
    {
        /// <summary>
        /// Groups <see cref="CookieConsentRecord"/>s by date and calculates the acceptance rate.
        /// </summary>
        /// <param name="q">The <see cref="CookieConsentRecord"/> query.</param>
        /// <param name="sinceDate"></param>
        /// <param name="beforeDate"></param>
        /// <returns></returns>
        public static IQueryable<ConsentByDay> GetConsentRateByDay(this IQueryable<CookieConsentRecord> q, DateTime sinceDate, DateTime beforeDate)
        {
            if (sinceDate > beforeDate)
            {
                throw new ArgumentException("Invalid date range");
            }

            sinceDate = sinceDate.Date;
            beforeDate = beforeDate.Date.AddDays(1);

            var result = q.Where(x => x.Date > sinceDate && x.Date < beforeDate)
                .GroupByDate(x => x.Date) //.GroupBy(x => x.Date.Year.ToString() + "-" + x.Date.Month.ToString() + "-" + x.Date.Day.ToString())
                .OrderBy(x => x.Key)
                .Select(x => new ConsentByDay
                {
                    Accepted = x.Sum(r => r.IsAccepted ? 1 : 0),
                    Count = x.Count(),
                    Date = DateTime.Parse(x.Key)
                });

            return result;
        }
    }
}
