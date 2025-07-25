using System.ComponentModel.DataAnnotations;

namespace BG_IMPACT.Business.Command.ProductTemplate.Queries
{
    public class GetProductTemplateListByStoreIdQuery : IRequest<ResponseObject>
    {
        [Required]
        public Guid StoreId { get; set; }
        
        //public bool IsRent { get; set; }
        //public Paging Paging { get; set; } = new Paging();

        public class GetProductTemplateListByStoreIdQueryHandler : IRequestHandler<GetProductTemplateListByStoreIdQuery, ResponseObject>
        {
            private readonly IProductTemplateRepository _productTemplateRepository;

            public GetProductTemplateListByStoreIdQueryHandler(IProductTemplateRepository productTemplateRepository)
            {
                _productTemplateRepository = productTemplateRepository;
            }
            public async Task<ResponseObject> Handle(GetProductTemplateListByStoreIdQuery request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                object param = new
                {
                    request.StoreId,
                    //request.IsRent,
                    //request.Paging.PageNum,
                    //request.Paging.PageSize
                };


                var result = await _productTemplateRepository.spProductTemplateGetListByStoreID(param);
                if (result == null)
                {
                    return new ResponseObject
                    {
                        StatusCode = "404",
                        Message = "Hệ thống đang bảo trì",
                        Data = null
                    };
                }
                var listRaw = ((IEnumerable<dynamic>) result).ToList();
                if (!listRaw.Any())
                {
                    return new ResponseObject
                    {
                        StatusCode = "200",
                        Message = "Danh sách trống",
                        Data = null
                    };
                }
                return response;
            }
        }
    }
}
