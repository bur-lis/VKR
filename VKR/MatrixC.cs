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

        public static SparseMatrix NewU(int v, Dictionary<int, List<string>> yp)
        {
            SparseMatrix matrix = new SparseMatrix((int)Math.Pow(2, v), (int)Math.Pow(2, v));
            string[] mas = new string[matrix.ColumnCount];

            for (int i = 0; i < matrix.ColumnCount; i++)
                
            mas[i] = Convert.ToString(i, 2).PadLeft((int)Math.Log(matrix.ColumnCount, 2), '0');

            return matrix;
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
