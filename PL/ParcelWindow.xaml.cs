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
        public ParcelWindow(int id, BlApi.IBL blobject)
        {
            InitializeComponent();
            bl = blobject;
            addMode.Visibility = Visibility.Hidden;
            actMode.DataContext = bl.GetParcel(id);

        }
        public ParcelWindow(BlApi.IBL blobject)
        {
            InitializeComponent();
            actMode.Visibility = Visibility.Hidden;

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
    }
}
