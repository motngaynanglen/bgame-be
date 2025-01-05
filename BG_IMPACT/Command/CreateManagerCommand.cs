using BG_IMPACT.Repositories.Interfaces;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BG_IMPACT.Command
{
    public class CreateManagerCommand : IRequest<object>
    {
        [Required]
        public Guid StoreId { get; set; }
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTimeOffset DateOfBirth { get; set; }
        public class CreateManagerCommandHandler : IRequestHandler<CreateManagerCommand, object>
        {
            public readonly IAccountRepository _accountRepository;

            public CreateManagerCommandHandler(IAccountRepository accountRepository)
            {
                _accountRepository = accountRepository;
            }
            public async Task<object> Handle(CreateManagerCommand request, CancellationToken cancellationToken)
            {
                object param = new
                {
                    store_id = request.StoreId,
                    username = request.Username,
                    password = request.Password,
                    phone_number = request.PhoneNumber ?? string.Empty,
                    email = request.Email ?? string.Empty,
                    role = "MANAGER",
                    full_name = request.FullName,
                    date_of_birth = request.DateOfBirth,
                };

                object result = await _accountRepository.spAccountCreateManager(param);
                return Task.FromResult(result);
            }
        }
    }
}
