using MathNet.Numerics.LinearAlgebra.Complex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace VKR
{
    static class Schema
    {
        public static string PtoTen(int wiresCount, int columnCount, ComboBox[] cbs)
        {
            string[] columns = new string[columnCount];
            string[] wires = new string[wiresCount];
            Dictionary<int,List<string>> controls = new Dictionary<int, List<string>>();
            string columnResult = "";
            int numberGate = 0;

            for (int i = 0; i < cbs.Length; i++) { int k = i % columnCount; columns[k] += cbs[i].Text.ToUpper() + Convert.ToChar(8853); }
            for (int i = 0; i < columns.Length; i++)
            {
                if (columns[i].Contains('W') || columns[i].Contains('V'))
                {
                    foreach (char c in columns[i])
                    {
                        if (c == Convert.ToChar(8853))
                        {
                            if (wires[numberGate].IndexOf('W') != -1 || wires[numberGate].IndexOf('V') != -1)
                            {
                                int numberControlled = Convert.ToInt32(wires[numberGate].Substring(1, wires[numberGate].Length - 1));
                                string gateControlled = cbs[i * wiresCount + (wiresCount - 1 - numberControlled)].Text;
                                string controlling = wires[numberGate].Substring(0, 1) + Convert.ToString(wiresCount - numberGate - 1);

                            if (controls.ContainsKey(numberControlled)) controls[numberControlled].Add(controlling);
                                else controls.Add(numberControlled, new List<string> {gateControlled,controlling });
                                columnResult += "I" + Convert.ToChar(8853);
                            }
                            else columnResult += wires[numberGate] + Convert.ToChar(8853);
                            numberGate++;
                        }
                        else wires[numberGate] += c;
                    }
                    foreach (int e in controls.Keys)
                    {
                        columnResult = columnResult.Remove((wiresCount - e - 1)*2, 1);
                        columnResult = columnResult.Insert((wiresCount - e- 1)*2 , "I");
                    }
                    int l = 0;
                    for (int j = 0; l < controls.Count; j++)
                    {
                        if (!MatrixC.arrayGates.ContainsKey("U" + j.ToString()))
                        {
                            columnResult = columnResult.Substring(0, columnResult.Length - 1) + "*" + "U" + j.ToString() + "*";
                            MatrixC.arrayGates.Add("U" + j.ToString(), MatrixC.NewU(wiresCount, controls.ElementAt(l).Key, controls[controls.ElementAt(l).Key]));
                            MatrixC.nameGates.Add("U" + j.ToString());
                            SparseMatrix mm = MatrixC.arrayGates["U0"];
                            l++;
                        }
                    }
                    columns[i] = columnResult;
                }
                else columns[i] = columns[i].Substring(0, columns[i].Length - 1) + "*";
            }
            string result = "";
            foreach (string column in columns) result += column;
            return result.Substring(0, result.Length - 1); 
        }
    }
}
