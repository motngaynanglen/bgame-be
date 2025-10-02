using BG_IMPACT.Business.Command.News.Commands;
using BG_IMPACT.Repository.Repositories.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BG_IMPACT.Business.Command.Store.Commands
{
    public class UpdateStoreCommand : IRequest<ResponseObject>
    {
        [Required]
        public Guid Id { get; set; }
        public string StoreName { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Hotline { get; set; } = string.Empty;
        public string Latitude { get; set; } = string.Empty;
        public string Longitude { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public class UpdateStoreCommandHandler : IRequestHandler<UpdateStoreCommand, ResponseObject>
        {
            private readonly IStoreRepository _storeRepository;

            public UpdateStoreCommandHandler(IStoreRepository storeRepository)
            {
                _storeRepository = storeRepository;
            }

            public async Task<ResponseObject> Handle(UpdateStoreCommand request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();
                    object param = new
                    {

                        StoreId = request.Id,
                        request.StoreName,
                        request.Image,
                        request.Address,
                        request.Hotline,
                        request.Latitude,
                        request.Longitude,
                        request.Email,
                    };

                    var result = await _storeRepository.spStoreUpdate(param);
                    var dict = result as IDictionary<string, object>;

                    if (dict != null && Int64.TryParse(dict["Status"].ToString(), out _) == true)
                    {
                        _ = Int64.TryParse(dict["Status"].ToString(), out long statusCode);

                        if (statusCode == 1)
                        {
                            response.StatusCode = "404";
                            response.Message = "Không tìm thấy cửa hàng.";
                        }
                        else
                        {
                            response.StatusCode = "200";
                            response.Message = "Cập nhật cửa hàng thành công.";
                        }
                    
                }
                return response;
            }
                
            }
        }
    }