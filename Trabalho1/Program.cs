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
        private const int ORIGINAL_SIZE = 10000000;

        public static void Main(string[] args)
        {    
            //Array A0 (Randômico)
            uint[] A0 = new uint[ORIGINAL_SIZE];

            //Array A1 (Ordenado crescente)
            uint[] A1 = new uint[ORIGINAL_SIZE];

            LoadOriginalArray(A0);

            A1 = A0;

            //Usa ShellSorte para ordenar o array original que será utilizado nos teste.
            ShellSort("r", ORIGINAL_SIZE, A1);
        }

        /// <summary>
        /// Carrega em memória o Array original.
        /// </summary>
        private static void LoadOriginalArray(params uint[] array)
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
                    array[cont] = br.ReadUInt32();
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

        /// <summary>
        /// Ordena o vetor utilizando o algoritmo InsertionSort
        /// </summary>
        /// <param name="arrayType">Tipo de array: (o) Ordenado, (i) Inverso ou (r) Randômico</param>
        /// <param name="size">Tamanho do array (sempre passo o array original com tamanho original, mas esse size que decide até que tamanho ele ordena</param>
        /// <param name="array">Array a ser ordenado</param>
        private static void InsertSort(string arrayType, int size, params uint[] array)
        {
            DateTime begin = DateTime.Now;
            int swaps = 0;
            int comparisons = 0;

            for (int i = 1; i < size; i++)
            {
                uint key = array[i];
                int j = i - 1;

                comparisons++;

                while ((j >= 0) && array[j] > key)
                {
                    swaps++;
                    array[j + 1] = array[j];
                    j--;
                }

                array[j + 1] = key;
            }

            string output = "ISBL, " + arrayType + ", " + size.ToString() + ", " + swaps + ", " + comparisons + ", " + (DateTime.Now.Subtract(begin)).Milliseconds.ToString() + "ms";
        }

        /// <summary>
        /// Ordena o vetor utilizando o algoritmo ShellSort
        /// </summary>
        /// <param name="arrayType">Tipo de array: (o) Ordenado, (i) Inverso ou (r) Randômico</param>
        /// <param name="size">Tamanho do array (sempre passo o array original com tamanho original, mas esse size que decide até que tamanho ele ordena</param>
        /// <param name="array">Array a ser ordenado</param>
        private static void ShellSort(string arrayType, int size, params uint[] array)
        {
            DateTime begin = DateTime.Now;
            int swaps = 0;
            int comparisons = 0;
            int i, j, value;
            int gap = 1;

            while (gap < size)
            {
                gap = 3 * gap + 1;
            }

            while (gap > 1)
            {
                gap /= 3;
                for (i = gap; i < size; i++)
                {
                    value = (int)array[i];
                    j = i - gap;

                    comparisons++;
                    while (j >= 0 && value < array[j])
                    {
                        swaps++;
                        array[j + gap] = array[j];
                        j -= gap;
                    }

                    array[j + gap] = (uint)value;
                }
            }

            string output = "SheS, " + arrayType + ", " + size.ToString() + ", " + swaps + ", " + comparisons + ", " + (DateTime.Now.Subtract(begin)).Milliseconds.ToString() + "ms";
        }
        
                /// <summary>
        /// Ordena o vetor utilizando o algoritmo CombSort
        /// </summary>
        /// <param name="arrayType">Tipo de array: (o) Ordenado, (i) Inverso ou (r) Randômico</param>
        ///    /// <param name="array">Array a ser ordenado</param>
        /// <param name="size">Tamanho do array (sempre passo o array original com tamanho original, mas esse size que decide até que tamanho ele ordena</param>
        private static void CombSort(string arrayType, uint[] array, uint size)
        {
            DateTime begin = DateTime.Now;
            int swaps = 0;
            int comparisons = 0;
            uint gap = size;
            bool swapped = true;

            while (gap > 1 || swapped)
            {
                if (gap > 1)
                {
                    gap = (uint)(gap / 1.247330950103979);
                }
                uint i = 0;
                swapped = false;
                while (i + gap < size)
                {
                    comparisons++;
                    if (array[i].CompareTo(array[i + gap]) > 0)
                    {
                        swaps++;
                        uint t = array[i];
                        array[i] = array[i + gap];
                        array[i + gap] = t;
                        swapped = true;
                    }
                    i++;
                }
            }

            string output = "CbSt, " + arrayType + ", " + size.ToString() + ", " + swaps + ", " + comparisons + ", " + (DateTime.Now.Subtract(begin)).Milliseconds.ToString() + "ms";

        }
    }
}
