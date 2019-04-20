using FullCRM.Models.Frontend;
using FullCRM.Services;
using FullCRM.Settings.Frontend;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullCRM.Controllers
{
    [Route("api/[controller]")]
    public class FrontendController : Controller
    {
        private readonly FrontendService _frontendService;

        public FrontendController(FrontendService frontendService)
        {
            _frontendService = frontendService;
        }

        [Authorize]
        [Route("onStart")]
        public FrontendSettingsModel OnStart()
        {
            var userId = long.Parse(User.Identity.Name);
            return _frontendService.GetSettings(userId);
        }
    }
}
