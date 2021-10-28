﻿using Microsoft.AspNetCore.Mvc;
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
    [Route("Cells")]
    public class CellsController : ControllerBase
    {
        public MainService mainService; //

        public CellsController(MainService _mainService)
        {
            mainService = _mainService;
        }

        [HttpGet]
        [Route("")]
        public ContentResult GetCells()
        {
            List<Note> cells = mainService.bddCells.selectCells();

            if (cells != null)
            {
                return new ContentResult()
                {
                    StatusCode = 200,
                    Content = Static.jsonResponseArray(200, typeof(Note), cells)
                };
            }
            else
            {
                return new ContentResult()
                {
                    StatusCode = 404,
                    Content = Static.jsonResponseError(404, "error getting cells")
                };
            }
        }

    }
}
