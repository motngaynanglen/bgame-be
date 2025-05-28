namespace BG_IMPACT.Business.Command.ProductGroup.Queries
{
    public class GetProductGroupListQuery : IRequest<ResponseObject>
    {
        public class GetProductGroupListQueryHandler : IRequestHandler<GetProductGroupListQuery, ResponseObject>
        {
            public readonly IProductGroupRepository _productGroupRepository;

            public GetProductGroupListQueryHandler(IProductGroupRepository productGroupRepository)
            {
                _productGroupRepository = productGroupRepository;
            }

            public async Task<ResponseObject> Handle(GetProductGroupListQuery request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                var result = await _productGroupRepository.spProductGroupGetList();

                response.StatusCode = "200";
                response.Data = result;

                return response;
            }
        }
    }
}
