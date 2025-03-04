﻿using BG_IMPACT.Models;
using BG_IMPACT.Repositories.Implementations;
using BG_IMPACT.Repositories.Interfaces;
using MediatR;

namespace BG_IMPACT.Command.Product.Queries
{
    public class GetProductListQuery : IRequest<ResponseObject>
    {
        public string Search {  get; set; } = string.Empty;
        public List<string> Filter { get; set; } = [];

        public class GetProductListQueryHandler : IRequestHandler<GetProductListQuery, ResponseObject>
        {
            private readonly IProductRepository _productRepository;

            public GetProductListQueryHandler(IProductRepository productRepository)
            {
                _productRepository = productRepository;
            }
            public async Task<ResponseObject> Handle(GetProductListQuery request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                object param = new
                {
                    request.Search
                };

                var result = await _productRepository.spProductGetList(param);
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
