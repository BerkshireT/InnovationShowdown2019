using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Threading;

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

            //Hide();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += TimerTick;
            timer.Start();
        }

        void TimerTick(object sender, EventArgs e)
        {
            var fs = new FileStream("..\\..\\TEST.txt", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using (var sr = new StreamReader(fs))
            {
                var message = sr.ReadLine();
                this.Dispatcher.Invoke(() =>
                {
                    Show();
                    switch (message)
                    {
                        case "1": // Single Press
                            heldKnock.Text = "Someone has entered the room.";
                            break;
                        case "2": // Hold Press
                            heldKnock.Text = "Someone is waiting for you.";
                            break;
                        case "3": // Double Press
                            heldKnock.Text = "Someone needs to speak with you.";
                            break;
                    }
                });
            }
        }
    }
}
