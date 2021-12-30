using System;
using BlApi;
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
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for DroneToList.xaml
    /// </summary>
    public partial class DronesListWindow : Window
    {
        
        IBL bl=BlFactory.GetBl();
        public DronesListWindow(IBL blObject)
        {
            InitializeComponent();
            bl = blObject;

            //droneToListListView.ItemsSource = bl.GetAllDrones();
            droneToListListView.DataContext = bl.GetAllDrones();
            StatusSelector.ItemsSource = Enum.GetValues(typeof(DroneConditions));
            WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories)); 
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new Drone(bl).ShowDialog();
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
            if (WeightSelector.SelectedItem==null && StatusSelector.SelectedItem == null)
            {
                //droneToListListView.ItemsSource = bl.GetAllDrones();
                droneToListListView.DataContext = bl.GetAllDrones();
            }
            if (WeightSelector.SelectedIndex != -1 && StatusSelector.SelectedIndex != -1)
                droneToListListView.DataContext = bl.GetAllDrones(dro => dro.MaxWeight == (BO.WeightCategories)WeightSelector.SelectedIndex && dro.Conditions == (BO.DroneConditions)StatusSelector.SelectedIndex);
            else if (WeightSelector.SelectedIndex != -1 && StatusSelector.SelectedIndex == -1)
                droneToListListView.DataContext = bl.GetAllDrones(dro => dro.MaxWeight == (BO.WeightCategories)WeightSelector.SelectedIndex);
            else if (WeightSelector.SelectedIndex == -1 && StatusSelector.SelectedIndex != -1)
                droneToListListView.DataContext = bl.GetAllDrones(dro => dro.Conditions == (BO.DroneConditions)StatusSelector.SelectedIndex);
            
               
        }
        private void droneToListListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DroneToList d = (DroneToList)droneToListListView.SelectedItem;
            new Drone(d, bl).ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            StatusSelector.SelectedItem = null;
            WeightSelector.SelectedItem = null;
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            FilterByCombiBox();
        }
    }
}
