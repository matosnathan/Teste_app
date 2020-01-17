using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Junto.Seguros.API.Commons;
using Junto.Seguros.Domain.Auths;
using Junto.Seguros.Domain.Auths.Contracts;
using Junto.Seguros.Domain.Commons;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Junto.Seguros.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _appService;
        private readonly IDomainNotificationProvider _notificationProvider;
        public AuthController(IDomainNotificationProvider notificationProvider, IAuthService appService)
        {
            _notificationProvider = notificationProvider;
            _appService = appService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(AuthCommand command)
        {
            var result = _appService.Login(command);

            if (_notificationProvider.HasErrors())
                return BadRequest(new FailedResult("Bad Request", _notificationProvider.GetErrors()));

            return Ok(result);
        }

      
    }
}