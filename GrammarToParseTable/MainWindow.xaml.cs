﻿using GrammarToParseTable.Classes;
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
        void bindDataGrid(Rule r)
        {
            DataGrid_Production.Items.Add(new production() { Number = rules.Count(), Production = r.ToString() });
        }

        /// <summary>
        /// Clears the datagrid and adds rules to it
        /// </summary>
        /// <param name="rs">List of rules</param>
        void bindDataGrid(List<Rule> rs)
        {
            DataGrid_SimplifiedProduction.Items.Clear();
            int i = 1;
            foreach (Rule rule in rs)
                DataGrid_SimplifiedProduction.Items.Add(new production() { Number = i++, Production = rule.ToString() });
        }

        class production
        {
            public int Number { get; set; }
            public String Production { get; set; }
        }

        /// <summary>
        /// Add new rule to rules and update datagrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_AddNewGrammar_Click(object sender, RoutedEventArgs e)
        {
            Nonterminal left = null;
            List<List<Symbol>> rights = new List<List<Symbol>>();

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
            rules.Add(r);
            bindDataGrid(r);
        }

        private void Button_AddEpsilonToGrammar_Click(object sender, RoutedEventArgs e)
        {
            TextBox_GrammarRights.Text += " ε ";
        }

        private void Button_AddOrToGrammer_Click(object sender, RoutedEventArgs e)
        {
            TextBox_GrammarRights.Text += " | ";
        }

        /// <summary>
        /// Simplyfies rules
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Simpify_Click(object sender, RoutedEventArgs e)
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
                        simplified_Rules.Add(new Rule(left, r));
                    }
                }
            }

            rules.Clear();
            rules = simplified_Rules;
            bindDataGrid(rules);
        }

        private void Button_GenerateParseTable_Click(object sender, RoutedEventArgs e)
        {
            ParseTable parseTable = new ParseTable(rules);
            Console.WriteLine("firsts:");
            foreach (KeyValuePair<Rule, HashSet<Symbol>> entry in parseTable.firsts)
            {
                Console.Write(entry.Key + " : {");
                foreach (Symbol s in entry.Value)
                {
                    Console.Write("{0}", s.character);
                }
                Console.WriteLine("}");
            }           

        }
    }
}
