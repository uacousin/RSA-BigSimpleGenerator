using System;

using System.Numerics;

namespace lab2
{
    class Sender
    {
        public RSA rsa;
        public int len;
        private BigInteger p;
        private BigInteger q;
        private BigInteger phi;
        public BigInteger e;
        public BigInteger n;
        private BigInteger d;
        private BigInteger M;
        public BigInteger openM;
        public BigInteger S;
        public BigInteger C;
        private BigInteger k;

        public Sender(int len)
        {
            
            this.len = len;
            p = BigSimleGenerator.GoodBigSimple(len);
            q = BigSimleGenerator.GoodBigSimple(len);
            n = p * q;            
            phi = (q - 1) * (p - 1);
            e = BigInteger.Pow(2,16) + 1;
            d = BigSimleGenerator.Reverse(e, phi);
        }
        public BigInteger  K()
        {
            int len_n = BigSimleGenerator.Len(n);
            L89 l89 = new L89();
            k = BigSimleGenerator.RandomBig1(l89, len_n - 1);
            return k;

        }

        public BigInteger Init_S()
        {
            S = BigInteger.ModPow(k, d, n);
            return S;
        }

        public BigInteger Init_K1()
        {
            rsa.K1= BigInteger.ModPow(k, rsa.e2, rsa.n2);
            return rsa.K1;
        }

        public BigInteger Init_S1()
        {
            rsa.S1= BigInteger.ModPow(S, rsa.e2, rsa.n2);
            return rsa.S1;
        }                               

        public Sender (Sender A)
        {
            
            BigInteger[] c = BigSimleGenerator.GenPair(A.len, A.n);
            p = c[0];
            q = c[1];
            n = p * q;
            phi = (q - 1) * (p - 1);
            e = BigInteger.Pow(2, 16) + 1;
            d = BigSimleGenerator.Reverse(e, phi);
        }
                
        public void PrintAll()
        {
            Console.WriteLine("p="+ p.ToString("X"));
            Console.WriteLine("q=" + q.ToString("X"));
            Console.WriteLine("n=" + n.ToString("X"));
            Console.WriteLine("phi=" + phi.ToString("X"));
            Console.WriteLine("e=" + e.ToString("X"));
            Console.WriteLine("d=" + d.ToString("X"));
        }
        public BigInteger Get_d()
        {
            return d;
        }
        public BigInteger Cypher (BigInteger M)
        {
            //Console.WriteLine("M="+M.ToString("X"));            
            return BigInteger.ModPow(M,e, n);
        }        
        public BigInteger DeCypher ( BigInteger C)
        {
            return BigInteger.ModPow(C,d, n);
        }
        public BigInteger DigitalW ()
        {

            S = BigInteger.ModPow(openM, d, n);
            return S;
        }
        public bool Check(BigInteger M, BigInteger S)
        {
            return BigInteger.ModPow(S, rsa.e1, rsa.n1) == M;
        }
        public bool calc_check()
        {
            k= BigInteger.ModPow(rsa.K1, d, n);
            S = BigInteger.ModPow(rsa.S1, d, n);
            return k == BigInteger.ModPow(S, rsa.e2, rsa.n2);
        }
        public BigInteger Get_K()
        {
            return k;
        }

    }
    

    class RSA
    {
        public  BigInteger n1;
        public  BigInteger e1;
        public  BigInteger n2;
        public  BigInteger e2;
        public BigInteger K1;
        public BigInteger S1;        
        public RSA  ( Sender A , Sender B)
        {
            n1 = A.n;
            e1 = A.e;
            n2 = B.n;
            e2 = B.e;
        }

             
    }
}
