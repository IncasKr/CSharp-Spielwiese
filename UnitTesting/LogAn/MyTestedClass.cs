namespace LogAn
{
    public static class MyTestedClass
    {
        private static ILogger _logger = null;

        public static void SetLogger(ILogger logger)
        {
            _logger = logger;            
        }

        public static void DoSomething()
        {
            _logger.LogError("param value 1 is a string", 2, "param value 3 is a string");
        }
    }
}
