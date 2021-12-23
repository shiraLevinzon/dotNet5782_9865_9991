using System;
using IBL.BO;
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
    /// Interaction logic for Drone.xaml
    /// </summary>
    public partial class Drone : Window
    {
        IBL.IBL bl;
        int temp;
        bool a, b;
        DateTime d;
        DronesListWindow dronesListWindow;
        public Drone(DroneToList d, IBL.IBL blobject)
        {
            InitializeComponent();

            bl = blobject;

            AddBottun.Visibility = Visibility.Hidden;
            ADDgrid.Visibility = Visibility.Hidden;
            UPDATEgrid.DataContext = d;
            modelTextBox.IsEnabled = true;
            updateBottun.IsEnabled = false;
            StationIdComboBox.Visibility = Visibility.Hidden;
            temp = Convert.ToInt32(d.Conditions);
            refrash();


        }
        public Drone(IBL.IBL blobject,DronesListWindow d)
        {
            InitializeComponent();         
            dronesListWindow = d;
            bl = blobject;
            a = false;
            b = false;
            UPDATEgrid.Visibility = Visibility.Hidden;
            AddBottun.IsEnabled = false;
            Bottun1.Visibility = Visibility.Hidden;
            Bottun2.Visibility = Visibility.Hidden;
            updateBottun.Visibility = Visibility.Hidden;
            maxWeightComboBox.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            StationIdComboBox.ItemsSource = blobject.GetAllBaseStation().Select(b=> b.ID);

        }
        public void refrash()
        {
            if (temp == 1)
            {
                Bottun2.Visibility = Visibility.Visible;

                Bottun1.Content = "sent drone to charge";
                Bottun2.Content = "assing drone to parcel";
            }
            if (temp == 0)
            {
                Bottun1.Content = "relese drone from charge";
                Bottun2.Visibility = Visibility.Hidden;
            }
            if (temp == 2)
            {
                Bottun2.Visibility = Visibility.Visible;

                Bottun1.Content = "Package collection";
                Bottun2.Content = "Package delivery";
            }
        }
        private void AddBottun_Click_1(object sender, RoutedEventArgs e)
        {
            
            IBL.BO.Drone drone = new IBL.BO.Drone()
            {
                ID = int.Parse(iDTextBox.Text),
                Model = modelTextBox1.Text,
                MaxWeight = ((IBL.BO.WeightCategories)Convert.ToInt32(maxWeightComboBox.SelectedItem)),
                location = new Location() { },
                PackageInTransfer = new ParcelInTransfer() { }

            };
            try
            {
                if (StationIdComboBox.SelectedIndex != -1)
                {
                    bl.AddDrone(drone, Convert.ToInt32(StationIdComboBox.SelectedItem));
                    MessageBox.Show("add drone sucsess", "avigail haniflaa", MessageBoxButton.OK, MessageBoxImage.Information);
                    dronesListWindow.FilterByCombiBox();

                    this.Close();
                }
                else
                {
                    MessageBox.Show("choose base station id", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                  
                }

            }
            catch (DuplicateIdException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void CancelBottun_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void modelTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            updateBottun.IsEnabled = true;
        }

        private void updateBottun_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.UpdateDrone(Convert.ToInt32(iDLabel.Content), modelTextBox.Text);
                MessageBox.Show("update sucsess");
            }
            catch (MissingIdException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Bottun1_Click(object sender, RoutedEventArgs e)
        {
            //להוסיף קאצים לפי הפעולות
            if (temp == 1)
            {
                try
                {
                    bl.DroneToCharging(Convert.ToInt32(iDLabel.Content));
                    d = DateTime.Now;
                    temp = 0;
                    refrash();
                    MessageBox.Show("sending Drone To Charging sucess");
                    
                }
                catch (IDAL.DO.DuplicateIdException ex)
                {
                    MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (IDAL.DO.MissingIdException ex)
                {
                    MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);

                }
                catch (ImproperMaintenanceCondition ex)
                {
                    MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);

                }
                catch (Exception)
                {
                    MessageBox.Show("something wrong", "", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }
            else if (temp == 0)
            {
                try
                {

                    bl.ReleaseDroneFromCharging(Convert.ToInt32(iDLabel.Content),(d - DateTime.Now));
                    temp = 1;
                    refrash();
                    MessageBox.Show("relese drone from charge sucess");
                }
                catch (MissingIdException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (temp == 2)
            {
                try
                {
                    
                    bl.CollectParcelByDrone(Convert.ToInt32(iDLabel.Content));
                    temp = 2;
                    refrash();
                    MessageBox.Show("relese drone from charge sucess");
                }
                catch (MissingIdException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Bottun2_Click(object sender, RoutedEventArgs e)
        {
            if (temp == 1)
            {
                try
                {
                    bl.AssignPackageToDrone(Convert.ToInt32(iDLabel.Content));
                    temp = 2;
                    refrash();
                    MessageBox.Show("Assign Package To Drone sucess");
                }             
                catch (Exception)
                {
                    MessageBox.Show("something wrong", "", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }         
            else if (temp == 2)
            {
                try
                {
                    bl.DeliveryOfPackageByDrone(Convert.ToInt32(iDLabel.Content));
                    temp = 1;
                    refrash();
                    MessageBox.Show("relese drone from charge sucess");
                }
                catch (MissingIdException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void iDTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            a = true;
            if (a && b && maxWeightComboBox.SelectedIndex != -1)
            {
                AddBottun.IsEnabled = true;
            }
        }

        private void modelTextBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            b = true;
            if(a && b && maxWeightComboBox.SelectedIndex!=-1)
            {
                AddBottun.IsEnabled = true;
            }
        }
    }
}
