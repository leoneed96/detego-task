using Detego_RfidReaderAdapter;
using Detego.Communication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Detego.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RfidController : ControllerBase
    {
        private readonly IRfidReaderAdapter _adapter;
        public RfidController(IRfidReaderAdapter adapter)
        {
            _adapter = adapter;
        }

        [HttpGet]
        [Route("status")]
        public IActionResult GetStatus()
        {
            return Ok(_adapter.GetState());
        }

        [HttpPost]
        [Route("activate")]
        public IActionResult Activate()
        {
            _adapter.Activate();
            return Ok(_adapter.GetState());
        }
        [HttpPost]
        [Route("deactivate")]
        public IActionResult Deactivate()
        {
            _adapter.Deactivate();
            return Ok(_adapter.GetState());
        }
    }
}
