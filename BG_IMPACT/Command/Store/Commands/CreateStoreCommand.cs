using BG_IMPACT.Repositories.Interfaces;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BG_IMPACT.Command.Store.Commands
{
    public class CreateStoreCommand : IRequest<object>
    {
        [Required]
        public string StoreName { get; set; } = string.Empty;
        [Required]
        public string Address { get; set; } = string.Empty;
        public string Hotline { get; set; } = string.Empty;
        public string Lattitude { get; set; } = string.Empty;
        public string Longtitude { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public class CreateStoreCommandHandler : IRequestHandler<CreateStoreCommand, object>
        {

            public readonly IStoreRepository _storeRepository;

            public CreateStoreCommandHandler(IStoreRepository storeRepository)
            {
                _storeRepository = storeRepository;
            }
            public async Task<object> Handle(CreateStoreCommand request, CancellationToken cancellationToken)
            {
                object param = new
                {
                    store_name = request.StoreName,
                    adress = request.Address,
                    hotline = request.Hotline,
                    lattitude = request.Lattitude,
                    longtitude = request.Longtitude,
                    email = request.Email,
                };

                object result = await _storeRepository.spStoreCreate(param);
                return result;
            }
        }
    }
}
