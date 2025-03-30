using BG_IMPACT.Repositories.Interfaces;
using MediatR;
using static BG_IMPACT.Models.StatusBase;

namespace BG_IMPACT.Command.Account.Commands
{
    public class UpdateCustomerProfileCommand : IRequest<object>
    {
        public Guid AccountId { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public string Image { get; set; }
        public Gender Gender { get; set; }
    }
    public class UpdateCustomerProfileCommandHandler : IRequestHandler<UpdateCustomerProfileCommand, object>
    {
        private readonly ICustomerRepository _customerRepository;

        public UpdateCustomerProfileCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<object> Handle(UpdateCustomerProfileCommand request, CancellationToken cancellationToken)
        {
            var genderStr = request.Gender.ToString().ToUpper(); 
            var parameters = new
            {
                AccountId = request.AccountId,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                FullName = request.FullName,
                DateOfBirth = request.DateOfBirth,
                Image = request.Image,
                Gender = genderStr
            };

            var result = await _customerRepository.spCustomerUpdateProfile(parameters);
            return result;
        }
    }

}