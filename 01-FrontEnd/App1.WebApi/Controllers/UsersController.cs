using System;
using App1.Domain.Services;
using App1.Domain.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace App1.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : Controller
    {
        #region | Construtor
        private readonly IRememberService rememberService;
        private readonly IRegisterService registerService;
        private readonly IAuthenticateService authenticateService;
        private readonly IMemoryCache memoryCache;

        public UsersController(IRememberService RememberService, IRegisterService RegisterService, IAuthenticateService AuthenticateService, IMemoryCache memory)
        {
            rememberService = RememberService;
            registerService = RegisterService;
            authenticateService = AuthenticateService;
            memoryCache = memory;
        }
        #endregion

        #region | Login
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody]LoginFormVIewModel model)
        {
            model.Ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            ResultViewModel result = this.authenticateService.Authenticate(model);
            return Ok(result);
        }
        #endregion

        #region | Register
        [HttpGet]
        [Route("Register")]
        [AllowAnonymous]
        public IActionResult Register()
        {
            Item[] list = this.registerService.Genders();
            return Ok(new ResultViewModel()
            {
                Success = true,
                Message = string.Empty,
                Data = list
            });
        }
        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]
        public IActionResult Register([FromBody]RegisterViewModel model)
        {
            model.Ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            ResultViewModel result = this.registerService.Register(model);
            return Ok(result);
        }
        [HttpGet]
        [Route("ConfirmEmail")]
        [AllowAnonymous]
        public IActionResult ConfirmEmail(Guid Id)
        {
            //checando se o dado esta em cache
            bool encontrado = memoryCache.TryGetValue(Id.ToString(), out ResultViewModel result);
            if (encontrado) return Ok(result);

            //recuperando o dado
            string Ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            result = this.registerService.ConfirmEmail(Id, Ip);

            //colocando o dado em cache
            memoryCache.Set<ResultViewModel>(Id.ToString(), result, TimeSpan.FromMinutes(2));

            return Ok(result);
        }
        #endregion

        #region | Lembrar senha
        [HttpPost]
        [Route("Remember")]
        [AllowAnonymous]
        public IActionResult Remember([FromBody]RememberViewModel model)
        {
            model.Ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            ResultViewModel result = this.rememberService.Remember(model);
            return Ok(result);
        }

        [HttpGet]
        [Route("Remember")]
        [AllowAnonymous]
        public IActionResult RememberPassword(Guid Id)
        {
            string Ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            ResultViewModel result = this.rememberService.Remember(Id, Ip);
            return Ok(result);
        }

        [HttpPut]
        [Route("Remember")]
        [AllowAnonymous]
        public IActionResult RememberCallBack([FromBody]RememberPasswordViewModel model)
        {
            model.Ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            ResultViewModel result = this.rememberService.Remember(model);
            return Ok(result);
        }
        #endregion
    }
}