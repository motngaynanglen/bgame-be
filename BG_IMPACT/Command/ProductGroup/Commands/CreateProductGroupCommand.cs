using BG_IMPACT.Repositories.Interfaces;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BG_IMPACT.Command.ProductGroup.Commands
{
    public class CreateProductGroupCommand : IRequest<object>
    {
        [Required]
        public string GroupName { get; set; } = string.Empty;

        public class CreateProductGroupCommandHandler : IRequestHandler<CreateProductGroupCommand, object>
        {
            public readonly IProductGroupRepository _productGroupRepository;

            public CreateProductGroupCommandHandler(IProductGroupRepository productGroupRepository)
            {
                _productGroupRepository = productGroupRepository;
            }

            public async Task<object> Handle(CreateProductGroupCommand request, CancellationToken cancellationToken)
            {
                object param = new
                {
                    GroupName = request.GroupName,
                    Creator = "ADMIN"
                };

                var result = await _productGroupRepository.spProductGroupCreate(param);

                return result;
            }
        }
    }
}
