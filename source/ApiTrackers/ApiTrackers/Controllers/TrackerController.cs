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
                mainService.bdd.connectOpen();

                List<Tracker> trackers = mainService.bddTracker.selectTrackers();

                if (trackers != null)
                {
                    return new ContentResult()
                    {
                        StatusCode = 200,
                        Content = ObjectUtils.JsonResponseBuilder(200, trackers)
                    };
                }
                else
                {
                    return new ContentResult()
                    {
                        StatusCode = 404,
                        Content = ObjectUtils.JsonResponseBuilder(404, "error getting trackers")
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
        public ContentResult GetTracker(int id)
        {
            try {
                mainService.bdd.connectOpen();

                Tracker tracker = mainService.bddTracker.selectTracker(id);

                if (tracker != null)
                    return new ContentResult()
                    {
                        StatusCode = 200,
                        Content = ObjectUtils.JsonResponseBuilder(200, tracker)
                    };
                else
                    return new ContentResult()
                    {
                        StatusCode = 404,
                        Content = ObjectUtils.JsonResponseBuilder(404, "unfounded tracker")
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

        [Route("")]
        [HttpPost]
        public ContentResult CreateTracker([FromBody] TrackerCreateDTO dto)
        {
            try{
                mainService.bdd.connectOpen();

                int idUser = dto.idUser;

                Tracker trackerToInsert = dto.toTracker();

                Tracker trackerResp = mainService.bddTracker.insertTracker(trackerToInsert);

                if (trackerResp != null)
                    return new ContentResult()
                    {
                        StatusCode = 200,
                        Content = ObjectUtils.JsonResponseBuilder(200, trackerResp)
                    };
                else
                    return new ContentResult()
                    {
                        StatusCode = 404,
                        Content = ObjectUtils.JsonResponseBuilder(404, "error creation tracker")
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
        public ContentResult DeleteTracker([FromQuery] int id = -1, [FromQuery] int idUser = -1)
        {
            try
            {
                mainService.bdd.connectOpen();

                if (idUser < 0 && id < 0) return new ContentResult()
                {
                    StatusCode = 404,
                    Content = ObjectUtils.JsonResponseBuilder(404, "id and idUser attributes missing.")
                };

                if (idUser < 0) return new ContentResult()
                {
                    StatusCode = 404,
                    Content = ObjectUtils.JsonResponseBuilder(404, "idUser attribute missing.")
                };


                if (id < 0) return new ContentResult()
                {
                    StatusCode = 404,
                    Content = ObjectUtils.JsonResponseBuilder(404, "id attribute missing.")
                };

                Tracker tracker = mainService.bddTracker.deleteTracker(id, idUser);

                if (tracker != null)
                    return new ContentResult()
                    {
                        StatusCode = 200,
                        Content = ObjectUtils.JsonResponseBuilder(200, tracker)
                    };
                else
                    return new ContentResult()
                    {
                        StatusCode = 404,
                        Content = ObjectUtils.JsonResponseBuilder(404, "unfounded tracker")
                    };
            }
            catch(Exception ex)
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
        public ContentResult UpdateTracker([FromBody] TrackerUpdateDTO dto)
        {
           
            try
            {
                mainService.bdd.connectOpen();

                int idUser = dto.idUser;

                Tracker trackerToInsert = dto.toTracker();
                Tracker trackerResp = mainService.bddTracker.updateTracker(trackerToInsert, idUser);

                if (trackerResp != null)
                    return new ContentResult()
                    {
                        StatusCode = 200,
                        Content = ObjectUtils.JsonResponseBuilder(200, trackerResp)
                    };
                else
                    return new ContentResult()
                    {
                        StatusCode = 404,
                        Content = ObjectUtils.JsonResponseBuilder(404, "error modifying tracker")
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
