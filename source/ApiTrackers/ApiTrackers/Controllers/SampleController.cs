using Microsoft.AspNetCore.Mvc;
using System;
using ApiTrackers.Objects;
using ApiTrackers;
using ApiTrackers.Exceptions;
using ApiTrackers.DTO_ApiParameters;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ApiSamples.Controllers
{
    [ApiController]
    [Route("Samples")]
    public class SampleController : ControllerBase
    {
        public Main mainService; //

        public SampleController(Main _mainService)
        {
            mainService = _mainService;
        }

        [HttpGet]
        [Route("")]
        public ContentResult GetSamples()
        {
            try {
                mainService.bdd.connectOpen();

                List<Sample> samples = mainService.bddSamples.selectSamples();

                if (samples != null)
                {
                    return new ContentResult()
                    {
                        StatusCode = 200,
                        Content = Static.jsonResponseArray<Sample>(200, samples)
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
            finally
            {
                mainService.bdd.connectClose();
            }
        }

        [HttpGet]
        [Route("{id}")]
        public ContentResult GetSample(int id)
        {
            try
            {
                mainService.bdd.connectOpen();
                Sample sample = mainService.bddSamples.selectSample(id);

                if (sample != null)
                    return new ContentResult()
                    {
                        StatusCode = 200,
                        Content = Static.jsonResponseObject(200, sample)
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
            finally
            {
                mainService.bdd.connectClose();
            }
        }

        [Route("")]
        [HttpPost]
        public ContentResult CreateSample([FromBody] SampleCreateDTO dto)
        {
            try
            {
                mainService.bdd.connectOpen();

                Sample sampleToInsert = dto.toSample();

                Sample sampleResp = mainService.bddSamples.insertSample(sampleToInsert);

                if (sampleResp != null)
                    return new ContentResult()
                    {
                        StatusCode = 200,
                        Content = Static.jsonResponseObject(200, sampleResp)
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
            finally
            {
                mainService.bdd.connectClose();
            }
        }

        [HttpDelete]
        [Route("")]
        public ContentResult DeleteSample([FromQuery] int id = -1)
        {
            try
            {
                mainService.bdd.connectOpen();

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
                        Content = Static.jsonResponseObject(200, sample)
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
            finally
            {
                mainService.bdd.connectClose();
            }
        }
        [HttpPut]
        [Route("")]
        public ContentResult UpdateSample([FromBody] SampleUpdateDTO dto)
        {
            try
            {
                mainService.bdd.connectOpen();
                Sample sampleToInsert = dto.toSample();
                 Sample sampleResp = mainService.bddSamples.updateSample(sampleToInsert, dto.id);

                if (sampleResp != null)
                    return new ContentResult()
                    {
                        StatusCode = 200,
                        Content = Static.jsonResponseObject(200, sampleResp)
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
            finally
            {
                mainService.bdd.connectClose();
            }
        }


    }

}
