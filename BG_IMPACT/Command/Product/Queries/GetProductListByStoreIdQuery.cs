using BG_IMPACT.Models;
using BG_IMPACT.Repositories.Implementations;
using BG_IMPACT.Repositories.Interfaces;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BG_IMPACT.Command.Product.Queries
{
    public class GetProductListByStoreIdQuery : IRequest<ResponseObject>
    {
        public Guid StoreId { get; set; }
        [Required]
        public bool IsRent { get; set; }
        public Paging Paging { get; set; } = new Paging();

        public class GetProductListByStoreIdQueryHandler : IRequestHandler<GetProductListByStoreIdQuery, ResponseObject>
        {
            private readonly IProductRepository _productRepository;

            public GetProductListByStoreIdQueryHandler(IProductRepository productRepository)
            {
                _productRepository = productRepository;
            }
            public async Task<ResponseObject> Handle(GetProductListByStoreIdQuery request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                object param = new
                {
                    request.StoreId,
                    request.IsRent,
                    request.Paging.PageNum,
                    request.Paging.PageSize
                };

                var result = await _productRepository.spProductGetListByStoreId(param);
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
