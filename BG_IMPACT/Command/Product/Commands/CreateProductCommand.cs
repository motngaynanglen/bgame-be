using BG_IMPACT.Repositories.Interfaces;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BG_IMPACT.Command.Product.Commands
{
    public class CreateProductCommand : IRequest<object>
    {
        [Required]
        public Guid ProductTemplateId { get; set; }
        [Required]
        public double Number { get; set; }

        public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, object>
        {
            private readonly IProductRepository _productRepository;

            public CreateProductCommandHandler(IProductRepository productRepository)
            {
                _productRepository = productRepository;
            }

            public async Task<object> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {
                string Creator = "ADMIN";

                object param = new
                {
                    request.ProductTemplateId,
       
                    Creator,
                };

                var result = await _productRepository.spProductCreateTemplate(param);

                return result;
            }
        }
    }
}
