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
        public static List<string> nameGates = new List<string> { "X", "Y", "Z", "H", "T" };

        public static Dictionary<string, SparseMatrix> arrayGates = new Dictionary<string, SparseMatrix>()
        { {"I", SparseMatrix.OfArray(new Complex[,] { { 1, 0 }, { 0, 1 } }) },
          {"X", SparseMatrix.OfArray(new Complex[,] { { 0, 1 }, { 1, 0 } }) },
          {"Y",SparseMatrix.OfArray(new Complex[,] { { 0, new Complex(0, -1) },{ new Complex(0, 1), 0 } }) } ,
          {"Z",SparseMatrix.OfArray(new Complex[,] { { 1, 0 }, {0, -1 } }) },
          {"H",SparseMatrix.OfArray(new Complex[,] { { 1 / Math.Sqrt(2), 1 / Math.Sqrt(2) }, {1 / Math.Sqrt(2), -1 / Math.Sqrt(2) } }) },
          {"T",SparseMatrix.OfArray(new Complex[,] { { 1, 0 },{ 0, Math.Exp((new Complex(0, 1)).Imaginary * (Math.PI / 8)) } })} };

        public static SparseMatrix NewU(int v, int numberYpr, List<string> znach)
        {
            SparseMatrix matrix = new SparseMatrix((int)Math.Pow(2, v), (int)Math.Pow(2, v));

            string[] condition = new string[v - znach.Count + 1],
                     binaryNumbers = MatrixC.ArrayofBinaryNumbers(matrix.ColumnCount);
            string partCondition = "",s;


            for (int i = 0; i < v; i++) partCondition += "*";
            StringBuilder str = new StringBuilder(partCondition);
            for (int i = 1; i < znach.Count; i++)
            {
              if (znach[i].Substring(0, 1) == "W") s = "1"; else s = "0";
                
                str[v - Convert.ToInt32(znach[i].Substring(1, znach[i].Length - 1))-1] = Convert.ToChar(s);
            }
            partCondition = str.ToString();

            List<string> lol = new List<string> { partCondition };

            List<string> lol1 = fun(lol);

            for (int i = 0; i < binaryNumbers.Length; i++)
            {
                var rowMatrix1 = new Complex[matrix.ColumnCount];
                for (int j = 0; j < binaryNumbers[i].Length; j++) rowMatrix1[j] = int.Parse(binaryNumbers[i][j].ToString());
                if (lol1.Contains(binaryNumbers[i])) // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! + посмотреть какой индекс в матрице отвечает за столбец а какой за строку
                {
                    SparseMatrix newColoumn = new SparseMatrix(binaryNumbers[0].Length,1);
                    SparseMatrix zer = new SparseMatrix(binaryNumbers[0].Length, 1);
                    zer[binaryNumbers[0].Length - 1, 0] = 1;

                    if (Convert.ToInt32(binaryNumbers[numberYpr]) == 0) newColoumn[binaryNumbers[0].Length - 1, 0] = 1;
                    else newColoumn[0, 0] = 1;
                    Console.WriteLine(arrayGates[znach[0]]);
                    Console.WriteLine(newColoumn);
                    SparseMatrix rezultColoumn = arrayGates[znach[0]] * newColoumn;

                    Console.WriteLine(rezultColoumn);
                    if (rezultColoumn == zer) { rowMatrix1[numberYpr] = 0; }
                    else {
                        int numberElem = v - numberYpr - 1;
                        rowMatrix1[numberElem + binaryNumbers[i].Length] = rezultColoumn[1, 0];
                    };

                for (int j = 0; j < matrix.ColumnCount / 2; j++) if (j != numberYpr + binaryNumbers[i].Length) rowMatrix1[j + matrix.ColumnCount / 2] = int.Parse(binaryNumbers[i][j].ToString());

                
                }
                else for (int j = 0; j < matrix.ColumnCount / 2; j++) rowMatrix1[j + matrix.ColumnCount / 2] = int.Parse(binaryNumbers[i][j].ToString());
                
                for (int q = 0; q < rowMatrix1.Length; q++) matrix[i, q] = rowMatrix1[q];
            }
            return matrix;
        }

        private static List<string> fun(List<string> lol)
        {
            if (lol.Any(sss => sss.Contains('*')))
            {
                var abc = new List<string>();
                foreach (string s in lol)
                {
                    var abc1 = new StringBuilder(s);
                    var abc2 = new StringBuilder(s);
                    abc1[s.IndexOf('*')] = '0';
                    abc2[s.IndexOf('*')] = '1';
                    abc.Add(abc1.ToString());
                    abc.Add(abc2.ToString());
                }
                return fun(abc);
            }
            else
            {
                return lol;
            }
            throw new NotImplementedException();
        }

        public static string[] ArrayofBinaryNumbers(int size)
        {
            string[] binaryNumbers = new string[size];  
            for (int i = 0; i < size; i++)
                 binaryNumbers[i] = Convert.ToString(i, 2).PadLeft((int)Math.Log(size, 2), '0');
            return binaryNumbers;
        }

        public static SparseMatrix GetMatrix(TextBox[] b, int size)
        {
            SparseMatrix matrix = new SparseMatrix(size, size);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    matrix[i, j] = StrinComplex(b[i * size + j].Text);
                }
            }
            return matrix;
        }

        public static Complex StrinComplex(string sourceStr)
        {
            string realStr = "",  
                   imaginaryStr = "";
            int k = 0;
            double realPart, 
                   imaginaryPart;
            foreach (char c in sourceStr)
            {
                if (c != '(' && c != ')')
                    if (c == ',') k++;
                    else
                    {
                        if (k == 0) realStr += c;
                        else imaginaryStr += c;
                    }
            }
            if (realStr == "") realPart = 0; else realPart = Convert.ToDouble(realStr);
            if (imaginaryStr == "") imaginaryPart = 0; else imaginaryPart = Convert.ToDouble(imaginaryStr);
            return new Complex (realPart, imaginaryPart);
        }

        public static string MatinVn(SparseMatrix matrix)
        {
            int size = matrix.ColumnCount;
            string result = "";
            string[] binaryNumbers = ArrayofBinaryNumbers(size);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                    if (matrix[i, j] != 0) result += matrix[i, j].ToString() + 
                                                     "|" + binaryNumbers[i] + "><" + binaryNumbers[j] + "|" + " + ";
            }
            if (result == "") return "";
            return result.Substring(0, result.Length - 2); ;
        }
    }
}
