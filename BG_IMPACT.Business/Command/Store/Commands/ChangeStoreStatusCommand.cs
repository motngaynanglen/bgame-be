using System.ComponentModel.DataAnnotations;

namespace BG_IMPACT.Business.Command.Store.Commands
{
    public class ChangeStoreStatusCommand : IRequest<object>
    {
        [Required]
        public Guid Id { get; set; }

        public class ChangeStoreStatusCommandHandler : IRequestHandler<ChangeStoreStatusCommand, object>
        {
            private readonly IStoreRepository _storeRepository;

            public ChangeStoreStatusCommandHandler(IStoreRepository storeRepository)
            {
                _storeRepository = storeRepository;
            }
            public async Task<object> Handle(ChangeStoreStatusCommand request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                object param = new
                {
                    StoreId = request.Id
                };

                var result = await _storeRepository.spStoreChangeStatus(param);
                var dict = result as IDictionary<string, object>;

                if (dict != null && Int64.TryParse(dict["Status"].ToString(), out _) == true)
                {
                    _ = Int64.TryParse(dict["Status"].ToString(), out long count);

                    if (count == 1)
                    {
                        response.StatusCode = "404";
                        response.Message = "Cửa hàng không tồn tại.";
                    }
                    else
                    {
                        response.StatusCode = "200";
                        response.Message = "Cập nhật trạng thái cửa hàng thành công.";
                    }
                }
                else
                {
                    response.StatusCode = "404";
                    response.Message = "Cập nhật trạng thái cửa hàng thất bại. Xin hãy thử lại sau.";
                }

                return response;
            }
        }
    }
}
