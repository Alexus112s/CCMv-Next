using CCMvNext.Models.CookieConsent;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using CCMvNext.BusinessLogic.CookieConsents;

namespace CCMvNext.Tests
{
    [TestClass]
    public class BusinessLogicTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetConsentRateByDay_SinceDateAfterBeforeDate_ArgumentException()
        {
            // Arrange
            var sinceDate = new DateTime(100);
            var beforeDate = new DateTime(1);
            var data = Array.Empty<CookieConsentRecord>().AsQueryable();

            // Act
            data.GetConsentRateByDay(sinceDate, beforeDate);

            // Assert

        }

        [TestMethod]
        public void GetConsentRateByDay_EqualAcceptanceRate_AcceptanceRate50Percent()
        {
            // Arrange
            var sinceDate = new DateTime(2021, 01, 01);
            var beforeDate = new DateTime(2021, 12, 12);
            var data = new CookieConsentRecord[] {
            new CookieConsentRecord
            {
                Date = new DateTime(2021, 02, 01).AddHours(2),
                IsAccepted = true,
            },
            new CookieConsentRecord
            {
                Date = new DateTime(2021, 02, 01).AddHours(1),
                IsAccepted = false,
            }
            }.AsQueryable();

            // Act
            var res = data.GetConsentRateByDay(sinceDate, beforeDate).ToArray();

            // Assert
            Assert.AreEqual(1, res.Length);
            Assert.AreEqual(50, res[0].ConcentRatePercent);
            Assert.AreEqual(new DateTime(2021, 02, 01), res[0].Date);
        }

        [TestMethod]
        public void GetConsentRateByDay_TwoDaysAcceptanceRate_TwoRecords()
        {
            // Arrange
            var sinceDate = new DateTime(2021, 01, 01);
            var beforeDate = new DateTime(2021, 12, 12);
            var data = new CookieConsentRecord[] {
            new CookieConsentRecord
            {
                Date = new DateTime(2021, 02, 01).AddHours(2),
                IsAccepted = true,
            },
            new CookieConsentRecord
            {
                Date = new DateTime(2021, 02, 01).AddHours(1),
                IsAccepted = false,
            },
            new CookieConsentRecord
            {
                Date = new DateTime(2021, 02, 03).AddHours(1),
                IsAccepted = false,
            }
            }.AsQueryable();

            // Act
            var res = data.GetConsentRateByDay(sinceDate, beforeDate).ToArray();

            // Assert
            Assert.AreEqual(2, res.Length);
            Assert.AreEqual(50, res[0].ConcentRatePercent);
            Assert.AreEqual(new DateTime(2021, 02, 01), res[0].Date);
            Assert.AreEqual(0, res[1].ConcentRatePercent);
            Assert.AreEqual(new DateTime(2021, 02, 03), res[1].Date);
        }
    }
}
