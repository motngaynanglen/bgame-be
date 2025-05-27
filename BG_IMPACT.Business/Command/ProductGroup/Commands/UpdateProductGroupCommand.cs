using System.ComponentModel.DataAnnotations;

namespace BG_IMPACT.Business.Command.ProductGroup.Commands
{
    public class UpdateProductGroupCommand : IRequest<ResponseObject>
    {
        [Required]
        public Guid ProductGroupID { get; set; }

        [Required]
        public string GroupName { get; set; } = string.Empty;

    }

    public class UpdateProductHandler : IRequestHandler<UpdateProductGroupCommand, ResponseObject>
    {
        private readonly IProductGroupRepository _productGroupRepository;

        public UpdateProductHandler(IProductGroupRepository productGroupRepository)
        {
            _productGroupRepository = productGroupRepository;
        }

        public async Task<ResponseObject> Handle(UpdateProductGroupCommand request, CancellationToken cancellationToken)
        {
            ResponseObject response = new();

            object param = new
            {
                request.ProductGroupID,
                request.GroupName,

            };

            var result = await _productGroupRepository.spProductGroupUpdate(param);
            var dict = result as IDictionary<string, object>;
            if (dict != null && Int64.TryParse(dict["Status"].ToString(), out _) == true)
            {
                _ = Int64.TryParse(dict["Status"].ToString(), out long statusCode);

                if (statusCode == 2)
                {
                    response.StatusCode = "404";
                    response.Message = "Không tìm thấy sản phẩm.";
                }
                else
                {
                    response.StatusCode = "200";
                    response.Message = "Cập nhật sản phẩm thành công.";
                }
            }
            else
            {
                response.StatusCode = "404";
                response.Message = "Cập nhật sản phẩm thất bại. Xin hãy thử lại sau.";
            }
            return response;
        }
    }
}
