using System;
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
using System.Configuration;

namespace DesktopApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            foreach ( string key in ConfigurationManager.AppSettings.AllKeys )
            {
                MessageBox.Show($"key={key}, value={ConfigurationManager.AppSettings[key]}");
            }

            foreach ( ConnectionStringSettings cs in ConfigurationManager.ConnectionStrings)
            {
                MessageBox.Show($"connection string: name={cs.Name}, value={cs.ConnectionString}");
            }
        }
    }
}
