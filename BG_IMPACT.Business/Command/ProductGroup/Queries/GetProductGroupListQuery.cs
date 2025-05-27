namespace BG_IMPACT.Business.Command.ProductGroup.Queries
{
    public class GetProductGroupListQuery : IRequest<object>
    {
        public class GetProductGroupListQueryHandler : IRequestHandler<GetProductGroupListQuery, object>
        {
            public readonly IProductGroupRepository _productGroupRepository;

            public GetProductGroupListQueryHandler(IProductGroupRepository productGroupRepository)
            {
                _productGroupRepository = productGroupRepository;
            }

            public async Task<object> Handle(GetProductGroupListQuery request, CancellationToken cancellationToken)
            {
                var result = await _productGroupRepository.spProductGroupGetList();

                return result;
            }
        }
    }
}
