using System;
using System.Net.NetworkInformation;

namespace _CS5573_BruteForce.Driver
{
    public class Pinging
    {
        private Ping _pingSender;

        public Pinging()
        {
           _pingSender = new Ping();
        }

        public bool Ping(string ipAddress, int numEchoes)
        {
            try
            {
                long totalTime = 0;
                int timeout = 120;

                Console.WriteLine("Pinging Ip: " + ipAddress);
                for (int i = 0; i < numEchoes; i++)
                {
                    PingReply reply = _pingSender.Send(ipAddress, timeout);
                    Console.WriteLine("Ip Reponse Status: " + reply.Status);

                    if (reply.Status == IPStatus.Success)
                    {
                        totalTime += reply.RoundtripTime;
                    }
                    else
                    {
                        return false;
                    }                   
                }
                Console.WriteLine("Total Time in mS: " + totalTime / numEchoes);

                return true;

            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
