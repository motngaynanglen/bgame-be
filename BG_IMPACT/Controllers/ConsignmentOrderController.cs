using BG_IMPACT.Command.BookList.Queries;
using BG_IMPACT.Command.ConsignmentOrder.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BG_IMPACT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsignmentOrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ConsignmentOrderController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [Authorize(Roles = "STAFF,MANAGER")]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateConsignmentOrderCommand command)
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
        [HttpPost("cancel")]
        public async Task<IActionResult> Cancel([FromBody] CancelConsignmentOrderCommand command)
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
        public async Task<IActionResult> GetList([FromBody] GetConsignmentOrderListQuery command)
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

        [HttpPost("get-by-id")]
        public async Task<IActionResult> GetById([FromBody] GetConsignmentOrderByIdQuery command)
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
        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] UpdateConsignmentOrderCommand command)
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
