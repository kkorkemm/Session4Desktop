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

            TextResult.Text = "Выберите склад для получения списка";
        }

        /// <summary>
        /// Обновление листа при фильтрации
        /// </summary>
        private void UpdateList()
        {
            var warehouse = ComboWarehouse.SelectedItem as Warehouses;

            if (warehouse == null)
            {
                MessageBox.Show("Выберите отдел", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var orderItems = AppData.GetContext().OrderItems;
            var parts = AppData.GetContext().Parts.Select(x => new
            {
                x.Name,

                CurrentStock = orderItems.Where(p => p.PartID == x.ID && p.Orders.DestinationWarehouseID == warehouse.ID).Sum(p => (Decimal?)p.Amount) - orderItems.Where(p => p.PartID == x.ID && p.Orders.SourceWarehouseID == warehouse.ID).Sum(p => (Decimal?)p.Amount),

                ReceivedStock = orderItems.Where(p => p.PartID == x.ID && p.Orders.DestinationWarehouseID == warehouse.ID).Sum(p => (Decimal?)p.Amount)
            }).ToArray();

            if (RadioCurrent.IsChecked == true)
            {
                parts = parts.Where(p => p.CurrentStock > 0).ToArray();
            }
            if (RadioOut.IsChecked == true)
            {
                parts = parts.Where(p => p.CurrentStock < 1).ToArray();
            }
            if (RadioReceived.IsChecked == true)
            {
                parts = parts.Where(p => p.ReceivedStock > 0).ToArray();
            }

            List<Parts> partList = new List<Parts>();

            foreach(var item in parts)
            {
                Parts part = new Parts()
                {
                    Name = item.Name,
                    CurrentStock = item.CurrentStock,
                    ReceivedStock = item.ReceivedStock
                };
                partList.Add(part);
            }

            GridReport.ItemsSource = partList;
            TextResult.Text = "Result: ";
        }

        /// <summary>
        /// View Batch Numbers
        /// </summary>
        private void BtnView_Click(object sender, RoutedEventArgs e)
        {
            /// что здесь делать
        }

        /// <summary>
        /// Назад на главную страницу
        /// </summary>
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Navigation.MainFrame.Navigate(new InvertoryManagementPage());
        }

        /// <summary>
        /// Фильтрация
        /// </summary>
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
