using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shroomworld_Console
{
    internal static class File
    {
        public static void Load(ref List<string> destination, string path)
        {
            destination = new List<string>();
            try
            {
                using (System.IO.StreamReader sr = new System.IO.StreamReader(path))
                {
                    while (!sr.EndOfStream)
                    {
                        destination.Add(sr.ReadLine());
                    }
                    sr.Close();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static void Save(List<string> file, string path, bool overwrite)
        {
            using(System.IO.StreamWriter sw = new System.IO.StreamWriter(path, !overwrite))
            {
                foreach (string line in file)
                {
                    sw.WriteLine();
                }
                sw.Close();
            }
        }
    }
}
