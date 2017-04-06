using System;
using System.Numerics;

namespace lab2
{
    class BigSimleGenerator
    {
        public static  BigInteger BinToDec(string value)
        {
            // BigInteger can be found in the System.Numerics dll
            BigInteger res = 0;

            // I'm totally skipping error handling here
            foreach (char c in value)
            {
                res <<= 1;
                res += c == '1' ? 1 : 0;
            }

            return res;
        }
        public static BigInteger RandomBig1(L gen, int max)
        {
            BigInteger l = new BigInteger();
            string num = gen.nNext_bit(max );
            //Console.WriteLine(num);
            l = BinToDec(num);
            return l;
        }
        public static BigInteger RandomBig(L gen, int len)
        {           
            BigInteger l = new BigInteger();
            string num = gen.nNext_bit(len - 1);
            num = "1" + num;            
            l = BinToDec(num);
            return l;
        }
        public static bool MillerRabin(BigInteger p, BigInteger k)
        {
            BigInteger p_1 = p - 1;
            BigInteger d = p_1;
            int s = 0;
            while( d%2==0)
            {
                d = d/ 2;
                s++;
            }
            bool super_simple=false;
            int temp_len = p_1.ToByteArray().Length-1;
            int len = temp_len*8+Convert.ToString(p_1.ToByteArray()[temp_len], 2).Length;
            
            L89 l89 = new L89();
            Random gen_len = new Random();
          
            for (int i=0; i<k; i++)
            {                                
                BigInteger x = RandomBig1(l89,len );
                if(x==0)
                {
                    x = RandomBig1(l89, len) ;
                }
                if (x%2 == 0)
                {
                    x += 1;
                }
                //Console.WriteLine(x);
                
                if (BigInteger.GreatestCommonDivisor(p, x) > 1)
                {
                    return false;
                }
                
                BigInteger xr = BigInteger.ModPow(x, d, p);
                if (xr == 1||xr == p-1)
                    super_simple = true;
                
                else
                {              
                    for (int r = 1; r < s; r++)
                    {
                        xr = BigInteger.ModPow(xr, 2, p);
                        if (xr == p-1)
                        {
                            super_simple = true;
                            break;
                        }
                        if (xr == 1)
                        {
                            return false;
                        }                  
                    }
                    if (super_simple == false)
                        return false;
                }      
            }
            return true;
            
        }
        public static BigInteger  RandomSimple(int len )
        {
            L89 l89 = new L89();
            BigInteger x = RandomBig(l89, len);            
            if (x % 2 == 0)
                x++;
            byte flag = 0;
            BigInteger max = BigInteger.Pow(2, len);
            int i = 1;
            while (MillerRabin(x, 1000) == false)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(x.ToString("X"));
                x += 2 ;                
                i++;
                if (i == max / 2)
                    return 0;
                //Console.WriteLine(x + " " + x / 4 + " " + i);
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(x.ToString("X"));
            return x;
        }
        public static BigInteger GoodBigSimple(int len)
        {
            BigInteger ps = RandomSimple(len);
            //Console.WriteLine(ps);
            int i = 1;
            BigInteger p = 2 * ps * i + 1;
            while (MillerRabin(p, 1000) == false)
            {
                i++;
                p = 2 * ps * i + 1;
                                   

            }            
            return p;
        }
        public static int Len (BigInteger num)
        {
            int temp_len = num.ToByteArray().Length - 1;
            int len = temp_len * 8 + Convert.ToString(num.ToByteArray()[temp_len], 2).Length;
            return len;
        }
        public static BigInteger[] GenPairs(int len)
        {
            

            BigInteger p= GoodBigSimple(len);
            BigInteger q = GoodBigSimple(len);
            BigInteger ps = GoodBigSimple(len);
            BigInteger qs = GoodBigSimple(len);

            while (p*q>ps*qs)
            {
                ps = GoodBigSimple(len);
                qs = GoodBigSimple(len);
            }
            BigInteger[] res = new BigInteger[4] { p, q, ps ,qs};
            return res;

        }
        public static BigInteger[] GenPair(int len, BigInteger n)
        {
              
            BigInteger p = GoodBigSimple(len);
            BigInteger q = GoodBigSimple(len);

            while (p * q < n)
            {
                p = GoodBigSimple(len);
                q = GoodBigSimple(len);
            }
            BigInteger[] res = new BigInteger[2] { p, q};
            return res;

        }
        public static BigInteger gcd(BigInteger a, BigInteger b, ref  BigInteger  x, ref BigInteger y)
        {
            if (a==0)
            {
                x = 0;
                y = 1;
                return b;
            }
            BigInteger x1= 1;
            BigInteger y1=1;
            BigInteger d = gcd(b % a, a, ref x1, ref  y1);
            x = y1 - (b / a) * x1;
            y = x1;
            return d; 
        }
        public static BigInteger Reverse(BigInteger e, BigInteger p)
        {

            BigInteger x=1;
            BigInteger y = 1; 
            gcd(e, p, ref x, ref y );
            return BigInteger.ModPow(x+p, 1, p);
        }
    }
}
