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
    /// Interaction logic for DroneInChargingWindow.xaml
    /// </summary>
    public partial class DroneInChargingWindow : Window
    {
        IBL bl;
        public DroneInChargingWindow(IBL bll,BaseStation bs)
        {
            InitializeComponent();
            bl = bll;
            listOfDroneInCharge.ItemsSource =bl.GetAllDroneInCharge(bs.ID);
        }
        private void listOfDroneInCharge_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            BO.DroneInCharging d = (BO.DroneInCharging)listOfDroneInCharge.SelectedItem;
            BO.Drone dr = bl.GetDrone(d.ID);
            BO.DroneToList droneTo = new DroneToList()
            {
                ID = dr.ID,
                Conditions = dr.Conditions,
                BatteryStatus = dr.BatteryStatus,
                MaxWeight = dr.MaxWeight,
                Model = dr.Model,
                PackagNumberOnTransferred = 0,
                location=new Location()
                {
                    Latitude=dr.location.Latitude,
                    Longitude=dr.location.Longitude,
                }
            };
            new Drone(droneTo, bl).ShowDialog();
        }
    }
}
