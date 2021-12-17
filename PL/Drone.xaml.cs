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
        int Status;
        int Weight;
        DronesListWindow dronesListWindow;
        public Drone(DroneToList d, IBL.IBL blobject)
        {
            InitializeComponent();

            bl = blobject;
            AddBottun.Visibility = Visibility.Hidden;
            CancelAddBottun.Visibility = Visibility.Hidden;
            idTextBox.Text = Convert.ToString(d.ID);
            modelTextBox.Text = Convert.ToString(d.Model);
            weightComboBox.Text = Convert.ToString(d.MaxWeight);
            BatteryStatusTextBox.Text = Convert.ToString(d.BatteryStatus);
            conditionTextBox.Text = Convert.ToString(d.Conditions);
            latitudeTextBox.Text = Convert.ToString(d.location.Latitude);
            longtitudeTextBox.Text = Convert.ToString(d.location.Longitude);

            idTextBox.IsEnabled = false;
            weightComboBox.IsEnabled = false;
            BatteryStatusTextBox.IsEnabled = false;
            conditionTextBox.IsEnabled = false;
            latitudeTextBox.IsEnabled = false;
            longtitudeTextBox.IsEnabled = false;


        }
        public Drone(IBL.IBL blobject,int s, int w,DronesListWindow d)
        {
            InitializeComponent();
            Status = s;
            Weight = w;
            dronesListWindow = d;
            bl = blobject;
            battery.Visibility = Visibility.Hidden;
            condition.Visibility = Visibility.Hidden;
            latitude.Visibility = Visibility.Hidden;
            longtitude.Visibility = Visibility.Hidden;
            BatteryStatusTextBox.Visibility = Visibility.Hidden;
            conditionTextBox.Visibility = Visibility.Hidden;
            latitudeTextBox.Visibility = Visibility.Hidden;
            longtitudeTextBox.Visibility = Visibility.Hidden;
            UpdateButton.Visibility = Visibility.Hidden;
            CancelAddBottun.Visibility = Visibility.Visible;
            StationId.Visibility = Visibility.Visible;
            StationIdComboBox.Visibility = Visibility.Visible;
            weightComboBox.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            StationIdComboBox.ItemsSource = blobject.GetAllBaseStation().Select(b=> b.ID);

        }

        private void AddBottun_Click(object sender, RoutedEventArgs e)
        {
            IBL.BO.Drone drone = new IBL.BO.Drone()
            {
                ID = int.Parse(idTextBox.Text),
                Model = modelTextBox.Text,
                MaxWeight = ((IBL.BO.WeightCategories)Convert.ToInt32(weightComboBox.SelectedItem)),
                location = new Location() { },
                PackageInTransfer=new ParcelInTransfer() { }
                
            };
            try
            {

                if (StationIdComboBox.SelectedIndex != -1)
                {
                    BaseStation b = bl.GetBaseStation(Convert.ToInt32(StationIdComboBox.SelectedItem));
                    bl.AddDrone(drone, b.ID);
                    MessageBoxResult mbResult = MessageBox.Show("add drone sucsess", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                    switch (mbResult)
                    {
                        case MessageBoxResult.OK:
                            if(Weight!=-1&&Status!=-1)
                                dronesListWindow.DronesListView.ItemsSource = bl.GetAllDrones(dro => dro.MaxWeight == (IBL.BO.WeightCategories)Weight && dro.Conditions == (IBL.BO.DroneConditions)Status);
                            else if(Weight != -1 && Status ==-1)
                                dronesListWindow.DronesListView.ItemsSource = bl.GetAllDrones(dro => dro.MaxWeight == (IBL.BO.WeightCategories)Weight);
                            else if (Weight == -1 && Status != -1)
                                dronesListWindow.DronesListView.ItemsSource = bl.GetAllDrones(dro => dro.Conditions == (IBL.BO.DroneConditions)Status);
                            this.Close();
                            break;
                    }
                }
                else
                {
                    MessageBoxResult mbResult = MessageBox.Show("choose base station id", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    switch (mbResult)
                    {
                        case MessageBoxResult.OK:
                            break;
                    }

                }

            }
            catch(DuplicateIdException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void idTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (idTextBox.Text.Length <= 5)
            {
                AddBottun.IsEnabled = true;
                UpdateButton.IsEnabled = true;
            }
            else
            {
                AddBottun.IsEnabled = false;
                UpdateButton.IsEnabled = false;
            }
        }

        private void CancelAddBottun_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void UpdateButton_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.UpdateDrone(Convert.ToInt32(idTextBox.Text), modelTextBox.Text);
                MessageBox.Show("update sucsess");
            }
            catch (MissingIdException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
