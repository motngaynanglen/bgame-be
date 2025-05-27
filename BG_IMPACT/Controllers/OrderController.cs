using BG_IMPACT.Command.Account.Commands;
using BG_IMPACT.Command.Account.Queries;
using BG_IMPACT.Command.Order.Commands;
using BG_IMPACT.Command.Order.Queries;
using BG_IMPACT.Command.Product.Queries;
using CloudinaryDotNet.Actions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BG_IMPACT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "CUSTOMER,STAFF")]
        [HttpPost("create-order")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand query)
        {
            try
            {
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize(Roles = "CUSTOMER")]
        [HttpPost("create-order-by-customer")]
        public async Task<IActionResult> CreateOrderByCustomer([FromBody] CreateOrderByCustomerCommand query)
        {
            try
            {
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPost("get-by-id")]
        public async Task<IActionResult> GetById(GetOrderByIdQuery command)
        {
            try
            {
                var result = await _mediator.Send(command);
                if (result.StatusCode == "200")
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [Authorize(Roles = "CUSTOMER,STAFF")]
        [HttpPost("get-order-history")]
        public async Task<IActionResult> GetOrderHistory([FromBody] GetOrderHistoryQuery query)
        {
            try
            {
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [Authorize(Roles = "STAFF")]
        [HttpPost("update-order-delivery-info")]
        public async Task<IActionResult> UpdateOrderDeliveryInfo([FromBody] UpdateOrderDeliveryInfoCommand query)
        {
            try
            {
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [Authorize(Roles = "STAFF")]
        [HttpPost("update-order-to-paid")]
        public async Task<IActionResult> UpdateOrderToPaid([FromBody] UpdateStatusOrderToPaidCommand query)
        {
            try
            {
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize(Roles = "STAFF")]
        [HttpPost("update-order-to-sending")]
        public async Task<IActionResult> UpdateOrderToSending([FromBody] UpdateStatusOrderToSendingCommand query)
        {
            try
            {
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize(Roles = "STAFF")]
        [HttpPost("update-order-to-sent")]
        public async Task<IActionResult> UpdateOrderToSent([FromBody] UpdateStatusOrderToSentCommand query)
        {
            try
            {
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        
        [Authorize(Roles = "STAFF")]
        [HttpPost("get-unclaim-order")]
        public async Task<IActionResult> GetUnclaimOrder([FromBody] GetOrderGetUnclaimQuery query)
        {
            try
            {
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
