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
        public Drone(DroneToList d=null)
        {
            if (d!=null)
            {
                //idTextBox.Text = d.ID.ToString();
                //modelTextBox.Text = d.Model.ToString();
                //weightTextBox.Text = d.MaxWeight.ToString();
                //conditionTextBox.Text = d.Conditions.ToString();
                //latitudeTextBox.Text = d.location.Latitude.ToString();
                //longtitudeTextBox.Text = d.location.Longitude;


            }
            InitializeComponent();
        }
    }
}
