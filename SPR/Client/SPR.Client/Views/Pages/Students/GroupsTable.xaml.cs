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

namespace SPR.Client.Views.Pages.Students
{
    /// <summary>
    /// Interaction logic for GroupsTable.xaml
    /// </summary>
    public partial class GroupsTable : UserControl
    {
        public GroupsTable()
        {
            InitializeComponent();
        }

        private void SelectItemClick(object sender, RoutedEventArgs e)
        {
            var buttonElement = (Button)sender;
            listView.SelectedItem = buttonElement.DataContext;
        }
    }
}
