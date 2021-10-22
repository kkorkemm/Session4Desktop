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
            GridReport.ItemsSource = AppData.GetContext().OrderItems.ToList();
        }

        /// <summary>
        /// Обновление листа при фильтрации
        /// </summary>
        private void UpdateList()
        {
            var warehouse = ComboWarehouse.SelectedItem as Warehouses;
            var list = AppData.GetContext().OrderItems.ToList();

            if (RadioCurrent.IsChecked == true)
            {
                
            }
            if (RadioOut.IsChecked == true)
            {

            }
            if (RadioReceived.IsChecked == true)
            {
                list = list.Where(p => p.Orders.DestinationWarehouseID == warehouse.ID).ToList();
            }

            GridReport.ItemsSource = list;
        }

        /// <summary>
        /// View Batch Numbers
        /// </summary>
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

        private void ComboWarehouse_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateList();
        }

        private void RadioCurrent_Checked(object sender, RoutedEventArgs e)
        {
            UpdateList();
        }
    }
}
