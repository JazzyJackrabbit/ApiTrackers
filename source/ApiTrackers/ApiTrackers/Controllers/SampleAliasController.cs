using Microsoft.AspNetCore.Mvc;
using System;
using ApiTrackers.Objects;
using ApiTrackers;
using ApiTrackers.Exceptions;
using ApiTrackers.DTO_ApiParameters;
using System.Collections.Generic;

namespace ApiSampleAliass.Controllers
{
    [ApiController]
    [Route("SamplesAlias")]
    public class SampleAliasController : ControllerBase
    {
        public Main mainService; //

        public SampleAliasController(Main _mainService)
        {
            mainService = _mainService;
        }

        [HttpGet]
        [Route("")]
        public ContentResult GetSamplesAlias([FromQuery] int idUser = -1, [FromQuery] int idSample = -1)
        {
            try { 

                if(idUser < 0)
                    return new ContentResult()
                    {
                        StatusCode = 400,
                        Content = Static.jsonResponseError(400, "Missing idUser parameter.")
                    };

                if (idSample < 0) // by User
                {
                    List<SampleAlias> samplesAlias = mainService.bddSamplesAlias.selectSamplesAliasByIdUser(idUser);

                    if (samplesAlias != null)
                    {
                        return new ContentResult()
                        {
                            StatusCode = 200,
                            Content = Static.jsonResponseArray(200, typeof(SampleAlias), samplesAlias)
                        };
                    }
                    else
                    {
                        return new ContentResult()
                        {
                            StatusCode = 404,
                            Content = Static.jsonResponseError(404, "error getting sampleAliass")
                        };
                    }
                }
                else // by both
                {
                    SampleAlias sampleAlias = mainService.bddSamplesAlias.selectSampleAlias(idUser, idSample);

                    if (sampleAlias != null)
                    {
                        return new ContentResult()
                        {
                            StatusCode = 200,
                            Content = Static.jsonResponseArray(200, typeof(SampleAlias), sampleAlias)
                        };
                    }
                    else
                    {
                        return new ContentResult()
                        {
                            StatusCode = 404,
                            Content = Static.jsonResponseError(404, "error getting sampleAliass")
                        };
                    }
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
        public ContentResult CreateSampleAlias([FromBody] SampleAliasCreateDTO dto)
        {
            try { 
                SampleAlias sampleAliasToInsert = dto.toSampleAlias();

                SampleAlias sampleAliasResp = mainService.bddSamplesAlias.insertSampleAlias(sampleAliasToInsert);

                if (sampleAliasResp != null)
                    return new ContentResult()
                    {
                        StatusCode = 200,
                        Content = Static.jsonResponseObject(200, typeof(SampleAlias), sampleAliasResp)
                    };
                else
                    return new ContentResult()
                    {
                        StatusCode = 404,
                        Content = Static.jsonResponseError(404, "error creation sampleAlias")
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
        public ContentResult DeleteSampleAlias([FromQuery] int id = -1)
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

                SampleAlias sampleAlias = mainService.bddSamplesAlias.deleteSampleAlias(id, idUser);

                if (sampleAlias != null)
                    return new ContentResult()
                    {
                        StatusCode = 200,
                        Content = Static.jsonResponseObject(200, typeof(SampleAlias), sampleAlias)
                    };
                else
                    return new ContentResult()
                    {
                        StatusCode = 404,
                        Content = Static.jsonResponseError(404, "unfounded sampleAlias")
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
        public ContentResult UpdateSampleAlias([FromBody] SampleAliasUpdateDTO dto)
        {
            try { 
                SampleAlias sampleAliasToInsert = dto.toSampleAlias();
                sampleAliasToInsert.id = dto.id;
                SampleAlias sampleAliasResp = mainService.bddSamplesAlias.updateSampleAlias(sampleAliasToInsert);

                if (sampleAliasResp != null)
                    return new ContentResult()
                    {
                        StatusCode = 200,
                        Content = Static.jsonResponseObject(200, typeof(SampleAlias), sampleAliasResp)
                    };
                else
                    return new ContentResult()
                    {
                        StatusCode = 404,
                        Content = Static.jsonResponseError(404, "error modifying sampleAlias")
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
