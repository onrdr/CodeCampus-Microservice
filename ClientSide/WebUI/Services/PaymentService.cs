using WebUI.Models.FakePayments;
using WebUI.Services.Interfaces;

namespace WebUI.Services;

public class PaymentService : IPaymentService
{
    private readonly HttpClient _httpClient;

    public PaymentService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> ReceivePayment(PaymentInfoInput paymentInfoInput)
    {
        var response = await _httpClient.PostAsJsonAsync("fakepayments", paymentInfoInput);

        return response.IsSuccessStatusCode;
    }
}