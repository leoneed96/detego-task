using Detego.DAL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Detego.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CrudController : ControllerBase
    {
        private readonly IDataStorage _dataStorage;
        public CrudController(IDataStorage dataStorage)
        {
            _dataStorage = dataStorage;
        }

        [HttpGet]
        [Route("getRecords")]
        public async Task<IActionResult> GetRecords()
        {
            var data = _dataStorage.GetRecords();
            var result = data.GroupBy(x => new { x.Transponder.Id, x.Transponder.SeenCount })
                .Select(x => new
                {
                    x.Key.Id,
                    ReadDate = x.Max(a => a.ReadDate),
                    x.Key.SeenCount
                })
                .OrderByDescending(x => x.ReadDate)
                .ToList();

            await Task.Delay(700);
            return Ok(result);
        }

        [HttpPost]
        [Route("removeRecord")]
        public IActionResult Remove(string recordId)
        {
            try
            {
                _dataStorage.RemoveRecord(recordId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
