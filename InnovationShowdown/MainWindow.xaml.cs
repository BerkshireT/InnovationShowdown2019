using System;
using System.IO;
using System.Media;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Amazon.SQS;
using Amazon.SQS.Model;

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

            makeTransparent(true);

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(updateMessage);
            timer.Start();
        }


        private async void updateMessage(object sender, EventArgs e)
        {
            int mode = updateUrgency();
            if (mode != 0)
            {
                SystemSounds.Hand.Play();
                makeTransparent(false);
                switch (mode)
                {
                    case 1:
                        knock.Text = "Someone has entered the room";
                        break;
                    case 2:
                        knock.Text = "Someone is waiting for you";
                        break;
                    case 3:
                        knock.Text = "Someone needs to speak with you";
                        break;
                }
                await Task.Delay(6000);
                makeTransparent(true);
            }
        }

        private void makeTransparent(bool transparent)
        {
            if (transparent)
            {
                knock.Opacity = 0;
                title.Opacity = 0;
                AlertIcon.Opacity = 0;
                AlertIcon_Copy.Opacity = 0;
                this.Opacity = 0;
            }
            else
            {
                knock.Opacity = 1;
                title.Opacity = 1;
                AlertIcon.Opacity = 1;
                AlertIcon_Copy.Opacity = 1;
                this.Opacity = 1;
            }
        }

        private int updateUrgency()
        {
            int currentUrgency = 0;
            var accessKey = "key";
            var secretKey = "key";
            var sqsConfig = new AmazonSQSConfig();
            sqsConfig.ServiceURL = "http://sqs.us-east-1.amazonaws.com";
            var sqsClient = new AmazonSQSClient(accessKey, secretKey, sqsConfig);
            var receiveMessageRequest = new ReceiveMessageRequest();
            var queueUrl = "https://sqs.us-east-1.amazonaws.com/275098837840/InnovationShowdownButton5";
            receiveMessageRequest.QueueUrl = queueUrl;
            var receiveMessageResponse = sqsClient.ReceiveMessage(receiveMessageRequest);
            var result = receiveMessageResponse.Messages;
            foreach (var message in result)
            {
                for (int i = 1; i <= 3; i++)
                {
                    if (message.Body.Contains("urgency=" + i))
                    {
                        currentUrgency = i;
                        Console.Write(currentUrgency);
                    }
                }
                var deleteMessageRequest = new DeleteMessageRequest();
                deleteMessageRequest.QueueUrl = queueUrl;
                deleteMessageRequest.ReceiptHandle = message.ReceiptHandle;
                var response = sqsClient.DeleteMessage(deleteMessageRequest);
            }
            return currentUrgency;
        }
    }
}
