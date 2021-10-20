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
        public WarehouseManagementPage()
        {
            InitializeComponent();

            ComboSourceHauses.ItemsSource = AppData.GetContext().Warehouses.ToList();
            ComboDestHouses.ItemsSource = AppData.GetContext().Warehouses.ToList();
            ComboParts.ItemsSource = AppData.GetContext().Parts.ToList();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnRemove_Click(object sender, RoutedEventArgs e)
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
