namespace BG_IMPACT.Business.Command.Store.Queries
{
    public class GetStoreListQuery : IRequest<ResponseObject>
    {
        public string Search { get; set; } = string.Empty;
        public List<string> Filter { get; set; } = [];

        public class GetStoreListQueryHandler : IRequestHandler<GetStoreListQuery, ResponseObject>
        {
            private readonly IStoreRepository _storeRepository;

            public GetStoreListQueryHandler(IStoreRepository storeRepository)
            {
                _storeRepository = storeRepository;
            }
            public async Task<ResponseObject> Handle(GetStoreListQuery request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                object param = new
                {
                    request.Search
                };

                var result = await _storeRepository.spStoreGetList(param);
                var list = ((IEnumerable<dynamic>)result).ToList();

                if (list.Count > 0)
                {
                    response.StatusCode = "200";
                    response.Data = list;
                    response.Message = string.Empty;
                }
                else
                {
                    response.StatusCode = "404";
                    response.Message = "Không tìm thấy cửa hàng nào.";
                }

                return response;
            }
        }
    }
}
