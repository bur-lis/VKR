using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra.Complex;
using System.Numerics;
namespace VKR
{
    class Vnesh
    {
        public SparseMatrix VNinMat(string vnesh)
        {
            string s = "";
            int k = 0;
            List<string> znach = new List<string>();
            List<string> m = new List<string>();
            foreach (char c in vnesh)
            {
                if (c != '+')
                {
                    if (c == '|')
                    {
                        if (k == 0)
                        {
                            k = 1;
                            znach.Add(s);
                            s = "";
                        }
                        else
                        {
                            k = 0;
                            m.Add(s);
                            s = "";
                        }
                    }
                    else s += c;
                }
            }
            int len = (int)Math.Pow(2,((m.ElementAt(0)).Length - 2) / 2);
            string[] mas = new string[len];
            for (int i = 0; i < len; i++)
                mas[i] = Convert.ToString(i, 2).PadLeft((int)Math.Log(len, 2),'0');
   
               string s1 = "";
            SparseMatrix m1 = new SparseMatrix(len, len);
            for (int i =0; i<m1.ColumnCount; i++)
            {
                for (int j = 0; j < m1.ColumnCount; j++)
                {
                    s1 = mas[i] + "><" + mas[j];
                    //int l1 = m.FindIndex(abc => abc == s1);
                    int l1 = m.IndexOf(s1);
                    if (l1 == -1) m1[i,j] = new Complex(0, 0);
                    else
                    {
                        string c = "", mm = ""; k = 0;
                        foreach (char l in znach.ElementAt(l1))
                        {
                            if (l != '(' && l != ')')
                            {
                                if (l == ',') k++;
                                else
                                {
                                    if (k == 0) c += l;
                                    else mm += l;
                                }
                            }
                        }
                        m1[i, j] = new Complex(Convert.ToInt32(c), Convert.ToInt32(mm));
                    }
                }
            }
            return m1;
        }

        public string MatinVn(SparseMatrix m1)
        {
            int      len = m1.ColumnCount;
            string   rez = "";
            string[] mas = new string[len];

            for (int i = 0; i< len; i++)
             mas[i] = Convert.ToString(i, 2).PadLeft((int)Math.Log(len,2), '0');
            
            
            for (int i = 0;i < len; i++)
            {
                for (int j = 0; j < len; j++)
                if (m1[i, j] != 0) rez += m1[i, j].ToString() + "|" + mas[i] + "><" + mas[j] + "|" + " + ";
            }
            if (rez == "") return "";
            return rez.Substring(0, rez.Length - 2); ;
        }
    }
}
