using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Reflection.Metadata;
using System.Xml.Serialization;

namespace SelectionSort
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> numbers = new List<int>();
            numbers.FillListRandom(-20, 20, 20);
            Console.WriteLine($"{String.Join(" ", numbers)}\n");

            SelectionSort(numbers);

            Console.WriteLine($"{String.Join(" ", numbers)}\n");

        }
        public static void BubbleSort(List<int> numbers)
        {
            //Projdu Xkrát kolekci; X je počet poležek - 1, zamezí se tím šahání na neexistující položky, které by byly mimo rozsah
            for (int i = 0; i < numbers.Count - 1; i++)
            {
                //Založím boolean hasSorted (třídil), tato proměnná bude hlídat jestli se prohodila čísla a je použita později pro předčasné ukončení algoritmu
                bool hasSorted = false;
                //Projdu Xkrát kolekci; X je počet poležek - 1, opět se tím zamezí šahání mimo rozsah
                for (int j = 0; j < numbers.Count - 1; j++)
                {
                    //Pokud je aktuální položka na indexu J větší než následující vzájemně se prohodí a hasSorted se nastaví na true
                    if (numbers[j] > numbers[j + 1])
                    {
                        (numbers[j + 1], numbers[j]) = (numbers[j], numbers[j + 1]);
                        hasSorted = true;
                    }
                }
                //Vypíše aktuální podobu kolekce ve formátu [Počítadlo průchodů]. [{Položka} {Položka} {Položka}...]
                Console.WriteLine($"{i}. {String.Join(" ", numbers)}\n");
                //Pokud se neprohodily čísla [hasSorted == false] ukončí předčasně algoritmus
                if (!hasSorted) return;
            }
        }
        public static void SelectionSort(List<int> numbers)
        {
            int lowestNumber;
            for (int i = 0; i < numbers.Count; i++)
            {
                //Považuji první prvek za nejnižšší
                lowestNumber = numbers[i];
                for (int j = i + 1; j < numbers.Count; j++)
                {
                    //Procházím celou kolekci od prvku, který následuje po našem vybraném prvku
                    if (numbers[j] < lowestNumber)
                    {
                        //Pokud najdu nový nejnižší prvek prohodím je a uložím si nové nejnižší číslo
                        lowestNumber = numbers[j];
                        (numbers[i], numbers[j]) = (numbers[j], numbers[i]);
                    }
                }
            }
        }
        public static void InsertionSort(List<int> numbers)
        {
            //Projdu Xkrát kolekci; X je počet položek v kolekci
            //První položku automaticky bereme jako seřazenou a tak začíná druhou položkou (index 1)
            for (int i = 1; i < numbers.Count; i++)
            {
                //Uložíme si index do proměnné hole a hodnotu v kolekci na pozici indexu
                int value = numbers[i];
                int hole = i;
                //Provádíme smyčku dokud je index větší než 0 a zároveň je hodnota na pozici [index - 1] větší než hodnota na pozici index
                while (hole > 0 && numbers[hole - 1] > value)
                {
                    //Hodnota na pozici [index - 1] se přesune na index
                    numbers[hole] = numbers[hole - 1];
                    //Snížíme index o -1
                    hole--;
                }
                //Uložíme hodnotu, kterou jsme si uložili na začátku průchodu do kolekce na index
                numbers[hole] = value;
                //Vypíše průběžný stav kolekce
                Console.WriteLine($"{String.Join(" ", numbers)}\n");
            }
        }
        public static void HeapSort(List<int> numbers)
        {

            // Sestavíme heap
            for (int i = numbers.Count / 2 - 1; i >= 0; i--)
            {
                CreateHeap(numbers, numbers.Count, i);
            }
            // Vytaháme prvek po prvku z Kolekce
            for (int i = numbers.Count - 1; i > 0; i--)
            {
                // Přesuneme první prvek na konec
                (numbers[0], numbers[i]) = (numbers[i], numbers[0]);

                // Znovu sestavíme heap
                CreateHeap(numbers, i, 0);
            }
        }
        static void CreateHeap(List<int> numbers, int size, int i)
        {
            int father = i; // otec
            int leftSon = 2 * i + 1; // levý syn
            int rightSon = 2 * i + 2; // pravý syn

            // Pokud jsou synové větší než otec
            if (leftSon < size && numbers[leftSon] > numbers[father])
                father = leftSon;

            if (rightSon < size && numbers[rightSon] > numbers[father])
                father = rightSon;

            //Pokud není otec první prvek přesunu otce na první prvek
            if (father != i)
            {
                (numbers[i], numbers[father]) = (numbers[father], numbers[i]);

                // Znovu sestavíme heao
                CreateHeap(numbers, size, father);
            }
        }
        public static void MergeSort(List<int> numbers)
        {
            //Zastavení rekurze
            if (numbers.Count < 2) return;
            //Uložím si index prostředního čísla
            int middle = numbers.Count / 2;
            //Založím listy do kterých sleju levou a pravou část původní kolekce
            List<int> leftList = new List<int>();

            for (int i = 0; i < middle; i++)
            {
                leftList.Add(numbers[i]);
            }

            List<int> rightList = new List<int>();

            for (int i = middle; i < numbers.Count; i++)
            {
                rightList.Add(numbers[i]);
            }
            //Zavolám MergeSort na oddělenou levou a pravou část původní kolekce
            MergeSort(leftList);
            MergeSort(rightList);
            //Sloučím původní kolekci, respektive přepíšu, levou a pravou částí
            Merge(numbers,leftList,rightList);
        }
        static void Merge(List<int> numbers, List<int> leftList, List<int> rightList)
        {
            //Indexování
            int leftIndex = 0;
            int rightIndex = 0;
            //Dokud nepřetečou indexy mimo rozměry kolekcí
            while (leftIndex < leftList.Count && rightIndex < rightList.Count)
            {
                //Porovnám hodnoty na indexech v obou kolekcích, pokud je hodnota v pravé kolekci větší nebo rovna, uloží se hodnota z levé kolekce, protože je menší a naopak
                if (rightList[rightIndex] >= leftList[leftIndex])
                {
                    numbers[leftIndex + rightIndex] = leftList[leftIndex];
                    leftIndex++;
                }
                else
                {
                    numbers[leftIndex + rightIndex] = rightList[rightIndex];
                    rightIndex++;
                }
                
            }
            //Protože rozdíl mezi pravou a levou kolekcí by měl být maximálně jeden prvek měl by stačit zápis IF ELSE, ale pro klid duše jsem se rozhodl pro zápis 2 IFy
            if (leftIndex < leftList.Count)
            {
                //Slévání nedotřízeného čísla
                while (leftIndex < leftList.Count)
                {
                    numbers[leftIndex + rightIndex] = leftList[leftIndex];
                    leftIndex++;
                }
            }
            if(rightIndex < rightList.Count)
            {
                //Slévání nedotřízeného čísla
                while (rightIndex < rightList.Count)
                {
                    numbers[leftIndex + rightIndex] = rightList[rightIndex];
                    rightIndex++;
                }
            }
        }
        static void QuickSort(List<int> numbers, int leftBound, int rightBound)
        {
            //Ošetření neintuitivních limitů a ukončení rekurze
            if (rightBound <= leftBound || rightBound > numbers.Count - 1)
                return;
            //Najdeme optimální umístění pivotu
            int pivot = QuickSortPartition(numbers, leftBound, rightBound);

            QuickSort(numbers, leftBound, pivot - 1);
            QuickSort(numbers, pivot + 1, rightBound);
        }
        static int QuickSortPartition(List<int> numbers, int leftBound, int rightBound)
        {

            // Pivot se umístí na konec, optimálnější umístění by bylo buďto náhodné nebo medián první, poslední a prostředí hodnoty je také dobrá možnost
            int pivot = numbers[rightBound];

            //Uložím si index, jako index nám poslouží spodní hranice - 1
            int index = (leftBound - 1);

            for (int i = leftBound; i <= rightBound - 1; i++)
            {
                if (numbers[i] < pivot)
                {

                    //Pokud je aktuální položka menší než pivot prohodím aktuální položku a položku na indexu
                    index++;
                    (numbers[index], numbers[i]) = (numbers[i], numbers[index]);
                }
            }
            //Prohodím položku za indexem a položku na pravé horní hranici
            (numbers[index + 1], numbers[rightBound]) = (numbers[rightBound], numbers[index + 1]);

            return (index + 1);
        }
        public static void CountingSort(List<int> numbers)
        {
            int min = int.MaxValue;
            int max = int.MinValue;
            //Založím si nový list a naplním ho hodnotami, kterými poté přepíšu původní list, tento přístup ulehčuje psaní kódu
            List<int> returnList = new List<int>();
            returnList.FillList(0, numbers.Count);
            //Najdu nejvyšší a nejnižší hodnotu
            foreach (var number in numbers)
            {
                if (min > number)
                    min = number;
                if (max < number)
                    max = number;
            }
            //Založím si a naplním počítací kolekci
            List<int> countingList = new List<int>();

            countingList.FillList(0, max - min + 1);
            for (int i = 0; i < numbers.Count; i++)
            {
                //Zvýším hodnotu na indexu [Aktuální hodnota v původní kolekci na indexu i - minimální hodnota] o 1
                countingList[numbers[i] - min]++;
            }
            int j = 0;

            for (int i = 0; i < countingList.Count; i++)
            {
                //Provedu pro celou délku počítací kolekce
                //Uložím aktuální hodnotu z počítací kolekce
                int value = countingList[i];
                while (value > 0)
                {
                    //Dokud je hodnota vyšší než 0 budu ji snižovat a přepisovat hodnotu na indexu j v návraté kolekci o [index i + mininámlní hodnota]
                    returnList[j] = i + min;
                    j++;
                    value--;
                }
            }
            //Přepíšu původní kolekci novou kolekcí, také lze provést například za pomocí  RETURNu
            for (int i = 0; i < numbers.Count; i++)
            {
                numbers[i] = returnList[i];
            }
        }
    }
}