using System.ComponentModel.DataAnnotations;

namespace BG_IMPACT.Business.Command.ProductGroupRef.Commands
{
    public class UpdateProductGroupRefCommand : IRequest<ResponseObject>
    {
        [Required]
        public Guid GroupRefID { get; set; }

        [Required]
        public string GroupRefName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

    }

    public class UpdateProductHandler : IRequestHandler<UpdateProductGroupRefCommand, ResponseObject>
    {
        private readonly IProductGroupRefRepository _productGroupRefRepository;

        public UpdateProductHandler(IProductGroupRefRepository productGroupRefRepository)
        {
            _productGroupRefRepository = productGroupRefRepository;
        }

        public async Task<ResponseObject> Handle(UpdateProductGroupRefCommand request, CancellationToken cancellationToken)
        {
            ResponseObject response = new();

            object param = new
            {
                request.GroupRefID,
                request.GroupRefName,
                request.Description,
            };

            var result = await _productGroupRefRepository.spProductGroupRefUpdate(param);
            var dict = result as IDictionary<string, object>;
            if (dict != null && Int64.TryParse(dict["Status"].ToString(), out _) == true)
            {
                _ = Int64.TryParse(dict["Status"].ToString(), out long statusCode);

                if (statusCode == 3)
                {
                    response.StatusCode = "404";
                    response.Message = "Không tìm thấy sản phẩm.";
                }
                else if (statusCode == 2)
                {
                    response.StatusCode = "404";
                    response.Message = "Không tìm thấy nhóm sản phẩm.";
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