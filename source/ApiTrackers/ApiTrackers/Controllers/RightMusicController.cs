using Microsoft.AspNetCore.Mvc;
using System;
using ApiTrackers.Objects;
using ApiTrackers;
using ApiTrackers.Exceptions;
using ApiTrackers.DTO_ApiParameters;
using System.Collections.Generic;

namespace ApiRightMusics.Controllers
{
    [ApiController]
    [Route("RightMusics")]
    public class RightMusicController : ControllerBase
    {
        public MainService mainService; //

        public RightMusicController(MainService _mainService)
        {
            mainService = _mainService;
        }

        [HttpGet]
        [Route("")]
        public ContentResult GetRightMusic([FromQuery] int idUser = -1, [FromQuery] int idTracker = -1)
        {
            try { 
                if (idUser < 0 && idTracker < 0) 
                    return new ContentResult()
                    {
                        StatusCode = 400,
                        Content = Static.jsonResponseError(400, "Missing parameters.")
                    };
                else if(idUser < 0)         // Will Return   all users by tracker id 
                {
                    List<RightMusic> rightMusics_resp = mainService.bddRightMusics.selectRightMusics_bytrackerid(idTracker);
                    if (rightMusics_resp != null)
                        return new ContentResult()
                        {
                            StatusCode = 200,
                            Content = Static.jsonResponseArray(200, typeof(RightMusic), rightMusics_resp)
                        };
                    else
                        return new ContentResult()
                        {
                            StatusCode = 400,
                            Content = Static.jsonResponseError(400, "unfounded RightMusic datas.")
                        };
                }
                else if (idTracker < 0)     // Will Return   all trackers by user id 
                {
                    List<RightMusic> rightMusics_resp = mainService.bddRightMusics.selectRightMusics_byuserid(idUser);
                    if (rightMusics_resp != null)
                        return new ContentResult()
                        {
                            StatusCode = 200,
                            Content = Static.jsonResponseArray(200, typeof(RightMusic), rightMusics_resp)
                        };
                    else
                        return new ContentResult()
                        {
                            StatusCode = 400,
                            Content = Static.jsonResponseError(400, "unfounded RightMusic datas.")
                        };
                }
                else                            // Will Return   the specific rightMusic object
                {
                    RightMusic rightMusic_resp = mainService.bddRightMusics.selectRightMusic(idTracker, idUser);
                    if(rightMusic_resp!=null)
                        return new ContentResult()
                        {
                            StatusCode = 200,
                            Content = Static.jsonResponseObject(200, typeof(RightMusic), rightMusic_resp)
                        }; 
                    else 
                        return new ContentResult()
                        {
                            StatusCode = 400,
                            Content = Static.jsonResponseError(400, "unfounded RightMusic datas.")
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
        [Route("")]
        [HttpPost]
        public ContentResult PostRightMusic([FromBody] RightMusicChangeDTO dto)
        {
            try
            {
                RightMusic right = dto.toRightMusic();
                RightMusic rightCheck = mainService.bddRightMusics.createRightMusic(right.right, right.idTracker, right.idUser);

                if (rightCheck != null)
                    return new ContentResult()
                    {
                        StatusCode = 200,
                        Content = Static.jsonResponseObject(200, typeof(RightMusic), rightCheck)
                    };
                else
                    throw new Exception();

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
        [HttpPut]
        public ContentResult PutRightMusic([FromBody] RightMusicChangeDTO dto)
        {

            try
            {
                RightMusic right = dto.toRightMusic();
                RightMusic rightCheck = mainService.bddRightMusics.changeRightMusic(right.right, right.idTracker, right.idUser);

                if (rightCheck != null)
                    return new ContentResult()
                    {
                        StatusCode = 200,
                        Content = Static.jsonResponseObject(200, typeof(RightMusic), rightCheck)
                    };
                else
                    throw new Exception();

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
