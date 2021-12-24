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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace PL
{
    /// <summary>
    /// Interaction logic for DroneToList.xaml
    /// </summary>
    public partial class DronesListWindow : Window
    {
        
        IBL.IBL bl;
        public DronesListWindow(IBL.IBL blObject)
        {
            InitializeComponent();
            bl = blObject;
            droneToListListView.DataContext = bl.GetAllDrones();
            StatusSelector.ItemsSource = Enum.GetValues(typeof(DroneConditions));
            WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories)); 
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new Drone(bl, this).ShowDialog();
        }
        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterByCombiBox();
        }

        private void WeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterByCombiBox();
        }
         public void FilterByCombiBox()
         {

            if (WeightSelector.SelectedIndex != -1 && StatusSelector.SelectedIndex != -1)
                droneToListListView.ItemsSource = bl.GetAllDrones(dro => dro.MaxWeight == (IBL.BO.WeightCategories)WeightSelector.SelectedIndex && dro.Conditions == (IBL.BO.DroneConditions)StatusSelector.SelectedIndex);
            else if (WeightSelector.SelectedIndex != -1 && StatusSelector.SelectedIndex == -1)
                droneToListListView.ItemsSource = bl.GetAllDrones(dro => dro.MaxWeight == (IBL.BO.WeightCategories)WeightSelector.SelectedIndex);
            else if (WeightSelector.SelectedIndex == -1 && StatusSelector.SelectedIndex != -1)
                droneToListListView.ItemsSource = bl.GetAllDrones(dro => dro.Conditions == (IBL.BO.DroneConditions)StatusSelector.SelectedIndex);
            else
            {
                WeightSelector.SelectedIndex = -1;
                StatusSelector.SelectedIndex = -1;
                droneToListListView.ItemsSource = bl.GetAllDrones();
            }
               ;
        }
        private void droneToListListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DroneToList d = (DroneToList)droneToListListView.SelectedItem;
            new Drone(d, bl).ShowDialog();
        }
    }
}
