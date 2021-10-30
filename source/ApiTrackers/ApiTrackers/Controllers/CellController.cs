using Microsoft.AspNetCore.Mvc;
using System;
using ApiTrackers.Objects;
using ApiTrackers;
using ApiTrackers.Exceptions;
using ApiTrackers.DTO_ApiParameters;
using System.Collections.Generic;

namespace ApiCells.Controllers
{
    [ApiController]
    [Route("Cells")]
    public class CellController : ControllerBase
    {
        public MainService mainService; //

        public CellController(MainService _mainService)
        {
            mainService = _mainService;
        }

        [HttpGet]
        [Route("")]
        public ContentResult GetCells([FromQuery] int idTracker = -1, [FromQuery] int id = -1)
        {
            //TODO idTracker
            try {

                if (id < 0)
                {
                    List<Note> cells = mainService.bddCells.selectCells(idTracker);

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
                else
                {
                    Note cell = mainService.bddCells.selectCell(id, idTracker);

                    if (cell != null)
                        return new ContentResult()
                        {
                            StatusCode = 200,
                            Content = Static.jsonResponseObject(200, typeof(Note), cell)
                        };
                    else
                        return new ContentResult()
                        {
                            StatusCode = 404,
                            Content = Static.jsonResponseError(404, "unfounded cell")
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

        [Route("")]
        [HttpPost]
        public ContentResult CreateCell([FromBody] CellCreateDTO dto)
        {
            //TODO idTracker
            try { 
                Note cellToInsert = dto.toCell();

                int idTracker = cellToInsert.parentTracker.idTracker;

                Note cellResp = mainService.bddCells.insertCell(cellToInsert);

                if (cellResp != null)
                    return new ContentResult()
                    {
                        StatusCode = 200,
                        Content = Static.jsonResponseObject(200, typeof(Note), cellResp)
                    };
                else
                    return new ContentResult()
                    {
                        StatusCode = 404,
                        Content = Static.jsonResponseError(404, "error creation cell")
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
        public ContentResult DeleteCell([FromBody] CellDeleteDTO dto)
        {
            //TODO idTracker

            try
            {
                int idUser = 1;
                //TODO AUTHENT TOKEN

                Note cell = mainService.bddCells.deleteCell(dto.id, idUser);

                int idTracker = cell.parentTracker.idTracker;

                if (cell != null)
                    return new ContentResult()
                    {
                        StatusCode = 200,
                        Content = Static.jsonResponseObject(200, typeof(Note), cell)
                    };
                else
                    return new ContentResult()
                    {
                        StatusCode = 404,
                        Content = Static.jsonResponseError(404, "unfounded cell")
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
        public ContentResult UpdateCell([FromBody] CellUpdateDTO dto)
        {
            //TODO idTracker
            try { 
                Note cellToInsert = dto.toCell();
                Note cellResp = mainService.bddCells.updateCell(cellToInsert, dto.id);

                int idTracker = cellToInsert.parentTracker.idTracker;

                if (cellResp != null)
                    return new ContentResult()
                    {
                        StatusCode = 200,
                        Content = Static.jsonResponseObject(200, typeof(Note), cellResp)
                    };
                else 
                    return new ContentResult()
                    {
                        StatusCode = 404,
                        Content = Static.jsonResponseError(404, "error modifying cell")
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
