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


                mainService.bddTracker.insertTracker(module.getTracker());

                JArray respArr = new JArray();

                foreach (Sample sample in module.getSamples()) {

                    JObject objrow = new JObject();

                    if (sample != null)
                    {
                        Sample sampleRespInsert = mainService.bddSamples.insertSample(sample);

                        if (sampleRespInsert != null)
                        {
                            objrow.Add("sample", Static.ConvertToJObject(sampleRespInsert));
                        }
                        else
                        {
                            objrow.Add("sample", "KO");
                        }
                    }
                    else
                    {
                        objrow.Add("sample", "KO");
                    }

                    respArr.Add(objrow);

                }

                JObject respSamples = new JObject();
                respSamples.Add("samples", respArr);

                return new ContentResult()
                {
                    StatusCode = 200,
                    Content = Static.jsonResponseObject(200, respSamples)
                };

            }
            catch (Exception ex)
            {
                return new ContentResult()
                {
                    StatusCode = 400,
                    Content = Static.jsonResponseError(400, "Error Traitment Module from ModArchive: " + ex.Message)
                };
            }
            finally
            {
                mainService.bdd.connectClose();
            }
        }
        
    }

}
