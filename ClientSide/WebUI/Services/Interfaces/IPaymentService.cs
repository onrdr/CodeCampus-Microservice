using WebUI.Models.FakePayments;

namespace WebUI.Services.Interfaces;

public interface IPaymentService
{
    Task<bool> ReceivePayment(PaymentInfoInput paymentInfoInput);
}