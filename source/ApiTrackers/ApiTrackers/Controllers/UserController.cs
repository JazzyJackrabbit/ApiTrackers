using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using static ApiTrackers.BDD_MainService;
using ApiTrackers.Objects;
using System.IO;
using System.Text;
using ApiTrackers.ApiParams;
using ApiTrackers.DTO_ApiParameters;
using ApiTrackers.Exceptions;

namespace ApiTrackers.Controllers
{
    [ApiController]
    [Route("User")]
    public class UserController : ControllerBase
    {
        public MainService mainService; //

        public UserController(MainService _mainService)
        {
            mainService = _mainService;
        }

        [HttpGet]
        [Route("{id}")]
        public ContentResult GetUser(int id)
        {
            User user = mainService.bddUser.selectUser(id);

            if (user != null)
                return new ContentResult()
                {
                    StatusCode = 200,
                    Content = Static.jsonResponseObject(200, typeof(User), user)
                };
            else
                return new ContentResult()
                {
                    StatusCode = 404,
                    Content = Static.jsonResponseError(404, "unfounded user")
                };

        }

        [HttpPost]
        [Route("Create")]
        public ContentResult CreateUser([FromBody] UserCreateDTO dto)
        {
            User user = dto.toUser();

            User userResp = mainService.bddUser.insertUser(user);

            if (userResp != null)
                return new ContentResult()
                {
                    StatusCode = 200,
                    Content = Static.jsonResponseObject(200, typeof(User), userResp)
                };
            else
                return new ContentResult()
                {
                    StatusCode = 404,
                    Content = Static.jsonResponseError(404, "error creation user")
                };
        }
        [HttpPost]
        [Route("Delete")]
        public ContentResult DeleteUser([FromBody] UserDeleteDTO dto)
        {
            User user = mainService.bddUser.deleteUser(dto.id);

            if (user != null)
                return new ContentResult()
                {
                    StatusCode = 200,
                    Content = Static.jsonResponseObject(200, typeof(User), user)
                };
            else
                return new ContentResult()
                {
                    StatusCode = 404,
                    Content = Static.jsonResponseError(404, "unfounded user")
                };
        }
        [HttpPost]
        [Route("Update")]
        public ContentResult UpdateUser([FromBody] UserUpdateDTO dto){
            User userToInsert = dto.toUser();
       
            User userResp = mainService.bddUser.updateUser(userToInsert, dto.id);

            if (userResp != null)
                return new ContentResult()
                {
                    StatusCode = 200,
                    Content = Static.jsonResponseObject(200, typeof(User), userResp)
                };
            else
                return new ContentResult()
                {
                    StatusCode = 404,
                    Content = Static.jsonResponseError(404, "error modifying user")
                };
        }

    }
}
