using Microsoft.AspNetCore.Mvc;
using System;
using ApiTrackers.Objects;
using ApiTrackers;
using ApiTrackers.Exceptions;
using ApiTrackers.DTO_ApiParameters;

namespace ApiCells.Controllers
{
    [ApiController]
    [Route("Cell")]
    public class CellController : ControllerBase
    {
        public MainService mainService; //

        public CellController(MainService _mainService)
        {
            mainService = _mainService;
        }


        [HttpGet]
        [Route("{id}")]
        public ContentResult GetCell(int id)
        {
            Note cell = mainService.bddCells.selectCell(id);

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

        [Route("Create")]
        [HttpPost]
        public ContentResult CreateCell([FromBody] CellCreateDTO dto)
        {
            Note cellToInsert = dto.toCell();

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

        [HttpPost]
        [Route("Delete")]
        public ContentResult DeleteCell([FromBody] CellDeleteDTO dto)
        {
            try
            {
                int idUser = 8;
                //TODO AUTHENT TOKEN

                Note cell = mainService.bddCells.deleteCell(dto.id, idUser);

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
        [HttpPost]
        [Route("Update")]
        public ContentResult UpdateCell([FromBody] CellUpdateDTO dto)
        {
            Note cellToInsert = dto.toCell();
            Note cellResp = mainService.bddCells.updateCell(cellToInsert, dto.id);

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

    }
}
