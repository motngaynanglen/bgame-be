using BG_IMPACT.Repositories.Interfaces;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BG_IMPACT.Command.Product.Commands
{
    public class CreateProductUnknownCommand : IRequest<object>
    {
        [Required]
        public string GroupName { get; set; } = string.Empty;
        [Required]
        public Guid GroupId { get; set; }
        [Required]
        public string Prefix { get; set; } = string.Empty;
        [Required]
        public string GroupRefName { get; set; } = string.Empty;
        [Required]
        public Guid ProductGroupRefId { get; set; }
        [Required]
        public string ProductName { get; set; } = string.Empty;
        [Required]
        public string Image { get; set; } = string.Empty;
        [Required]
        public double Price { get; set; }


        public class CreateProductUnknownCommandHandler : IRequestHandler<CreateProductUnknownCommand, object>
        {
            private readonly IProductRepository _productRepository;

            public CreateProductUnknownCommandHandler(IProductRepository productRepository)
            {
                _productRepository = productRepository;
            }

            public async Task<object> Handle(CreateProductUnknownCommand request, CancellationToken cancellationToken)
            {
                string Creator = "ADMIN";

                object param = new
                {
                    request.GroupName,
                    request.GroupId,
                    request.Prefix,
                    request.GroupRefName,
                    request.ProductGroupRefId,
                    request.ProductName,
                    request.Image,
                    request.Price,
                    Creator,
                };

                var result = await _productRepository.spProductCreateUnknown(param);

                return result;
            }
        }
    }
}
