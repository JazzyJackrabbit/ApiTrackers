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

   
        [Route("ModArchive/Samples/{id}")]
        [HttpPost]
        public ContentResult InsertSamples(int id)
        {
            try
            {
                //TODO: you need to be connect (user connection missing).

                mainService.bdd.connectOpen();

                int id_ModArchive = id;

                 User userTest = mainService.bddUser.selectUser(1);
                 Module module = new Module(userTest, id_ModArchive);



                mainService.bddTracker.insertTracker(module.getTracker());

                foreach(Sample sample in module.getSamples()) { 
                    mainService.bddSamples.insertSample(sample);
                }

                foreach (Note note in module.getNotes())
                    mainService.bddCells.insertCell(note);
                


                return new ContentResult()
                {
                    StatusCode = 200,
                    Content = Static.jsonResponseObject(200, "==> ")
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
