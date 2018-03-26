using Renci.SshNet;
using System;

namespace _CS5573_BruteForce.Ssh
{
    public class SSHConnection
    {
        public bool Connect(string host, int port, string username, string password)
        {
            try
            {
                using (var client = new SshClient(host, port, username, password))
                {
                    client.Connect();

                    Console.WriteLine("Connection Established !! ");
                    Console.WriteLine("Type commands to run them in the target machine (q to quit).");

                    //able to enter executable command lines while connected
                    while (client.IsConnected) 
                    {
                        Console.Write(host + " > ");
                        var input = Console.In.ReadLine();

                        if(input.ToLower() == "q" || input.ToLower() == "quit")
                        {
                            break;
                        }

                        var response = client.RunCommand(input);

                        if(response.Result != "")
                        {
                            Console.WriteLine(response.Result);
                        }
                        else if (response.Error != "")
                        {
                            Console.WriteLine(response.Error);
                        }
                    }
                    return true;
                }
            }catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
