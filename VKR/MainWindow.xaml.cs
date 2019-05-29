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

        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                if (e.ClickCount == 2)
                {
                    AdjustWindowSize();
                }
                else
                {
                    Application.Current.MainWindow.DragMove();
                }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// MaximizedButton_Clicked
        /// </summary>
        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            AdjustWindowSize();
        }

        /// <summary>
        /// Minimized Button_Clicked
        /// </summary>
        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// Adjusts the WindowSize to correct parameters when Maximize button is clicked
        /// </summary>
        private void AdjustWindowSize()
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
                MaximizeButton.Content = "1";
            }
            else
            {
                this.WindowState = WindowState.Maximized;
                MaximizeButton.Content = "2";
            }

        }

        TextBox sizeMatrixTb = new TextBox();
            TextBox numberWiresTb = new TextBox();
            TextBox numberColumnsTb = new TextBox();
            TextBox tensorProductTb = new TextBox();
            TextBox vneshProductTb = new TextBox();
            TextBox nameNewMatrixTb = new TextBox();
            TextBox[] matrixTb = new TextBox[0];
            TextBox[] newMatrixTb = new TextBox[4];
            ComboBox[] schemaCb = new ComboBox[0];
            Border b = new Border();
            Grid newOp = new Grid();



        private void MatrixButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteAll();

            Page_Matrix_Drawing();
            MatrixBR.BorderThickness = new Thickness(0, 0, 0, 1);
            MatrixButton.Style = (Style)FindResource("Click");
            BasicElementsBR.BorderThickness = new Thickness(0, 0, 1, 1);
            BasicElementsButton.Style = (Style)FindResource("Button");
            ProductsBR.BorderThickness = new Thickness(0, 0, 1, 1);
            ProductsButton.Style = (Style)FindResource("Button");
            SchemaBR.BorderThickness = new Thickness(0, 0, 1, 1);
            SchemaButton.Style = (Style)FindResource("Button");
            OperatorBR.BorderThickness = new Thickness(0, 0, 1, 1);
            OperatorButton.Style = (Style)FindResource("Button");


            try
            {
                if (tensorProductTb.Text != "") ShowMatrix(Products.VNinMat(tensorProductTb.Text));
                else
                {
                  if (vneshProductTb.Text != "") ShowMatrix(Products.TeninMat(vneshProductTb.Text));
                }

                

               numberWiresTb.Text = "";
                numberColumnsTb.Text = "";
                nameNewMatrixTb.Text = "";
                tensorProductTb.Text = "";
                vneshProductTb.Text = "";
                schemaCb = new ComboBox[0];
                newMatrixTb = new TextBox[0];
            }
            catch { }
        }

        public void ShowMatrix(SparseMatrix matrix)
        {
            sizeMatrixTb.Text = matrix.ColumnCount.ToString();
            
            Console.Write(matrix);
            for (int i = 0; i < matrix.ColumnCount; i++)
            {
                for (int j = 0; j < matrix.ColumnCount; j++)
                {
                    string h = matrix[i, j].ToString();
                    matrixTb[i * matrix.ColumnCount  + j].Text = h;
                }
            }
        }

        private void Page_Matrix_Drawing()
        {
            DeleteAll();

            Label sizeMatrixLb = new Label();
            grid1.Children.Add(sizeMatrixLb);
            grid1.Children.Add(sizeMatrixTb);
             
            sizeMatrixLb.Width = 200;
            sizeMatrixLb.Height = 30;
            sizeMatrixLb.HorizontalAlignment = HorizontalAlignment.Left;
            sizeMatrixLb.VerticalAlignment = VerticalAlignment.Top;
            sizeMatrixLb.Margin = new Thickness(10, 15, 0, 15);
            sizeMatrixLb.Content = "Введите размер матрицы:";
            sizeMatrixTb.Margin = new Thickness(210, 20, 0, 15);

            sizeMatrixTb.TextChanged += MatrixDrawing;
            sizeMatrixTb.TextInput += MatrixDrawing;
        }

        private void MatrixDrawing(object sender, EventArgs e)
        {
            grid2.Children.Clear();
            try
            {
                int sizeMatrix,
                    rightIndent = 190,
                    topIndent = 25,
                    tbTextInt = Convert.ToInt32(sizeMatrixTb.Text);

                sizeMatrix = tbTextInt * tbTextInt;
                if (tbTextInt < 33 && tbTextInt % 2 == 0)
                {
                    matrixTb = new TextBox[sizeMatrix];
                    for (int i = 0; i < sizeMatrix; i++)
                    {
                        matrixTb[i] = new TextBox();
                        grid2.Children.Add(matrixTb[i]);
                        if (i % Convert.ToInt32(sizeMatrixTb.Text) == 0) { topIndent  += 25; rightIndent = 10; }
                        matrixTb[i].Margin = new Thickness(rightIndent, topIndent , 10, 15);
                        rightIndent += 37;
                    } 
                }
            }
            catch { }
        } 
        
        private void SchemaButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteAll();
            PageSchemaDrawing();
            
            MatrixBR.BorderThickness = new Thickness(0, 0, 1, 1);
            ProductsButton.Style = (Style)FindResource("Button");
            BasicElementsBR.BorderThickness = new Thickness(0, 0, 1, 1);
            ProductsButton.Style = (Style)FindResource("Button");
            ProductsBR.BorderThickness = new Thickness(0, 0, 1, 1);
            ProductsButton.Style = (Style)FindResource("Button");
            SchemaBR.BorderThickness = new Thickness(0, 0, 0, 1);
            SchemaButton.Style = (Style)FindResource("Click");
            OperatorBR.BorderThickness = new Thickness(0, 0, 1, 1);
            ProductsButton.Style = (Style)FindResource("Button");



            if (vneshProductTb.Text != "") TeninShem(vneshProductTb.Text);
            tensorProductTb.Text = "";
            vneshProductTb.Text = "";
            matrixTb = new TextBox[0];
            sizeMatrixTb.Text = "";
        }

        private void SchemaDrawing(object sender, EventArgs e)
        {
            grid2.Children.Clear();
            
            try
            {
                int rightIndent = 50,
                    topIndent = 90,
                    numberColumns = Convert.ToInt32(numberWiresTb.Text),
                    numberWires = Convert.ToInt32(numberColumnsTb.Text);

                schemaCb = new ComboBox[numberColumns * numberWires];
                for (int i = 0; i < numberColumns; i++)
                {
                    for (int j = 0; j < numberWires; j++)
                    {
                        Label wireLb = new Label();
                        wireLb.Content = "_____________";
                        wireLb.FontWeight = FontWeights.ExtraBold;
                        wireLb.Margin = new Thickness(rightIndent - 40, topIndent - 5, 10, 15);
                        grid2.Children.Add(wireLb);

                        schemaCb[i * numberWires + j] = new ComboBox();
                        grid2.Children.Add(schemaCb[i * numberWires + j]);
                        schemaCb[i * numberWires + j].Margin = new Thickness(rightIndent, topIndent, 10, 15);
                        schemaCb[i * numberWires + j].PreviewTextInput += InputValidation;
                        rightIndent += 85;
                        

                        for (int q = 0; q < MatrixC.nameGates.Count; q++)
                            schemaCb[i * numberWires + j].Items.Add(MatrixC.nameGates[q]);
                        schemaCb[i * numberWires + j].Name = "c"+ Convert.ToString(i * numberWires + j);
                        for (int q=0;q<numberColumns;q++)
                        {
                            if (q != i) schemaCb[i * numberWires + j].Items.Add("W"+ Convert.ToString(numberColumns-q-1));
                            if (q != i) schemaCb[i * numberWires+ j].Items.Add("V" + Convert.ToString(numberColumns - q - 1));
                        }
                    }
                    Label wireEndLb = new Label();
                    wireEndLb.Content = "_____________";
                    wireEndLb.FontWeight = FontWeights.ExtraBold;

                    wireEndLb.Margin = new Thickness(rightIndent - 40, topIndent - 5, 10, 15);
                    grid2.Children.Add(wireEndLb);
                    topIndent += 65; rightIndent = 50;
                }
             }
            catch { } 
        } 

        private void InputValidation(object sender, TextCompositionEventArgs e)
        {
            ComboBox cbValidation = (ComboBox)sender;
            string regexStr = "[V,W,";

            if (numberWiresTb.Text != "")
            {
                int m = Convert.ToInt32(numberWiresTb.Text);
                for (int q = 0; q < m; q++)
                {
                    if (q != Convert.ToInt32(cbValidation.Name.Substring(1,cbValidation.Name.Length - 1)))
                    {
                        regexStr += Convert.ToString(m - q - 1) + ","; 
                    }
                }
            }
            for (int i = 0; i < MatrixC.nameGates.Count; i++) regexStr += MatrixC.nameGates[i] + ",";
            Regex reg = new Regex(regexStr.Substring(0,regexStr.Length-1)+"]");
            e.Handled = !reg.IsMatch(e.Text.ToUpper());
        }

        public void PageSchemaDrawing()
        {
            Label numberWiresLb = new Label();
            Label numberColumnsLb = new Label();
            
            grid2.Children.Clear();
            grid1.Children.Clear();

            grid1.Children.Add(numberWiresLb);
            grid1.Children.Add(numberColumnsLb);
            grid1.Children.Add(numberWiresTb);
            grid1.Children.Add(numberColumnsTb);
 
            numberWiresLb.Width = 230;
            numberWiresLb.Height = 30;
            numberWiresLb.Margin = new Thickness(10, 15, 10, 15);
            numberWiresLb.Content = "Введите количество проводов:";

            numberWiresTb.Width = 30;
            numberWiresTb.Height = 20;
            numberWiresTb.Margin = new Thickness(250, 20, 10, 15);
            numberWiresTb.TextChanged += SchemaDrawing;
            
            numberColumnsLb.Width = 450;
            numberColumnsLb.Height = 30;
            numberColumnsLb.Margin = new Thickness(10, 40, 10, 15);
            numberColumnsLb.Content = "Введите максимальное количество элементов на проводе:";

            numberColumnsTb.Width = 30;
            numberColumnsTb.Height = 20;
            numberColumnsTb.Margin = new Thickness(450, 45, 10, 15);
            numberColumnsTb.TextChanged += SchemaDrawing;
        }  

        public void NewMatrixDrawing()
        {
            Button addNewMatrixBt = new Button();
            Label newMatrixLb = new Label();
            Label newGateLb = new Label();
            Label nameNewGateLb = new Label();
            b.BorderBrush = new SolidColorBrush(Color.FromRgb(184, 182, 182));
            b.BorderThickness = new Thickness(1,25,1,1);
            b.Width = 200;
            b.Height = 300;
            b.Child = newOp;

            grid2.Children.Add(b);
            newOp.Children.Add(newMatrixLb);
            newOp.Children.Add(newGateLb);
            newOp.Children.Add(nameNewGateLb);
            newOp.Children.Add(nameNewMatrixTb);
            
            b.HorizontalAlignment = HorizontalAlignment.Right;
            b.VerticalAlignment = VerticalAlignment.Top;
            b.Background = new SolidColorBrush(Color.FromRgb(238, 238, 238));
            
            
            b.Margin = new Thickness(0, 30,30,0);


            newGateLb.Width = 200;
            newGateLb.Height = 30;
            newGateLb.Margin = new Thickness(20, 35, 0, 0);
            newGateLb.Content = "Hовый оператор:";

            nameNewGateLb.Width = 100;
            nameNewGateLb.Height = 30;
            nameNewGateLb.Margin = new Thickness(20, 60, 0, 0);
            nameNewGateLb.Content = "Введите имя:";

            newMatrixLb.Width = 170;
            newMatrixLb.Height = 30;
            newMatrixLb.Margin = new Thickness(20, 85, 0, 0);
            newMatrixLb.Content = "Введите матрицу:";

            nameNewMatrixTb.Width = 30;
            nameNewMatrixTb.Height = 20;
            nameNewMatrixTb.Margin = new Thickness(130, 65, 0, 0);
 
            int rightIndent = 110, topIndent = 120;
            newMatrixTb = new TextBox[4];
            for (int i = 0; i < 4; i++)
            {
                newMatrixTb[i] = new TextBox();
                newMatrixTb[i].Width = 30;
                newMatrixTb[i].Height = 20;
                newMatrixTb[i].VerticalAlignment = VerticalAlignment.Top;
                newMatrixTb[i].Margin = new Thickness(rightIndent , topIndent, 0, 0);
                newOp.Children.Add(newMatrixTb[i]);
                Grid.SetColumn(newMatrixTb[i], 1);
                rightIndent += 35;
                if (i == 1) { topIndent += 25;  rightIndent = 110; }
            }
            Border b1 = new Border();
            newOp.Children.Add(b1);
            b1.Child = addNewMatrixBt;
            b1.Style = (Style)FindResource("AddBorder");
            addNewMatrixBt.Style = (Style)FindResource("Add");
            addNewMatrixBt.Click += AddNewMatrix_Click;

        } 

        private void AddNewMatrix_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SparseMatrix newGateMatrix = new SparseMatrix(2, 2);
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        newGateMatrix[i, j] = MatrixC.StrinComplex(newMatrixTb[i * 2 + j].Text);
                    }
                }
                MatrixC.arrayGates.Add(nameNewMatrixTb.Text.ToUpper(), newGateMatrix);
                MatrixC.nameGates.Add(nameNewMatrixTb.Text.ToUpper());

                nameNewMatrixTb.Text = "";
                newMatrixTb[0].Text = "";
                newMatrixTb[1].Text = "";
                newMatrixTb[2].Text = "";
                newMatrixTb[3].Text = "";

                var arrayGates = MatrixC.arrayGates.Where(x => x.Value.ColumnCount == 2).ToDictionary(i => i.Key, i => i.Value);
                DrawingElements(arrayGates);

                NewMatrixDrawing();
            }
            catch { }
            
        }

        private void ProductsButton_Click(object sender, RoutedEventArgs e)
        {
            MatrixBR.BorderThickness = new Thickness(0, 0, 1, 1);
            MatrixButton.Style = (Style)FindResource("Button");
            BasicElementsBR.BorderThickness = new Thickness(0, 0, 1, 1);
            BasicElementsButton.Style = (Style)FindResource("Button");
            ProductsBR.BorderThickness = new Thickness(0, 0, 0, 1);
            ProductsButton.Style = (Style)FindResource("Click");
            SchemaBR.BorderThickness = new Thickness(0, 0, 1, 1);
            SchemaButton.Style = (Style)FindResource("Button");
            OperatorBR.BorderThickness = new Thickness(0, 0, 1, 1);
            OperatorButton.Style = (Style)FindResource("Button");


            try
            {
                if (schemaCb.Length != 0 && numberWiresTb.Text != "" && numberColumnsTb.Text != "")
                    vneshProductTb.Text = Schema.PtoTen(Convert.ToInt32(numberWiresTb.Text), Convert.ToInt32(numberColumnsTb.Text),schemaCb);

                if (matrixTb.Length != 0)
                {
                    tensorProductTb.Text = MatrixC.MatinVn(MatrixC.GetMatrix(matrixTb, Convert.ToInt32(sizeMatrixTb.Text)));
                    vneshProductTb.Text = Products.twoMatinTen(MatrixC.MatrixTwoLevel(MatrixC.GetMatrix(matrixTb, Convert.ToInt32(sizeMatrixTb.Text))));


                }


                sizeMatrixTb.Text = "";
                numberColumnsTb.Text = "";
                numberWiresTb.Text = "";
                nameNewMatrixTb.Text = "";

                matrixTb = new TextBox[0];
                schemaCb = new ComboBox[0];
                newMatrixTb = new TextBox[0];
            DeleteAll();
            ProductsDrawing();
            }
            catch(Exception ex) { MessageBox.Show("Проверьте правильность введенных данных"); Console.WriteLine(ex.StackTrace); }
        }

        public void ProductsDrawing()
        {
            Label vneshLb = new Label();
            Label tensorLb = new Label();

            grid1.Children.Add(vneshLb);
            grid2.Children.Add(tensorLb);
            grid1.Children.Add(tensorProductTb);
            grid2.Children.Add(vneshProductTb);

            vneshLb.Width = 300;
            vneshLb.Height = 30;
            vneshLb.Margin = new Thickness(10, 5, 0, 15);
            vneshLb.Content = "Введите внешнее произведениие:";

            tensorLb.Width = 300;
            tensorLb.Height = 30;
            tensorLb.Margin = new Thickness(10, 155, 10, 15);
            tensorLb.Content = "Введите операторное представление:";

            tensorProductTb.FontWeight = FontWeights.Bold;
            tensorProductTb.Width = 630;
            tensorProductTb.Height = 30;
            tensorProductTb.Margin = new Thickness(10, 40, 0, 15);

            vneshProductTb.Width = 630;
            vneshProductTb.Height = 30;
            vneshProductTb.Margin = new Thickness(10, 190, 0, 15);
        } 

        private void BasicElements_Click(object sender, RoutedEventArgs e)
        {
            MatrixBR.BorderThickness = new Thickness(0, 0, 1, 1);
            MatrixButton.Style = (Style)FindResource("Button");
            BasicElementsBR.BorderThickness = new Thickness(0, 0, 0, 1);
            BasicElementsButton.Style = (Style)FindResource("Click");
            ProductsBR.BorderThickness = new Thickness(0, 0, 1, 1);
            ProductsButton.Style = (Style)ProductsButton.FindResource("Button");
            SchemaBR.BorderThickness = new Thickness(0, 0, 1, 1);
            SchemaButton.Style = (Style)FindResource("Button");
            OperatorBR.BorderThickness = new Thickness(0, 0, 1, 1);
            OperatorButton.Style = (Style)FindResource("Button");


            var arrayGates = MatrixC.arrayGates.Where(x => x.Value.ColumnCount != 2).ToDictionary(i => i.Key, i => i.Value);
            DrawingElements(arrayGates);
        }

        private void DeleteAll()
        {
            grid1.Children.Clear();
            grid2.Children.Clear();
            b.Child = null;
            newOp.Children.Clear();

        }

        private void DeleteAll_Clic(object sender, RoutedEventArgs e)
        {
            DeleteAll();
        }

        private void Operator_Click(object sender, RoutedEventArgs e)
        {
            MatrixBR.BorderThickness = new Thickness(0, 0, 1, 1);
            MatrixButton.Style = (Style)FindResource("Button");
            BasicElementsBR.BorderThickness = new Thickness(0, 0, 1, 1);
            BasicElementsButton.Style = (Style)FindResource("Button");
            ProductsBR.BorderThickness = new Thickness(0, 0, 1, 1);
            ProductsButton.Style = (Style)FindResource("Button");
            SchemaBR.BorderThickness = new Thickness(0, 0, 1, 1);
            SchemaButton.Style = (Style)FindResource("Button");
            OperatorBR.BorderThickness = new Thickness(0, 0, 0, 1);
            OperatorButton.Style = (Style)FindResource("Click");

            var arrayGates = MatrixC.arrayGates.Where(x => x.Value.ColumnCount == 2).ToDictionary(i => i.Key, i => i.Value);
            DrawingElements(arrayGates);

            NewMatrixDrawing();
        }

        public void DrawingElements(Dictionary<string, SparseMatrix> arrayGates)
        {
            int x = (int)grid2.Width;
            List<string> name = arrayGates.Keys.ToList();
            DeleteAll();
            int k = 20, n = 30;
            for (int i = 0; i < name.Count; i++)
            {
                Label l = new Label();
                Label l0 = new Label();

                grid2.Children.Add(l);
                grid2.Children.Add(l0);

                l.Content = name[i];
                l0.Content = "=";

                l.Margin = new Thickness(k, n + 7, 0, 0);
                l0.Margin = new Thickness(k + 45, n + 10, 0, 0);
                l.FontSize = 25;
                l0.FontSize = 20;

                SparseMatrix m1 = MatrixC.arrayGates[name[i]];
                int columnCount = m1.ColumnCount;
                int m = k;
                for (int j = 0; j < columnCount; j++)
                {
                    for (int q = 0; q < columnCount; q++)
                    {
                        m += 80;
                        Label newLb = new Label();
                        grid2.Children.Add(newLb);
                        newLb.HorizontalContentAlignment = HorizontalAlignment.Center;
                        newLb.FontSize = 12;
                        newLb.Height = 35;
                        newLb.Width = 80;
                        newLb.Margin = new Thickness(m, n, 0, 0);
                        newLb.Content = "(" + Math.Round(arrayGates[name[i]][j, q].Real, 2) + ","
                                            + Math.Round(arrayGates[name[i]][j, q].Imaginary, 2) + ")";
                    }
                    n += 25;
                    m = k;
                }

                n = n - 25 * columnCount;
                k += 120 * columnCount;
                if (k + 60 + 80 * arrayGates[name[i]].ColumnCount > 700) { n += 50 * columnCount; k = 20; }
            }
        }

        private void TeninShem(string ten)
        {
            int numberWires = 0, numberColumns = 0;
            List<List<string>> product = Products.TeninLst(ten);
            for (int i = 0; i < product.Count; i++)
            {
                if (product[i].Count != 1 || MatrixC.arrayGates[product[i][0]].ColumnCount == 2)
                {
                    numberWires = product[i].Count;
                    numberColumns++;
                }
                else
                {
                    string start = Convert.ToString(Convert.ToInt32(Products.lol(product[i][0])[0]), 2);
                    string finish = Convert.ToString(Convert.ToInt32(Products.lol(product[i][0])[1]), 2);
                    int zero = finish.Length - start.Length;
                    string m = start.PadLeft(zero+1, '0');
                    List<string> lst = Products.Grey(start.PadLeft(zero, '0'), finish);
                    numberWires = (int)Math.Pow(2, lst[0].Length);
                    numberColumns = lst.Count*2 - 3;
                }
                
            }
            numberColumnsTb.Text = numberColumns.ToString();
            numberWiresTb.Text = numberWires.ToString();
            int k = 0; 
            for (int i = 0; i < product.Count; i++)
            {
                if (product[i].Count != 1 || MatrixC.arrayGates[product[i][0]].ColumnCount == 2)
                {
                    for (int j = 0; j < product[i].Count; j++)
                    {
                        schemaCb[j*numberColumns+i].Text = product[i][j];
                        k++;
                    }
                }
                else
                {

                }
            }
        }

        ////////////////////////////////////////////
    }
}
