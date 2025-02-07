using BG_IMPACT.Repositories.Implementations;
using BG_IMPACT.Repositories.Interfaces;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BG_IMPACT.Command.Login.Commands
{
    public class CreateCustomerCommand : IRequest<object>
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTimeOffset DateOfBirth { get; set; }
        public class CreateCustomerComandHandler : IRequestHandler<CreateCustomerCommand, object>
        {
            public readonly IAccountRepository _accountRepository;

            public CreateCustomerComandHandler(IAccountRepository accountRepository)
            {
                _accountRepository = accountRepository;
            }
            public async Task<object> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
            {
                object param = new
                {
                    username = request.Username,
                    password = request.Password,
                    phone_number = request.PhoneNumber ?? string.Empty,
                    email = request.Email ?? string.Empty,
                    role = "CUSTOMER",
                    full_name = request.FullName,
                    date_of_birth = request.DateOfBirth,
                };

                object result = await _accountRepository.spAccountCreateCustomer(param);
                return Task.FromResult(result);
            }
        }
    }
}
