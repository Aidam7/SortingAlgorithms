using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelectionSort
{
    static class Utility
    {
        //Min - Spodní limit
        //Max - Horní limit
        //Length - Požadovaná délka kolekce
        public static void FillListRandom(this List<int> list, int min, int max, int length)
        {
            Random rnd = new();
            for (int i = 0; i < length; i++)
            {
                //Přidá do kolekce novou náhodně vygenerovanou hodnotu
                list.Add(rnd.Next(min, max));
            }
        }
        //Value - Hodnota, kterou chceme kolekci naplnit
        //Length - Požadovaná délka kolekce
        public static void FillList(this List<int> list, int value, int length)
        {
            for (int i = 0; i < length; i++)
            {
                //Přidá do kolekce novou hodnotu
                list.Add(value);
            }
        }
    }
}
