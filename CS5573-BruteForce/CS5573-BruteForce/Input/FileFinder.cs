using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using _CS5573_BruteForce.PasswordCreator;

namespace _CS5573_BruteForce.Input
{
    public class FileFinder
    {
        public List<String> inputFiles { get; set; }
        
        public FileFinder()
        {
            inputFiles = new List<String>();
            
            var textfiles = from file in Directory.EnumerateFiles(AppDomain.CurrentDomain.BaseDirectory + "input", "*.txt", SearchOption.AllDirectories)
                        select new
                        {
                            File = file,
                        };
            foreach (var f in textfiles)
            {
                inputFiles.Add(f.File);
            }
        }

        public void ListFiles()
        {            
            var count = 0;

            Console.Write("------------------------------- CS5573 Brute Force Password Attacker ------------------------------ \n\n");
            Console.Write("Current Character Array = [a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,\n" +
                "                           A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,\n" +
                "                           1,2,3,4,5,6,7,8,9,0] \n\n");
            Console.Write("Generating Brute Force Passwords of maximum length 4 to text file... \n");
            PasswordMaker.MakeBruteForcePasswords();         
          
            Console.Write("\r\n");
            Console.WriteLine(String.Format("The following {0} will be used for the Password attack. \r\n", inputFiles.Count>0 ? "text file(s)" : "dictionary"));
            foreach (var f in inputFiles)
            {
                Console.Write(" {0, -2}) {1, -15}", ++count, Path.GetFileName(f));
                if(count%4 == 0)
                {
                    Console.Write("\r\n\n");
                }
            }
            Console.Write("\r\n\r\n");
        }
    }
}
