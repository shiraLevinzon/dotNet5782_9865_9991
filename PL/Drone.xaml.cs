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
        DateTime d = DateTime.MinValue;
        DronesListWindow dronesListWindow;
        public Drone(DroneToList d, IBL.IBL blobject, DronesListWindow dro)
        {
            InitializeComponent();
            dronesListWindow = dro;
            bl = blobject;

            AddBottun.Visibility = Visibility.Hidden;
            ADDgrid.Visibility = Visibility.Hidden;
            UPDATEgrid.DataContext = d;
            modelTextBox.IsEnabled = true;
            updateBottun.IsEnabled = false;
            StationIdComboBox.Visibility = Visibility.Hidden;
            temp = Convert.ToInt32(d.Conditions);
            Refresh();
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
        public void Refresh()
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
                    MessageBox.Show("add drone sucsess", "ADD OPTION", MessageBoxButton.OK, MessageBoxImage.Information);
                    //dronesListWindow.FilterByCombiBox();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("choose base station id", "ERROR", MessageBoxButton.OK, MessageBoxImage.Exclamation);                  
                }
            }
            catch (ImproperMaintenanceCondition ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (IBL.BO.MissingIdException ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (IBL.BO.DuplicateIdException ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (IBL.BO.TheDroneDnotShip ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (IBL.BO.PackageTimesException ex)
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
            catch (IBL.BO.MissingIdException ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (IBL.BO.DuplicateIdException ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (IBL.BO.TheDroneDnotShip ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (IBL.BO.PackageTimesException ex)
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
                        TimeSpan t = new TimeSpan(3, 0, 0);

                        bl.ReleaseDroneFromCharging(Convert.ToInt32(iDLabel.Content), t);
                        temp = 1;
                        Refresh();
                        MessageBox.Show("relese drone from charge sucess");
                        break;
                    case 1:
                        bl.DroneToCharging(Convert.ToInt32(iDLabel.Content));
                        d = DateTime.Now;
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
            catch (IBL.BO.MissingIdException ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (IBL.BO.DuplicateIdException ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (IBL.BO.TheDroneDnotShip ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (IBL.BO.PackageTimesException ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
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
                        temp = 2;
                        Refresh();
                        MessageBox.Show("Assign Package To Drone sucess");
                        break;
                    case 2:
                        bl.DeliveryOfPackageByDrone(Convert.ToInt32(iDLabel.Content));
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
            catch (IBL.BO.MissingIdException ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (IBL.BO.DuplicateIdException ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (IBL.BO.TheDroneDnotShip ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (IBL.BO.PackageTimesException ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            UPDATEgrid.DataContext = bl.GetAllDrones(d => d.ID == Convert.ToInt32(iDLabel.Content));
            dronesListWindow.FilterByCombiBox();
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
