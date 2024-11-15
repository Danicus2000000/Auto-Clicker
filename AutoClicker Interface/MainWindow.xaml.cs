using System;
using System.Text.RegularExpressions;
using System.Windows;
namespace AutoClicker_Interface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private System.Timers.Timer? clicktime;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartStopBtnClick(object sender, RoutedEventArgs e)
        {
            if (startStop_btn.Content.ToString() == "Start")
            {
                if (Convert.ToInt32(timeDelay_txt.Text) >= 500)
                {
                    MessageBoxResult result = MessageBox.Show("Are you sure you want to enable the autoclicker with a " + timeDelay_txt.Text + " millisecond delay?", "Enable?", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        startStop_btn.Content = "Stop";
                        clicktime = new(Convert.ToInt32(timeDelay_txt.Text));
                        if (clickCount_cmb.SelectedItem.ToString() == "Single Click")
                        {
                            clicktime.Elapsed += ClickTimerSingle_tick;
                        }
                        else
                        {
                            clicktime.Elapsed += ClickTimerDouble_tick;
                        }
                        clicktime.Start();
                    }
                }
                else
                {
                    MessageBox.Show("To protect the system 500 miliseconds is the minimum delay between clicks. " + timeDelay_txt.Text + " millisecond(s) is too short!", "Time delay too short.", MessageBoxButton.OK);
                }
            }
            else
            {
                startStop_btn.Content = "Start";
                clicktime ??= new System.Timers.Timer();
                clicktime.Elapsed -= ClickTimerDouble_tick;
                clicktime.Elapsed -= ClickTimerSingle_tick;
                clicktime?.Stop();
            }
        }

        private void ClickTimerSingle_tick(object? sender, System.Timers.ElapsedEventArgs e)
        {
            MouseEvents.MouseEvent(MouseEvents.MouseEventFlags.LeftDown);
            MouseEvents.MouseEvent(MouseEvents.MouseEventFlags.LeftUp);
        }

        private void ClickTimerDouble_tick(object? sender, System.Timers.ElapsedEventArgs e)
        {
            MouseEvents.MouseEvent(MouseEvents.MouseEventFlags.LeftDown);
            MouseEvents.MouseEvent(MouseEvents.MouseEventFlags.LeftUp);
            MouseEvents.MouseEvent(MouseEvents.MouseEventFlags.LeftDown);
            MouseEvents.MouseEvent(MouseEvents.MouseEventFlags.LeftUp);
        }

        private void TimeDelayTxtPreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }
        private static readonly Regex _regex = new("[^0-9.-]+"); //regex that matches disallowed text
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }
    }
}
