namespace Payment
{
    internal class PaymentProcessor
    {
        internal IPaymentProcessing wsProxy;

        public PaymentProcessor(IPaymentProcessing proxy)
        {
            wsProxy = proxy;
        }

        public bool TakePayment(int paymentId, int customerId, double amount)
        {
            return wsProxy.TakePayment(paymentId, customerId, amount);
        }
    }
}
