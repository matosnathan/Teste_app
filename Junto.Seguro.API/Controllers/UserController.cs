using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Junto.Seguros.API.Commons;
using Junto.Seguros.Domain.Commons;
using Junto.Seguros.Domain.Users.Commands;
using Junto.Seguros.Domain.Users.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Junto.Seguros.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _appService;
        private readonly IDomainNotificationProvider _notificationProvider;
        public UserController(IDomainNotificationProvider notificationProvider, IUserService appService) 
        {
            _notificationProvider = notificationProvider;
            _appService = appService;
        }

        [HttpPost("")]
        public async Task<ActionResult> Post(UserCreateCommand command)
        {
            var result = await _appService.PostAsync(command);

            if (_notificationProvider.HasErrors())
                return BadRequest(new FailedResult("Bad Request", _notificationProvider.GetErrors()));

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(long id)
        {
            var result = await _appService.GetAsync(id);

            if (_notificationProvider.HasErrors())
                return BadRequest(new FailedResult("Bad Request", _notificationProvider.GetErrors()));

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(UserUpdateCommand command)
        {
            var result = await _appService.UpdateAsync(command);

            if (_notificationProvider.HasErrors())
                return BadRequest(new FailedResult("Bad Request", _notificationProvider.GetErrors()));

            return Ok(result);
        
        }

        [HttpGet("")]
        public async Task<ActionResult> GetAll()
        {
            var result = await _appService.GetAll();

            if (_notificationProvider.HasErrors())
                return BadRequest(new FailedResult("Bad Request", _notificationProvider.GetErrors()));

            return Ok(result);
        }

        [HttpPut("change-password")]
        public async Task<ActionResult> ChangePassword(UserChangePasswordCommand command)
        {
            await _appService.ChangePassword(command);

            if (_notificationProvider.HasErrors())
                return BadRequest(new FailedResult("Bad Request", _notificationProvider.GetErrors()));

            return Ok();
            
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            await _appService.DeleteAsync(id);

            if (_notificationProvider.HasErrors())
                return BadRequest(new FailedResult("Bad Request", _notificationProvider.GetErrors()));

            return Ok();
        }
    }
}