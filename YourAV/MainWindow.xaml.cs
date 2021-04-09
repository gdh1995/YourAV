using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Management;
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

using static YourAV.ManagementHelper;

namespace YourAV
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public string AVName { get; set; } = "YourAV";
        public string AVGuid { get; set; } = Guid.NewGuid().ToString();
        public SolidColorBrush AccentColorBrush
            => IsAntivirusInstalled() ? Brushes.ForestGreen : Brushes.DarkRed;
        public string StatusText => IsAntivirusInstalled() ? "\u2713" : "\u274C";
        public string ButtonContent
            => IsAntivirusInstalled() ? "关闭" : "开启";
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Card_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (IsAntivirusInstalled())
            {
                RemoveAllAntivirus();
                //RemoveAllAntivirus2();
            }
            else
            {
                AddAntivirus(AVName, AVGuid);
                //AddAntivirus2(AVName, AVGuid);
            }
            RestartService("Windows Management Instrumentation");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AccentColorBrush)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(StatusText)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ButtonContent)));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AVGuid = Guid.NewGuid().ToString();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AVGuid)));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
