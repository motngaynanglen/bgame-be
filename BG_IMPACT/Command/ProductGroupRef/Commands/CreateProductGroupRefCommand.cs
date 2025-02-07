using BG_IMPACT.Command.ProductGroup.Commands;
using BG_IMPACT.Repositories.Interfaces;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BG_IMPACT.Command.ProductGroupRef.Commands
{
    public class CreateProductGroupRefCommand : IRequest<object>
    {
        [Required]
        public Guid GroupId { get; set; }
        [Required]
        public string Prefix { get; set; } = string.Empty;
        [Required]
        public string GroupRefName { get; set; } = string.Empty;
        public class CreateProductGroupRefCommandHandler : IRequestHandler<CreateProductGroupRefCommand, object>
        {
            public readonly IProductGroupRefRepository _productGroupRefRepository;

            public CreateProductGroupRefCommandHandler(IProductGroupRefRepository productGroupRefRepository)
            {
                _productGroupRefRepository = productGroupRefRepository;
            }

            public async Task<object> Handle(CreateProductGroupRefCommand request, CancellationToken cancellationToken)
            {
                object param = new
                {
                    GroupId = request.GroupId,
                    Prefix = request.Prefix,
                    GroupRefName = request.GroupRefName,
                    Creator = "ADMIN"
                };

                var result = await _productGroupRefRepository.spProductGroupRefCreate(param);

                return result;
            }
        }
    }
}
