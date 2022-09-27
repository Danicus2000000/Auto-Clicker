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
namespace AutoClicker_Interface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static MouseEvents mouseController=new MouseEvents(); //initalises mouse controller class
        private Thread clickEventInitaliser;
        public MainWindow()
        {
            InitializeComponent();
            /*
            Thread.Sleep(625);//sleep for a sword swing then play
            MouseEvent(MouseEventFlags.LeftDown);
            MouseEvent(MouseEventFlags.LeftUp);
             */
        }

        private void startStop_btn_Click(object sender, RoutedEventArgs e)
        {
            if (startStop_btn.Content.ToString() == "Start") 
            {
                startStop_btn.Content = "Stop";
                clickEventInitaliser= new Thread(() => runClickEvent(Convert.ToInt32(timeDelay_txt.Text)));
                clickEventInitaliser.Start();
            }
            else if(startStop_btn.Content.ToString() == "Stop") 
            {
                startStop_btn.Content = "Start";
            }
        }
        
        private void runClickEvent(int timeDelay) 
        {
            this.Dispatcher.Invoke(() =>
            {
                while (startStop_btn.Content.ToString() == "Stop")
                {
                    Thread.Sleep(timeDelay);
                    MouseEvents.MouseEvent(MouseEvents.MouseEventFlags.LeftDown);
                    MouseEvents.MouseEvent(MouseEvents.MouseEventFlags.LeftUp);
                }
            });
        }
    }
}
