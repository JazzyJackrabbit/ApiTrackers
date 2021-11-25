using Microsoft.AspNetCore.Mvc;
using System;
using ApiTrackers;
using ApiTrackers.Exceptions;
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
            try
            {
                mainService.bdd.connectOpen();

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
            finally
            {
                mainService.bdd.connectClose();
            }
        }

        [Route("")]
        [HttpPost]
        public ContentResult CreateCell([FromBody] CellCreateDTO dto)
        {
            try
            {
                mainService.bdd.connectOpen();
                Note cellToInsert = dto.toCell();

                int idTracker = cellToInsert.parentTracker.id;

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
            finally
            {
                mainService.bdd.connectClose();
            }
        }

        [HttpDelete]
        [Route("")]
        public ContentResult DeleteCell([FromQuery] int id = -1)
        {
            try
            {
                mainService.bdd.connectOpen();
                int idUser = 1;

                if(id<0) return new ContentResult()
                {
                    StatusCode = 403,
                    Content = ObjectUtils.JsonResponseBuilder(404, "unauthorized access.")
                };

                Note cell = mainService.bddCells.deleteCell(id, idUser);

                int idTracker = cell.parentTracker.id;

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
            finally
            {
                mainService.bdd.connectClose();
            }
        }
        [HttpPut]
        [Route("")]
        public ContentResult UpdateCell([FromBody] CellUpdateDTO dto)
        {
            try
            {
                mainService.bdd.connectOpen();
                Note cellToInsert = dto.toCell();
                Note cellResp = mainService.bddCells.updateCell(cellToInsert, dto.id);

                int idTracker = cellToInsert.parentTracker.id;

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
            finally
            {
                mainService.bdd.connectClose();
            }
        }

    }

}
