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
using BO;
using BlApi;


namespace PL
{
    /// <summary>
    /// Interaction logic for ParcelWindow.xaml
    /// </summary>
    public partial class ParcelWindow : Window
    {
        IBL bl;
        BO.Parcel parcel;
        public ParcelWindow(int id, BlApi.IBL blobject)
        {
            InitializeComponent();
            bl = blobject;
            parcel= bl.GetParcel(id);
            addMode.Visibility = Visibility.Hidden;
            actMode.DataContext = parcel;
            if(parcel.DroneInParcel!=null)
            {
                showDrone.IsEnabled = true;
            }
        }
        public ParcelWindow(BlApi.IBL blobject)
        {
            InitializeComponent();
            actMode.Visibility = Visibility.Hidden;
            priorityComboBox.ItemsSource = Enum.GetValues(typeof(Priorities));
            weightComboBox.ItemsSource = Enum.GetValues(typeof(WeightCategories));
        }

        private void AddBottun_Click(object sender, RoutedEventArgs e)
        {
            BO.Parcel p = new Parcel()
            {
                Sender = new CustomerInParcel { ID = Convert.ToInt32(iDTextBox1) },
                Receiver = new CustomerInParcel { ID = Convert.ToInt32(iDTextBox1) },
                Weight=(WeightCategories)Convert.ToInt32(weightComboBox.SelectedItem),
                Priority = (BO.Priorities)Convert.ToInt32(priorityComboBox.SelectedItem),
            };
            try
            {
                bl.AddParcel(p);
                MessageBox.Show("add drone sucsess", "ADD OPTION", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (DuplicateIdException ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelBottun_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void showSender_Click(object sender, RoutedEventArgs e)
        {
            new CustomerInParcelWindow(parcel.Sender).ShowDialog();

        }

        private void showResiver_Click(object sender, RoutedEventArgs e)
        {
            new CustomerInParcelWindow(parcel.Receiver).ShowDialog();
        }

        private void showDrone_Click(object sender, RoutedEventArgs e)
        {
            new DroneInParcelWindow(parcel.DroneInParcel).ShowDialog();

        }
    }
}
