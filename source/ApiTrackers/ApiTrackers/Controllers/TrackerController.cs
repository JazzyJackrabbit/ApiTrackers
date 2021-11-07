﻿using Microsoft.AspNetCore.Mvc;
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

namespace ApiTrackers.Controllers
{
    [ApiController]
    [Route("Trackers")]
    public class TrackerController : ControllerBase
    {
        public Main mainService; //

        public TrackerController(Main _mainService)
        {
            mainService = _mainService;
        }

        [HttpGet]
        [Route("")]
        public ContentResult GetTrackers()
        {
            try { 
                List<Tracker> trackers = mainService.bddTracker.selectTrackers(true);

                if (trackers != null)
                {
                    return new ContentResult()
                    {
                        StatusCode = 200,
                        Content = Static.jsonResponseArray<Tracker>(200, trackers)
                    };
                }
                else
                {
                    return new ContentResult()
                    {
                        StatusCode = 404,
                        Content = Static.jsonResponseError(404, "error getting trackers")
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
        public ContentResult GetTracker(int id)
        {
            try { 
                Tracker tracker = mainService.bddTracker.selectTracker(id, true);

                if (tracker != null)
                    return new ContentResult()
                    {
                        StatusCode = 200,
                        Content = Static.jsonResponseObject(200, tracker)
                    };
                else
                    return new ContentResult()
                    {
                        StatusCode = 404,
                        Content = Static.jsonResponseError(404, "unfounded tracker")
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

        [Route("")]
        [HttpPost]
        public ContentResult CreateTracker([FromBody] TrackerCreateDTO dto)
        {
            try {

                int idUser = dto.idUser;

                Tracker trackerToInsert = dto.toTracker();

                Tracker trackerResp = mainService.bddTracker.insertTracker(trackerToInsert, true);

                if (trackerResp != null)
                    return new ContentResult()
                    {
                        StatusCode = 200,
                        Content = Static.jsonResponseObject(200, trackerResp)
                    };
                else
                    return new ContentResult()
                    {
                        StatusCode = 404,
                        Content = Static.jsonResponseError(404, "error creation tracker")
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
        public ContentResult DeleteTracker([FromQuery] int id = -1, [FromQuery] int idUser = -1)
        {
            try
            {
                //TODO AUTHENT TOKEN

                if (idUser < 0 && id < 0) return new ContentResult()
                {
                    StatusCode = 404,
                    Content = Static.jsonResponseError(404, "id and idUser attributes missing.")
                };

                if (idUser < 0) return new ContentResult()
                {
                    StatusCode = 404,
                    Content = Static.jsonResponseError(404, "idUser attribute missing.")
                };


                if (id < 0) return new ContentResult()
                {
                    StatusCode = 404,
                    Content = Static.jsonResponseError(404, "id attribute missing.")
                };

                Tracker tracker = mainService.bddTracker.deleteTracker(id, idUser, true);

                if (tracker != null)
                    return new ContentResult()
                    {
                        StatusCode = 200,
                        Content = Static.jsonResponseObject(200, tracker)
                    };
                else
                    return new ContentResult()
                    {
                        StatusCode = 404,
                        Content = Static.jsonResponseError(404, "unfounded tracker")
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
        [HttpPut]
        [Route("")]
        public ContentResult UpdateTracker([FromBody] TrackerUpdateDTO dto)
        {
           
            try
            {
                //TODO   check  token authent    idUser == 
                int idUser = dto.idUser;

                Tracker trackerToInsert = dto.toTracker();
                Tracker trackerResp = mainService.bddTracker.updateTracker(trackerToInsert, dto.id, idUser, true);

                if (trackerResp != null)
                    return new ContentResult()
                    {
                        StatusCode = 200,
                        Content = Static.jsonResponseObject(200, trackerResp)
                    };
                else
                    return new ContentResult()
                    {
                        StatusCode = 404,
                        Content = Static.jsonResponseError(404, "error modifying tracker")
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
