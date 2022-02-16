using BlApi;
using BO;
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
using System.Windows.Shapes;
namespace PL
{
    /// <summary>
    /// Interaction logic for CostumerWindow.xaml
    /// </summary>
    public partial class CostumerWindow : Window
    {
        IBL bl;
        int temp;
        int id;
        public CostumerWindow(BO.CustomerToList customer,IBL bll)
        {
            InitializeComponent();
            bl = bll;
            id = customer.ID;
            addMode.Visibility = Visibility.Collapsed;
            actMode.DataContext = bl.GetCustomer(customer.ID);
            AddOrUpdate.Content = "Update";
            PackagesFromCustomer.Visibility = Visibility.Visible;
            PackagesFromCustomer.Content = "view all parcel from costumer";
            PackagesToCustomer.Visibility = Visibility.Visible;
            PackagesToCustomer.Content = "view all parcel to costumer";
            PersonDetails.Text = " " + customer.Name + "  " + customer.ID;
            temp = 1;
        }
        public CostumerWindow(BO.Customer customer, IBL bll,bool flag)
        {
            InitializeComponent();
            bl = bll;
            id = customer.ID;
            addMode.Visibility = Visibility.Collapsed;
            actMode.DataContext = customer;
            AddOrUpdate.Content = "Update";
            AddP.Visibility = Visibility.Visible;
            PackagesFromCustomer.Visibility = Visibility.Visible;
            PackagesFromCustomer.Content = "view all parcel from costumer";
            PackagesToCustomer.Visibility = Visibility.Visible;
            PackagesToCustomer.Content = "view all parcel to costumer";
            PersonDetails.Text = " " + customer.Name + "  " + customer.ID;
            temp = 1;
        }
        public CostumerWindow( IBL bll)
        {
            InitializeComponent();
            actMode.Visibility = Visibility.Collapsed;
            bl = bll;
            AddOrUpdate.Content = "ADD";
            PackagesFromCustomer.Visibility = Visibility.Collapsed;
            PackagesToCustomer.Visibility = Visibility.Collapsed;
            DeleteCustumer.Visibility = Visibility.Collapsed;
            temp = 0;
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddOrUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (temp)        
                {
                    case 0:
                        if (iDTextBox.Text == "" || nameTextBox1.Text == "" || phoneTextBox1.Text == "" || latitudeTextBox.Text == "" || longitudeTextBox.Text == "")
                        {
                            MessageBox.Show("Enter All the Base Station Details", "ERROR", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            BO.Customer cs = new BO.Customer()
                            {
                                ID = Convert.ToInt32(iDTextBox.Text),
                                Name = nameTextBox1.Text,
                                Location = new Location()
                                {
                                    Latitude = Convert.ToDouble(latitudeTextBox.Text),
                                    Longitude = Convert.ToDouble(longitudeTextBox.Text)
                                },
                                Phone = phoneTextBox1.Text,
                            };
                            bl.AddCustomer(cs);
                            MessageBox.Show("add New Costumer sucsess", "ADD OPTION", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.Close();
                        }
                        break;
                    case 1:
                        bl.UpdateCustomer(Convert.ToInt32(iDTextBlock.Text), nameTextBox.Text, Convert.ToString(phoneTextBox.Text));
                        MessageBox.Show("update Costumer sucsess", "UPDATE OPTION", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                        break;
                }
            }
            catch (ImproperMaintenanceCondition ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.MissingIdException ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.DuplicateIdException ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void PackagesFromCustomer_Click(object sender, RoutedEventArgs e)
        {
            //BO.Customer cs = new BO.Customer()
            //{
            //    ID = Convert.ToInt32(iDTextBlock.Text),
            //    Name = Convert.ToString(nameTextBox.Text),
            //    Location = new Location()
            //    {
            //        Latitude = Convert.ToDouble(latitudeTextBlock.Text),
            //        Longitude = Convert.ToDouble(longitudeTextBlock.Text)
            //    },
            //    Phone = Convert.ToString(phoneTextBox.Text),
            //};
            new ParcelAtCustomerWindow(id, bl, 0).ShowDialog();
        }

        private void PackagesToCustomer_Click(object sender, RoutedEventArgs e)
        {
            //BO.Customer cs = new BO.Customer()
            //{
            //    ID = Convert.ToInt32(iDTextBlock.Text),
            //    Name = Convert.ToString(nameTextBox.Text),
            //    Location = new Location()
            //    {
            //        Latitude = Convert.ToDouble(latitudeTextBlock.Text),
            //        Longitude = Convert.ToDouble(longitudeTextBlock.Text)
            //    },
            //    Phone = Convert.ToString(phoneTextBox.Text),
            //};
            new ParcelAtCustomerWindow(id, bl, 1).ShowDialog();
        }

        private void iDTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void AddP_Click(object sender, RoutedEventArgs e)
        {
            new ParcelWindow(bl,id).ShowDialog();
        }
        private void DeleteCustumer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.DeleteCustomer(Convert.ToInt32(iDTextBlock.Text));
                MessageBox.Show($"delete Customer succeeded");
                this.Close();
            }
            catch (BO.EntityHasBeenDeleted ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
