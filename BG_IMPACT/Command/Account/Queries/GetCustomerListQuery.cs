using BG_IMPACT.Repositories.Interfaces;
using MediatR;

namespace BG_IMPACT.Command.Account.Queries
{
    public class GetCustomerListQuery : IRequest<object>
    {
        public class GetCustomerListQueryHandler : IRequestHandler<GetCustomerListQuery, object>
        {
            private readonly ICustomerRepository _customerRepository;

            public GetCustomerListQueryHandler(ICustomerRepository customerRepository)
            {
                _customerRepository = customerRepository;
            }

            public async Task<object> Handle(GetCustomerListQuery request, CancellationToken cancellationToken)
            {
                var result = await _customerRepository.spCustomerGetList();
                return result;
            }
        }

    }
}
