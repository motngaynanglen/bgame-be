using System.ComponentModel.DataAnnotations;

namespace BG_IMPACT.Business.Command.Product.Queries
{
    public class GetProductListByGroupRefIdQuery : IRequest<ResponseObject>
    {
        [Required]
        public Guid ProductGroupRefId { get; set; }
        
        //public bool IsRent { get; set; }
        //public Paging Paging { get; set; } = new Paging();

        public class GetProductListByGroupRefIdQueryHandler : IRequestHandler<GetProductListByGroupRefIdQuery, ResponseObject>
        {
            private readonly IProductRepository _productRepository;

            public GetProductListByGroupRefIdQueryHandler(IProductRepository productRepository)
            {
                _productRepository = productRepository;
            }
            public async Task<ResponseObject> Handle(GetProductListByGroupRefIdQuery request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                object param = new
                {
                   request.ProductGroupRefId
                };


                var result = await _productRepository.spProductGetListByGroupRefId(param);
                if (result == null)
                {
                    response.StatusCode = "404";
                    response.Message = "Không tìm thấy sản phẩm mẫu nào.";
                    return response;
                }
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
                    response.Message = "Không tìm thấy sản phẩm nào.";
                }


                return response;
            }
        }
    }
}
