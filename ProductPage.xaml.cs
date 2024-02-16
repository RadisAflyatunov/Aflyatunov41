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

namespace Aflyatunov41
{
    /// <summary>
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        int mCount = 0;
        List<Product> TableList;
        public ProductPage(User user)
        {
            InitializeComponent();
            if (user != null)
            {
                FIOTB.Text = user.UserSurname + " " + user.UserName + " " + user.UserPatronymic;
                switch (user.UserRole)
                {
                    case 1:
                        RoleTB.Text = "Администратор"; break;
                    case 2:
                        RoleTB.Text = "Клиент"; break;
                    case 3:
                        RoleTB.Text = "Менеджер"; break;
                }
                URole.Visibility = Visibility.Visible;
                RoleTB.Visibility = Visibility.Visible;
            }
            else
            {
                FIOTB.Text = "Гость";
                URole.Visibility = Visibility.Hidden;
                RoleTB.Visibility = Visibility.Hidden;
            }

            List<Product> currentProducts = Aflyatunov41Entities.GetContext().Product.ToList();
            ProductListView.ItemsSource = currentProducts;

            TableList = Aflyatunov41Entities.GetContext().Product.ToList();
            mCount = TableList.Count;
            MCount.Text = Aflyatunov41Entities.GetContext().Product.ToList().Count.ToString();

            Filter.SelectedIndex = 0;

            Update();

        }

        private void Update()
        {
            var currentProducts = Aflyatunov41Entities.GetContext().Product.ToList();

            if (Filter.SelectedIndex == 0)
            {
                currentProducts = currentProducts.Where(p => p.ProductDiscountAmount >= 0 && p.ProductDiscountAmount <= 100).ToList();
            }

            if (Filter.SelectedIndex == 1)
            {
                currentProducts = currentProducts.Where(p => p.ProductDiscountAmount >= 0 && p.ProductDiscountAmount <= 9.99).ToList();
            }

            if (Filter.SelectedIndex == 2)
            {
                currentProducts = currentProducts.Where(p => p.ProductDiscountAmount >= 10 && p.ProductDiscountAmount <= 14.99).ToList();
            }

            if (Filter.SelectedIndex == 3)
            {
                currentProducts = currentProducts.Where(p => p.ProductDiscountAmount >= 15 && p.ProductDiscountAmount <= 100).ToList();
            }

            if (RButtonUp.IsChecked.Value)
            {
                currentProducts = currentProducts.OrderBy(p => p.ProductCost).ToList();
            }

            if (RButtonDown.IsChecked.Value)
            {
                currentProducts = currentProducts.OrderByDescending(p => p.ProductCost).ToList();
            }

            currentProducts = currentProducts.Where(p => p.ProductName.ToLower().Contains(Search.Text.ToLower())).ToList();
            CurAmount.Text = currentProducts.Count.ToString();

            ProductListView.ItemsSource = currentProducts;

        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }

        private void RButtonUp_Checked(object sender, RoutedEventArgs e)
        {
            Update();
        }

        private void RButtonDown_Checked(object sender, RoutedEventArgs e)
        {
            Update();
        }

        private void Filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }
    }
}

