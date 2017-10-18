using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho1
{
    class Program
    {
        /// <summary>
        /// Tamanho do array
        /// </summary>
        private const int SIZE = 10000000;

        //Array original do jeito que está
        private static uint[] originalArray = new uint[SIZE];

        public static void Main(string[] args)
        {
            LoadOriginalArray();
        }

        /// <summary>
        /// Carrega em memória o Array original.
        /// </summary>
        private static void LoadOriginalArray()
        {
            string fileName = "randomnumbers.bin";
            string filePath = Path.Combine(Environment.CurrentDirectory, fileName);
            int cont = 0;

            FileStream file = new FileStream(filePath, FileMode.Open);
            BinaryReader br = new BinaryReader(file);

            try
            {
                while (true)
                {
                    originalArray[cont] = br.ReadUInt32();
                    cont++;
                }
            }
            catch (EndOfStreamException)
            {
                file.Close();
                br.Close();
            }
        }

        private static void SortArray()
        {

        }
    }
}
