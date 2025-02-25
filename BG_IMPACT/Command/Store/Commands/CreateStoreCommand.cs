using BG_IMPACT.Models;
using BG_IMPACT.Repositories.Interfaces;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BG_IMPACT.Command.Store.Commands
{
    public class CreateStoreCommand : IRequest<ResponseObject>
    {
        [Required]
        public string StoreName { get; set; } = string.Empty;
        [Required]
        public string Address { get; set; } = string.Empty;
        public string Hotline { get; set; } = string.Empty;
        public string Lattitude { get; set; } = string.Empty;
        public string Longtitude { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public class CreateStoreCommandHandler : IRequestHandler<CreateStoreCommand, ResponseObject>
        {

            public readonly IStoreRepository _storeRepository;

            public CreateStoreCommandHandler(IStoreRepository storeRepository)
            {
                _storeRepository = storeRepository;
            }
            public async Task<ResponseObject> Handle(CreateStoreCommand request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                object param = new
                {
                    request.StoreName,
                    request.Address,
                    request.Hotline,
                    request.Lattitude,
                    request.Longtitude,
                    request.Email,
                };

                var result = await _storeRepository.spStoreCreate(param);
                var dict = result as IDictionary<string, object>;

                if (dict != null && Int64.TryParse(dict["Status"].ToString(), out _) == true)
                {
                    _ = Int64.TryParse(dict["Status"].ToString(), out long count);

                    if (count == 0)
                    {
                        response.StatusCode = "200";
                        response.Message = "Tạo cửa hàng thành công";
                    }
                    else 
                    {
                        response.StatusCode = "404";
                        response.Message = "Tên cửa hàng đã tồn tại";
                    }
                }
                else
                {
                    response.StatusCode = "404";
                    response.Message = "Tạo cửa hàng thất bại. Xin hãy thử lại sau.";
                }

                return response;
            }
        }
    }
}
