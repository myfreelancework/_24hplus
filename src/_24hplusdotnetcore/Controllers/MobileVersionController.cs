using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _24hplusdotnetcore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace _24hplusdotnetcore.Controllers
{
    [ApiController]
    public class MobileVersionController : ControllerBase
    {
        private readonly ILogger<MobileVersionController> _logger;
        private readonly MobileVersionServices _mobileVersionServices;
        public MobileVersionController(ILogger<MobileVersionController> logger, MobileVersionServices mobileVersionServices)
        {
            _logger = logger;
            _mobileVersionServices = mobileVersionServices;
        }
    }
}