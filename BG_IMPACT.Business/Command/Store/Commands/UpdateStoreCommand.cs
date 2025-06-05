using BG_IMPACT.Business.Command.News.Commands;
using BG_IMPACT.Repository.Repositories.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BG_IMPACT.Business.Command.Store.Commands
{
    public class UpdateStoreCommand : IRequest<ResponseObject>
    {
        [Required]
        public Guid StoreId { get; set; }
        public string StoreName { get; set; }
        public string Image { get; set; }
        public string Address { get; set; }
        public string Hotline { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Email { get; set; }



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

                        request.StoreId,
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
                            response.Message = "Không tìm thấy bản tin.";
                        }
                        else
                        {
                            response.StatusCode = "200";
                            response.Message = "Cập nhật tin tức thành công.";
                        }
                    
                }
                return response;
            }
                
            }
        }
    }