using GrammarToParseTable.Classes;
using GrammarToParseTable.Grammer;
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

        List<Rule> rules = new List<Rule>();

        /// <summary>
        /// Adds new rule to datagrid
        /// </summary>
        /// <param name="r"></param>
        void bindDataGrid(List<Rule> rs)
        {
            DataGrid_Production.Items.Clear();
            int i = 1;
            foreach (Rule rule in rs)
                DataGrid_Production.Items.Add(new production() { Number = i++, Production = rule.ToString() });
        }

        /// <summary>
        /// Clears the datagrid and adds rules to it
        /// </summary>
        /// <param name="rs">List of rules</param>
        void bindDataGrid_simplyfied(List<Rule> rs)
        {
            DataGrid_SimplifiedProduction.Items.Clear();
            int i = 1;
            foreach (Rule rule in rs)
                DataGrid_SimplifiedProduction.Items.Add(new production() { Number = i++, Production = rule.ToString() });
        }

        /// <summary>
        /// This class is only for UI element: Datagrid - nothing else
        /// </summary>
        class production
        {
            public int Number { get; set; }
            public String Production { get; set; }
        }

        /// <summary>
        /// No multiple characters in left side
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_GrammarLeft_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBox_GrammarLeft.Text != "")
            {
                TextBox_GrammarLeft.Text = TextBox_GrammarLeft.Text[0].ToString();
                TextBox_GrammarLeft.CaretIndex = TextBox_GrammarLeft.Text.Length;
                TextBox_GrammarRights.Focus();
            }
        }

        /// <summary>
        /// Add new rule to rules and update datagrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_AddNewGrammar_Click(object sender, RoutedEventArgs e)
        {
            // Check if to input the sample grammar
            if (TextBox_GrammarRights.Text.ToLower().Contains("sample") && TextBox_GrammarLeft.Text == "")
            {
                // Insert the sample grammar
                dataGrid_FiFoTable.Items.Clear();
                int i = TextBox_GrammarRights.Text.Contains("1") ? 1 : 0;
                rules = new Sample(i).rules;
                bindDataGrid(rules);
                TextBox_GrammarRights.Text = "";
                Button_GenerateParseTable.Focus();
            }
            else
            {
                Nonterminal left = null;
                List<List<Symbol>> rights = new List<List<Symbol>>();

                try
                {
                    // Handeling LeftSide
                    if (TextBox_GrammarLeft.Text != "")
                        if (TextBox_GrammarLeft.Text.ToCharArray().Length > 1)
                            throw new Exception("Left value cannot have more than one symbol.");
                        else
                            left = new Nonterminal(TextBox_GrammarLeft.Text.ToCharArray()[0]);
                    else
                        throw new Exception("Value cannot be null.");

                    // Handeling RightSide
                    if (TextBox_GrammarRights.Text != "")
                    {
                        List<Symbol> symbols = new List<Symbol>();

                        String[] vars = TextBox_GrammarRights.Text.Replace(" ", string.Empty).Replace("\t", string.Empty).Split('|');
                        foreach (String var in vars)
                        {
                            if (var != "")
                            {
                                foreach (char symbol in var)
                                {
                                    if (Char.IsUpper(symbol))
                                        symbols.Add(new Nonterminal(symbol));
                                    else
                                        symbols.Add(new Terminal(symbol));
                                }
                                rights.Add(symbols);
                                symbols = new List<Symbol>();
                            }
                        }
                    }
                    else
                        throw new Exception("Value cannot be null.");

                    Rule r = new Rule(left, rights);
                    // apply a simple duplex checking filte
                    if (!doesContains(r, rules))
                    {
                        rules.Add(r);
                        bindDataGrid(rules);
                    }

                    TextBox_GrammarLeft.Text = "";
                    TextBox_GrammarRights.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// On the right side text box press enter to add the rule
        /// And also focus to left text box then
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_GrammarRights_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Button_AddNewGrammar_Click(null, null);
                TextBox_GrammarLeft.Focus();
            }
        }

        /// <summary>
        /// Simply just check the rules if they look same
        /// </summary>
        /// <param name="r"></param>
        /// <param name="rs"></param>
        /// <returns></returns>
        private bool doesContains(Rule r, List<Rule> rs)
        {
            foreach (Rule rule in rs)
            {
                if (rule.ToString() == r.ToString())
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Adds an 'epsilon' character to grammer right side
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_AddEpsilonToGrammar_Click(object sender, RoutedEventArgs e)
        {
            TextBox_GrammarRights.Text += " ε ";
            TextBox_GrammarRights.CaretIndex = TextBox_GrammarRights.Text.Length;
        }

        /// <summary>
        /// Adds an 'or' character to grammer right side
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_AddOrToGrammer_Click(object sender, RoutedEventArgs e)
        {
            TextBox_GrammarRights.Text += " | ";
            TextBox_GrammarRights.CaretIndex = TextBox_GrammarRights.Text.Length;
        }

        /// <summary>
        /// Simplifies rules
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private List<Rule> Simplify_Rules(List<Rule> rules)
        {
            List<Rule> simplified_Rules = new List<Rule>();

            foreach (Rule rule in rules)
            {
                Nonterminal left = rule.left;
                foreach (List<Symbol> right in rule.right)
                {
                    List<List<Symbol>> r = new List<List<Symbol>>();
                    if (/*Check if rule was not added before*/true)
                    {
                        r.Add(right);
                        Rule newRule = new Rule(left, r);
                        //check if the rule already exists
                        if (!doesContains(newRule, simplified_Rules))
                            simplified_Rules.Add(newRule);
                    }
                }
            }

            bindDataGrid_simplyfied(simplified_Rules);
            return simplified_Rules;
        }

        /// <summary>
        /// Check to delete a rule
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGrid_Production_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            production p = null;
            try
            {
                p = (production)DataGrid_Production.SelectedItem;
                if (e.Key == Key.Delete && p != null)
                {
                    if (MessageBox.Show(
                            "You are about to delete the folloing rule.\n" +
                            p.Production + "\n" +
                            "Do you want to permanently delete it?",
                            "Delete a rule",
                            MessageBoxButton.YesNo,
                            MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        rules.RemoveAt(p.Number - 1);
                        bindDataGrid(rules);
                        DataGrid_SimplifiedProduction.Items.Clear();
                        // REORDER RULES!
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// Generate parse table; calculate firsts and follows
        /// And binds datum with Fio datagrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_GenerateParseTable_Click(object sender, RoutedEventArgs e)
        {
            ParseTable parseTable = new ParseTable(Simplify_Rules(rules));
            //parseTable.Print_Firsts();
            //parseTable.Print_Follows();

            dataGrid_FiFoTable.Items.Clear();

            List<ParseTable.FiFoItem> items = parseTable.getFiFoItems();
            foreach (ParseTable.FiFoItem item in items)
                dataGrid_FiFoTable.Items.Add(item);

            
        }
    }
}
