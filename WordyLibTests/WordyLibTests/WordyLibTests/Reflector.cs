using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WordyLibTests
{
    class Reflector
    {
        public static void GetAssemblyNames()
        {
            Assembly ass = Assembly.GetAssembly(typeof(WordyLib.Wordy));
            string name = ass.FullName;
            Console.WriteLine(name);
            string version = ass.ImageRuntimeVersion;
            Console.WriteLine(version.ToString());
            Console.WriteLine("................");
        }

    }
}
