namespace BG_IMPACT.Business.Command.BookList.Commands
{
    public class CheckBookListTransactionCommand : IRequest<ResponseObject>
    {
        public required WebhookType webhookBody { get; set; }

        public class CheckBookListTransactionCommandHandler : IRequestHandler<CheckBookListTransactionCommand, ResponseObject>
        {
            public async Task<ResponseObject> Handle(CheckBookListTransactionCommand request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                var clientId = "1090cdba-a0a1-4b0d-97ab-3062efa9d28e";
                var apiKey = "b4a1062e-fb09-4534-bc0f-3962810ef99a";
                var checksumKey = "4bd745d368dfdfccbb99a60a6acea20e58d4aa8fd5f83b3db173aa58a48475b8";

                var payOS = new PayOS(clientId, apiKey, checksumKey);
                var domain = "https://bg-impact.io.vn";

                WebhookData webhookData = payOS.verifyPaymentWebhookData(request.webhookBody);


                return response;
            }
        }
    }
}
