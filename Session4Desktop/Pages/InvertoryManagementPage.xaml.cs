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
    /// Логика взаимодействия для InvertoryManagementPage.xaml
    /// </summary>
    public partial class InvertoryManagementPage : Page
    {
        public InvertoryManagementPage()
        {
            InitializeComponent();

            GridOrders.ItemsSource = AppData.GetContext().Orders.ToList().OrderBy(p => p.Date);
        }

        /// <summary>
        /// Переход на страницу заказов
        /// </summary>
        private void MenuPurchase_Click(object sender, RoutedEventArgs e)
        {
            Navigation.MainFrame.Navigate(new PurchaseOrderPage(null));
        }

        /// <summary>
        /// Переход на страницу управления складом
        /// </summary>
        private void MenuWarehouse_Click(object sender, RoutedEventArgs e)
        {
            Navigation.MainFrame.Navigate(new WarehouseManagementPage());
        }

        /// <summary>
        /// Переход на страницу отчета
        /// </summary>
        private void MenuReport_Click(object sender, RoutedEventArgs e)
        {
            Navigation.MainFrame.Navigate(new InvertoryReportPage());
        }

        private void GridParts_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            
        }

        /// <summary>
        /// Редактирование заказа
        /// </summary>
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Navigation.MainFrame.Navigate(new PurchaseOrderPage((sender as Button).DataContext as Orders));
        }

        /// <summary>
        /// Удаление заказа
        /// </summary>
        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            Orders selectedOrder = (sender as Button).DataContext as Orders;

            MessageBoxResult result = MessageBox.Show("Вы действительно хотите удалить запись?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    AppData.GetContext().Orders.Remove(selectedOrder);
                    AppData.GetContext().SaveChanges();

                    MessageBox.Show("Запись успешно удалена!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);

                    GridOrders.ItemsSource = AppData.GetContext().Orders.ToList().OrderBy(p => p.Date);
                }
                catch
                {
                    MessageBox.Show("Удаление записи невозможно", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
        }
    }
}