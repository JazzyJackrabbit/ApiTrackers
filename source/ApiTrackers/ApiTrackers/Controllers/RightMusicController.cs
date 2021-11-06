using Microsoft.AspNetCore.Mvc;
using System;
using ApiTrackers.Objects;
using ApiTrackers;
using ApiTrackers.Exceptions;
using ApiTrackers.DTO_ApiParameters;
using System.Collections.Generic;
using MailingTest;
using MySql.Data.MySqlClient;

namespace ApiRightMusics.Controllers
{
    [ApiController]
    [Route("RightMusics")]
    public class RightMusicController : ControllerBase
    {
        public Main mainService; //

        public RightMusicController(Main _mainService)
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
                    List<RightMusic> rightMusics_resp = mainService.bddRightMusics.selectRightMusics_bytrackerid(idTracker, true);
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
                    List<RightMusic> rightMusics_resp = mainService.bddRightMusics.selectRightMusics_byuserid(idUser, true);
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
                    RightMusic rightMusic_resp = mainService.bddRightMusics.selectRightMusic(idTracker, idUser, true);
                    if (rightMusic_resp!=null)
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
                mainService.bdd.connectOpen(false); //<<<< todo mettre ceci dans bdd 
                MySqlTransaction mysqltransaction = mainService.bdd.getSqlConnection().BeginTransaction();

                try
                {
                    RightMusic rightCheck = mainService.bddRightMusics.createRightMusic(dto.toRightMusic(), false);

                    mysqltransaction.Commit();

                    // Check 
                    Tracker tracker = mainService.bddTracker.selectTracker(rightCheck.idTracker, false);
                    User userowner = mainService.bddUser.selectUser(tracker.idUser, false);
                    User usergiven = mainService.bddUser.selectUser(rightCheck.idUser, false);
                    
                    // Sending Mail
                    MailService mailService = new MailService();
                    mailService.sendMail_GivenWriteAccess(tracker, userowner, usergiven);


                    return new ContentResult()
                    {
                        StatusCode = 200,
                        Content = Static.jsonResponseObject(200, typeof(RightMusic), rightCheck)
                    };

                }
                catch (Exception ex1)
                {
                    try
                    {
                        mysqltransaction.Rollback();
                    }
                    catch (Exception ex2)
                    {
                        if (mysqltransaction.Connection != null)
                        {
                            Console.WriteLine("An exception of type " + ex2.GetType() +
                            " was encountered while attempting to roll back the transaction.");
                        }

                    }

                    Console.WriteLine("An exception of type " + ex1.GetType() + " was encountered while inserting the data.");
                    Console.WriteLine("Neither record was written to database.");

                    return new ContentResult()
                    {
                        StatusCode = 500,
                        Content = Static.jsonResponseError(500, "Internal Error: " + ex1.Message)
                    };
                }
                finally
                {
                    mainService.bdd.connectClose(false);
                }
            }
            catch {
                return new ContentResult()
                {
                    StatusCode = 500,
                    Content = Static.jsonResponseError(500, "Internal Connection Error.")
                };
            }
        }

        [Route("")]
        [HttpPut]
        public ContentResult PutRightMusic([FromBody] RightMusicChangeDTO dto)
        {
            try
            {
                mainService.bdd.connectOpen(false);
                MySqlTransaction mysqltransaction = mainService.bdd.getSqlConnection().BeginTransaction();

                try
                {
                    RightMusic rightCheck = mainService.bddRightMusics.changeRightMusic(dto.toRightMusic(), false);

                    // Check 
                    Tracker tracker = mainService.bddTracker.selectTracker(rightCheck.idTracker, false);
                    User userowner = mainService.bddUser.selectUser(tracker.idUser, false);
                    User usergiven = mainService.bddUser.selectUser(rightCheck.idUser, false);

                    // Sending Mail
                    MailService mailService = new MailService();
                    mailService.sendMail_GivenWriteAccess(tracker, userowner, usergiven);

                    mysqltransaction.Commit();

                    return new ContentResult()
                    {
                        StatusCode = 200,
                        Content = Static.jsonResponseObject(200, typeof(RightMusic), rightCheck)
                    };

                }
                catch (Exception ex1)
                {
                    try
                    {
                        mysqltransaction.Rollback();
                    }
                    catch (Exception ex2)
                    {
                        if (mysqltransaction.Connection != null)
                        {
                            Console.WriteLine("An exception of type " + ex2.GetType() +
                            " was encountered while attempting to roll back the transaction.");
                        }

                    }

                    Console.WriteLine("An exception of type " + ex1.GetType() + " was encountered while inserting the data.");
                    Console.WriteLine("Neither record was written to database.");

                    return new ContentResult()
                    {
                        StatusCode = 500,
                        Content = Static.jsonResponseError(500, "Internal Error: " + ex1.Message)
                    };
                }
                finally
                {
                    mainService.bdd.connectClose(false);
                }
            }
            catch
            {
                return new ContentResult()
                {
                    StatusCode = 500,
                    Content = Static.jsonResponseError(500, "Internal Connection Error.")
                };
            }
        }
      

    }
}
