namespace BG_IMPACT.Business.Command.Login.Commands
{
    public class RefreshTokenCommand : IRequest<ResponseObject>
    {
        public string RefreshToken { get; set; } = string.Empty;

        public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, ResponseObject>
        {
            private readonly IAccountRepository _accountRepository;
            public RefreshTokenCommandHandler(IAccountRepository accountRepository)
            {
                _accountRepository = accountRepository;
            }
            public async Task<ResponseObject> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                object param = new
                {
                    request.RefreshToken
                };

                //var result = await _accountRepository.spAccountCheckRefreshToken(param);
                //if ()
                //{

                //}

                return response;
            }
        }
    }
}
