using System;
using System.Numerics;

namespace lab2
{
    class Program
    {
        
       
        static void Main(string[] args)
        {                       
            Sender A = new Sender(128);
            Sender B = new Sender(A);
            RSA rsa = new RSA(A, B);
            A.rsa = rsa;
            B.rsa = rsa;          
            
            int c = -1;

            while (c!=0)
            {
                Console.WriteLine("1.Sender Info\n2. Cypher\n3.DeCypher\n4.Didital Write\n5.Send key\n6.Recieve key\n7.Get Secret\n8.Recievr Info\n\n0.Exit");
                Console.Write("\nYour choose: ");
                c =int.Parse(Console.ReadLine());
                Console.WriteLine();
                switch (c)
                {
                    case 1:
                        A.PrintAll();
                        break;
                    case 2:
                        Console.Write("Enter M = ");
                        Console.WriteLine("C = "+A.Cypher(BigInteger.Parse(Console.ReadLine(), System.Globalization.NumberStyles.AllowHexSpecifier)).ToString("X"));
                        break;
                    case 3:
                        Console.Write("Enter C = ");
                        Console.WriteLine("M = "+ A.DeCypher(BigInteger.Parse(Console.ReadLine(), System.Globalization.NumberStyles.AllowHexSpecifier)).ToString("X"));
                        break;
                    case 4:
                        Console.Write("Enter M = ");
                        A.openM= BigInteger.Parse(Console.ReadLine(), System.Globalization.NumberStyles.AllowHexSpecifier);
                        Console.WriteLine("S = " + A.DigitalW().ToString("X"));
                        Console.WriteLine(B.Check(A.openM, A.S));                        
                        break;
                    case 5:
                        Console.Write("nB = ");
                        rsa.n2 = BigInteger.Parse(Console.ReadLine(), System.Globalization.NumberStyles.AllowHexSpecifier);

                        Console.Write("nA = "+A.n.ToString("X")+"\ndA = "+A.Get_d().ToString("X"));
                        Console.WriteLine();
                        Console.Write("nB = " + B.n.ToString("X") + "\neB = " + B.e.ToString("X"));
                        Console.WriteLine();
                        Console.WriteLine("K = "+A.K().ToString("X"));
                        Console.WriteLine("S = "+A.Init_S().ToString("X"));
                        Console.WriteLine();
                        Console.WriteLine("K1 = "+A.Init_K1().ToString("X"));
                        Console.WriteLine("S1 = "+A.Init_S1().ToString("X"));                       
                        break;
                    case 6:      
                                          
                        Console.Write("nB = " + B.n.ToString("X") + "\ndB = " + B.Get_d().ToString("X"));
                        Console.WriteLine();
                        Console.Write("nA = " + A.n.ToString("X") + "\neA = " + A.e.ToString("X"));
                        Console.WriteLine();
                        Console.WriteLine("K1 = "+rsa.K1.ToString("X"));
                        Console.WriteLine("S1 = "+rsa.S1.ToString("X"));
                        Console.WriteLine();
                        Console.WriteLine("Auth: "+B.calc_check());
                        Console.WriteLine("K = " + B.Get_K().ToString("X"));
                        Console.WriteLine("S = " + B.S.ToString("X"));
                   
                        break;
                    case 7:
                        Console.Write("nA = ");
                        rsa.n2=BigInteger.Parse(Console.ReadLine(), System.Globalization.NumberStyles.AllowHexSpecifier);
                        Console.Write("eA = ");
                        rsa.e2 = BigInteger.Parse(Console.ReadLine(), System.Globalization.NumberStyles.AllowHexSpecifier);
                        Console.Write("K1 = ");
                        rsa.K1= BigInteger.Parse(Console.ReadLine(), System.Globalization.NumberStyles.AllowHexSpecifier);
                        Console.Write("S1 = ");
                        rsa.S1= BigInteger.Parse(Console.ReadLine(), System.Globalization.NumberStyles.AllowHexSpecifier);
                        Console.WriteLine();
                        Console.WriteLine(A.calc_check() ); 
                        Console.Write("k = "+ A.Get_K().ToString("X") );
                        Console.Write("\nS = "+ A.S.ToString("X"));
                        
                        break;
                    case 8:
                        B.PrintAll();
                        break;
                    case 0: break; 
                }
                Console.WriteLine();
                
            }   
        }
    }
}
