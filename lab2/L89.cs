using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    class L
    {
        public byte[] state { set; get; }
        public byte[] a { set; get; }
        public int n;
        public L(int n, byte[] a)
        {
            this.a = a;
            this.n = n;
            state = new byte[n];
            Random gen = new Random();
            for (int i = 0; i < n; i++)
            {
                state[i] = (byte)gen.Next(0, 2);

            }
            state[gen.Next(0, n)] = 1;

        }
        public byte Next()
        {
            byte next = 0;
            for (int i = 0; i < n; i++)
            {
                next += (byte)(a[i] * state[i]);
                next %= 2;
            }
            byte res = state[0];
            for (int i = 0; i < n - 1; i++)
            {
                state[i] = state[i + 1];
            }
            state[n - 1] = next;
            return res;
        }
        public string Get_state()
        {
            string res = "";
            for (int i = 0; i < n; i++)
            {
                res += state[i].ToString();
            }
            return res;
        }
        public byte Next_byte()
        {
            string res = "";
            for (int i = 0; i < 8; i++)
            {
                res = res + this.Next().ToString();

            }
            
            return Convert.ToByte(res, 2);

        }

        public byte[] nNext_byte(int n)
        {
            byte[] res = new byte[n];
            for (int i = 0; i < n; i++)
            {
                res[i] = this.Next_byte();
            }
            return res;
        }
        public string nNext_bit(int n)
        {
            string res = "";
            for (int i = 0; i < n; i++)
            {
                res =res+ this.Next();
            }
            return res;
        }
    }    
    class L89 : L
    {
        public L89() : base(89, new byte[89])
        {
            for (int i = 0; i < n; i++)
            {
                if (i == 1 || i == 52)
                    a[i] = 1;
                else a[i] = 0;


            }

        }
    }
    

}


