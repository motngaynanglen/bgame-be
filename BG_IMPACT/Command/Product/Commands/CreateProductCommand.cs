using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BG_IMPACT.Command.Product.Commands
{
    public class CreateProductCommand : IRequest<object>
    {
        [Required]
        public Guid StoreId { get; set; }
        [Required]
        public string ProductName { get; set; } = string.Empty;
        [Required]
        public Guid ProductGroupId { get; set; }

        public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, object>
        {
            public async Task<object> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {
                object param = new
                {

                };

                return param;
            }
        }
    }
}
