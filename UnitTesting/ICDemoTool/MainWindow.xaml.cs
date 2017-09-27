using System;
using System.Windows;
using System.Windows.Data;

namespace ICDemoTool
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

        private void btConnection_Click(object sender, RoutedEventArgs e)
        {
            btConnection.Content = (btConnection.Content.ToString() == "Connect") ? "Disconnect" : "Connect";
        }
    }

    public class BooleanAndConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            foreach (object value in values)
            {
                if ((value is bool) && (bool)value == true)
                {
                    return false;
                }
                else if((value is String) && (String)value == "Connect")
                {
                    return false;
                }
            }
            return true;
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException("BooleanAndConverter is a OneWay converter.");
        }
    }
}
