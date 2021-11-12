using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using ApiTrackers.DTO_ApiParameters.Module;
using ApiTrackers.Objects;

namespace ApiTrackers.Controllers
{
    [ApiController]
    [Route("Upload")]
    public class ModuleController : ControllerBase
    {
        public Main mainService; 

        public ModuleController(Main _mainService)
        {
            mainService = _mainService;
        }

        [Route("")]
        [HttpPost]
        public ContentResult InsertModules(ModulesConvertDTO _dto)
        {
            try{

                mainService.bdd.connectOpen();

                User currentUser = new User();
                currentUser.id = 1;     // todo change < _dto.idUser ?
                List<Module> modules = _dto.toModules(currentUser);

                foreach(Module module in modules) { 

                    // todor response faire la liste des fichier OK ou KO
                    mainService.bddTracker.insertTracker(module.getTracker());


                    bool isConverted = false;
                    bool isInseredOnDB = false;

                    if (isConverted) { 
                        if (isInseredOnDB)
                        {
                            
                        }
                       
                        else
                        {

                            return new ContentResult()
                            {
                                StatusCode = 500,
                                Content = Static.jsonResponseError(500, "internal database error.")
                            };
                        }
                    }
                    else { 
                        return new ContentResult()
                        {
                            StatusCode = 404,
                            Content = Static.jsonResponseError(404, "error conversion module")
                        };
                    }
                }

                return new ContentResult()
                {
                    StatusCode = 200,
                    Content = Static.jsonResponseObject(200, "OK")
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
