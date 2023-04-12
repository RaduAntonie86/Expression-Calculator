using System;
using System.Collections;
using System.Linq;

class Calculator
{
    public static void swap(ref double a, ref double b)
    {
        double t;
        t = a;
        a = b;
        b = t;
    }
    static bool EsteOperatie<T>(T c)
    {
        switch (c)
        {
            // The function may receive either a char or a string
            // This is why we account for every symbol twice
            case '+':
            case "+":
            case '-':
            case "-":
            case '*':
            case "*":
            case '/':
            case "/":
            case '^':
            case "^":
                return true;
            default:
                return false;
        }
    }
    static int EvalueazaOperatie<T>(T c)
    {
        switch (c)
        {
            // The function may receive either a char or a string
            // This is why we account for every symbol twice
            case '+': 
            case "+":
            case '-':
            case "-":
                return 1;
            case '*':
            case "*":
            case '/':
            case "/":
                return 2;
            case '^':
            case "^":
                return 3;
            default:
                return 0;
        }
    }
    static double EvalueazaSemn(string c, double a, double b)
    {
        switch (c)
        {
            case "+":
                return Adunare(a, b);
            case "-":
                return Scadere(a, b);
            case "*":
                return Inmultire(a, b);
            case "/":
                return Impartire(a, b);
            case "^":
                return Putere(a, b);
            default:
                Console.WriteLine("Aceasta operatie nu este suportata.");
                return 0.0;
        }
    }
    static double EvalueazaNumar(string S, int start, int end)
    {
        double num = 0.0;
        if(end == 0) // We have to account for when we are given a single digit
            return (double)S[start] - '0';
        for (int i = start; i <= end; i++)
        {
            if (char.IsNumber(S[i]))
                num = num * 10.0 + (double)S[i] - '0';
        }
        return num;
    }
    static double Adunare(double a, double b)
    {
        float rez = (float)a + (float)b;
        return rez;
    }
    static double Scadere(double a, double b)
    {
        float rez = (float)a - (float)b;
        return rez;
    }
    static double Inmultire(double a, double b)
    {
        float rez = (float)a * (float)b;
        return rez;
    }
    static double Putere(double a, double b)
    {
        float rez = (float) Math.Pow(a, b);
        return rez;
    }
    static double Impartire(double a, double b)
    {
        // We can't store the number 0 in a double, and 0.0 does not throw an exception
        // The result of dividing by 0.0 is infinity
        if (b != 0.0)
            return a / b;
        Console.WriteLine("Impartirea la 0 este imposibila.");
        return 0.0;
    }
    //Creating the postfix form
    static ArrayList CreeazaPoloneza(string expresie)
    {
        int start = 0;
        ArrayList semne = new ArrayList();
        ArrayList poloneza = new ArrayList();
        while (expresie != "" && (char.IsDigit(expresie[0]) || EsteOperatie(expresie[0])))
        {
            int semnStart = 0;
            int semnPrio = 0;
            int opCut = 0;
            int length = 0;
            semne.Clear();
            string expresietemp = "";
            // Hacky way to check where the last digit is later
            expresie += " ";
            // We will look for all of the operations first
            for (int i = 0; i < expresie.Length; i++)
            {
                if (EvalueazaOperatie(expresie[i]) > semnPrio)
                {
                    semne.Add(char.ToString(expresie[i]));
                    semnPrio = EvalueazaOperatie(expresie[i]);
                    semnStart = i;
                }
                else
                {
                    if (expresie[i] == ')')
                    {
                        semnPrio = 0;
                        semnStart = i;
                        break;
                    }
                    if (expresie[i] == '(')
                    {
                        semne.Clear();
                        semnPrio = 0;
                        semnStart = i;
                        length = i;
                    }
                }
            }
            // For some reason they always were in the exact opposite order
            semne.Reverse();
            // Checking to see where the first number after the operator ends
            if (semnPrio > 0)
            {
                for (int i = semnStart + 1; i < expresie.Length; i++)
                {
                    if (EvalueazaOperatie(expresie[i]) > 0)
                        break;
                    else if (char.IsNumber(expresie[i]))
                    {
                        for (int j = i; j < expresie.Length; j++)
                            if (!char.IsNumber(expresie[j]) && expresie[j] != '.' && expresie[j] != ',')
                            {
                                semnStart = j;
                                break;
                            }
                        break;
                    }
                }
            }
            // Processing all of the numbers
            for (int i = length; i <= semnStart; i++)
            {
                if (char.IsNumber(expresie[i]))
                {
                    if (i > 0 && char.IsNumber(expresie[i - 1]))
                    {
                        poloneza.RemoveAt(poloneza.Count - 1);
                        poloneza.Add(EvalueazaNumar(expresie, start, i).ToString());
                    }
                    else
                    {
                        start = i;
                        poloneza.Add(EvalueazaNumar(expresie, start, i).ToString());
                    }
                }
                else if ((expresie[i] == '.' || expresie[i] == ',') && i > 0 && char.IsNumber(expresie[i - 1]))
                {
                    poloneza.RemoveAt(poloneza.Count - 1);
                    double numar1 = EvalueazaNumar(expresie, start, i);
                    for (int j = i + 1; j < expresie.Length; j++)
                    {
                        if (!char.IsNumber(expresie[j]))
                        {
                            double numar2 = 0.0;
                            numar2 = EvalueazaNumar(expresie, i + 1, j);
                            numar2 /= Math.Pow(10, numar2.ToString().Length);
                            numar1 += numar2;
                            poloneza.Add(numar1.ToString());
                            i += numar2.ToString().Length - 1;
                            break;
                        }
                    }
                }
                opCut = i;
            }
            poloneza.AddRange(semne);
            // Cutting out the part we have already included in the postfix form
            if (length > 0)
                expresietemp = expresie.Substring(0, length);
            expresie = expresietemp + expresie.Substring(opCut + 1);
        }
        return poloneza;
    }

    public static double EvalueazaExpresie(string expresie)
    {
        ArrayList poloneza = new ArrayList();
        poloneza = CreeazaPoloneza(expresie);
        // Showing the postfix form on screen (for testing)
        /*
        foreach(string s in poloneza)
            Console.Write(s + " ");
        Console.WriteLine();
        */
        bool opereaza = false;
        double rez = 0.0;
        while (poloneza.Count > 1)
        {
            double a = 0.0;
            double b = 0.0;
            int pos1 = 0;
            int pos2 = 0;
            int pos3 = 0;
            for (int i = 0; i < poloneza.Count; i++)
            {
                if (double.TryParse((string)poloneza[i], out _))
                {
                    // Gotta do this otherwise the numbers might end up in the wrong order
                    if (b > 0.0 && a > 0.0)
                    {
                        swap(ref a, ref b);
                    }
                    else if (!opereaza)
                    {
                        a = double.Parse((string)poloneza[i]);
                        pos1 = i;
                    }
                    else
                    {
                        b = double.Parse((string)poloneza[i]);
                        pos3 = i;
                    }
                    opereaza = !opereaza;
                }
                else if (EvalueazaOperatie(poloneza[i]) > 0)
                {
                    rez = EvalueazaSemn((string)poloneza[i], a, b);
                    opereaza = false;
                    pos2 = i;
                    break;
                }
            }
            // Floats are weird
            rez = (int)(rez * 1000);
            rez /= 1000;
            // Cutting out the part we have already handled
            ArrayList polTemp = new ArrayList();
            if (pos1 > 0)
            {
                for (int i = 0; i < pos3; i++)
                    polTemp.Add(poloneza[i]);
            }
            polTemp.Add(rez.ToString());
            if (pos2 < poloneza.Count)
            {
                for (int i = pos2 + 1; i < poloneza.Count; i++)
                    polTemp.Add(poloneza[i]);
            }
            poloneza = polTemp;
        }
        return rez;
    }
}
