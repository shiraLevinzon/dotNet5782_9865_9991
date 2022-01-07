using System;
using BlApi;
using BO;
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
using System.Windows.Shapes;
namespace PL
{
    /// <summary>
    /// Interaction logic for ParcelAtCustomerWindow.xaml
    /// </summary>
    public partial class ParcelAtCustomerWindow : Window
    {
        IBL bl;
        public ParcelAtCustomerWindow(int id,IBL bll,int i)
        {
            InitializeComponent();
            bl = bll;
            if (i == 0)
                listOfPersonalParcel.ItemsSource = bl.GetCustomer(id).PackagesFromCustomer;
            else
                listOfPersonalParcel.ItemsSource = bl.GetCustomer(id).PackagesToCustomer;

        }
        private void listOfPersonalParcel_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            BO.ParcelAtCustomer parcelTo = (ParcelAtCustomer)listOfPersonalParcel.SelectedItem;
            new parcelInTransferWindow(parcelTo.ID, bl).ShowDialog();
        }

        private void listOfPersonalParcel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
