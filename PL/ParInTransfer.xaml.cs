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
    /// Interaction logic for ParInTransfer.xaml
    /// </summary>
    public partial class ParInTransfer : Window
    {
        BlApi.IBL bl;
        public ParInTransfer(int id, BlApi.IBL blobject)
        {
            InitializeComponent();
            bl = blobject;
            BO.Drone drone = new BO.Drone();
            drone = bl.GetDrone(id);
            PITgrid.DataContext = drone.PackageInTransfer;
        }
    }
}
