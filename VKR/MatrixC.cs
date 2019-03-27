using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MathNet.Numerics.LinearAlgebra.Complex;
using System.Numerics;
using System.Windows.Controls;

namespace VKR
{
    class MatrixC
    {
        Complex b = new Complex(0, 3);
        SparseMatrix m1 = new SparseMatrix(1, 3);
        public static List<string> znach = new List<string> { " X", " Y", " Z", " H", " T" };

        public static Dictionary<string, SparseMatrix> mar = new Dictionary<string, SparseMatrix>()
        { {" I", SparseMatrix.OfArray(new Complex[,] { { 1, 0 }, { 0, 1 } }) },
          {" X", SparseMatrix.OfArray(new Complex[,] { { 0, 1 }, { 1, 0 } }) },
          {" Y",SparseMatrix.OfArray(new Complex[,] { { 0, new Complex(0, -1) },{ new Complex(0, 1), 0 } }) } ,
          {" Z",SparseMatrix.OfArray(new Complex[,] { { 1, 0 }, {0, -1 } }) },
          {" H",SparseMatrix.OfArray(new Complex[,] { { 1 / Math.Sqrt(2), 1 / Math.Sqrt(2) }, {1 / Math.Sqrt(2), -1 / Math.Sqrt(2) } }) },
          {" T",SparseMatrix.OfArray(new Complex[,] { { 1, 0 },{ 0, Math.Exp((new Complex(0, 1)).Imaginary * (Math.PI / 8)) } })} };

        public static SparseMatrix GetMatrix(TextBox[] b, int size)
        {
            SparseMatrix m1 = new SparseMatrix(size, size);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    m1[i, j] = new Complex(MtoStr(b[i * size + j].Text)[0],
                                           MtoStr(b[i * size + j].Text)[1]);
                }
            }
            return m1;
        }

        public static double[] MtoStr(string s)
        {
            string c = ""; string m = ""; int k = 0, cc, mm;
            foreach (char l in s)
            {
                if (l != '(' && l != ')')
                    if (l == ',') k++;
                    else
                    {
                        if (k == 0) c += l;
                        else m += l;
                    }
            }
            if (c == "") cc = 0; else cc = Convert.ToInt32(c);
            if (m == "") mm = 0; else mm = Convert.ToInt32(m);
            return new double[] { cc, mm };
        }

        public static string MatinVn(SparseMatrix m1)
        {
            int len = m1.ColumnCount;
            string rez = "";
            string[] mas = new string[len];

            for (int i = 0; i < len; i++)
                mas[i] = Convert.ToString(i, 2).PadLeft((int)Math.Log(len, 2), '0');


            for (int i = 0; i < len; i++)
            {
                for (int j = 0; j < len; j++)
                    if (m1[i, j] != 0) rez += m1[i, j].ToString() + "|" + mas[i] + "><" + mas[j] + "|" + " + ";
            }
            if (rez == "") return "";
            return rez.Substring(0, rez.Length - 2); ;
        }
    }
}
