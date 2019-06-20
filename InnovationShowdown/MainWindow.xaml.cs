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

namespace InnovationShowdown
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            singleKnock.Visibility = Visibility.Hidden;
            doubleKnock.Visibility = Visibility.Hidden;
            heldKnock.Visibility = Visibility.Hidden;

            string[] args = Environment.GetCommandLineArgs();

            if (args.Length == 2)
            {
                switch (args[1])
                {
                    case "1":
                        singleKnock.Visibility = Visibility.Visible;
                        singleKnock.Text = "";
                        break;
                    case "2":
                        doubleKnock.Visibility = Visibility.Visible;
                        doubleKnock.Text = "";
                        break;
                    case "3":
                        heldKnock.Visibility = Visibility.Visible;
                        heldKnock.Text = "";
                        break;
                }

                // Make window close
            }
        }
    }
}
