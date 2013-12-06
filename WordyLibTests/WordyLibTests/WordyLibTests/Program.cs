using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordyLib;
using System.Reflection;


namespace WordyLibTests
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            Version version = assembly.GetName().Version;
            string loadMessage = "Loaded Wordy Lib Tests version: " + version.ToString();
            Console.WriteLine(loadMessage);
            Console.WriteLine("...........");
            Reflector.GetAssemblyInfo();
            Console.WriteLine("Beginning Tests...");
            string url = "http://www.cnn.com";
            Console.WriteLine("Reference URL is: " + url);
            Console.WriteLine("...........");

           var foo = Wordy.getKeywords(url);
            




            Console.ReadKey();
        }
    }
}
