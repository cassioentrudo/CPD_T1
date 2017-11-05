using System;
using System.IO;

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

            //Array A2 (Ordenado decrescente)
            uint[] A2 = new uint[ORIGINAL_SIZE];

            LoadOriginalArray(A0); //Carrega array do arquivo (randômico)

            A1 = SetA1(A0); //Monta A1 de forma ordenada crescente
            A2 = SetA2(A1); //Monta A2 de forma decrescente

            //Para o array A0
            //InsertSort("R", 1000, (uint[])A0.Clone());
            //ShellSort("R", 1000, (uint[])A0.Clone(), true);
            //CombSort("R", (uint[])A0.Clone(), 1000);
            ////Quicksort("R", (uint[])A0.Clone(), 1000);
            //SelectionSort("R", 1000, (uint[])A0.Clone());
            //Heapsort("R", 1000, (uint[])A0.Clone());
            //MergeSort("R", (uint[])A0.Clone(), 1000);

            //Para o array A1
            InsertSort("O", 1000, (uint[])A1.Clone());
            ShellSort("O", 1000, (uint[])A1.Clone(), true);
        }

        #region Métodos para carregar array do arquivo, criação das versões ordenadas crescente e decrescente e escrita de resultados

        /// <summary>
        /// Configura A1 de forma crescente
        /// </summary>
        private static uint[] SetA1(uint[] array)
        {
            uint[] newArray = (uint[])array.Clone();
            ShellSort("r", ORIGINAL_SIZE, (newArray), false);

            return newArray;
        }

        /// <summary>
        /// Configura A2 de forma decrescente
        /// </summary>
        private static uint[] SetA2(uint[] array)
        {
            uint[] newArray = (uint[])array.Clone();

            Array.Reverse(newArray);

            return newArray;
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

        private static void WriteFile(string line)
        {
            string fileName = "R00252847.txt";
            string filePath = Path.Combine(Environment.CurrentDirectory, fileName);

            try
            {
                FileStream file = File.Open(filePath, FileMode.Append, FileAccess.Write);
                StreamWriter sw = new StreamWriter(file);

                sw.WriteLine(line);

                sw.Flush();
                sw.Close();
            }
            catch (Exception)
            {
                //TODO
            }
        }

        #endregion

        #region Algoritmos de Inserção
        /// <summary>
        /// Ordena o vetor utilizando o algoritmo InsertionSort
        /// </summary>
        /// <param name="arrayType">Tipo de array: (o) Ordenado, (i) Inverso ou (r) Randômico</param>
        /// <param name="size">Tamanho do array (sempre passo o array original com tamanho original, mas esse size que decide até que tamanho ele ordena</param>
        /// <param name="array">Array a ser ordenado</param>
        private static void InsertSort(string arrayType, int size, uint[] array)
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

            string output = "ISBL, " + arrayType + ", " + size.ToString() + ", " + swaps + ", " + comparisons + ", " + (DateTime.Now.Subtract(begin)).TotalMilliseconds.ToString() + "ms";
            WriteFile(output);
        }

        /// <summary>
        /// Ordena o vetor utilizando o algoritmo ShellSort
        /// </summary>
        /// <param name="arrayType">Tipo de array: (o) Ordenado, (i) Inverso ou (r) Randômico</param>
        /// <param name="size">Tamanho do array (sempre passo o array original com tamanho original, mas esse size que decide até que tamanho ele ordena</param>
        /// <param name="array">Array a ser ordenado</param>
        private static void ShellSort(string arrayType, int size, uint[] array, bool writeFile)
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

            if (writeFile)
            {
                string output = "SheS, " + arrayType + ", " + size.ToString() + ", " + swaps + ", " + comparisons + ", " + (DateTime.Now.Subtract(begin)).TotalMilliseconds.ToString() + "ms";
                WriteFile(output);
            }           
        }

        #endregion

        #region Algoritmos de Trocas

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

            string output = "CbSt, " + arrayType + ", " + size.ToString() + ", " + swaps + ", " + comparisons + ", " + (DateTime.Now.Subtract(begin)).TotalMilliseconds.ToString() + "ms";
            WriteFile(output);
        }


        /// <summary>
        /// Ordena o vetor utilizando o algoritmo QuickSort. Calcula o tempo, trocas e comparacoes.
        /// </summary>
        /// <param name="arrayType">Tipo de array: (o) Ordenado, (i) Inverso ou (r) Randômico</param>
        /// <param name="array">Array a ser ordenado</param>
        /// <param name="size">Tamanho do array (sempre passo o array original com tamanho original, mas esse size que decide até que tamanho ele ordena</param>
        private static void Quicksort(string arrayType, uint[] array, uint size)
        {
            DateTime begin = DateTime.Now;
            int[] dados = new int[2];
            dados[0] = 0; //swaps
            dados[1] = 0; //comparisons

            Quicksort2(dados, array, 0, size - 1);

            int swaps = dados[0];
            int comparisons = dados[1];

            string output = "QukS, " + arrayType + ", " + size.ToString() + ", " + swaps + ", " + comparisons + ", " + (DateTime.Now.Subtract(begin)).TotalMilliseconds.ToString() + "ms";
            WriteFile(output);

        }

        /// <summary>
        /// Funcao auxiliar da QuickSort. Ordena o vetor utilizando o algoritmo QuickSort.
        /// </summary>
        /// <param name="dados">Array para guardar o numero de swaps(dados[0]) e comparisons(dados[1]). 
        /// <param name="array">Array a ser ordenado.
        /// <param name="primeiro">Primeira posicao do array. Normalmente zero.</param>
        /// <param name="ultimo">Ultima posicao do array.
        private static void Quicksort2(int[] dados, uint[] array, uint primeiro, uint ultimo)
        {
            uint baixo, alto, meio, pivo, repositorio;
            baixo = primeiro; //i dos slides
            alto = ultimo;    //j dos slides
            meio = (uint)((baixo + alto) / 2);
            dados[0]++;
            dados[1]++;
            pivo = array[meio];

            while (baixo <= alto)
            {
                while (array[baixo] < pivo)
                {
                    baixo++;
                    dados[1]++; //comparisons
                }
                while (array[alto] > pivo)
                {
                    alto--;
                    dados[1]++; //comparisons
                }
                if (baixo < alto)
                {
                    repositorio = array[baixo];
                    array[baixo++] = array[alto];
                    array[alto--] = repositorio;
                    dados[0]++; //swaps
                }
                else
                {
                    if (baixo == alto)
                    {
                        baixo++;
                        alto--;
                    }
                }
            }

            if (alto > primeiro)
                Quicksort2(dados, array, primeiro, alto);
            if (baixo < ultimo)
                Quicksort2(dados, array, baixo, ultimo);
        }

        #endregion

        #region Algoritmos de Seleção

        private static void SelectionSort(string arrayType, int size, uint[] array)
        {
            int swaps = 0;
            int comparisons = 0;

            DateTime begin = DateTime.Now;

            for (int indice = 0; indice < size; ++indice)
            {
                comparisons++;
                int indiceMenor = indice;

                for (int indiceSeguinte = indice + 1; indiceSeguinte < size; ++indiceSeguinte)
                {
                    if (array[indiceSeguinte] < array[indiceMenor])
                    {
                        indiceMenor = indiceSeguinte;
                    }
                }

                swaps++;
                uint aux = array[indice];
                array[indice] = array[indiceMenor];
                array[indiceMenor] = aux;
            }

            string output = "SelS, " + arrayType + ", " + size.ToString() + ", " + swaps + ", " + comparisons + ", " + (DateTime.Now.Subtract(begin)).TotalMilliseconds.ToString() + "ms";
            WriteFile(output);
        }

        private static void Heapsort(string arrayType, int size, uint[] array)
        {
            int n = size;
            int i = n / 2, pai, filho;
            uint t;
            int swaps = 0;
            int comparisons = 0;

            DateTime begin = DateTime.Now;

            while (true)
            {
                comparisons++;

                if (i > 0)
                {
                    i--;
                    t = array[i];
                }
                else
                {
                    n--;
                    if (n == 0) return;
                    t = array[n];
                    array[n] = array[0];
                }

                pai = i;
                filho = i * 2 + 1;

                while (filho < n)
                {
                    if ((filho + 1 < n) && (array[filho + 1] > array[filho]))
                        filho++;

                    if (array[filho] > t)
                    {
                        swaps++;
                        array[pai] = array[filho];
                        pai = filho;
                        filho = pai * 2 + 1;
                    }
                    else
                    {
                        break;
                    }
                }

                array[pai] = t;
            }

            string output = "HepS, " + arrayType + ", " + size.ToString() + ", " + swaps + ", " + comparisons + ", " + (DateTime.Now.Subtract(begin)).TotalMilliseconds.ToString() + "ms";
            WriteFile(output);
        }

        #endregion

        #region Algoritmos de Intercalação

        /// <summary>
        /// Ordena usando o algoritmo MergeSort, calcula ao numero de comparacoes e trocas.
        /// </summary>
        /// <param name="dados">array para guardar o numero de trocas e caomparaçoes</param>
        /// <param name="array">Array a ser ordenado</param>
        /// <param name="low">Primeiro indice da parte a ser ordenada</param>
        /// <param name="high">ultimo indice da parte a ser ordenada</param>
        public static void MergeSort2(int[] dados, uint[] array, uint low, uint high)
        {
            if (low < high)
            {
                uint middle = (low / 2) + (high / 2);
                MergeSort2(dados, array, low, middle);
                MergeSort2(dados, array, middle + 1, high);
                Merge(dados, array, low, middle, high);
            }
        }

        /// <summary>
        /// Funçao auxiliar do MergeSort que intercala.
        /// </summary>
        /// <param name="dados">array para guardar o numero de trocas e caomparaçoes</param>
        /// <param name="array">Array a ser ordenado</param>
        /// <param name="low">Primeiro indice da parte a ser ordenada</param>
        /// <param name="middle">indice do meio da parte a ser oerdenada</param>
        /// <param name="high">ultimo indice da parte a ser ordenada</param>
        private static void Merge(int[] dados, uint[] array, uint low, uint middle, uint high)
        {
            uint left = low;
            uint right = middle + 1;
            uint[] tmp = new uint[(high - low) + 1];
            uint tmpIndex = 0;

            while ((left <= middle) && (right <= high))
            {
                dados[1]++; //cada if uma comparison.
                if (array[left] < array[right])
                    tmp[tmpIndex++] = array[left++];
                else
                {
                    tmp[tmpIndex++] = array[right++];
                    dados[0]++;  // se o da esquerda for maior q o da direita, swap.
                }

            }

            while (left <= middle)
                tmp[tmpIndex++] = array[left++];

            while (right <= high)
                tmp[tmpIndex++] = array[right++];


            for (int i = 0; i < tmp.Length; i++)
                array[low + i] = tmp[i];
        }

        /// <summary>
        /// Funcao para chamar o MergeSort. Ordena o vetor utilizando o algoritmo MergeSort. Calcula o tempo, trocas e comparacoes.
        /// </summary>
        /// <param name="arrayType">Tipo de array: (o) Ordenado, (i) Inverso ou (r) Randômico</param>
        /// <param name="array">Array a ser ordenado</param>
        /// <param name="size">Tamanho do array (sempre passo o array original com tamanho original, mas esse size que decide até que tamanho ele ordena</param>
        static private void MergeSort(string arrayType, uint[] array, uint size)
        {
            DateTime begin = DateTime.Now;
            int[] dados = new int[2];
            dados[0] = 0; //swaps
            dados[1] = 0; //comparisons
            MergeSort2(dados, array, 0, size - 1);
            int swaps = dados[0];
            int comparisons = dados[1];

            string output = "MerS, " + arrayType + ", " + size.ToString() + ", " + swaps + ", " + comparisons + ", " + (DateTime.Now.Subtract(begin)).TotalMilliseconds.ToString() + "ms";
            WriteFile(output);
        }

        #endregion
            
         /// <summary>
        /// Funcao para chamar a Pesquisa Binaria. Calcula o tempo, trocas e comparacoes.
        /// </summary>
        /// <param name="chave">Numero a ser encontrado</param>
        /// <param name="array">Array a ser pesquisado</param>
        /// <param name="size">Tamanho do array</param>
        private static void PesquisaBinaria(int chave, uint[] array, int size)
        {
            DateTime begin = DateTime.Now;
            int[] dados = new int[2];
            dados[0] = 0; // swaps
            dados[1] = 0; // comparisons

            
            PesquisaBinaria2(dados, chave, array, 0, size - 1);

            int swaps = dados[0];
            int comparisons = dados[1];

            string output = "Bbin, " + "o, " + size.ToString() + ", " + swaps + ", " + comparisons + ", " + (DateTime.Now.Subtract(begin)).TotalMilliseconds.ToString() + "ms";
            
        }


        /// <summary>
        /// Pesquisa uma chave em um array, devolve -1 se nao encontrado e o indice se encontrado.
        /// </summary>
        /// <param name="dados">array para calcular o numero de trocas e comparacoes</param>
        /// <param name="chave">Numero a ser encontrado</param>
        /// <param name="array">Array a ser pesquisado</param>
        /// <param name="left">valor mais a esquerda do segmento para ser pesquisado</param>
        /// <param name="right">valor mais a direita do segmento para ser pesquisado</param>
        private static int PesquisaBinaria2(int[] dados, int chave, uint[] array, int left, int right)
        {
            int meio = (left + right) / 2;
            dados[1]++;
            if (array[meio] == chave)
                return meio;

            if (left >= right)
                return -1; // não encontrado
            else
            {
                dados[1]++;
                if (array[meio] < chave)
                    return PesquisaBinaria2(dados, chave, array, meio + 1, right);
                else
                    return PesquisaBinaria2(dados, chave, array, left, meio - 1);
            }

        }

        /// <summary>
        /// Funcao para chamar a Pesquisa Linear. Calcula o tempo, trocas e comparacoes.
        /// </summary>
        /// <param name="array">array a ser pesuisado</param>
        /// <param name="size">tamanho do array</param>
        /// <param name="chave">Chave a ser encontrada</param>
        private static void PesquisaLinear(uint[] array, int size, int chave)
        {
            DateTime begin = DateTime.Now;
            int[] dados = new int[2];
            dados[0] = 0; // swaps
            dados[1] = 0; // comparisons


            PesquisaLinear2(dados, array, chave, size);

            int swaps = dados[0];
            int comparisons = dados[1];

            string output = "BLin, " + "o, " + size.ToString() + ", " + swaps + ", " + comparisons + ", " + (DateTime.Now.Subtract(begin)).TotalMilliseconds.ToString() + "ms";
            Console.WriteLine(output);
        }


        /// <summary>
        /// Pesquisa uma chave em um array, devolve -1 se nao encontrado e o indice se encontrado.
        /// </summary>
        /// <param name="dados">array para calcular o numero de trocas e comparacoes</param>
        /// <param name="array">array a ser pesquisado</param>
        /// <param name="size">tamanho do array</param>
        /// <param name="chave">chave a ser enzontrada</param>
        private static int PesquisaLinear2(int[] dados, uint[] array, int size, int chave)
        {
         

            for (int i = 0; i < size; i++)
            {
                dados[1]++;
                if (array[i] == chave)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
