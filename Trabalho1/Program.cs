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

            uint[] OrderedArray = new uint[SIZE];
            InsertSort(OrderedArray);
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
                //Aqui acontece uma exception, mas é porque chegou no fim do arquivo
                file.Close();
                br.Close();
            }
        }

        private static void InsertSort(params uint[] array)
        {
            array = originalArray;

            for (int i = 1; i < SIZE; i++)
            {
                uint key = array[i];

                int j = i - 1;

                while ((j > 0)  && array[j] > key)
                {
                    array[j+1] = array[j];
                }

                array[j+1] = key;
            }
        }

        private static void SortArray()
        {
            
        }
    }
}
