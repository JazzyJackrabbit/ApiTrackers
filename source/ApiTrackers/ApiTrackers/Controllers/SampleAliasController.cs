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
                mainService.bdd.connectOpen();

                if(idUser < 0)
                    return new ContentResult()
                    {
                        StatusCode = 400,
                        Content = ObjectUtils.JsonResponseBuilder(400, "Missing idUser parameter.")
                    };

                if (idSample < 0) // by User
                {
                    List<SampleAlias> samplesAlias = mainService.bddSamplesAlias.selectSamplesAliasByIdUser(idUser);

                    if (samplesAlias != null)
                    {
                        return new ContentResult()
                        {
                            StatusCode = 200,
                            Content = ObjectUtils.JsonResponseBuilder(200, samplesAlias)
                        };
                    }
                    else
                    {
                        return new ContentResult()
                        {
                            StatusCode = 404,
                            Content = ObjectUtils.JsonResponseBuilder(404, "error getting sampleAliass")
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
                            Content = ObjectUtils.JsonResponseBuilder(200, sampleAlias)
                        };
                    }
                    else
                    {
                        return new ContentResult()
                        {
                            StatusCode = 404,
                            Content = ObjectUtils.JsonResponseBuilder(404, "error getting sampleAliass")
                        };
                    }
                }
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

        [Route("")]
        [HttpPost]
        public ContentResult CreateSampleAlias([FromBody] SampleAliasCreateDTO dto)
        {
            try
            {
                mainService.bdd.connectOpen();

                SampleAlias sampleAliasToInsert = dto.toSampleAlias();

                SampleAlias sampleAliasResp = mainService.bddSamplesAlias.insertSampleAlias(sampleAliasToInsert);

                if (sampleAliasResp != null)
                    return new ContentResult()
                    {
                        StatusCode = 200,
                        Content = ObjectUtils.JsonResponseBuilder(200, sampleAliasResp)
                    };
                else
                    return new ContentResult()
                    {
                        StatusCode = 404,
                        Content = ObjectUtils.JsonResponseBuilder(404, "error creation sampleAlias")
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

        [HttpDelete]
        [Route("{idSample}/User/{idUserTarget}")]
        public ContentResult DeleteSampleAlias(int idSample = -1, int idUserTarget =-1)
        {
            try
            {
                mainService.bdd.connectOpen();

                int idUser = 1;
                //TODO AUTHENT TOKEN

                if (idSample < 0) return new ContentResult()
                {
                    StatusCode = 404,
                    Content = ObjectUtils.JsonResponseBuilder(404, "idSample attribute missing.")
                };
                if (idUserTarget < 0) return new ContentResult()
                {
                    StatusCode = 404,
                    Content = ObjectUtils.JsonResponseBuilder(404, "idUserTarget attribute missing.")
                };
                if (idUserTarget < 0 && idSample < 0) return new ContentResult()
                {
                    StatusCode = 404,
                    Content = ObjectUtils.JsonResponseBuilder(404, "idUserTarget and idSample attributes missing.")
                };


                SampleAlias sampleAlias = mainService.bddSamplesAlias.deleteSampleAlias(idSample, idUserTarget, idUser);

                if (sampleAlias != null)
                    return new ContentResult()
                    {
                        StatusCode = 200,
                        Content = ObjectUtils.JsonResponseBuilder(200, sampleAlias)
                    };
                else
                    return new ContentResult()
                    {
                        StatusCode = 404,
                        Content = ObjectUtils.JsonResponseBuilder(404, "unfounded sampleAlias")
                    };
            }
            catch (ForbiddenException ex)
            {
                return new ContentResult()
                {
                    StatusCode = ex.code,
                    Content = ObjectUtils.JsonResponseBuilder(ex.code, ex.Message)
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
        [HttpPut]
        [Route("")]
        public ContentResult UpdateSampleAlias([FromBody] SampleAliasUpdateDTO dto)
        {
            try
            {
                mainService.bdd.connectOpen();

                SampleAlias sampleAliasToInsert = dto.toSampleAlias();
                SampleAlias sampleAliasResp = mainService.bddSamplesAlias.updateSampleAlias(sampleAliasToInsert);

                if (sampleAliasResp != null)
                    return new ContentResult()
                    {
                        StatusCode = 200,
                        Content = ObjectUtils.JsonResponseBuilder(200, sampleAliasResp)
                    };
                else
                    return new ContentResult()
                    {
                        StatusCode = 404,
                        Content = ObjectUtils.JsonResponseBuilder(404, "error modifying sampleAlias")
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
