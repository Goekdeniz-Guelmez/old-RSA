
class von_schule
{

    static public void Main(String[] args)
    {
        Console.ForegroundColor = ConsoleColor.Green; // schriftfarbe aud grün setzten wie in der Matrix
        Random rd = new Random(); //erschaffe ein neues Random Generator objekt mit dem namen rd

        rndk: 
            int rand_num = rd.Next(1, 4); //Generiere eine random nummer zwischen 1 und 10
            int rand_num2 = rd.Next(1, 4); //Hier auch

        if (rand_num == rand_num2) //Wenn beide Nummern gleich sind, generiert er eine neue zahl und macht es so lange bis beiede Zahlen NICHT gleich sind
        {
            goto rndk;
        }

        List<int> primes = findPrimes(0, 50); //Erschaft ein Array mit Priemzahlen die von 0 bis 50 gehen NICHT das es 50 Priemzahlen in der Liste gibt sondern geht die Anzahl an Priemzahlen bis 50

        int publickey = primes[rand_num]; //Der gibt die Priemzahl die in der Stelle der Generierten Nummer ist 
        int privatekey = primes[rand_num2]; //Hier das gleiche 
        //Console.WriteLine("Public Key " + publickey); //Gibt aus
        //Console.WriteLine("Private Key " + privatekey); //Hier auch

        int N = Nberechnen(publickey, privatekey); //speichert das gegebene Parameter von der Methode aus
        int phi = phiberechnen(publickey, privatekey); //Das Gleiche
        int e = teilerfremd(phi); //Das Gleiche
        double d = dberechnen(e, phi); //Das Gleiche

        //Console.WriteLine();
        Console.WriteLine("Öffentlicher Schlüssel: (" + e + ", " + N + ")");
        Console.WriteLine("Privater Schlüssel: (" + d + ", " + N + ")");

        Console.WriteLine();

        Console.WriteLine("Note eingeben"); //Jetzt kommt der User Imput

        double m = Convert.ToInt32(Console.ReadLine()); //Speichert die eingegebene Note ein und Convertiert sie in eine Double und nennt sie m

        double c = verschlüsseln(m, e, N); // Jetzt Verschlüsselt er die Note

        entschlüsseln(c, d, N); // Jetzt Entschlüsselt er die Note


        /*
        foreach (int prime in primes)
        {
            Console.Write(prime.ToString() + "; ");
        }
        */
    }


    //------------------------------------------------------------------------------------------------------------------------------------------------
    public static List<int> findPrimes(int min, int max) // Hier addiert er die Zahlen die True (also eine Priemzahl ist) in die Liste
    {
        List<int> primes = new List<int>(); //Erstellen eines Listen Objekts in dem alle Priemzahlen gespeichert werden
        bool[] numbers = new bool[max + 1];

        //if (max < 500) return primes; //Wenn die maximale, eingegebene zahl unter 500 ist gibt er 

        for (int i = 0; i <= max; i++)
        {
            numbers[i] = true;
        }

        int p = 2;

        while (p * p <= max && p * p >= min)
        {
            for (int i = p + p; i <= max; i += p)
            {
                numbers[i] = false;
            }

            int x = p + 1;
            while (numbers[x] == false) x++;
            p = x;
        }

        for (int i = 2; i <= max; i++)
        {
            if (numbers[i] == true) primes.Add(i);
        }

        return primes;
    }


    //------------------------------------------------------------------------------------------------------------------------------------------------
    public static int Nberechnen(int p, int q)
    {
        //Console.WriteLine();
        //Console.WriteLine("Nberechnen --------------------------------------------------------------------------------------");

        int N = p * q;
        //Console.WriteLine("N ist " + N);

        return N;
    }


    //------------------------------------------------------------------------------------------------------------------------------------------------
    public static int phiberechnen(int p, int q)
    {
        //Console.WriteLine();
        //Console.WriteLine("phiberechnen --------------------------------------------------------------------------------------");

        int phi = (p-1) * (q-1);

        //Console.WriteLine("Phi ist " + phi);

        return phi;
    }

    //------------------------------------------------------------------------------------------------------------------------------------------------
    public static int teilerfremd(int phi) // um e heraus zu finden muss e größer asl 0 und kleiner als phi sein
    {
        //Console.WriteLine();
        //Console.WriteLine("Teilerfremd --------------------------------------------------------------------------------------");

        int phin = phi / 2; // Damit e nicht so groß wird habe ich phi in 2 geteilt, also e muss jetzt kleiner asl phi / 2 sein. Diese Zahl heißt phin
        List<int> primes = findPrimes(0, phin); // zwei Primzahlen sind Teilerfremd, also erstellt er eine liste von Primzahlen zwischen 0 und phin

        var totalprimes = primes.Count(); // da zählte er wie viele Zahlen in der Liste sind
        //Console.WriteLine("alle Zahlen bei e " + totalprimes);

        Random rd = new Random();
        //int totalprimesweniger = totalprimes / 2;
        int rand_e = rd.Next(1, totalprimes); // jetz erstellt er eine random nummer die zwischen 1 und totalprimes ist, das wird die e zahl sein

        int e = primes[rand_e]; // die obere Zahl ist die position, zb, rand_e = 4, also ist die 4 Zahl in der Liste ist e

        //Console.WriteLine("e ist " + e);

        return e;
    }


    //------------------------------------------------------------------------------------------------------------------------------------------------
    public static double dberechnen(int e, int phi)
    {
        //Console.WriteLine();
        //Console.WriteLine("dberechnen --------------------------------------------------------------------------------------");
        int d = 0; // hier setzte ich d als 0

        while ((e * d) % phi != 1) // So lange die Gleichung ungleich 1 ist 
        {
            if (d < 0) { d = 0; } // Wenn d kleiner als 0 (also im minus ist) setzte er d = 0 
            d++; // d wird um 1 hochgezählt
        }

        //Console.WriteLine((e * d) % phi);
        //Console.WriteLine("d ist " + d);

        return d;
    }


    //------------------------------------------------------------------------------------------------------------------------------------------------
    public static double verschlüsseln(double m, int e, int N)
    {
        Console.WriteLine();
        Console.WriteLine("verschlüsseln --------------------------------------------------------------------------------------");
        double en = Convert.ToDouble(e);

        double var = Math.Pow(m, en);
        //Console.WriteLine("Power verschl " + var);
        double c = var % N;
        Console.WriteLine("Verschlüsselte Note " + c);

        return c;
    }


    //------------------------------------------------------------------------------------------------------------------------------------------------
    public static double entschlüsseln(double c, double d, int N)
    {
        Console.WriteLine();
        Console.WriteLine("entschlüsseln --------------------------------------------------------------------------------------");

        double var = Math.Pow(c, d);
        //Console.WriteLine("Power entschl " + var);
        double m = var % N;

        Console.WriteLine("erntschlüsselte Note " + m);

        return m;
    }
}