﻿using ME.Wpf.Mvvm;
using System.Windows;

namespace IRC_Helpdesk_Message_Composer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IDialog
    {
        public MainWindow()
        {
            InitializeComponent();
            //this.DataContext = this;
        }
    }
}
