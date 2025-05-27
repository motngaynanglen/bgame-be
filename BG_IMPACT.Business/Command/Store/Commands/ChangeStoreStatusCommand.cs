using System.ComponentModel.DataAnnotations;

namespace BG_IMPACT.Business.Command.Store.Commands
{
    public class ChangeStoreStatusCommand : IRequest<object>
    {
        [Required]
        public Guid StoreId { get; set; }
        [Required]
        public string Status { get; set; } = string.Empty;
        public class ChangeStoreStatusCommandHandler : IRequestHandler<ChangeStoreStatusCommand, object>
        {
            private readonly IStoreRepository _storeRepository;

            public ChangeStoreStatusCommandHandler(IStoreRepository storeRepository)
            {
                _storeRepository = storeRepository;
            }
            public Task<object> Handle(ChangeStoreStatusCommand request, CancellationToken cancellationToken)
            {
                object param = new
                {
                    store_id = request.StoreId,
                    status = request.Status,
                };

                return (Task<object>)param;
            }
        }
    }
}
