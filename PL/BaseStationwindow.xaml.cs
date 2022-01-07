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
    /// Interaction logic for BaseStationwindow.xaml
    /// </summary>
    public partial class BaseStationwindow : Window
    {
        IBL bl;
        int temp;
        public BaseStationwindow(IBL bll)
        {
            InitializeComponent();
            bl = bll;
            actMode.Visibility = Visibility.Collapsed;
            buttonBaseStation.Content = "ADD";
            droneInChargeList.Visibility = Visibility.Collapsed;
            temp = 0;
        }
        public BaseStationwindow(BaseStationToList bs, BlApi.IBL blobject)
        {
            InitializeComponent();
            bl = blobject;
            actMode.DataContext = bl.GetBaseStation(bs.ID);
            addMode.Visibility = Visibility.Collapsed;
            buttonBaseStation.Content = "Update";
            string temp1 = "Station Details " + bs.ID;
            droneInChargeList.Visibility = Visibility.Visible;
            title.Content = temp1;
            temp = 1;
        }
        private void buttonBaseStation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (temp)
                {
                    case 0:
                        if (iDTextBox.Text == "" || stationNameTextBox1.Text == "" || freeChargingSlotsTextBox1.Text == "" || latitudeTextBox.Text == "" || longitudeTextBox.Text == "")
                        {
                            MessageBox.Show("Enter All the Base Station Details", "ERROR", MessageBoxButton.OK, MessageBoxImage.Information);

                        }
                        else
                        {
                            BO.BaseStation bs = new BO.BaseStation()
                            {
                                ID = Convert.ToInt32(iDTextBox.Text),
                                StationName = Convert.ToString(stationNameTextBox1.Text),
                                BaseStationLocation = new Location()
                                {
                                    Latitude = Convert.ToDouble(latitudeTextBox.Text),
                                    Longitude = Convert.ToDouble(longitudeTextBox.Text)
                                },
                                FreeChargingSlots = Convert.ToInt32(freeChargingSlotsTextBox1.Text)
                            };
                            if (bs.FreeChargingSlots > 6)
                                bs.FreeChargingSlots = 5;
                            bl.AddBaseStation(bs);
                            MessageBox.Show("add Base Station sucsess", "ADD OPTION", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.Close();
                        }
                        break;
                    case 1:
                        bl.UpdateBaseStation(Convert.ToInt32(iDTextBlock.Text), stationNameTextBox.Text, Convert.ToInt32(freeChargingSlotsTextBox.Text));
                        MessageBox.Show("update Base Station sucsess", "UPDATE OPTION", MessageBoxButton.OK, MessageBoxImage.Information);
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
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
        private void droneInChargeList_Click(object sender, RoutedEventArgs e)
        {
            BO.BaseStation bs = new BO.BaseStation()
            {
                ID = Convert.ToInt32(iDTextBlock.Text),
                StationName = Convert.ToString(stationNameTextBox.Text),
                BaseStationLocation = new Location()
                {
                    Latitude = Convert.ToDouble(latitudeTextBlock.Text),
                    Longitude = Convert.ToDouble(longitudeTextBlock.Text)
                },
                FreeChargingSlots = Convert.ToInt32(freeChargingSlotsTextBox.Text)
            };
            new DroneInChargingWindow(bl,bs).ShowDialog();
        }
    }
}
