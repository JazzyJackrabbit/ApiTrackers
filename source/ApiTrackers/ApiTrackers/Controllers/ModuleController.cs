using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using ApiTrackers.DTO_ApiParameters.Module;
using ApiTrackers.Objects;
using System.Net.Http;
using System.IO;
using System.Text;
using System.Linq;
using System.Web;
using System.Diagnostics;

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

   
        [Route("ModArchive/{id}")]
        [HttpPost]
        public ContentResult InsertModule(int id)
        {
            try
            {
                //TODO: you need to be connect (user connection missing).

                mainService.bdd.connectOpen();

                int id_ModArchive = id;

                User userTest = mainService.bddUser.selectUser(1);
                Module module = new Module(userTest, id_ModArchive);

                module = module;
                module = module;
                module = module;
                module = module;
                module = module;

                /*
                string resp = @"C:\Users\Alexandre\Desktop\serverTempModulesFiles\moonshine.mod";

                Console.Write("## LOAD MODULE TO CONVERT (PATH) ## : " + resp);
                // resp = Console.ReadLine();
                SongModule module2 = module.getSongModule();

                Stream stttt = module.getLastApiStream();
              
                stttt = stttt;
                stttt = stttt;

                if (module2.Patterns.Count > 0) module2 = module2;


                module2 = module2; 
                module2 = module2; 


               /* if (
                    sm != null &&
                    sm.SongName != null)
                {
                    sm = sm;
                }
                else
                {
                    sm = sm;
                }*/
                /*
                string t = module.getSongModule().SongName;

                bool isConverted = false;  //todo
                bool isInseredOnDB = false;
                */
                return new ContentResult()
                {
                    StatusCode = 200,
                    Content = Static.jsonResponseObject(200, "==> ")
                };
                /*
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
                

                return new ContentResult()
                {
                    StatusCode = 200,
                    Content = Static.jsonResponseObject(200, "OK")
                };*/
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
