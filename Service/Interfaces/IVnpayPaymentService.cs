using Domain.DTO.Request;

namespace Service.Interfaces
{
    public interface IVnpayPaymentService
    {
        string CreatePayment(CreatePaymentRequest createPaymentRequest);

        string CreatePaymentForUserCredit(CreatePaymentUserRequest createPaymentRequest);

        int GetPaymentResult();
    }
}
