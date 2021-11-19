﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using ApiTrackers.Objects;
using ApiTrackers.DTO_ApiParameters;
using Microsoft.AspNetCore.Identity;

namespace ApiTrackers.Controllers
{
    [ApiController]
    [Route("Users")]
    public class UserController : ControllerBase
    {
        public Main mainService; //

        public UserController(Main _mainService)
        {
            mainService = _mainService;
        }

        [HttpGet]
        [Route("")]
        public ContentResult GetUsers()
        {
            try {

                mainService.bdd.connectOpen();

                List<User> users = mainService.bddUser.selectUsers();

                if (users != null)
                {
                    return new ContentResult()
                    {
                        StatusCode = 200,
                        Content = Static.jsonResponseArray<User>(200, users)
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
            finally
            {
                mainService.bdd.connectClose();
            }
        }

        [HttpGet]
        [Route("{id}")]
        public ContentResult GetUser(int id)
        {
            try
            {
                mainService.bdd.connectOpen();
                User user = mainService.bddUser.selectUser(id);

                if (user != null)
                    return new ContentResult()
                    {
                        StatusCode = 200,
                        Content = Static.jsonResponseObject(200, user)
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
            finally
            {
                mainService.bdd.connectClose();
            }
        }

        [HttpPost]
        [Route("")]
        public ContentResult CreateUser([FromBody] UserCreateDTO dto)
        {
            try { 
 
                mainService.bdd.connectOpen();
                User user = dto.toUser();

                User userResp = mainService.bddUser.insertUser(user);

                if (userResp != null)
                    return new ContentResult()
                    {
                        StatusCode = 200,
                        Content = Static.jsonResponseObject(200, userResp)
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
            finally
            {
                mainService.bdd.connectClose();
            }
        }
        [HttpDelete]
        [Route("")]
        public ContentResult DeleteUser([FromQuery] int id = -1)
        {
            try
            {

                mainService.bdd.connectOpen();
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
                        Content = Static.jsonResponseObject(200, user)
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
            finally
            {
                mainService.bdd.connectClose();
            }
        }
        [HttpPut]
        [Route("")]
        public ContentResult UpdateUser([FromBody] UserUpdateDTO dto){

            try
            {
                mainService.bdd.connectOpen();
                User userToInsert = dto.toUser();
       
                User userResp = mainService.bddUser.updateUser(userToInsert, dto.id);

                if (userResp != null)
                    return new ContentResult()
                    {
                        StatusCode = 200,
                        Content = Static.jsonResponseObject(200, userResp)
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
            finally
            {
                mainService.bdd.connectClose();
            }
        }


    }

}
