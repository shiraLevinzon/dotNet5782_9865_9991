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
        public ParcelAtCustomerWindow(BO.Customer custoer,IBL bll,int i)
        {
            InitializeComponent();
            bl = bll;
            if (i == 0)
                listOfPersonalParcel.ItemsSource = bl.GetCustomer(custoer.ID).PackagesFromCustomer;
            else
                listOfPersonalParcel.ItemsSource = bl.GetCustomer(custoer.ID).PackagesToCustomer;

        }

        private void listOfPersonalParcel_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.ParcelToList parcelTo = (ParcelToList)listOfPersonalParcel.SelectedItem;
            new parcelInTransferWindow(parcelTo.ID,bl).ShowDialog();
        }
    }
}
