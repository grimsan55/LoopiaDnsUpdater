namespace LoopiaDnsUpdater 
{
    internal class Program
    {
        const int oneDayInMilliSeconds = 86400000;

        private static async Task Main(string[] args)
        {
            while (true)
            {
                var result = await DnsUpdater.UpdateDns();

                Console.WriteLine(result);

                Thread.Sleep(oneDayInMilliSeconds);
            }
        }
    }
}

