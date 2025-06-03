namespace BG_IMPACT.Business.Command.Supplier.Queries
{
    public class GetSupplierListQuery : IRequest<ResponseObject>
    {
       
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


                var result = await _supplierRepository.spSupplierGetList();
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
                    response.Message = "Không tìm thấy supplier nào.";
                }
                return response;
            }
        }
    }
}
