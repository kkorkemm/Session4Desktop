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

namespace Session4Desktop.Pages
{
    using Base;

    /// <summary>
    /// Логика взаимодействия для InvertoryReportPage.xaml
    /// </summary>
    public partial class InvertoryReportPage : Page
    {
        public InvertoryReportPage()
        {
            InitializeComponent();

            ComboWarehouse.ItemsSource = AppData.GetContext().Warehouses.ToList();
        }

        private void BtnView_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Назад на главную страницу
        /// </summary>
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Navigation.MainFrame.GoBack();
        }
    }
}
