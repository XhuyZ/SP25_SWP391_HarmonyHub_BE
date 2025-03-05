using Domain.DTO.Request;

namespace Service.Interfaces
{
    public interface IVnPayService
    {
        string CreatePayment(CreatePaymentRequest createPaymentRequest);

        int GetPaymentResult();
    }
}
