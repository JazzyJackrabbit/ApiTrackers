using Microsoft.AspNetCore.Mvc;
using System;
using ApiTrackers.Objects;
using ApiTrackers;
using ApiTrackers.Exceptions;
using ApiTrackers.DTO_ApiParameters;
using System.Collections.Generic;

namespace ApiSamples.Controllers
{
    [ApiController]
    [Route("Samples")]
    public class SampleController : ControllerBase
    {
        public MainService mainService; //

        public SampleController(MainService _mainService)
        {
            mainService = _mainService;
        }

        [HttpGet]
        [Route("")]
        public ContentResult GetSamples()
        {
            try { 
                List<Sample> samples = mainService.bddSamples.selectSamples();

                if (samples != null)
                {
                    return new ContentResult()
                    {
                        StatusCode = 200,
                        Content = Static.jsonResponseArray(200, typeof(Sample), samples)
                    };
                }
                else
                {
                    return new ContentResult()
                    {
                        StatusCode = 404,
                        Content = Static.jsonResponseError(404, "error getting samples")
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
        public ContentResult GetSample(int id)
        {
            try { 
                Sample sample = mainService.bddSamples.selectSample(id);

                if (sample != null)
                    return new ContentResult()
                    {
                        StatusCode = 200,
                        Content = Static.jsonResponseObject(200, typeof(Sample), sample)
                    };
                else
                    return new ContentResult()
                    {
                        StatusCode = 404,
                        Content = Static.jsonResponseError(404, "unfounded sample")
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
        public ContentResult CreateSample([FromBody] SampleCreateDTO dto)
        {
            try { 
                Sample sampleToInsert = dto.toSample();

                Sample sampleResp = mainService.bddSamples.insertSample(sampleToInsert);

                if (sampleResp != null)
                    return new ContentResult()
                    {
                        StatusCode = 200,
                        Content = Static.jsonResponseObject(200, typeof(Sample), sampleResp)
                    };
                else
                    return new ContentResult()
                    {
                        StatusCode = 404,
                        Content = Static.jsonResponseError(404, "error creation sample")
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
        public ContentResult DeleteSample([FromQuery] int id = -1)
        {
            try
            {
                int idUser = 1;
                //TODO AUTHENT TOKEN

                if (id < 0) return new ContentResult()
                {
                    StatusCode = 404,
                    Content = Static.jsonResponseError(404, "id attribute missing.")
                };

                Sample sample = mainService.bddSamples.deleteSample(id, idUser);

                if (sample != null)
                    return new ContentResult()
                    {
                        StatusCode = 200,
                        Content = Static.jsonResponseObject(200, typeof(Sample), sample)
                    };
                else
                    return new ContentResult()
                    {
                        StatusCode = 404,
                        Content = Static.jsonResponseError(404, "unfounded sample")
                    };
            }
            catch (ForbiddenException ex)
            {
                return new ContentResult()
                {
                    StatusCode = ex.code,
                    Content = Static.jsonResponseError(ex.code, ex.Message)
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
        [HttpPut]
        [Route("")]
        public ContentResult UpdateSample([FromBody] SampleUpdateDTO dto)
        {
            try { 
                Sample sampleToInsert = dto.toSample();
                 Sample sampleResp = mainService.bddSamples.updateSample(sampleToInsert, dto.id);

                if (sampleResp != null)
                    return new ContentResult()
                    {
                        StatusCode = 200,
                        Content = Static.jsonResponseObject(200, typeof(Sample), sampleResp)
                    };
                else
                    return new ContentResult()
                    {
                        StatusCode = 404,
                        Content = Static.jsonResponseError(404, "error modifying sample")
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
