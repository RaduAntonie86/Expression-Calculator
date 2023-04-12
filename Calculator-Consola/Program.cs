using System;

namespace Program
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Bine ati venit la calculatoru meu de cacat!\n");
            Console.WriteLine("Urmatoarele operatii sunt suportate:");
            Console.WriteLine(" + = adunare\n - = scadere\n * = inmultire\n / = impartire\n ^ = ridicare la putere\n . sau , = separator decimal\n");
            Console.WriteLine("Spatiile intre termeni sunt suportate, in limita bunului simt.\n");
            Console.WriteLine("Exemplu de expresii suportate:\n a+b\n a*(b+c)\n (a+b)*c\n a + (b * c) + d\n");
            Console.WriteLine("Pentru a incheia programul, introduceti cuvantul \"quit\" ca si expresie.\n");
            bool alearga = true;
            while (alearga)
            {
                Console.Write("Introduceti o expresie: ");
                string expresie = Console.ReadLine();
                if (expresie == "quit")
                    break;
                Calculator.EvalueazaExpresie(expresie);
            }
        }
    }
}
