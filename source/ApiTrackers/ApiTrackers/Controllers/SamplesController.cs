using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using static ApiTrackers.BDD_MainService;
using ApiTrackers.Objects;
using System.IO;
using System.Text;
using ApiTrackers.ApiParams;
using ApiTrackers.DTO_ApiParameters;
using ApiTrackers.Exceptions;

namespace ApiTrackers.Controllers
{
    [ApiController]
    [Route("Samples")]
    public class SamplesController : ControllerBase
    {
        public MainService mainService; //

        public SamplesController(MainService _mainService)
        {
            mainService = _mainService;
        }

        [HttpGet]
        [Route("")]
        public ContentResult GetSamples()
        {
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

    }
}
