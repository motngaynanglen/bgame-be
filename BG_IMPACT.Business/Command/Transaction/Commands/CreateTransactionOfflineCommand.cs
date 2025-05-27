namespace BG_IMPACT.Business.Command.Transaction.Commands
{
    public class CreateTransactionOfflineCommand : IRequest<ResponseObject>
    {
        public Guid BookListID { get; set; }

        public class CreateTransactionOfflineCommandHandler : IRequestHandler<CreateTransactionOfflineCommand, ResponseObject>
        {
            private readonly ITransactionRepository _transactionRepository;

            public CreateTransactionOfflineCommandHandler(ITransactionRepository transactionRepository)
            {
                _transactionRepository = transactionRepository;
            }
            public async Task<ResponseObject> Handle(CreateTransactionOfflineCommand request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                object param = new
                {
                    request.BookListID
                };

                var result = await _transactionRepository.spTransactionCreateOffline(param);
                var dict = result as IDictionary<string, object>;

                if (dict != null && Int64.TryParse(dict["Status"].ToString(), out _) == true)
                {
                    _ = Int64.TryParse(dict["Status"].ToString(), out long count);

                    if (count == 1)
                    {
                        response.StatusCode = "404";
                        response.Message = "Đơn thuê không tồn tại.";
                    }
                    else if (count == 2)
                    {
                        response.StatusCode = "404";
                        response.Message = "Đơn thuê này đã thanh toán.";
                    }
                    else
                    {
                        response.StatusCode = "200";
                        response.Message = "Thanh toán thành công.";
                    }
                }
                else
                {
                    response.StatusCode = "404";
                    response.Message = "Thanh toán thất bại. Xin hãy thử lại sau.";
                }

                return response;
            }
        }
    }
}
