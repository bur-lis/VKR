using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra.Complex;
using System.Numerics;
namespace VKR
{
    class Products
    {
        public static SparseMatrix VNinMat(string vnesh)
        {
            string newString = "";
            int k = 0;
            List<string> value = new List<string>();
            List<string> places = new List<string>();
            foreach (char c in vnesh)
            {
                if (c != '+')
                {
                    if (c == '|')
                    {
                        if (k == 0)
                        {
                            k = 1;
                            value.Add(newString);
                            newString = "";
                        }
                        else
                        {
                            k = 0;
                            places.Add(newString);
                            newString = "";
                        }
                    }
                    else newString += c;
                }
            }
            int lengthVN = (int)Math.Pow(2,((places.ElementAt(0)).Length - 2) / 2);
            string[] binaryNumbers = MatrixC.ArrayofBinaryNumbers(lengthVN);

            string place = "";
            SparseMatrix matrix = new SparseMatrix(lengthVN, lengthVN);
            for (int i =0;  i< matrix.ColumnCount; i++)
            {
                for (int j = 0; j < matrix.ColumnCount; j++)
                {
                    place = binaryNumbers[i] + "><" + binaryNumbers[j];
                    int indexPlace = places.IndexOf(place);
                    if (indexPlace == -1) matrix[i,j] = new Complex(0, 0);
                    else matrix[i, j] = MatrixC.StrinComplex(value.ElementAt(indexPlace));
                }
            }
            return matrix;
        }
    }
}
