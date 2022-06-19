using LoopiaDnsUpdater;
const int oneDayInMilliSeconds = 86400000;

while (true)
{
    var result = DnsUpdater.UpdateDns();

    Console.WriteLine(result);

    Thread.Sleep(oneDayInMilliSeconds);
}