using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _24hplusdotnetcore.Models;
using _24hplusdotnetcore.Services;
using Microsoft.AspNetCore.Mvc;

namespace _24hplusdotnetcore.Controllers
{
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly DemoService _demoService;
        public DemoController(DemoService demoService)
        {
            _demoService = demoService;
        }

        [HttpGet]
        [Route("api/demos")]
        public List<Demo> Get() => _demoService.Get();
        [HttpGet]
        [Route("api/demo/{id}")]
        public Demo GetDemoById(string id) => _demoService.Get(id);
    }
} 