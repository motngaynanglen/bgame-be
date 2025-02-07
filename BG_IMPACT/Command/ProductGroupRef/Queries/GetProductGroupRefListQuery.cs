using BG_IMPACT.Repositories.Interfaces;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BG_IMPACT.Command.ProductGroupRef.Queries
{
    public class GetProductGroupRefListQuery : IRequest<object>
    {
        [Required]
        public Guid GroupId { get; set; }
        public class GetProductGroupRefListQueryHandler : IRequestHandler<GetProductGroupRefListQuery, object>
        {
            public readonly IProductGroupRefRepository _productGroupRefRepository;

            public GetProductGroupRefListQueryHandler(IProductGroupRefRepository productGroupRefRepository)
            {
                _productGroupRefRepository = productGroupRefRepository;
            }
            public async Task<object> Handle(GetProductGroupRefListQuery request, CancellationToken cancellationToken)
            {
                object param = new
                {
                    GroupId = request.GroupId
                };

                var result = await _productGroupRefRepository.spProductGroupRefGetList(param);

                return result;
            }
        }
    }
}
