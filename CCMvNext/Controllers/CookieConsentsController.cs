using CCMvNext.Data.Core;
using CCMvNext.Data.Extensions;
using CCMvNext.Infrastructure.ReinforcedTypings;
using CCMvNext.Models.CookieConsent;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CCMvNext.Controllers
{
    /// <summary>
    /// Performs the <see cref="CookieConsentRecord"/> CRUD.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CookieConsentsController : ControllerBase
    {
        private readonly IRepository<CookieConsentRecord> _repository;

        public CookieConsentsController(IRepository<CookieConsentRecord> repository)
        {
            _repository = repository;
        }

        // GET: api/<CookieConsentsController>
        [HttpGet, InvokedFromAngular]
        public async Task<IEnumerable<CookieConsentRecord>> Get()
        {
            var data = await _repository.GetAllAsync();
            return data.OrderByDescending(x => x.Date); // ToDo: move ordering to the DB.
        }

        // GET api/<CookieConsentsController>/5
        [HttpGet("{id}"), InvokedFromAngular]
        public async Task<CookieConsentRecord> GetCookieConsentRecord(string id)
        {
            return await _repository.FindByIdAsync(id);
        }

        // POST api/<CookieConsentsController>
        [HttpPost, InvokedFromAngular]
        public async Task<CookieConsentRecord> Post([FromBody] CookieConsentRecord record)
        {
            return await _repository.CreateAsync(record);
        }

        // PUT api/<CookieConsentsController>/5
        [HttpPut("{id}"), InvokedFromAngular]
        public async Task Put(string id, [FromBody] CookieConsentRecord record)
        {
            await _repository.UpdateAsync(id, record);
        }

        // DELETE api/<CookieConsentsController>/5
        [HttpDelete("{id}"), InvokedFromAngular]
        public async Task Delete(string id)
        {
            await _repository.RemoveAsync(id);
        }
    }
}
