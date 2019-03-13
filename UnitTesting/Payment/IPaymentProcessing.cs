using System;
namespace Payment
{
    public interface IPaymentProcessing
    {
        bool TakePayment(int paymentId, int customerId, double amount);
        void DoSomething();
    }
}
