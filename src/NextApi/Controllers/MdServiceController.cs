using Microsoft.AspNetCore.Mvc;
using NextApi.Models;
using NextApi.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NextApi.Controllers
{
    public class MdServiceController : Controller
    {
        private readonly IService<MdModel> _service;
        public MdServiceController(IService<MdModel> service)
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
