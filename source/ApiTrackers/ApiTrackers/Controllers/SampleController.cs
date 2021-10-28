using Microsoft.AspNetCore.Mvc;
using System;
using ApiTrackers.Objects;
using ApiTrackers;
using ApiTrackers.Exceptions;
using ApiTrackers.DTO_ApiParameters;

namespace ApiSamples.Controllers
{
    [ApiController]
    [Route("Sample")]
    public class SampleController : ControllerBase
    {
        public MainService mainService; //

        public SampleController(MainService _mainService)
        {
            mainService = _mainService;
        }


        [HttpGet]
        [Route("{id}")]
        public ContentResult GetSample(int id)
        {
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

        [Route("Create")]
        [HttpPost]
        public ContentResult CreateSample([FromBody] SampleCreateDTO dto)
        {
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

        [HttpPost]
        [Route("Delete")]
        public ContentResult DeleteSample([FromBody] SampleDeleteDTO dto)
        {
            try
            {
                int idUser = 8;
                //TODO AUTHENT TOKEN

                Sample sample = mainService.bddSamples.deleteSample(dto.id, idUser);

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
        [HttpPost]
        [Route("Update")]
        public ContentResult UpdateSample([FromBody] SampleUpdateDTO dto)
        {
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

    }
}
