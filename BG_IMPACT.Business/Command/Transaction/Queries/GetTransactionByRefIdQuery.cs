using BG_IMPACT.Business.Command.Transaction.Commands;
using BG_IMPACT.Business.Config;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT.Business.Command.Transaction.Queries
{
    public class GetTransactionByRefIdQuery : IRequest<ResponseObject>
    {
        [Required]
        public Guid ReferenceID { get; set; }
        public class GetTransactionByRefIdQueryHandler : IRequestHandler<GetTransactionByRefIdQuery, ResponseObject>
        {
            public readonly ITransactionRepository _transactionRepository;

            public GetTransactionByRefIdQueryHandler(ITransactionRepository transactionRepository, IOptions<PayOsSettings> payOsSettings, PayOsCodeGenerator codeGenerator)
            {
                _transactionRepository = transactionRepository;
              
            }

            public async Task<ResponseObject> Handle(GetTransactionByRefIdQuery request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();
                object param = new
                {
                    request.ReferenceID,
                };
                var result = await _transactionRepository.spTransactionGetByRefId(param);
                var rawData = result as IDictionary<string, object>;
                if (rawData == null)
                {
                    response.StatusCode = "404";
                    response.Message = "Đơn hàng chưa có kênh thanh toán";
                    return response;
                }
                response.StatusCode = "200";
                response.Message = "Truy vấn thành công";
                response.Data = rawData;
                return response;
            }
        }
    }
}
