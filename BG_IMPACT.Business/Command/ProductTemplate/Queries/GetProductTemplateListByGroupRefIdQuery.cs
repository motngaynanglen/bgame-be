using System.ComponentModel.DataAnnotations;

namespace BG_IMPACT.Business.Command.ProductTemplate.Queries
{
    public class GetProductTemplateListByGroupRefIdQuery : IRequest<ResponseObject>
    {
        [Required]
        public Guid ProductGroupRefId { get; set; }
        
        //public bool IsRent { get; set; }
        //public Paging Paging { get; set; } = new Paging();

        public class GetProductTemplateListByGroupRefIdQueryHandler : IRequestHandler<GetProductTemplateListByGroupRefIdQuery, ResponseObject>
        {
            private readonly IProductTemplateRepository _productTemplateRepository;

            public GetProductTemplateListByGroupRefIdQueryHandler(IProductTemplateRepository productTemplateRepository)
            {
                _productTemplateRepository = productTemplateRepository;
            }
            public async Task<ResponseObject> Handle(GetProductTemplateListByGroupRefIdQuery request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                object param = new
                {
                   request.ProductGroupRefId
                };


                var result = await _productTemplateRepository.spProductTemplateGetListByGroupRefID(param);
                if (result == null)
                {
                    response.StatusCode = "404";
                    response.Message = "Hệ thống đang bảo trì.";
                    return response;
                }
                var listRaw = ((IEnumerable<dynamic>)result).ToList();

                if (!listRaw.Any())
                {
                    response.StatusCode = "200";
                    response.Data = listRaw;
                    response.Message = string.Empty;
                }

                return response;
            }
        }
    }
}
