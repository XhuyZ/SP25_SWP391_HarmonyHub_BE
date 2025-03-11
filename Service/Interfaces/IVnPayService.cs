using Domain.DTO.Request;

namespace Service.Interfaces
{
    public interface IVnPayService
    {
        Task<string> CreatePayment(CreatePaymentRequest createPaymentRequest);

        Task<int> GetPaymentResult();
    }
}
