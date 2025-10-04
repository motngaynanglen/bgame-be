namespace BG_IMPACT.Business.Command.Supplier.Queries
{
    public class GetSupplierListQuery : IRequest<ResponseObject>
    {
        public Paging Paging { get; set; } = new Paging();
        public class GetSupplierListQueryHandler : IRequestHandler<GetSupplierListQuery, ResponseObject>
        {
            private readonly ISupplierRepository _supplierRepository;

            public GetSupplierListQueryHandler(ISupplierRepository supplierRepository)
            {
                _supplierRepository = supplierRepository;
            }
            public async Task<ResponseObject> Handle(GetSupplierListQuery request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                object param = new
                {
                    request.Paging.PageNum,
                    request.Paging.PageSize
                };


                var result = await _supplierRepository.spSupplierGetList(param);
                var list = ((IEnumerable<dynamic>)result.suppliers).ToList();
                long count = result.totalCount;

                if (list.Count > 0)
                {
                    long pageCount = count / request.Paging.PageSize;

                    response.StatusCode = "200";
                    response.Data = list;
                    response.Message = string.Empty;
                    response.Paging = new PagingModel
                    {
                        PageNum = request.Paging.PageNum,
                        PageSize = request.Paging.PageSize,
                        PageCount = count % request.Paging.PageSize == 0 ? pageCount : pageCount + 1
                    };
                }
                else
                {
                    response.StatusCode = "404";
                    response.Message = "Không tìm thấy nhà cung cấp nào.";
                }
                return response;
            }
        }
    }
}
