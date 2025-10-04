using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT.Business.Command.Supplier.Queries
{
    public class GetSupplierByIdQuery : IRequest<ResponseObject>
    {
        public Guid SupplierId { get; set; }

        public class GetSupplierByIdQueryHandler : IRequestHandler<GetSupplierByIdQuery, ResponseObject>
        {
            private readonly ISupplierRepository _supplierRepository;
            private readonly IHttpContextAccessor _contextAccessor;

            public GetSupplierByIdQueryHandler(ISupplierRepository supplierRepository, IHttpContextAccessor contextAccessor)
            {
                _supplierRepository = supplierRepository;
                _contextAccessor = contextAccessor;
            }
            public async Task<ResponseObject> Handle(GetSupplierByIdQuery request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                object param = new
                {
                    request.SupplierId
                };

                var result = await _supplierRepository.spSupplierGetById(param);
                var dict = result as IDictionary<string, object>;

                if (dict != null && dict.Count > 0)
                {
                    response.StatusCode = "200";
                    response.Data = dict;
                    response.Message = string.Empty;
                }
                else
                {
                    response.StatusCode = "404";
                    response.Message = "Không tìm thấy vật phẩm.";
                }

                return response;
            }
        }
    }
}

