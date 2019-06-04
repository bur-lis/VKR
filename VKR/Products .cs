using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra.Complex;
using System.Numerics;
namespace VKR
{
    static class Products
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
                Console.Write(mat[0]);
                Console.Write(MatrixC.arrayGates["U0"]);
                bool b = (MatrixC.arrayGates["U0"].Equals(mat[0]));
                for (int j = 0; j< MatrixC.arrayGates.Count; j++)
                    if(MatrixC.arrayGates.ElementAt(j).Value.Equals(mat[i]))
                    {
                        resultTen += MatrixC.arrayGates.ElementAt(j).Key + "*";
                    }

            }
            if (resultTen == "" && mat.Count == 1 && mat[0].Equals(SparseMatrix.CreateDiagonal(mat[0].ColumnCount, mat[0].ColumnCount, 1)))
            {
                for(int i = 0; i< (int)(Math.Log(mat[0].ColumnCount)/Math.Log(2.0));i++)
                {
                    resultTen += "I" + Convert.ToChar(8855);
                }
            }
            return resultTen.Substring(0,resultTen.Length -1);
        }

        public static SparseMatrix TeninMat(string ten)
        {
            List<SparseMatrix> mat = new List<SparseMatrix>();
            SparseMatrix m;
            List<List<string>> product = TeninLst(ten);
            for (int i = 0; i< product.Count; i++)
            {
                for (int j = 0; j< product[i].Count; j++)
                {
                    if (j == 0)
                    {
                        Console.Write(MatrixC.arrayGates[product[i][j]]);
                        mat.Add(MatrixC.arrayGates[product[i][j]]);
                    }
                        
                    else
                        mat[i] = (SparseMatrix)mat[i].KroneckerProduct(MatrixC.arrayGates[product[i][j]]);
                }
            }
            if (mat.Count > 1) m = mat.Aggregate((x, y) => x * y);
            else m = mat[0];
            return m;
        }

        public static List<string> Grey(string startStr, string resultStr)
        {

            List<string> resultList = new List<string>();
            resultList.Add(startStr);
            for (int i = 0; i < startStr.Length; i++)
            {
                if (startStr[i] != resultStr[i])
                {
                    startStr = startStr.Remove(i, 1).Insert(i, resultStr[i].ToString());
                    resultList.Add(startStr);
                }
            }
            return resultList;
        }

        public static List<List<string>> TeninLst (string ten)
        {
            List<List<string>> product = new List<List<string>>();
            List<string> tenproduct = new List<string>();
            string s = "";
            for (int i = 0; i < ten.Length; i++)
            {
                if (ten[i] == '+' || ten[i] == Convert.ToChar(8855))
                {
                    tenproduct.Add(s);
                    s = "";
                }
                else
                {
                    if (ten[i] == '*')
                    {
                        tenproduct.Add(s);
                        s = "";
                        product.Add(tenproduct.ToList());
                        tenproduct.RemoveRange(0, tenproduct.Count);
                    }
                    else s += ten[i];

                    if (i == ten.Length - 1)
                    {
                        tenproduct.Add(s);
                        product.Add(tenproduct);
                    }
                }
            }
            return product;
        }



        public static string[] lol(string v)
        {
            string[] rezult = new string[3];
            SparseMatrix m = MatrixC.arrayGates[v];
            SparseMatrix mat = new SparseMatrix(2, 2);
            for (int i = 0; i < m.ColumnCount; i++)
            {
                for (int j = 0; j < m.ColumnCount; j++)
                {
                    if (j != i && m[i, j] != 0)
                    {
                        if (rezult[0] == null)
                        {
                            mat[0, 0] = m[j, j];
                            mat[0, 1] = m[i, j];
                            rezult[0] = i.ToString();
                        }
                        else
                        {
                            mat[1, 0] = m[i, j];
                            mat[1, 1] = m[i, i];
                            rezult[1] = i.ToString();
                        }
                    }
                }
            }
            Console.Write(m);

            int l = 0;
            Console.Write( "ksdhfklsdgh");
            Console.Write(rezult[1] + "!!!!!!!!!!!!!1");
            if (MatrixC.arrayGates.ContainsValue(mat))
            {
                //Console.Write(MatrixC.arrayGates["U1"]);
                //Console.Write(mat);
                while (rezult[2] == null)
                {
                    if (MatrixC.arrayGates.ElementAt(l).Value.Equals(mat))
                        rezult[2] = MatrixC.arrayGates.ElementAt(l).Key;
                    else l++;
                }
            }
            else
            {
                for (int j = 0; l < 1; j++)
                {
                    if (!MatrixC.arrayGates.ContainsKey("U" + j.ToString()))
                    {
                        MatrixC.arrayGates.Add("U" + j.ToString(), mat);
                        MatrixC.nameGates.Add("U" + j.ToString());
                        rezult[2] = "U" + j.ToString();
                        l++;
                    }
                }
            }
            
            return rezult;
        }
    }
}
