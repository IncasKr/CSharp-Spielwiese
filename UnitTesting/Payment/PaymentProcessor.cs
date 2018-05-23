namespace Payment
{
    public class PaymentProcessor
    {
        internal IPaymentProcessing wsProxy;
        
        public PaymentProcessor(IPaymentProcessing proxy)
        {
            wsProxy = proxy;            
        }

        public bool TakePayment(int paymentId, int customerId, double amount)
        {
            var res = wsProxy.TakePayment(paymentId, customerId, amount);
            wsProxy.DoSomething();
            return res;
        }       
    }
}
