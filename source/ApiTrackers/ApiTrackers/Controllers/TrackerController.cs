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
    [Route("Tracker")]
    public class TrackerController : ControllerBase
    {
        public MainService mainService; //

        public TrackerController(MainService _mainService)
        {
            mainService = _mainService;
        }

        [HttpGet]
        [Route("{id}")]
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

        [Route("Create")]
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
        [Route("Delete")]
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
        [Route("Update")]
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

    }
}
