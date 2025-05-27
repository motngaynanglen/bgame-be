namespace BG_IMPACT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControllerBase : Controller
    {
        private IMediator mediator;
        protected IMediator _mediator => mediator ?? HttpContext.RequestServices.GetService<IMediator>();
    }
}
