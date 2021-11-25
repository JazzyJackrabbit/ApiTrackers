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
using Newtonsoft.Json.Linq;

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

                List<Sample> samplesMdl = module.getSamples();

                if(samplesMdl == null)
                    return new ContentResult()
                    {
                        StatusCode = 500,
                        Content = ObjectUtils.JsonResponseBuilder(500, "Samples not found error.")
                    };
                

                mainService.bddTracker.insertTracker(module.getTracker());

                foreach(Sample sample in samplesMdl) { 
                    mainService.bddSamples.insertSample(sample);
                }

                foreach (Note note in module.getNotes())
                    mainService.bddCells.insertCell(note);
                

                //JObject samplesJson = new JObject()

                return new ContentResult()
                {
                    StatusCode = 200,
                    Content = ObjectUtils.JsonResponseBuilder(200, samplesMdl)
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
