using GrammarToParseTable.Classes;
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

namespace GrammarToParseTable
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            List<Symbol> r1 = new List<Symbol>();
            r1.Add(new Terminal('b'));
            r1.Add(new Terminal('a'));
            r1.Add(new Terminal('a'));

            List<Symbol> r2 = new List<Symbol>();
            r2.Add(new Terminal('a'));
            r2.Add(new Terminal('b'));
            r2.Add(new Nonterminal('A'));

            List<Symbol> r3 = new List<Symbol>();
            r3.Add(new Terminal('ε'));

            List<List<Symbol>> right = new List<List<Symbol>>();
            right.Add(r1);
            right.Add(r2);
            right.Add(r3);

            Rule r = new Rule(new Nonterminal('S'), right);
        }

        //Theme Button Events
        #region Theme Methods

        private void Button_Window_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Window_Maximize_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
            }
            else
            {
                WindowState = WindowState.Maximized;
            }
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                Border_Window.Margin = new Thickness(0, 0, 0, 0);
                Border_Window.BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
            }
            else
            {
                Border_Window.Margin = new Thickness(5, 5, 5, 5);
                Border_Window.BorderThickness = new Thickness(0, 0, 0, 0);
            }
        }

        private void Button_Window_Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void title_Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
                Button_Window_Maximize_Click(null, null);
            else
                DragMove();
        }
        #endregion
    }
}
