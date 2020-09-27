using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Sys;

namespace API.Controller
{
    [Route("api/[controller]/[action]/{id?}")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUnitofWork _unitofWork;
        public UsersController(IUserService userService, IUnitofWork unitofWork)
        {
            _userService = userService;
            _unitofWork = unitofWork;
        }


        [HttpGet]
        public IActionResult GetAllUser()
        {

            return Ok(_userService.GetAllUser());
        }

 
      
        public async Task<IActionResult> Login(string username, string password)
        {
            return Ok(await _userService.Authenticate(username, password));
        }

    }
}
