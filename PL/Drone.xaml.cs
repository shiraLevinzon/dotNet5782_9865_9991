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
using System.ComponentModel;


namespace PL
{
    /// <summary>
    /// Interaction logic for Drone.xaml
    /// </summary>
    public partial class Drone : Window
    {
        ListView listViewWin;
         IBL  bl;
        int temp;
        int idDrone;
        public Drone(DroneToList d, BlApi.IBL blobject,ListView l=null)
        {
            InitializeComponent();
            idDrone = d.ID;
            listViewWin = l;
            bl = blobject;
            addMode.Visibility = Visibility.Hidden;
            actMode.Visibility = Visibility.Visible;
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
        public Drone(BlApi.IBL blobject, ListView l=null)
        {
            listViewWin = l;
            InitializeComponent();         
            bl = blobject;
            actMode.Visibility = Visibility.Hidden;
            addMode.Visibility = Visibility.Visible;
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
                deleteDrone.Visibility = Visibility.Visible;
                deleteDrone.Content = "Delete Drone";
                Timegrid.Visibility = Visibility.Visible;
            }
            if (temp == 0)
            {
                Bottun1.Content = "relese drone from charge ";
                Timegrid.Visibility = Visibility.Visible;
                Bottun2.Visibility = Visibility.Hidden;
                timepicker1.Visibility = Visibility.Visible;
                WithSecondsTimePicker1.Visibility = Visibility.Visible;
                deleteDrone.Visibility = Visibility.Collapsed;

            }
            if (temp == 2)
            {
                Bottun1.Content = "Package collection";
                Bottun2.Content = "Package delivery";
                Bottun2.Visibility = Visibility.Visible;
                Timegrid.Visibility = Visibility.Hidden;
                timepicker1.Visibility = Visibility.Collapsed;
                WithSecondsTimePicker1.Visibility = Visibility.Collapsed;
                deleteDrone.Visibility = Visibility.Collapsed;
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
            catch(BO.EntityHasBeenDeleted ex)
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
                      //  TimeSpan s=new TimeSpan(t.Minutes,t.Seconds,0);
                        bl.ReleaseDroneFromCharging(Convert.ToInt32(iDLabel.Content), t);
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
            catch (BO.EntityHasBeenDeleted ex)
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
            catch (BO.EntityHasBeenDeleted ex)
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
        private void deleteDrone_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.DeleteDrone(Convert.ToInt32(iDLabel.Content));
                MessageBox.Show("delete Drone succeeded");
                this.Close();
            }
            catch (BO.EntityHasBeenDeleted ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        #region simulator
        internal BackgroundWorker worker;
        private void simulator()
        {
            worker = new BackgroundWorker() { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //שינויים בסוף התהליכון
            Bottun1.Visibility = Visibility.Visible;
            Bottun2.Visibility = Visibility.Visible;
            updateBottun.Visibility = Visibility.Visible;
            deleteDrone.Visibility = Visibility.Visible;
            CancelBottun.Visibility = Visibility.Visible;

        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //שינויים בתצוגה
            BO.DroneToList myDrone = bl.GetAllDrones().First(dro=> dro.ID== idDrone);
            //UPDATEgrid.DataContext = myDrone;//לא עובד משום מה ממש מוזר
            batteryStatusLabel.Text =Convert.ToString(myDrone.BatteryStatus);
            conditionsLabel.Content = (BO.DroneConditions)myDrone.Conditions;
            packagNumberOnTransferred.Content = myDrone.PackagNumberOnTransferred;
            latitudeLabel.Text = Convert.ToString(myDrone.location.Latitude);
            longitudeLabel.Text = Convert.ToString(myDrone.location.Longitude);

            listViewWin.FilterByCombiBoxOfDrone();
            listViewWin.FilterByCombiBoxOfBaseStation();
            listViewWin.FilterByCombiBoxOfParcel();
            listViewWin.listOfCostumer.ItemsSource = bl.GetAllCustomer();

            switch (myDrone.Conditions)
            {
                case DroneConditions.Available:
                    if (showParcel.IsEnabled==true) //the drone is free cuse he just done (we know that becuse the grid is opend) it is affter deliverd.
                    {
                        //display changes for thois stage
                        showParcel.IsEnabled =false;
                    }
                    else //the drone is in a free state that has come out of charge and not like before (not affter deliver).
                    {
                        //Update the list observer of BaseStations.
                        
                    }
                    break;
                case DroneConditions.maintenance:
                    break;

                case DroneConditions.delivery:
                    showParcel.IsEnabled = true;
                    break;
                default:
                    break;
            }

        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            bl.simula(idDrone, reportPro, IsTimeRun);
            

        }
        public void reportPro()
        {
            worker.ReportProgress(0);
        }
        public bool IsTimeRun()
        {
            return worker.CancellationPending;
        }
        private void simu_Click(object sender, RoutedEventArgs e)
        {
            simulator();
            worker.RunWorkerAsync();

            Bottun1.Visibility = Visibility.Hidden;
            Bottun2.Visibility = Visibility.Hidden;
            updateBottun.Visibility = Visibility.Hidden;
            deleteDrone.Visibility = Visibility.Hidden;
            CancelBottun.Visibility = Visibility.Hidden;
        }
        #endregion

        private void cancelSimu_Click(object sender, RoutedEventArgs e)
        {
            worker.CancelAsync();

            temp = Convert.ToInt32(conditionsLabel.Content);
            Refresh();

        }
    }
}
