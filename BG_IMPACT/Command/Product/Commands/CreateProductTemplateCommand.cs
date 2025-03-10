using BG_IMPACT.Repositories.Interfaces;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BG_IMPACT.Command.Product.Commands
{
    public class CreateProductTemplateCommand : IRequest<object>
    {
        [Required]
        public Guid ProductGroupRefId { get; set; }
        [Required]
        public string ProductName { get; set; } = string.Empty;
        [Required]
        public string Image { get; set; } = string.Empty;
        [Required]
        public double Price { get; set; }
        [Required]
        public double RentPrice { get; set; }

        public class CreateProductTemplateCommandHandler : IRequestHandler<CreateProductTemplateCommand, object>
        {
            private readonly IProductRepository _productRepository;

            public CreateProductTemplateCommandHandler(IProductRepository productRepository)
            {
                _productRepository = productRepository;
            }

            public async Task<object> Handle(CreateProductTemplateCommand request, CancellationToken cancellationToken)
            {
                string Creator = "ADMIN";

                object param = new
                {
                    request.ProductGroupRefId,
                    request.ProductName,
                    request.Image,
                    request.Price,
                    request.RentPrice,
                    Creator,
                };

                var result = await _productRepository.spProductCreateTemplate(param);

                return result;
            }
        }
    }
}
