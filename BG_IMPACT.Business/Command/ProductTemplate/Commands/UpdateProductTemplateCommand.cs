using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT.Business.Command.ProductTemplate.Commands
{
    public class UpdateProductTemplateCommand : IRequest<ResponseObject>
    {
        [Required]
        public Guid ProductTemplateId { get; set; }
        [Required]
        public string ProductName { get; set; } = string.Empty;
        [Required]
        public List<string> Images { get; set; } = [];
        [Required]
        public double Price { get; set; }
        [Required]
        public double RentPrice { get; set; }
        [Required]
        public double RentPricePerHour { get; set; }
        [Required]
        public int Difficulty { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public int NumberOfPlayerMin { get; set; }
        [Required]
        public int NumberOfPlayerMax { get; set; }
        [Required]
        public string Description { get; set; } = string.Empty;
        public int? Duration { get; set; } = null;
        public List<string> ListCategories { get; set; } = new();
        public string Publisher { get; set; } = string.Empty;
        public class UpdateProductTemplateCommandHandler : IRequestHandler<UpdateProductTemplateCommand, ResponseObject>
        {
            private readonly IProductTemplateRepository _productTemplateRepository;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public UpdateProductTemplateCommandHandler(IProductTemplateRepository productTemplateRepository, IHttpContextAccessor httpContextAccessor)
            {
                _productTemplateRepository = productTemplateRepository;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<ResponseObject> Handle(UpdateProductTemplateCommand request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                var context = _httpContextAccessor.HttpContext;

                string? ManagerID = null;

                string Image = String.Join("||", request.Images);

                string categories = String.Join("||", request.ListCategories);

                if (context != null && context.GetRole() == "MANAGER")
                {
                    ManagerID = context.GetName();

                    object param = new
                    {
                        request.ProductTemplateId,
                        request.ProductName,
                        Image,
                        request.Price,
                        request.RentPrice,
                        request.RentPricePerHour,
                        request.Difficulty,
                        request.Age,
                        request.NumberOfPlayerMin,
                        request.NumberOfPlayerMax,
                        request.Description,
                        request.Duration,
                        ListCategories = categories,
                        request.Publisher,
                        ManagerID,
                    };

                    var result = await _productTemplateRepository.spProductTemplateUpdate(param);
                    var dict = result as IDictionary<string, object>;

                    if (dict != null && Int64.TryParse(dict["Status"].ToString(), out _) == true)
                    {
                        _ = Int64.TryParse(dict["Status"].ToString(), out long count);

                        if (count == 1)
                        {
                            response.StatusCode = "404";
                            response.Message = "Không tìm thấy nhóm sản phẩm.";
                        }
                        else
                        {
                            response.StatusCode = "200";
                            response.Message = "Thêm sản phẩm thành công.";
                            string productID = dict["ProductID"].ToString() ?? string.Empty;
                            response.Data = productID;
                        }

                    }
                    else
                    {
                        response.StatusCode = "404";
                        response.Message = "Thêm sản phẩm thất bại. Xin hãy thử lại sau.";
                    }
                }
                else
                {
                    response.StatusCode = "403";
                    response.Message = "Bạn không có quyền sử dụng chức năng này.";
                }

                return response;
            }
        }
    }
}
