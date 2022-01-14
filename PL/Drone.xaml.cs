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
    /// Interaction logic for Drone.xaml
    /// </summary>
    public partial class Drone : Window
    {
         IBL  bl;
        int temp;
        public Drone(DroneToList d, BlApi.IBL blobject)
        {
            InitializeComponent();
            bl = blobject;
            addMode.Visibility = Visibility.Hidden;
            UPDATEgrid.DataContext = d;
            modelTextBox.IsEnabled = true;
            updateBottun.IsEnabled = false;
            StationIdComboBox.Visibility = Visibility.Hidden;
            temp = Convert.ToInt32(d.Conditions);
            Refresh();
            if (Convert.ToString(packagNumberOnTransferred.Content) != "0")
            {
                showParcel.IsEnabled = true;
            }
            if(d.Conditions==(BO.DroneConditions)0)
            {
                timepicker1.Visibility = Visibility.Visible;
                WithSecondsTimePicker1.Visibility = Visibility.Visible;
            }
            if(d.Conditions!=(BO.DroneConditions)0)
            {
                timepicker1.Visibility = Visibility.Collapsed;
                WithSecondsTimePicker1.Visibility = Visibility.Collapsed;
            }
        }
        public Drone(BlApi.IBL blobject)
        {
            InitializeComponent();         
            bl = blobject;
            actMode.Visibility = Visibility.Hidden;
            maxWeightComboBox.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            StationIdComboBox.ItemsSource = blobject.GetAllBaseStation().Select(b=> b.ID);
            timepicker1.Visibility = Visibility.Collapsed;
            WithSecondsTimePicker1.Visibility = Visibility.Collapsed;
        }
        public void Refresh()
        {
            if (temp == 1)
            {
                Bottun2.Visibility = Visibility.Visible;
                Bottun1.Content = "sent drone to charge";
                Bottun2.Content = "assing drone to parcel";
                Timegrid.Visibility = Visibility.Hidden;
            }
            if (temp == 0)
            {
                Bottun1.Content = "relese drone from charge ";
                Timegrid.Visibility = Visibility.Visible;
                Bottun2.Visibility = Visibility.Hidden;
                timepicker1.Visibility = Visibility.Visible;
                WithSecondsTimePicker1.Visibility = Visibility.Visible;

            }
            if (temp == 2)
            {
                Bottun1.Content = "Package collection";
                Bottun2.Content = "Package delivery";
                Bottun2.Visibility = Visibility.Visible;
                Timegrid.Visibility = Visibility.Hidden;
                timepicker1.Visibility = Visibility.Collapsed;
                WithSecondsTimePicker1.Visibility = Visibility.Collapsed;
            }
        }
        private void AddBottun_Click_1(object sender, RoutedEventArgs e)
        {
            
            BO.Drone drone = new BO.Drone()
            {
                ID = int.Parse(iDTextBox.Text),
                Model = modelTextBox1.Text,
                MaxWeight = ((BO.WeightCategories)Convert.ToInt32(maxWeightComboBox.SelectedItem)),
                location = new Location() { },
                PackageInTransfer = new BO.ParcelInTransfer() { }
            };
            try
            {
                if (StationIdComboBox.SelectedIndex == -1||iDTextBox.Text==null||modelTextBox1.Text==null||maxWeightComboBox.SelectedIndex==-1)
                {
                    MessageBox.Show("You did not fill in all the details", "ERROR", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    bl.AddDrone(drone, Convert.ToInt32(StationIdComboBox.SelectedItem));
                    MessageBox.Show("add drone sucsses", "ADD OPTION", MessageBoxButton.OK, MessageBoxImage.Information);

                    this.Close();

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
            catch (BO.TheDroneDnotShip ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.PackageTimesException ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
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
            catch (BO.TheDroneDnotShip ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.PackageTimesException ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Bottun1_Click(object sender, RoutedEventArgs e)
        {
            //להוסיף קאצים לפי הפעולות
            try
            {
                switch (temp)
                {
                    case 0:
                        TimeSpan t;
                        bool tmp= TimeSpan.TryParse(WithSecondsTimePicker1.Text,out t);
                        TimeSpan s=new TimeSpan(t.Minutes,t.Seconds,0);
                        bl.ReleaseDroneFromCharging(Convert.ToInt32(iDLabel.Content), s);
                        temp = 1;
                        Refresh();
                        MessageBox.Show("relese drone from charge sucess");
                        break;
                    case 1:
                        bl.DroneToCharging(Convert.ToInt32(iDLabel.Content));
                        temp = 0;
                        Refresh();
                        MessageBox.Show("sending Drone To Charging sucess");
                        break;
                    case 2:
                        bl.CollectParcelByDrone(Convert.ToInt32(iDLabel.Content));
                        temp = 2;
                        Refresh();
                        MessageBox.Show("Collect Parcel By Drone sucsses");
                        break;
                }
            }
            catch (BO.MissingIdException ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.DuplicateIdException ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.TheDroneDnotShip ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.PackageTimesException ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (ImproperMaintenanceCondition ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);

            }
         
            UPDATEgrid.DataContext = bl.GetAllDrones(d => d.ID == Convert.ToInt32(iDLabel.Content));
            //dronesListWindow.FilterByCombiBox();
        }


        private void Bottun2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (temp)
                {
                    case 1:
                        bl.AssignPackageToDrone(Convert.ToInt32(iDLabel.Content));
                        showParcel.IsEnabled = true;
                        temp = 2;
                        Refresh();
                        MessageBox.Show("Assign Package To Drone sucess");
                        break;
                    case 2:
                        bl.DeliveryOfPackageByDrone(Convert.ToInt32(iDLabel.Content));
                        showParcel.IsEnabled = false;
                        temp = 1;
                        Refresh();
                        MessageBox.Show("Delivery Of Package By Drone drone to customer sucess");
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
            catch (BO.TheDroneDnotShip ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.PackageTimesException ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            UPDATEgrid.DataContext = bl.GetAllDrones(d => d.ID == Convert.ToInt32(iDLabel.Content));
        }

     

        private void showParcel_Click(object sender, RoutedEventArgs e)
        {
            new parcelInTransferWindow(Convert.ToInt32(iDLabel.Content),bl).ShowDialog();
        }

        private void iDTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if((e.Key>=Key.D0&& e.Key<= Key.D9)||(e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

    }
}
