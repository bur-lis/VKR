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

        public static string twoMatinTen(List<SparseMatrix> mat)
        {
            string resultTen = "";
            for (int i  = 0; i< mat.Count; i++)
            {
                for(int j = 0; j< MatrixC.arrayGates.Count; j++)
                    if(MatrixC.arrayGates.ElementAt(j).Value == mat[i])
                    {
                        resultTen += MatrixC.arrayGates.ElementAt(j).Key + "*";
                    }

            }
            return resultTen.Substring(0,resultTen.Length -1);
        }

        public static SparseMatrix TeninMat(string ten)
        {
            List<List<string>> product = new List<List<string>>();
            List<string> tenproduct = new List<string>();
            List<SparseMatrix> mat = new List<SparseMatrix>();
            SparseMatrix m;
            string s="";
            for (int i = 0; i< ten.Length; i++)
            {
                if (ten[i] == '+' || ten[i] == Convert.ToChar(8853))
                {
                    tenproduct.Add(s);
                    s = "";
                }
                else
                {
                    if (ten[i] == '*')
                    {
                        if (tenproduct.Count == 0)
                        {
                            tenproduct.Add(s);
                            tenproduct.RemoveRange(0, tenproduct.Count);
                            s = "";
                        }
                        product.Add(tenproduct);
                        tenproduct.RemoveRange(0, tenproduct.Count);
                    }
                    else s += ten[i];

                    if(i == ten.Length - 1)
                    {
                        tenproduct.Add(s);
                        product.Add(tenproduct);
                    }
                }
            }
            for (int i = 0; i< product.Count; i++)
            {
                for (int j = 0; j< product[i].Count; j++)
                {
                    if (j == 0)
                        mat.Add(MatrixC.arrayGates[product[i][j]]);
                    else
                        mat[i] = (SparseMatrix)mat[i].KroneckerProduct(MatrixC.arrayGates[product[i][j]]);
                }
            }
            if (mat.Count > 1) m = mat.Aggregate((x, y) => x * y);
            else m = mat[0];
            return m;
        }
    }
}
