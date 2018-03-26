using _CS5573_BruteForce.PasswordReader;
using System;

namespace _CS5573_BruteForce.Driver
{
    public class CommandDriver
    {
        private Pinging _ping;
        private FileReader _fileReader;

        public CommandDriver()
        {
            _ping = new Pinging();
            _fileReader = new FileReader();
        }

        public void Process(string[] args = null)
        {
            var pingable = false;
            var host = "";
            var username = "";
            var password = "";
            var port = 23;

            try
            {
                if (args != null && args.Length >= 3)
                {
                    host = args[0];
                    pingable = _ping.Ping(host, 2);

                    if (pingable)
                    {
                        port = Convert.ToInt32(args[1]);
                        username = args[2];
                        Start(host, port, username, password);
                    }
                }
                else //ask for input
                {
                    Console.Write("\nEnter the Host/Ip: ");
                    host = Console.In.ReadLine();

                    pingable = _ping.Ping(host, 2);

                    if (pingable)
                    {
                        Console.WriteLine(String.Format("The host: {0} is reachable!\r\n", host));

                        Console.Write("Enter Port Number: ");
                        port = Convert.ToInt32(Console.In.ReadLine());

                        Console.Write("Enter UserName: ");
                        username = Console.In.ReadLine();

                        Start(host, port, username, password);
                    }
                }
            }catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void Start(string host, int port, string username, string password)
        {
            var pwdFiles = _fileReader.GetPasswordLists();

            _fileReader.SetSSH(host, port, username, password);

            foreach (var filepath in pwdFiles)
            {
                if (!_fileReader.broken)
                {
                    if (filepath.EndsWith(".txt"))
                    {
                        _fileReader.StreamTxt(filepath);
                    }                                       
                }
            }
        }
    }
}
