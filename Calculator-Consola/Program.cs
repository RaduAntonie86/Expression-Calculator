using System;

namespace Program
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Aceasta este versiunea de consola de la Calculatorul de Expresii!\n");
            Console.WriteLine("Urmatoarele operatii sunt suportate:");
            Console.WriteLine(" + = adunare\n - = scadere\n * = inmultire\n / = impartire\n ^ = ridicare la putere\n . sau , = separator decimal\n");
            Console.WriteLine("Spatiile intre termeni sunt suportate, in limita bunului simt.\n");
            Console.WriteLine("Pentru a incheia programul, introduceti cuvantul \"quit\" ca si expresie.\n");
            bool alearga = true;
            while (alearga)
            {
                Console.Write("Introduceti o expresie: ");
                string expresie = Console.ReadLine();
                if (expresie == "quit")
                    break;
                Console.WriteLine("\nRezultatul este " + Calculator.EvalueazaExpresie(expresie) + ".\n");
            }
        }
    }
}
