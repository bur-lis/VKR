﻿using System;
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
      
        
        private void MatrixButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteAll();
            Page_Matrix_Drawing();

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
            sizeMatrixLb.Margin = new Thickness(10, 5, 0, 15);
            sizeMatrixLb.Content = "Введите размер матрицы:";
            sizeMatrixTb.Margin = new Thickness(210, 10, 0, 15);

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
                    topIndent = 15,
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

            NewMatrixDrawing();
        }  

        public void NewMatrixDrawing()
        {
            Button addNewMatrixBt = new Button();
            Label newMatrixLb = new Label();
            Label newGateLb = new Label();
            Label nameNewGateLb = new Label();

            grid3.Children.Add(addNewMatrixBt);
            grid3.Children.Add(newMatrixLb);
            grid3.Children.Add(newGateLb);
            grid3.Children.Add(nameNewGateLb);
            grid3.Children.Add(nameNewMatrixTb); 
             
            newGateLb.Width = 200;
            newGateLb.Height = 30;
            newGateLb.Margin = new Thickness(10, 15, 10, 0);
            newGateLb.Content = "Hовый оператор:";

            nameNewGateLb.Width = 100;
            nameNewGateLb.Height = 30;
            nameNewGateLb.Margin = new Thickness(10, 40, 0, 0);
            nameNewGateLb.Content = "Введите имя:";

            newMatrixLb.Width = 170;
            newMatrixLb.Height = 30;
            newMatrixLb.Margin = new Thickness(10, 75, 10, 5);
            newMatrixLb.Content = "Введите матрицу:";

            nameNewMatrixTb.Width = 30;
            nameNewMatrixTb.Height = 20;
            nameNewMatrixTb.HorizontalAlignment = HorizontalAlignment.Center;
            nameNewMatrixTb.Margin = new Thickness(120, 45, 0, 0);
 
            int rightIndent = 110, topIndent = 70;
            newMatrixTb = new TextBox[4];
            for (int i = 0; i < 4; i++)
            {
                newMatrixTb[i] = new TextBox();
                newMatrixTb[i].Width = 30;
                newMatrixTb[i].Height = 20;
                newMatrixTb[i].HorizontalAlignment = HorizontalAlignment.Left;
                newMatrixTb[i].VerticalAlignment = VerticalAlignment.Top;
                newMatrixTb[i].Margin = new Thickness(topIndent, rightIndent, 10, 40);
                grid3.Children.Add(newMatrixTb[i]);
                Grid.SetColumn(newMatrixTb[i], 1);
                topIndent += 35;
                if (i == 1) { rightIndent += 25; topIndent = 70; }
            }

            addNewMatrixBt.Height = 30;
            addNewMatrixBt.Width = 80;
            addNewMatrixBt.Content = "Добавить";
            addNewMatrixBt.HorizontalAlignment = HorizontalAlignment.Center;
            addNewMatrixBt.VerticalAlignment = VerticalAlignment.Top;
            addNewMatrixBt.Margin = new Thickness(0, 170, 0, 0);
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
            }
            catch { }
            
        }

        private void ProductsButton_Click(object sender, RoutedEventArgs e)
        {
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
            catch(Exception ex) { MessageBox.Show("Проверьте правельность введенных данных"); Console.WriteLine(ex.StackTrace); }
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
            DeleteAll();
            int k = 0, n = 0;
            for (int i = 0; i< MatrixC.nameGates.Count; i++)
            {
                Label l = new Label();
                Label l0 = new Label();

                grid2.Children.Add(l);
                grid2.Children.Add(l0);

                l.Content = MatrixC.nameGates[i];
                l0.Content = "=";

                l.Margin  = new Thickness(k, n+ 7, 0, 0);
                l0.Margin = new Thickness(k + 45, n + 10, 0, 0);
                l.FontSize = 25;
                l0.FontSize = 20;

                SparseMatrix m1 = MatrixC.arrayGates[MatrixC.nameGates[i]];
                int columnCount = m1.ColumnCount;
                int m = k;
                for (int j = 0; j< columnCount; j++)
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
                        newLb.Content = "(" + Math.Round(MatrixC.arrayGates[MatrixC.nameGates[i]][j, q].Real, 2) + ","
                                            + Math.Round(MatrixC.arrayGates[MatrixC.nameGates[i]][j, q].Imaginary, 2) + ")";
                    }
                    n += 25;
                    m = k;
                }

                n = n - (int)25 * columnCount;
                k += 120*columnCount;
                if (k + 60 + 80* MatrixC.arrayGates[MatrixC.nameGates[i]].ColumnCount> 640) { n += 50*columnCount; k = 0; }
            }
                
        }
        

        private void DeleteAll()
        {
            grid1.Children.Clear();
            grid2.Children.Clear();
            grid3.Children.Clear();

        }

        private void DeleteAll_Clic(object sender, RoutedEventArgs e)
        {
            DeleteAll();
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
                    List<string> lst = Products.Grey(Products.lol(product[i][0])[0], Products.lol(product[i][0])[0]);
                    numberWires = (int)Math.Pow(2, lst[0].Length);
                    numberColumns = lst.Count - 1;
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
    }
}
