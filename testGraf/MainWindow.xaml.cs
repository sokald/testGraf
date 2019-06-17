using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using OxyPlot.Wpf;

namespace testGraf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModels.MainWindowModel viewModel;

        //varibles for calculate result
        int a, b, c;

        //varibles for set time refresh
        int time = 2000;

        public MainWindow()
        {
            viewModel = new ViewModels.MainWindowModel();
            DataContext = viewModel;
            
            InitializeComponent();
            
            // loop for calculate multiplication
            try
            {
                var i = loop();
            }
            catch (Exception e)
            {
                MessageBox.Show("balad dzialania prgramu " + e);
            }
        }

        // loop for calculate and show result
        public async Task<int> loop()
        {

            //object for random number
            Random rnd = new Random();
            int i = 5;

            while (true)
            {
                //code for calculate and show result
                try
                {
                    a = Int32.Parse(aTextBox.Text);
                    b = Int32.Parse(bTextBox.Text);
                    c = await calculate(a, b);
                    //progressBar.Value = c;
                    resultLabel.Content = "= " + c.ToString() + "+" + rnd.NextDouble();

                    //add new number in plot       
                    i += 5;
                    viewModel.add(new OxyPlot.DataPoint(i, c));
                    Plot1.RefreshPlot(true);
                }
                catch (Exception e)
                {
                    MessageBox.Show("blad dzialanai programu" + e);
                }
            }
            return 0;
        }  

        // button for new number w plot
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            viewModel.add();
            Plot1.RefreshPlot(true);
        }

        //button to set time refresh
        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            time = Int32.Parse(setTimeLabler.Text);
        }

        // calculate random number
        public async Task<int> calculate(int a, int b)
        {
            return await Task.Run(() =>
            {
                Thread.Sleep(time);
                return a * b;
            });
        }
    }
}
