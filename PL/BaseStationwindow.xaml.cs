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
        public BaseStationwindow(IBL bll)
        {
            InitializeComponent();
            bl = bll;

        }

        private void ColorZone_ColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {

        }
    }
}
