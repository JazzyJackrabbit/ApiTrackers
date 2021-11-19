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
                            Content = ObjectUtils.JsonResponseBuilder(200, cells)
                        };
                    }
                    else
                    {
                        return new ContentResult()
                        {
                            StatusCode = 404,
                            Content = ObjectUtils.JsonResponseBuilder(404, "error getting cells")
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
                            Content = ObjectUtils.JsonResponseBuilder(200, cell)
                        };
                    else
                        return new ContentResult()
                        {
                            StatusCode = 404,
                            Content = ObjectUtils.JsonResponseBuilder(404, "unfounded cell")
                        };
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
                        Content = ObjectUtils.JsonResponseBuilder(200, cellResp)
                    };
                else
                    return new ContentResult()
                    {
                        StatusCode = 404,
                        Content = ObjectUtils.JsonResponseBuilder(404, "error creation cell")
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
                    StatusCode = 403,
                    Content = ObjectUtils.JsonResponseBuilder(404, "unauthorized access.")
                };

                Note cell = mainService.bddCells.deleteCell(id, idUser);

                int idTracker = cell.parentTracker.idTracker;

                if (cell != null)
                    return new ContentResult()
                    {
                        StatusCode = 200,
                        Content = ObjectUtils.JsonResponseBuilder(200, cell)
                    };
                else
                    return new ContentResult()
                    {
                        StatusCode = 404,
                        Content = ObjectUtils.JsonResponseBuilder(404, "unfounded cell")
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
                        Content = ObjectUtils.JsonResponseBuilder(200, cellResp)
                    };
                else 
                    return new ContentResult()
                    {
                        StatusCode = 404,
                        Content = ObjectUtils.JsonResponseBuilder(404, "error modifying cell")
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
        }

    }

}
