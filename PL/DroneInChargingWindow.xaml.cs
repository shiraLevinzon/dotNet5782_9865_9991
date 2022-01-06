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
            IBL bl = bll;
            listOfDroneInCharge.ItemsSource = (System.Collections.IEnumerable)bl.GetDroneInCharge(bs.ID);
        }
    }
}
