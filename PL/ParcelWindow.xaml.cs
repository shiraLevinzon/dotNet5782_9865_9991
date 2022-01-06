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
                DeleteBottun.IsEnabled = false;
            }
        }
        public ParcelWindow(BlApi.IBL blobject)
        {
            InitializeComponent();
            bl = blobject;
            actMode.Visibility = Visibility.Hidden;
            priorityComboBox.ItemsSource = Enum.GetValues(typeof(Priorities));
            weightComboBox.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            ReceiverIDComboBox.ItemsSource = bl.GetAllCustomer().Select(cu => cu.ID);
            SenderIDComboBox.ItemsSource = bl.GetAllCustomer().Select(cu => cu.ID);

        }

        private void AddBottun_Click(object sender, RoutedEventArgs e)
        {
            BO.Parcel p = new Parcel()
            {
                Sender = new CustomerInParcel { ID = Convert.ToInt32(SenderIDComboBox.SelectedItem) },
                Receiver = new CustomerInParcel { ID = Convert.ToInt32(ReceiverIDComboBox.SelectedItem) },
                Weight=(WeightCategories)Convert.ToInt32(weightComboBox.SelectedItem),
                Priority = (BO.Priorities)Convert.ToInt32(priorityComboBox.SelectedItem),
            };
            try
            {
               if(priorityComboBox.SelectedIndex==-1|| weightComboBox.SelectedIndex == -1 || ReceiverIDComboBox.SelectedIndex == -1 || SenderIDComboBox.SelectedIndex == -1 )
                {
                    MessageBox.Show("You did not fill in all the details", "ERROR", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    bl.AddParcel(p);
                    MessageBox.Show("add parcel sucsess", "ADD OPTION", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                 

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

        private void iDTextBlock1_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            if(iDTextBlock1.Text!=null)
            {
                showDrone.IsEnabled = true;
            }
            else
            {
                DeleteBottun.IsEnabled=true;
            }
        }

        private void grid2_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            if(priorityComboBox.SelectedIndex!=-1&&weightComboBox.SelectedIndex!=-1&&ReceiverIDComboBox.SelectedIndex!=-1&&SenderIDComboBox.SelectedIndex!=-1)
            {
                AddBottun.IsEnabled = true;
            }
        }

        private void grid2_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (priorityComboBox.SelectedIndex != -1 && weightComboBox.SelectedIndex != -1 && ReceiverIDComboBox.SelectedIndex != -1 && SenderIDComboBox.SelectedIndex != -1)
            {
                AddBottun.IsEnabled = true;
            }
        }
    }
}
