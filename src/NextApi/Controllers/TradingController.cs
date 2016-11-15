using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NextApi.Models;
using NextApi.Services;

namespace NextApi.Controllers
{
    public class TradingController : Controller
    {
        private readonly IService<Trading> _service;
        public TradingController(IService<Trading> service)
        {
            this._service = service;
        }
        
        [HttpGet]
        public IActionResult Get()
        {
            var result = _service.GetNext();
            return Json(result);
        }
    }
}
