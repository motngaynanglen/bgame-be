using BG_IMPACT.Command.BookList.Commands;
using BG_IMPACT.Command.BookList.Queries;
using BG_IMPACT.Command.Product.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BG_IMPACT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookListController : ControllerBase
    {
        [Authorize(Roles = "STAFF,CUSTOMER")]
        [HttpPost("create-booklist")]
        public async Task<IActionResult> CreateBookList(CreateBookListCommand command)
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
        [HttpPost("end-booklist")]
        public async Task<IActionResult> EndBookList(EndBookListCommand command)
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
        [HttpPost("start-booklist")]
        public async Task<IActionResult> StartBookList(StartBookListCommand command)
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

        [Authorize(Roles = "CUSTOMER,STAFF,MANAGER")]
        [HttpPost("get-booklist-by-date")]
        public async Task<IActionResult> GetBookListByDate(GetBookListByDateQuery command)
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
        [HttpPost("extend-booklist")]
        public async Task<IActionResult> ExtendBookList(ExtendBookListCommand command)
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
