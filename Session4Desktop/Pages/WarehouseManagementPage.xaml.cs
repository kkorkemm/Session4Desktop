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
    /// Логика взаимодействия для WarehouseManagementPage.xaml
    /// </summary>
    public partial class WarehouseManagementPage : Page
    {
        private Orders CurrentOrder = new Orders();

        public WarehouseManagementPage(Orders order)
        {
            InitializeComponent();

            ComboSourceHauses.ItemsSource = AppData.GetContext().Warehouses.ToList();
            ComboDestHouses.ItemsSource = AppData.GetContext().Warehouses.ToList();
            ComboParts.ItemsSource = AppData.GetContext().Parts.ToList();

            if (order != null)
                CurrentOrder = order;
            else
            {
                CurrentOrder.Date = DateTime.Now;
                CurrentOrder.TransactionTypeID = 2;
            }

            DataContext = CurrentOrder;
        }

        /// <summary>
        /// Сохрание заказа
        /// </summary>
        private string CheckCanSaveOrder()
        {
            StringBuilder errors = new StringBuilder();

            if (CurrentOrder.Warehouses == null)
                errors.AppendLine("Укажите исходный склад");
            if (CurrentOrder.Warehouses1 == null)
                errors.AppendLine("Укажите склад назначения");
            if (CurrentOrder.Date == null)
                errors.AppendLine("Укажите дату");

            return errors.ToString();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (ComboParts.SelectedItem == null)
                errors.AppendLine("Выберите деталь");
            if (string.IsNullOrWhiteSpace(TextAmount.Text))
                errors.AppendLine("Укажите сумму");
            else
            {
                if (!double.TryParse(TextAmount.Text, out _))
                    errors.AppendLine("Суммой может быть положительное число");
                else if (Convert.ToDecimal(TextAmount.Text) < 0)
                    errors.AppendLine("Суммой может быть положительное число");
            }

            if (ComboParts.SelectedItem != null)
            {
                if ((ComboParts.SelectedItem as Parts).BatchNumberHasRequired == true)
                {
                    if (string.IsNullOrWhiteSpace(TextBatch.Text))
                        errors.AppendLine("Укажите номер партии");
                }
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (CurrentOrder.ID == 0)
            {
                MessageBoxResult result = MessageBox.Show("Сохранить заказ перед добавлением детали?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    string valid = CheckCanSaveOrder();

                    if (valid.Length == 0)
                    {
                        try
                        {
                            AppData.GetContext().Orders.Add(CurrentOrder);
                            AppData.GetContext().SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    else
                    {
                        MessageBox.Show(valid, "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }
            }

            OrderItems orderItem = new OrderItems()
            {
                OrderID = CurrentOrder.ID,
                PartID = (ComboParts.SelectedItem as Parts).ID,
                BatchNumber = (ComboParts.SelectedItem as Parts).BatchNumberHasRequired == true ? TextBatch.Text : null,
                Amount = Convert.ToDecimal(TextAmount.Text)
            };

            try
            {
                AppData.GetContext().OrderItems.Add(orderItem);
                AppData.GetContext().SaveChanges();

                MessageBox.Show("Деталь успешно добавлена!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);

                GridOrderItems.ItemsSource = AppData.GetContext().OrderItems.Where(p => p.OrderID == CurrentOrder.ID).ToList();

                ComboParts.SelectedItem = null;
                TextAmount.Text = "";
                TextBatch.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Удаление детали
        /// </summary>
        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            OrderItems selectedItem = (sender as Button).DataContext as OrderItems;

            MessageBoxResult result = MessageBox.Show("Вы действительно хотите удалить деталь?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    AppData.GetContext().OrderItems.Remove(selectedItem);
                    AppData.GetContext().SaveChanges();

                    MessageBox.Show("Деталь успешно удалена!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);

                    GridOrderItems.ItemsSource = AppData.GetContext().OrderItems.Where(p => p.OrderID == CurrentOrder.ID).ToList();
                }
                catch
                {
                    MessageBox.Show("Удаление детали невозможно", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            string valid = CheckCanSaveOrder();

            if (valid.Length > 0)
            {
                MessageBox.Show(valid, "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (CurrentOrder.OrderItems == null || CurrentOrder.OrderItems.Count == 0)
            {
                MessageBox.Show("Заказ возможен при наличии не менее одной детали", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (CurrentOrder.ID == 0)
            {
                AppData.GetContext().Orders.Add(CurrentOrder);
            }

            try
            {
                AppData.GetContext().SaveChanges();

                MessageBox.Show("Данные успешно сохранены!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);

                Navigation.MainFrame.Navigate(new InvertoryManagementPage());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Назад (Cancel)
        /// </summary>
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Navigation.MainFrame.GoBack();
        }
    }
}
