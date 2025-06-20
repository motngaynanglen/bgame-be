﻿using BG_IMPACT.Repository.Repositories.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT.Business.Command.Email.Commands
{
    public class SendSupplyOrderEmailCommand : IRequest<ResponseObject>
    {
        public Guid SupplyOrderId { get; set; }

        public class SendSupplyOrderEmailCommandHandler : IRequestHandler<SendSupplyOrderEmailCommand, ResponseObject>
        {
            private readonly ISupplyOrderRepository _supplyOrderRepo;
            private readonly IEmailServiceRepository _emailRepo;

            public SendSupplyOrderEmailCommandHandler(ISupplyOrderRepository supplyOrderRepo, IEmailServiceRepository emailRepo)
            {
                _supplyOrderRepo = supplyOrderRepo;
                _emailRepo = emailRepo;
            }

            public async Task<ResponseObject> Handle(SendSupplyOrderEmailCommand request, CancellationToken cancellationToken)
            {
                var paramEmail = new { request.SupplyOrderId };
                var resultEmail = await _supplyOrderRepo.spEmailGetSupplierEmailByOrderId(paramEmail);
                var emailData = resultEmail as IDictionary<string, object>;


                if (emailData != null && emailData["Email"].ToString() != null)
                {
                    var emailTo = emailData["Email"].ToString();

                    var paramItems = new { request.SupplyOrderId };
                    var resultItems = await _supplyOrderRepo.spGetSupplyItemsByOrderId(paramItems);
                    var itemList = ((IEnumerable<dynamic>)resultItems).ToList();

                    if (itemList.Count == 0)
                    {
                        return new ResponseObject
                        {
                            StatusCode = "404",
                            Message = "Không tìm thấy sản phẩm trong đơn hàng."
                        };
                    }


                    string subject = "[BG Impact] Thông tin đơn nhập hàng mới";

                    var htmlBuilder = new StringBuilder();
                    htmlBuilder.Append("<h2>Danh sách sản phẩm trong đơn hàng</h2>");
                    htmlBuilder.Append("<table border='1' cellpadding='6' cellspacing='0' style='border-collapse: collapse;'>");
                    htmlBuilder.Append("<tr><th>STT</th><th>Tên</th><th>Số lượng</th></tr>");

                    foreach (var item in itemList)
                    {
                        htmlBuilder.AppendFormat(
                            "<tr><td>{0}</td><td>{1}</td><td>{2}</td></tr>",
                            item.Index,
                            item.name,
                            item.quantity
                        );
                    }

                    htmlBuilder.Append("</table>");


                    await _emailRepo.SendEmailAsync(emailTo, subject, htmlBuilder.ToString());

                    return new ResponseObject
                    {
                        StatusCode = "200",
                        Message = "Đã gửi email thông tin đơn hàng thành công."
                    };
                }
                else
                {
                    return new ResponseObject
                    {
                        StatusCode = "400",
                        Message = "Không thể gửi email vì không tìm thấy email của nhà cung cấp."
                    };
                }
            }
        }
    }
}