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

            GridOrders.ItemsSource = AppData.GetContext().OrderItems.ToList().OrderBy(p => p.Orders.Date);
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
            Navigation.MainFrame.Navigate(new WarehouseManagementPage(null));
        }

        /// <summary>
        /// Переход на страницу отчета
        /// </summary>
        private void MenuReport_Click(object sender, RoutedEventArgs e)
        {
            Navigation.MainFrame.Navigate(new InvertoryReportPage());
        }

        /// <summary>
        /// Редактирование заказа
        /// </summary>
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var invertory = (sender as Button).DataContext as OrderItems;

            if (invertory.Orders.TransactionTypeID == 1)
            {
                Navigation.MainFrame.Navigate(new PurchaseOrderPage(invertory.Orders));
            }
            else
            {
                Navigation.MainFrame.Navigate(new WarehouseManagementPage(invertory.Orders));
            }
        }

        /// <summary>
        /// Удаление заказа
        /// </summary>
        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            OrderItems selectedOrder = (sender as Button).DataContext as OrderItems;

            MessageBoxResult result = MessageBox.Show("Вы действительно хотите удалить запись?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                var order = selectedOrder.Orders;

                if (order.OrderItems.Count < 2)
                {
                    MessageBox.Show("Удаление записи невозможно. Заказ состоит только из этой детали", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                try
                {
                    AppData.GetContext().OrderItems.Remove(selectedOrder);
                    AppData.GetContext().SaveChanges();

                    MessageBox.Show("Запись успешно удалена!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);

                    GridOrders.ItemsSource = AppData.GetContext().OrderItems.ToList().OrderBy(p => p.Orders.Date);
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