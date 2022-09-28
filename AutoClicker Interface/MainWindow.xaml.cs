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
using System.Windows.Threading;

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
        }

        private void startStop_btn_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(timeDelay_txt.Text, out int i))
            {
                if (startStop_btn.Content.ToString() == "Start")
                {
                    startStop_btn.Content = "Stop";
                    clickEventInitaliser = new Thread(() => runClickEvent(Convert.ToInt32(timeDelay_txt.Text)));
                    //Application.Current.Dispatcher.Invoke(runClickEvent, DispatcherPriority.ContextIdle);
                    clickEventInitaliser.Start();
                }
                else if (startStop_btn.Content.ToString() == "Stop")
                {
                    startStop_btn.Content = "Start";
                }
            }
            else 
            {
                MessageBox.Show("The delay entered is not a number.", "Invalid Value!");
            }
        }

        private void runClickEvent(int timeDelay)
        {
            string startStopState = "Stop";
            while (startStopState == "Stop")
            {
                //Application.Current.Dispatcher.Invoke(new Action(()=>this.startStop_btn.Content.ToString()), DispatcherPriority.ContextIdle);
                startStopState= Application.Current.Dispatcher.Invoke<string>(() => { return startStop_btn.Content.ToString(); },DispatcherPriority.Normal);
                Thread.Sleep(timeDelay);
                MouseEvents.MouseEvent(MouseEvents.MouseEventFlags.LeftDown);
                MouseEvents.MouseEvent(MouseEvents.MouseEventFlags.LeftUp);
            }
        }
    }
}
