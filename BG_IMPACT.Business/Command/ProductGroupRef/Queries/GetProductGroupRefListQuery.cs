using System.ComponentModel.DataAnnotations;

namespace BG_IMPACT.Business.Command.ProductGroupRef.Queries
{
    public class GetProductGroupRefListQuery : IRequest<ResponseObject>
    {
        [Required]
        public Guid GroupId { get; set; }
        public class GetProductGroupRefListQueryHandler : IRequestHandler<GetProductGroupRefListQuery, ResponseObject>
        {
            public readonly IProductGroupRefRepository _productGroupRefRepository;

            public GetProductGroupRefListQueryHandler(IProductGroupRefRepository productGroupRefRepository)
            {
                _productGroupRefRepository = productGroupRefRepository;
            }
            public async Task<ResponseObject> Handle(GetProductGroupRefListQuery request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                object param = new
                {
                    GroupId = request.GroupId
                };

                var result = await _productGroupRefRepository.spProductGroupRefGetList(param);

                response.StatusCode = "200";
                response.Data = result;

                return response;
            }
        }
    }
}
