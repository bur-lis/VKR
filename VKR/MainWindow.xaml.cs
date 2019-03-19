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
        List<string> znach = new List<string> {" X"," Y"," Z"," H"," T"};
        Dictionary<string, SparseMatrix> mar = new Dictionary<string, SparseMatrix>()
        { {" I", SparseMatrix.OfArray(new Complex[,] { { 1, 0 }, { 0, 1 } }) },
          {" X", SparseMatrix.OfArray(new Complex[,] { { 0, 1 }, { 1, 0 } }) },
          {" Y",SparseMatrix.OfArray(new Complex[,] { { 0, new Complex(0, -1) },{ new Complex(0, 1), 0 } }) } ,
          {" Z",SparseMatrix.OfArray(new Complex[,] { { 1, 0 }, {0, -1 } }) },
          {" H",SparseMatrix.OfArray(new Complex[,] { { 1 / Math.Sqrt(2), 1 / Math.Sqrt(2) }, {1 / Math.Sqrt(2), -1 / Math.Sqrt(2) } }) },
          {" T",SparseMatrix.OfArray(new Complex[,] { { 1, 0 },{ 0, Math.Exp((new Complex(0, 1)).Imaginary * (Math.PI / 8)) } })} };

        Vnesh VN = new Vnesh();
        MatrixC Matrix = new MatrixC(); 
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
            Console.WriteLine(m1);
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
            Grid.SetColumn(l, 1);
            Grid.SetColumn(t, 1);

            
            l.Width = 200;
            l.Height = 30;
            l.HorizontalAlignment = HorizontalAlignment.Left;
            l.VerticalAlignment = VerticalAlignment.Top;
            l.Margin = new Thickness(10, 5, 0, 15);
            l.Content = "Введите размер матрицы:";

            t.Margin = new Thickness(210, 10, 0, 15);
            t.TextChanged += textBox_TextChanged;
            t.TextInput += textBox_TextChanged;
        }
        public SparseMatrix GetMatrix()
        {
            SparseMatrix m1 = new SparseMatrix(Convert.ToInt32(t.Text), Convert.ToInt32(t.Text));
            for (int i = 0; i < Convert.ToInt32(t.Text); i++)
            {
                for (int j = 0; j < Convert.ToInt32(t.Text); j++)
                {
                    m1[i, j] = new Complex(MtoStr(b[i * Convert.ToInt32(t.Text)].Text)[0], 
                                           MtoStr(b[i * Convert.ToInt32(t.Text)].Text)[1]);
                }
            }
            return m1;
        }

        public double[] MtoStr (string s)
        {
            string c = ""; string m = ""; int k = 0,cc,mm;
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
            if(c == "") cc = 0; else cc = Convert.ToInt32(c);
            if (m == "") mm = 0; else mm = Convert.ToInt32(m);
            return new double[] { cc, mm };
        }
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
                b[i].FontWeight = FontWeights.Bold;
                k += 37;
            }
        }
        
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
                        cb[i * m + j].PreviewTextInput += lool;
                        k += 70;
                        

                        for (int q = 0; q < znach.Count; q++) cb[i * n + j].Items.Add(znach[q]);
                        cb[i * n + j].IsTextSearchEnabled = true;
                        cb[i * n + j].FontWeight = FontWeights.Bold;
                        cb[i * n + j].FontStyle = FontStyles.Italic;
                        cb[i * n + j].FontSize = 16;
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
        }

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
            for (int i = 0; i < znach.Count; i++) s += znach[i] + ",";
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
            
            lol1.Children.Add(t1);
            Grid.SetColumn(l1, 1);
            Grid.SetColumn(t1, 1);

            l1.Background = Brushes.MintCream;
            l1.Width = 200;
            l1.Height = 30;
            l1.HorizontalAlignment = HorizontalAlignment.Left;
            l1.VerticalAlignment = VerticalAlignment.Top;
            l1.Margin = new Thickness(10, 15, 10, 15);
            l1.Content = "Введите количество проводов:";
            l1.FontWeight = FontWeights.Bold;
            l1.FontStyle = FontStyles.Italic;

            t1.Width = 30;
            t1.Height = 20;
            t1.HorizontalAlignment = HorizontalAlignment.Left;
            t1.VerticalAlignment = VerticalAlignment.Top;
            t1.Margin = new Thickness(210, 20, 10, 15);
            t1.TextAlignment = TextAlignment.Center;
            lol1.Children.Add(l2);
            lol1.Children.Add(t2);
            Grid.SetColumn(l2, 1);
            Grid.SetColumn(t2, 1);
            t1.FontWeight = FontWeights.Bold;
            t2.FontWeight = FontWeights.Bold;

            l2.Background = Brushes.MintCream;
            l2.Width = 400;
            l2.Height = 30;
            l2.HorizontalAlignment = HorizontalAlignment.Left;
            l2.VerticalAlignment = VerticalAlignment.Top;
            l2.Margin = new Thickness(10, 40, 10, 15);
            l2.Content = "Введите максимальное количество элементов на проводе:";
            l2.FontWeight = FontWeights.Bold;
            l2.FontStyle = FontStyles.Italic;

            t2.Width = 30;
            t2.Height = 20;
            t2.HorizontalAlignment = HorizontalAlignment.Left;
            t2.VerticalAlignment = VerticalAlignment.Top;
            t2.Margin = new Thickness(380, 45, 10, 15);
            t2.TextAlignment = TextAlignment.Center;
            t2.TextChanged += textBox1_TextChanged;
            t1.TextChanged += textBox1_TextChanged;

            NewMat();
        }

        public void NewMat()
        {
            Label l3 = new Label();
            Label l4 = new Label();

            lol3.Children.Add(l3);
            lol3.Children.Add(l4);

            l3.Background = Brushes.MintCream;
            l3.Width = 200;
            l3.Height = 30;
            l3.HorizontalAlignment = HorizontalAlignment.Left;
            l3.VerticalAlignment = VerticalAlignment.Top;
            l3.Margin = new Thickness(10, 15, 10, 0);
            l3.Content = "Hовый оператор:";
            l3.FontWeight = FontWeights.Bold;
            l3.FontStyle = FontStyles.Italic;

            l4.Background = Brushes.MintCream;
            l4.Width = 100;
            l4.Height = 30;
            l4.HorizontalAlignment = HorizontalAlignment.Left;
            l4.VerticalAlignment = VerticalAlignment.Top;
            l4.Margin = new Thickness(10, 40, 0, 0);
            l4.Content = "Введите имя:";
            l4.FontWeight = FontWeights.Bold;
            l4.FontStyle = FontStyles.Italic;
            tm.Width = 30;
            tm.Height = 20;
            tm.VerticalAlignment = VerticalAlignment.Top;
            tm.HorizontalAlignment = HorizontalAlignment.Center;
            tm.Margin = new Thickness(120, 45, 0, 0);
            tm.TextAlignment = TextAlignment.Center;
            lol3.Children.Add(tm);

           // tm.PreviewTextInput += lool;

            Label l = new Label();
            lol3.Children.Add(l);
            Grid.SetColumn(l, 1);
            l.Background = Brushes.MintCream;
            l.Width = 170;
            l.Height = 30;
            l.HorizontalAlignment = HorizontalAlignment.Left;
            l.VerticalAlignment = VerticalAlignment.Top;
            l.Margin = new Thickness(10, 75, 10, 5);
            l.Content = "Введите матрицу:";
            l.FontWeight = FontWeights.Bold;
            l.FontStyle = FontStyles.Italic;
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
            Button b = new Button();
            b.HorizontalAlignment = HorizontalAlignment.Left;
            b.VerticalAlignment = VerticalAlignment.Top;
            b.Height = 30;
            b.Width = 80;
            b.Content = "Добавить";
            lol3.Children.Add(b);
            b.HorizontalAlignment = HorizontalAlignment.Center;
            b.Margin = new Thickness(0, 170, 0, 0);
            b.Background = Brushes.Beige;
            b.BorderBrush = Brushes.Gainsboro;
            b.FontWeight = FontWeights.Medium;
            b.Click += B_Click;
        }

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
                        double s = MtoStr(mat[i * 2 + j].Text)[0];
                        m1[i, j] = new Complex(MtoStr(mat[i * 2 + j].Text)[0], MtoStr(mat[i * 2 + j].Text)[1]);
                    }
                }
                mar.Add(" "+tm.Text.ToUpper(), m1);
                znach.Add(" "+tm.Text.ToUpper());
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
                if (b.Length != 0) t3.Text = VN.MatinVn(GetMatrix());
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
            lol1.Children.Add(l);
            lol1.Children.Add(t3);

            Label l1 = new Label();
            lol2.Children.Add(l1);
            lol2.Children.Add(t4);

            l.Background = Brushes.MintCream;
            l.Width = 300;
            l.Height = 30;
            l.HorizontalAlignment = HorizontalAlignment.Left;
            l.VerticalAlignment = VerticalAlignment.Top;
            l.Margin = new Thickness(10, 5, 0, 15);
            l.Content = "Введите внешнее произведениие:";
            l.FontWeight = FontWeights.Bold;
            l.FontStyle = FontStyles.Italic;

            t3.FontWeight = FontWeights.Bold;
            t3.Width = 630;
            t3.Height = 30;
            t3.HorizontalAlignment = HorizontalAlignment.Left;
            t3.VerticalAlignment = VerticalAlignment.Top;
            t3.Margin = new Thickness(10, 40, 0, 15);
            t3.TextAlignment = TextAlignment.Center;

            l1.Background = Brushes.MintCream;
            l1.Width = 300;
            l1.Height = 30;
            l1.HorizontalAlignment = HorizontalAlignment.Left;
            l1.VerticalAlignment = VerticalAlignment.Top;
            l1.Margin = new Thickness(10, 155, 10, 15);
            l1.Content = "Введите тензерное произведениие:";
            l1.FontWeight = FontWeights.Bold;
            l1.FontStyle = FontStyles.Italic;

            t4.FontWeight = FontWeights.Bold;
            t4.Width = 630;
            t4.Height = 30;
            t4.HorizontalAlignment = HorizontalAlignment.Left;
            t4.VerticalAlignment = VerticalAlignment.Top;
            t4.Margin = new Thickness(10, 190, 0, 15);
            t4.TextAlignment = TextAlignment.Center;

        }
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
            for (int i = 0; i< znach.Count; i++)
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

                l.Content = znach[i];
                l0.Content = "=";
                SparseMatrix m1 = mar[znach[i]];
                l1.Content = mar[znach[i]][0,0];
                l2.Content = mar[znach[i]][0,1];
                
                l3.Content = mar[znach[i]][1, 0];
                l4.Content = mar[znach[i]][1, 1];

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

                l.VerticalAlignment = VerticalAlignment.Top;
                l.HorizontalAlignment = HorizontalAlignment.Left;
                l0.VerticalAlignment = VerticalAlignment.Top;
                l0.HorizontalAlignment = HorizontalAlignment.Left;
                l1.VerticalAlignment = VerticalAlignment.Top;
                l1.HorizontalAlignment = HorizontalAlignment.Left;
                l2.VerticalAlignment = VerticalAlignment.Top;
                l2.HorizontalAlignment = HorizontalAlignment.Left;
                l3.VerticalAlignment = VerticalAlignment.Top;
                l3.HorizontalAlignment = HorizontalAlignment.Left;
                l4.VerticalAlignment = VerticalAlignment.Top;
                l4.HorizontalAlignment = HorizontalAlignment.Left;

                l.FontSize = 35;
                l.FontWeight = FontWeights.Bold;
                l0.FontSize = 20;
                l0.FontWeight = FontWeights.Bold;
                l1.FontSize = 18;
                l1.FontWeight = FontWeights.Bold;
                l2.FontSize = 18;
                l2.FontWeight = FontWeights.Bold;
                l3.FontSize = 18;
                l3.FontWeight = FontWeights.Bold;
                l4.FontSize = 18;
                l4.FontWeight = FontWeights.Bold;

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
                          if (!mar.ContainsKey("U" + j.ToString()))
                        {
                            s1 = s1.Substring(0, s1.Length - 1) + "*"+ "U" + j.ToString() + "*";
                            List<List<int>> yp1 = new List<List<int>>();
                            for (int q = 0; q < yp.Count; q++)
                            {
                             
                            }
                            // добавление матрицы в список
                            mar.Add("U" + j.ToString(), NewU(Convert.ToInt32(t1.Text),yp1));
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
