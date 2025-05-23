using BG_IMPACT.Command.Store.Commands;
using BG_IMPACT.Command.Store.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BG_IMPACT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateStoreCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("change-status")]
        public async Task<IActionResult> ChangeStatus(ChangeStoreStatusCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("get-list")]
        public async Task<IActionResult> GetList(GetStoreListQuery command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("get-list-by-group-ref-id")]
        public async Task<IActionResult> GetListByGroupRefId(GetStoreListByGroupRefIdQuery command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPost("get-list-by-product-template-id")]
        public async Task<IActionResult> GetStoreListAndProductCountByIdQuery(GetStoreListAndProductCountByIdQuery command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [Authorize(Roles = "STAFF,MANAGER")]
        [HttpGet("get-store-id")]
        public async Task<IActionResult> GetStoreID([FromQuery]GetStoreIDQuery command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
