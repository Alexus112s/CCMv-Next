using CCMvNext.Controllers;
using CCMvNext.Data.Core;
using CCMvNext.Models.CookieConsent;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace CCMvNext.Tests
{
    /// <summary>
    /// Yes, I can mock the DB and test Controller :) 
    /// I think partial test set is sufficient for the test task.
    /// </summary>
    [TestClass]
    public class ControllerTests
    {
        [TestMethod]
        public void CookieConsentsController_RecordCreated()
        {
            // Arrange
            var item = new CookieConsentRecord
            {
                Id = "test"
            };

            var mock = new Mock<IRepository<CookieConsentRecord>>(MockBehavior.Strict);
            mock.Setup(x => x.CreateAsync(It.IsAny<CookieConsentRecord>()))
                .Returns(Task.FromResult(item));

            var ctrl = new CookieConsentsController(mock.Object);

            // Act
            var task = ctrl.Post(item);
            task.Wait();
            var res = task.Result;

            // Assert
            Assert.AreEqual(item.Id, res.Id);
            mock.Verify(mock => mock.CreateAsync(It.IsAny<CookieConsentRecord>()), Times.Once(), "CreateAsync not called!");
        }

        [TestMethod]
        public void CookieConsentsController_RecordUpdated()
        {
            // Arrange
            var item = new CookieConsentRecord
            {
                Id = "test"
            };

            var mock = new Mock<IRepository<CookieConsentRecord>>(MockBehavior.Strict);
            mock.Setup(x => x.UpdateAsync(It.Is<string>(s => s == item.Id), It.IsAny<CookieConsentRecord>()))
                .Returns(Task.FromResult(0));

            var ctrl = new CookieConsentsController(mock.Object);

            // Act
            var task = ctrl.Put(item.Id, item);
            task.Wait();

            // Assert
            mock.Verify(mock => mock.UpdateAsync(It.Is<string>(s => s == item.Id), It.IsAny<CookieConsentRecord>()), Times.Once(), "UpdateAsync not called!");
        }
    }
}
