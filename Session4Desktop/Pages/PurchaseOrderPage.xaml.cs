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
    /// Логика взаимодействия для PurchaseOrderPage.xaml
    /// </summary>
    public partial class PurchaseOrderPage : Page
    {
        Orders CurrenOrder = new Orders();

        public PurchaseOrderPage(Orders order)
        {
            InitializeComponent();

            ComboHouses.ItemsSource = AppData.GetContext().Warehouses.ToList();
            ComboParts.ItemsSource = AppData.GetContext().Parts.ToList();
            ComboSupply.ItemsSource = AppData.GetContext().Suppliers.ToList();

            if (order != null)
                CurrenOrder = order;

            DataContext = CurrenOrder;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {

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
