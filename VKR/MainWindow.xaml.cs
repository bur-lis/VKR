using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MathNet.Numerics.LinearAlgebra.Complex;
using System.Numerics;
using System.Text.RegularExpressions;

namespace VKR
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        { 
            InitializeComponent();
        }
            TextBox t = new TextBox();
            TextBox t1 = new TextBox();
            TextBox t2 = new TextBox();
            TextBox t3 = new TextBox();
            TextBox t4 = new TextBox();
            TextBox t5 = new TextBox();
            TextBox tm = new TextBox();
            TextBox[] b = new TextBox[0];
            TextBox[] mat = new TextBox[4];
            ComboBox[] cb = new ComboBox[0];
      
        Vnesh VN = new Vnesh();
        private void matrixB_Click(object sender, RoutedEventArgs e)
        {
            
            del();
            M1();
            if (t3.Text != "") ShowMat(VN.VNinMat(t3.Text));
            t1.Text = "";
            t2.Text = "";
            tm.Text = "";
            t3.Text = "";
            t4.Text = "";
            t5.Text = "";
            cb = new ComboBox[0];
            mat = new TextBox[0];
        }

        public void ShowMat(SparseMatrix m1)
        {
             t.Text = m1.ColumnCount.ToString();
            for (int i = 0; i<m1.ColumnCount; i++)
            {
                for (int j = 0; j < m1.ColumnCount; j++)
                {
                    string h = m1[i, j].ToString();
                    b[i * m1.ColumnCount  + j].Text = h;
                }
            }
        }
        
        private void M1()
        {
            lol2.Children.Clear();
            lol1.Children.Clear();
            Label l = new Label();
            lol1.Children.Add(l);
            lol1.Children.Add(t);
             
            l.Width = 200;
            l.Height = 30;
            l.HorizontalAlignment = HorizontalAlignment.Left;
            l.VerticalAlignment = VerticalAlignment.Top;
            l.Margin = new Thickness(10, 5, 0, 15);
            l.Content = "Введите размер матрицы:";
            t.Margin = new Thickness(210, 10, 0, 15);

            t.TextChanged += textBox_TextChanged;
            t.TextInput += textBox_TextChanged;
        } // отрисовка
        
        private void textBox_TextChanged(object sender, EventArgs e)
        {
            lol2.Children.Clear();
            int m = 0,
                k = 190,
                l= 15;
            try {  m = Convert.ToInt32(t.Text) * Convert.ToInt32(t.Text); }
            catch { }

            b = new TextBox[m];
            for (int i = 0; i< m; i++)
            {
                b[i] = new TextBox();
                lol2.Children.Add(b[i]);
                if (i % Convert.ToInt32(t.Text) == 0) { l += 25; k = 10; }
                b[i].Margin = new Thickness(k, l, 10, 15);
                k += 37;
            }
        } // отрисовка матрицы
        
        private void patternB_Click(object sender, RoutedEventArgs e)
        {
            del();
            P();
            t3.Text = "";
            t4.Text = "";
            t5.Text = "";
            b = new TextBox[0];
            t.Text = "";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            lol2.Children.Clear();
            int m = 0,
                n = 0,
                k = 50,
                l = 90;
            try
            {
                m = Convert.ToInt32(t1.Text); n = Convert.ToInt32(t2.Text);
                cb = new ComboBox[m * n];
                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        Label lb = new Label();
                        lb.Content = "_____________";
                        lb.FontWeight = FontWeights.ExtraBold;
                        lb.Margin = new Thickness(k - 40, l - 5, 10, 15);
                        lol2.Children.Add(lb);

                        cb[i * n + j] = new ComboBox();
                        lol2.Children.Add(cb[i * n + j]);
                        cb[i * n + j].Margin = new Thickness(k, l, 10, 15);
                        cb[i * n + j].PreviewTextInput += lool;
                        k += 70;
                        

                        for (int q = 0; q < MatrixC.znach.Count; q++) cb[i * n + j].Items.Add(MatrixC.znach[q]);
                        cb[i * n + j].Name = "c"+Convert.ToString(i * n + j);
                        for (int q=0;q<m;q++)
                        {
                            if (q != i) cb[i * n + j].Items.Add("W"+ Convert.ToString(m-q-1));
                            if (q != i) cb[i * n+ j].Items.Add("V" + Convert.ToString(m - q - 1));
                        }
                    }
                    Label lb1 = new Label();
                    lb1.Content = "_____________";
                    lb1.FontWeight = FontWeights.ExtraBold;

                    lb1.Margin = new Thickness(k - 40, l - 5, 10, 15);
                    lol2.Children.Add(lb1);
                    l += 65; k = 50;
                }
             }
            catch { } 
        } // отрисовка схемы

        private void lool(object sender, TextCompositionEventArgs e)
        {
            ComboBox c = (ComboBox)sender;
            string s = "[V,W,";
            string st = c.Name.Substring(1,c.Name.Length - 1);
            if (t1.Text != "")
            {
                int m = Convert.ToInt32(t1.Text);
                for (int q = 0; q < m; q++)
                {
                    if (q != Convert.ToInt32(st)) s += Convert.ToString(m - q - 1) + ","; ;
                }
            }
            for (int i = 0; i < MatrixC.znach.Count; i++) s += MatrixC.znach[i] + ",";
            Regex reg = new Regex(s.Substring(0,s.Length-1)+"]");
            e.Handled = !reg.IsMatch(e.Text.ToUpper());
        }

        public void P()
        {
            Label l1 = new Label();
            Label l2 = new Label();
            
            lol2.Children.Clear();
            lol1.Children.Clear();

            lol1.Children.Add(l1);
            lol1.Children.Add(l2);
            lol1.Children.Add(t1);
            lol1.Children.Add(t2);
 
            l1.Width = 230;
            l1.Height = 30;
            l1.Margin = new Thickness(10, 15, 10, 15);
            l1.Content = "Введите количество проводов:";

            t1.Width = 30;
            t1.Height = 20;
            t1.Margin = new Thickness(250, 20, 10, 15);
            t1.TextChanged += textBox1_TextChanged;
            
            l2.Width = 450;
            l2.Height = 30;
            l2.Margin = new Thickness(10, 40, 10, 15);
            l2.Content = "Введите максимальное количество элементов на проводе:";

            t2.Width = 30;
            t2.Height = 20;
            t2.Margin = new Thickness(450, 45, 10, 15);
            t2.TextChanged += textBox1_TextChanged;

            NewMat();
        }  // отрисовка

        public void NewMat()
        {
            Button b = new Button();
            Label l = new Label();
            Label l3 = new Label();
            Label l4 = new Label();

            lol3.Children.Add(b);
            lol3.Children.Add(l);
            lol3.Children.Add(l3);
            lol3.Children.Add(l4);
            lol3.Children.Add(tm); 
             
            l3.Width = 200;
            l3.Height = 30;
            l3.Margin = new Thickness(10, 15, 10, 0);
            l3.Content = "Hовый оператор:";

            l4.Width = 100;
            l4.Height = 30;
            l4.Margin = new Thickness(10, 40, 0, 0);
            l4.Content = "Введите имя:";

            l.Width = 170;
            l.Height = 30;
            l.Margin = new Thickness(10, 75, 10, 5);
            l.Content = "Введите матрицу:";

            tm.Width = 30;
            tm.Height = 20;
            tm.HorizontalAlignment = HorizontalAlignment.Center;
            tm.Margin = new Thickness(120, 45, 0, 0);
 
            int k = 110, n = 70;
            mat = new TextBox[4];
            for (int i = 0; i < 4; i++)
            {
                mat[i] = new TextBox();
                mat[i].Width = 30;
                mat[i].Height = 20;
                mat[i].HorizontalAlignment = HorizontalAlignment.Left;
                mat[i].VerticalAlignment = VerticalAlignment.Top;
                mat[i].Margin = new Thickness(n, k, 10, 40);
                lol3.Children.Add(mat[i]);
                Grid.SetColumn(mat[i], 1);
                n += 35;
                if (i == 1) { k += 25; n = 70; }
            }

            b.Height = 30;
            b.Width = 80;
            b.Content = "Добавить";
            b.HorizontalAlignment = HorizontalAlignment.Center;
            b.VerticalAlignment = VerticalAlignment.Top;
            b.Margin = new Thickness(0, 170, 0, 0);
            b.Click += B_Click;
        } // отрисовка

        private void B_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SparseMatrix m1 = new SparseMatrix(2, 2);
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        string h = mat[i * 2 + j].Text;
                        double s = MatrixC.MtoStr(mat[i * 2 + j].Text)[0];
                        m1[i, j] = new Complex(MatrixC.MtoStr(mat[i * 2 + j].Text)[0], MatrixC.MtoStr(mat[i * 2 + j].Text)[1]);
                    }
                }
                MatrixC.mar.Add(" "+tm.Text.ToUpper(), m1);
                MatrixC.znach.Add(" "+tm.Text.ToUpper());
            }
            catch { }
            tm.Text = "";
            mat[0].Text = "";
            mat[1].Text = "";
            mat[2].Text = "";
            mat[3].Text = "";
        }

        
        private void vneshMB_Click(object sender, RoutedEventArgs e)
        {
                if (b.Length != 0) t3.Text = MatrixC.MatinVn(MatrixC.GetMatrix(b,Convert.ToInt32(t.Text)));
            try { if (cb.Length != 0 && t1.Text != "" && t2.Text != "") t4.Text = PtoTen(); }
            catch { }
                t.Text = "";
                t1.Text = "";
                t2.Text = "";
                tm.Text = "";
                b = new TextBox[0];
                cb = new ComboBox[0];
                mat = new TextBox[0];
            del();
            Etset();

        }

        public void Etset()
        {
            Label l = new Label();
            Label l1 = new Label();

            lol1.Children.Add(l);
            lol2.Children.Add(l1);
            lol1.Children.Add(t3);
            lol2.Children.Add(t4);

            l.Width = 300;
            l.Height = 30;
            l.Margin = new Thickness(10, 5, 0, 15);
            l.Content = "Введите внешнее произведениие:";

            l1.Width = 300;
            l1.Height = 30;
            l1.Margin = new Thickness(10, 155, 10, 15);
            l1.Content = "Введите тензерное произведениие:";

            t3.FontWeight = FontWeights.Bold;
            t3.Width = 630;
            t3.Height = 30;
            t3.Margin = new Thickness(10, 40, 0, 15);

            t4.Width = 630;
            t4.Height = 30;
            t4.Margin = new Thickness(10, 190, 0, 15);
        } // отрисовка

        private void Del1_Click(object sender, RoutedEventArgs e)
        {
            del();
        }

        public void del()
        {
            lol1.Children.Clear();
            lol2.Children.Clear();
            lol3.Children.Clear();
        }

        private void TenzerMB_Click(object sender, RoutedEventArgs e)
        {
            del();
            int k = 0, n = 60;
            for (int i = 0; i< MatrixC.znach.Count; i++)
            {
                Label l = new Label();
                Label l0 = new Label();
                Label l1 = new Label();
                Label l2 = new Label();
                Label l3 = new Label();
                Label l4 = new Label();

                lol2.Children.Add(l);
                lol2.Children.Add(l0);
                lol2.Children.Add(l1);
                lol2.Children.Add(l2);
                lol2.Children.Add(l3);
                lol2.Children.Add(l4);

                l.Content = MatrixC.znach[i];
                l0.Content = "=";
                SparseMatrix m1 = MatrixC.mar[MatrixC.znach[i]];
                l1.Content = MatrixC.mar[MatrixC.znach[i]][0,0];
                l2.Content = MatrixC.mar[MatrixC.znach[i]][0,1];
                
                l3.Content = MatrixC.mar[MatrixC.znach[i]][1, 0];
                l4.Content = MatrixC.mar[MatrixC.znach[i]][1, 1];

                l.Margin  = new Thickness(k, n, 0, 0);
                l0.Margin = new Thickness(k + 35, n + 10, 0, 0);
                l1.Margin = new Thickness(k + 60, n, 0, 0);
                l2.Margin = new Thickness(k + 115, n, 0, 0);
                l3.Margin = new Thickness(k + 60, n + 25, 0, 0);
                l4.Margin = new Thickness(k + 115, n + 25, 0, 0);

                l.Height = 60;
                l.Width = 50;
                l1.Height = 35;
                l1.Width = 60;
                l2.Height = 35;
                l2.Width = 60;
                l3.Height = 35;
                l3.Width = 60;
                l0.Height = 30;
                l0.Width = 60;
                l4.Height = 35;
                l4.Width = 60;

                l.FontSize = 35;
                l0.FontSize = 20;
                l1.FontSize = 18;
                l2.FontSize = 18;
                l3.FontSize = 18;
                l4.FontSize = 18;

                k += 200;
                if ((i + 1) % 3 == 0) { n += 100; k = 0; }

                

            }
        }

        public string PtoTen()
        {
            string[] colon = new string[Convert.ToInt32(t2.Text)];
            string[] str = new string[Convert.ToInt32(t1.Text)];
            List<List<int>> yp = new List<List<int>>();
            string s1 =""; int m = 0;

            for (int i = 0; i < cb.Length; i++) { int k = i % Convert.ToInt32(t2.Text); colon[k] += cb[i].Text.ToUpper() + Convert.ToChar(8853); }
            for (int i = 0; i< colon.Length; i++)
            {
                if (colon[i].Contains('W') || colon[i].Contains('V'))
                {
                    colon[i] = colon[i].Replace(" ", "");
                    for (int j = 0; j < colon[i].Length; j++)
                    {
                        if (colon[i][j] == Convert.ToChar(8853))
                        {
                            bool g = str[m].IndexOf('W') != -1|| str[m].IndexOf('V') != -1;
                            if (g)
                            {
                                yp.Add(new List<int> {colon[i].Length - j, Convert.ToInt32(str[m].Substring(1, str[m].Length - 1)), Convert.ToInt32(str[m].Substring(0,1))});
                                s1 += "I" + Convert.ToChar(8853);
                                s1 = s1.Remove(Convert.ToInt32(t1.Text) - Convert.ToInt32(str[m].Substring(1, str[m].Length - 1))-1,1);
                                s1 = s1.Insert(Convert.ToInt32(t1.Text) - Convert.ToInt32(str[m].Substring(1, str[m].Length - 1))-1, "I");

                            }
                            else s1 += str[m] + Convert.ToChar(8853);
                          m++;
                        }
                        else str[m] += colon[i][j];

                        
                    }
                    int l = 0;
                    for (int j = 0; l<yp.Count ;j++)
                        {
                          if (!MatrixC.mar.ContainsKey("U" + j.ToString()))
                        {
                            s1 = s1.Substring(0, s1.Length - 1) + "*"+ "U" + j.ToString() + "*";
                            List<List<int>> yp1 = new List<List<int>>();
                            for (int q = 0; q < yp.Count; q++)
                            {
                             
                            }
                            // добавление матрицы в список
                            MatrixC.mar.Add("U" + j.ToString(), NewU(Convert.ToInt32(t1.Text),yp1));
                        }
                        }
                    colon[i] = s1;
                }
                else colon[i] = colon[i].Substring(0, colon[i].Length - 1) + "*";
            }
            string s = "";
            for (int i = 0; i< colon.Length; i++) s += colon[i];
            return s.Substring(0, s.Length - 1);;
        }

        private SparseMatrix NewU(int v, List<List<int>> yp1)
        {
            throw new NotImplementedException();
        }
    }
}
