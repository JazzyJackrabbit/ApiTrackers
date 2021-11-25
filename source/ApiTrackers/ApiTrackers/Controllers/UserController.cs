using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using static ApiTrackers.SqlDatabase;
using ApiTrackers.Objects;
using System.IO;
using System.Text;
using ApiTrackers.ApiParams;
using ApiTrackers.DTO_ApiParameters;
using ApiTrackers.Exceptions;
using System.Net;
using MySql.Data.MySqlClient;

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
                        Content = ObjectUtils.JsonResponseBuilder(200, users)
                    };
                }
                else
                {
                    return new ContentResult()
                    {
                        StatusCode = 404,
                        Content = ObjectUtils.JsonResponseBuilder(404, "error getting user")
                    };
                }
            }
            catch (Exception ex)
            {
                return new ContentResult()
                {
                    StatusCode = 500,
                    Content = ObjectUtils.JsonResponseBuilder(500, "Internal Error: " + ex.Message)
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
            mainService.bdd.connectOpen();
            MySqlTransaction transaction =  mainService.bdd.sqlConnection.BeginTransaction(System.Data.IsolationLevel.Serializable);

            try
            {
                User user= mainService.bddUser.selectUser(id);
                transaction.Commit();
                if (user != null)
                    return new ContentResult()
                    {
                        StatusCode = 200,
                        Content = ObjectUtils.JsonResponseBuilder(200, user)
                    };
                else
                    return new ContentResult()
                    {
                        StatusCode = 404,
                        Content = ObjectUtils.JsonResponseBuilder(404, "unfounded user")
                    };
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return new ContentResult()
                {
                    StatusCode = 500,
                    Content = ObjectUtils.JsonResponseBuilder(500, "Internal Error: " + ex.Message)
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
                    Content = ObjectUtils.JsonResponseBuilder(200, userResp)
                };
            else
                return new ContentResult()
                {
                    StatusCode = 404,
                    Content = ObjectUtils.JsonResponseBuilder(404, "error creation user")
                };
            }
            catch (Exception ex)
            {
                return new ContentResult()
                {
                    StatusCode = 500,
                    Content = ObjectUtils.JsonResponseBuilder(500, "Internal Error: " + ex.Message)
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
                    Content = ObjectUtils.JsonResponseBuilder(404, "id attribute missing.")
                };

                User user = mainService.bddUser.deleteUser(id);

                if (user != null)
                    return new ContentResult()
                    {
                        StatusCode = 200,
                        Content = ObjectUtils.JsonResponseBuilder(200, user)
                    };
                else
                    return new ContentResult()
                    {
                        StatusCode = 404,
                        Content = ObjectUtils.JsonResponseBuilder(404, "unfounded user")
                    };
            }
            catch (Exception ex)
            {
                return new ContentResult()
                {
                    StatusCode = 500,
                    Content = ObjectUtils.JsonResponseBuilder(500, "Internal Error: " + ex.Message)
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
       
                User userResp = mainService.bddUser.updateUser(userToInsert);

                if (userResp != null)
                    return new ContentResult()
                    {
                        StatusCode = 200,
                        Content = ObjectUtils.JsonResponseBuilder(200, userResp)
                    };
                else
                    return new ContentResult()
                    {
                        StatusCode = 404,
                        Content = ObjectUtils.JsonResponseBuilder(404, "error modifying user")
                    };
            }
            catch (Exception ex)
            {
                return new ContentResult()
                {
                    StatusCode = 500,
                    Content = ObjectUtils.JsonResponseBuilder(500, "Internal Error: " + ex.Message)
                };
            }
            finally
            {
                mainService.bdd.connectClose();
            }
        }


    }

}
