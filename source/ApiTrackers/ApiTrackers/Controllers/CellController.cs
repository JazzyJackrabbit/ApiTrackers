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
        public Main mainService; //

        public CellController(Main _mainService)
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
                    List<Note> cells = mainService.bddCells.selectCells(idTracker, true);

                    if (cells != null)
                    {
                        return new ContentResult()
                        {
                            StatusCode = 200,
                            Content = Static.jsonResponseArray<Note>(200, cells)
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
                    Note cell = mainService.bddCells.selectCell(id, idTracker, true);

                    if (cell != null)
                        return new ContentResult()
                        {
                            StatusCode = 200,
                            Content = Static.jsonResponseObject(200, cell)
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

                Note cellResp = mainService.bddCells.insertCell(cellToInsert, true);

                if (cellResp != null)
                    return new ContentResult()
                    {
                        StatusCode = 200,
                        Content = Static.jsonResponseObject(200, cellResp)
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
        public ContentResult DeleteCell([FromQuery] int id = -1)
        {
            //TODO idTracker

            try
            {
                int idUser = 1;
                //TODO AUTHENT TOKEN

                if(id<0) return new ContentResult()
                {
                    StatusCode = 404,
                    Content = Static.jsonResponseError(404, "id attribute missing.")
                };

                Note cell = mainService.bddCells.deleteCell(id, idUser, true);

                int idTracker = cell.parentTracker.idTracker;

                if (cell != null)
                    return new ContentResult()
                    {
                        StatusCode = 200,
                        Content = Static.jsonResponseObject(200, cell)
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
                Note cellResp = mainService.bddCells.updateCell(cellToInsert, dto.id, true);

                int idTracker = cellToInsert.parentTracker.idTracker;

                if (cellResp != null)
                    return new ContentResult()
                    {
                        StatusCode = 200,
                        Content = Static.jsonResponseObject(200, cellResp)
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
