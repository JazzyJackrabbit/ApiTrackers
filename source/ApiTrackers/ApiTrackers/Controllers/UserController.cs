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
    [Route("Users")]
    public class UserController : ControllerBase
    {
        public MainService mainService; //

        public UserController(MainService _mainService)
        {
            mainService = _mainService;
        }

        [HttpGet]
        [Route("")]
        public ContentResult GetUsers()
        {
            try { 
                List<User> users = mainService.bddUser.selectUsers();

                if (users != null)
                {
                    return new ContentResult()
                    {
                        StatusCode = 200,
                        Content = Static.jsonResponseArray(200, typeof(User), users)
                    };
                }
                else
                {
                    return new ContentResult()
                    {
                        StatusCode = 404,
                        Content = Static.jsonResponseError(404, "error getting user")
                    };
                }
            }
            catch (Exception ex)
            {
                return new ContentResult()
                {
                    StatusCode = 500,
                    Content = Static.jsonResponseError(500, "Internal Error: " + ex.Message)
                };
            }
        }

        [HttpGet]
        [Route("{id}")]
        public ContentResult GetUser(int id)
        {
            try { 
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
            catch (Exception ex)
            {
                return new ContentResult()
                {
                    StatusCode = 500,
                    Content = Static.jsonResponseError(500, "Internal Error: " + ex.Message)
                };
            }
        }

        [HttpPost]
        [Route("")]
        public ContentResult CreateUser([FromBody] UserCreateDTO dto)
        {
            try { 
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
            catch (Exception ex)
            {
                return new ContentResult()
                {
                    StatusCode = 500,
                    Content = Static.jsonResponseError(500, "Internal Error: " + ex.Message)
                };
            }
        }
        [HttpDelete]
        [Route("")]
        public ContentResult DeleteUser([FromQuery] int id = -1)
        {
            try {
                if (id < 0) return new ContentResult()
                {
                    StatusCode = 404,
                    Content = Static.jsonResponseError(404, "id attribute missing.")
                };

                User user = mainService.bddUser.deleteUser(id);

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
            catch (Exception ex)
            {
                return new ContentResult()
                {
                    StatusCode = 500,
                    Content = Static.jsonResponseError(500, "Internal Error: " + ex.Message)
                };
            }
        }
        [HttpPut]
        [Route("")]
        public ContentResult UpdateUser([FromBody] UserUpdateDTO dto){
            try { 
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
            catch (Exception ex)
            {
                return new ContentResult()
                {
                    StatusCode = 500,
                    Content = Static.jsonResponseError(500, "Internal Error: " + ex.Message)
                };
            }
        }


    }

}
