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
    [Route("")]
    public class MainController : ControllerBase
    {
        public MainService mainService; //

        public MainController(MainService _mainService)
        {
            mainService = _mainService;
        }

        #region TRACKER

        [HttpGet]
        [Route("/Trackers/Get")]
        public ContentResult GetTrackers()
        {

            List<Tracker> trackers = mainService.bddTracker.selectTrackers();

            if (trackers!=null) {
                return new ContentResult()
                {
                    StatusCode = 200,
                    Content = Static.jsonResponseArray(200, typeof(Tracker), trackers)
                };
            }
            else
            {
                return new ContentResult() { 
                    StatusCode = 404,
                    Content = Static.jsonResponseError(404, "error getting trackers")
                };
            }
        }

        [HttpGet]
        [Route("/Tracker/Get/{id}")]
        public ContentResult GetTracker(int id)
        {
            Tracker tracker = mainService.bddTracker.selectTracker(id);

            if (tracker != null)
                return new ContentResult()
                {
                    StatusCode = 200,
                    Content = Static.jsonResponseObject(200, typeof(Tracker), tracker)
                };
            else
                return new ContentResult()
                {
                    StatusCode = 404,
                    Content = Static.jsonResponseError(404, "unfounded tracker")
                };

        } 

        [Route("/Tracker/Create")]
        [HttpPost]
        public ContentResult CreateTracker([FromBody] TrackerCreateDTO dto)
        {
            Tracker trackerToInsert = dto.toTracker();

            Tracker trackerResp = mainService.bddTracker.insertTracker(trackerToInsert);

            if (trackerResp != null)
                return new ContentResult()
                {
                    StatusCode = 200,
                    Content = Static.jsonResponseObject(200, typeof(Tracker), trackerResp)
                };
            else
                return new ContentResult()
                {
                    StatusCode = 404,
                    Content = Static.jsonResponseError(404, "error creation tracker")
                };

        }

        [HttpPost]
        [Route("/Tracker/Delete")]
        public ContentResult DeleteTracker([FromBody] TrackerDeleteDTO dto)
        {
            try {
                int idUser = 8;
                //TODO AUTHENT TOKEN

                Tracker tracker = mainService.bddTracker.deleteTracker(dto.id, idUser);

                if (tracker != null)
                    return new ContentResult()
                    {
                        StatusCode = 200,
                        Content = Static.jsonResponseObject(200, typeof(Tracker), tracker)
                    };
                else
                    return new ContentResult()
                    {
                        StatusCode = 404,
                        Content = Static.jsonResponseError(404, "unfounded tracker")
                    };
                }
            catch (ForbiddenException ex) {
                return new ContentResult()
                {
                    StatusCode = ex.code,
                    Content = Static.jsonResponseError(ex.code, ex.Message)
                };
            }
            catch(Exception ex)
            {
                return new ContentResult()
                {
                    StatusCode = 500,
                    Content = Static.jsonResponseError(500, "Internal Error: " + ex.Message)
                };
            }
        }
        [HttpPost]
        [Route("/Tracker/Update")]
        public ContentResult UpdateTracker([FromBody] TrackerUpdateDTO dto)
        {
            Tracker trackerToInsert = new Tracker();
            Tracker trackerResp = mainService.bddTracker.updateTracker(trackerToInsert, dto.id);

            if (trackerResp != null)
                return new ContentResult()
                {
                    StatusCode = 200,
                    Content = Static.jsonResponseObject(200, typeof(Tracker), trackerResp)
                };
            else
                return new ContentResult()
                {
                    StatusCode = 404,
                    Content = Static.jsonResponseError(404, "error modifying tracker")
                };
        }

        #endregion

        #region USERS

        [HttpGet]
        [Route("Users/Get")]
        public ContentResult GetUsers()
        {
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

        [HttpGet]
        [Route("/User/Get/{id}")]
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
        [Route("/User/Create")]
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
        [Route("/User/Delete")]
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
        [Route("/User/Update")]
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

        #endregion

    }
}
