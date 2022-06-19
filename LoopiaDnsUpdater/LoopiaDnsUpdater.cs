using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;

namespace LoopiaDnsUpdater
{
    public static class DnsUpdater
    {
        private static readonly HttpClient _client = new();
        private const string _ipLookUpUrl = "https://ipecho.net/plain";

        public static async Task<string> UpdateDns()
        {
            var myIpAddress = await _client.GetStringAsync(_ipLookUpUrl);

            if (!IsValidIp(myIpAddress))
                return "Invalid ip";

            var result = await UpdateDnsAsync(myIpAddress);

            return result;
        }

        private static async Task<string> UpdateDnsAsync(string myIpAddress)
        {
            try
            {
                var password = Environment.GetEnvironmentVariable("password");
                if (string.IsNullOrEmpty(password))
                    return "empty password from environment variables";

                var hostname = Environment.GetEnvironmentVariable("hostname");
                if (string.IsNullOrEmpty(hostname))
                    return "empty hostname from environment variables";

                var username = Environment.GetEnvironmentVariable("username");
                if (string.IsNullOrEmpty(username))
                    return "empty username from environment variables";

                var base64EncodedAuthenticationString = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));

                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);

                var dnsUpdateUrl = $"https://dyndns.loopia.se/?hostname={hostname}&myip={myIpAddress}&username={username}&password={password}";


                var result = await _client.GetStringAsync(dnsUpdateUrl);

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return $"something went wrong: {e.Message}";
            }
        }


        private static bool IsValidIp(string ip)
        {
            //Match pattern for IP address    
            string Pattern = @"^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$";
            //Regular Expression object    
            Regex check = new Regex(Pattern);

            //check to make sure an ip address was provided    
            if (string.IsNullOrEmpty(ip))
                //returns false if IP is not provided    
                return false;
            else
                //Matching the pattern    
                return check.IsMatch(ip, 0);
        }
    }
}
