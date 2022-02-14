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
    public  enum  TheNumberOfFreeeSlot {zero,one,two,three,four,five, Six,Seven,Eight,Nine,Ten};
    /// <summary>
    /// Interaction logic for ListView.xaml
    /// </summary>
    public partial class ListView : Window
    {
        IBL bl;
        public ListView(IBL bL)
        {

            InitializeComponent();
            bl = bL;
            listOfDrones.ItemsSource = bl.GetAllDrones();
            StatusSelector.ItemsSource = Enum.GetValues(typeof(DroneConditions));
            WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));

            listOfBaseStation.ItemsSource = bl.GetAllBaseStation();
            FreeSlot.ItemsSource = Enum.GetValues(typeof(TheNumberOfFreeeSlot));
            listOfCostumer.ItemsSource = bl.GetAllCustomer();

            listOfParcel.ItemsSource = bl.GetAllParcels();
            StatusParcelSelector.ItemsSource = Enum.GetValues(typeof(Situations));
        }

        #region Drone
        public void FilterByCombiBoxOfDrone()
        {
            if (WeightSelector.SelectedItem == null && StatusSelector.SelectedItem == null)
            {
                //droneToListListView.ItemsSource = bl.GetAllDrones();
                listOfDrones.ItemsSource = bl.GetAllDrones();
            }
            if (WeightSelector.SelectedIndex != -1 && StatusSelector.SelectedIndex != -1)
                listOfDrones.ItemsSource = bl.GetAllDrones(dro => dro.MaxWeight == (BO.WeightCategories)WeightSelector.SelectedIndex && dro.Conditions == (BO.DroneConditions)StatusSelector.SelectedIndex);
            else if (WeightSelector.SelectedIndex != -1 && StatusSelector.SelectedIndex == -1)
                listOfDrones.ItemsSource = bl.GetAllDrones(dro => dro.MaxWeight == (BO.WeightCategories)WeightSelector.SelectedIndex);
            else if (WeightSelector.SelectedIndex == -1 && StatusSelector.SelectedIndex != -1)
                listOfDrones.ItemsSource = bl.GetAllDrones(dro => dro.Conditions == (BO.DroneConditions)StatusSelector.SelectedIndex);
        }

        private void Clear1_Click(object sender, RoutedEventArgs e)
        {
            StatusSelector.SelectedItem = null;
        }
        private void Clear2_Click(object sender, RoutedEventArgs e)
        {
            WeightSelector.SelectedItem = null;
        }
        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterByCombiBoxOfDrone();
        }

        private void WeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterByCombiBoxOfDrone();

        }
        private void listOfDrones_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DroneToList d = (DroneToList)listOfDrones.SelectedItem;
            new Drone(d, bl).ShowDialog();
        }
        #endregion
        #region BaseStation

        public void FilterByCombiBoxOfBaseStation()
        {
            if (FreeSlot.SelectedItem == null)
                listOfBaseStation.ItemsSource = bl.GetAllBaseStation();
            else
                listOfBaseStation.ItemsSource = bl.GetAllBaseStation(bases => bases.FreeChargingSlots == (int)(TheNumberOfFreeeSlot)FreeSlot.SelectedIndex);
        }
        private void listOfbaseStation_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BaseStationToList bs = (BaseStationToList)listOfBaseStation.SelectedItem;
            new BaseStationwindow(bs, bl).ShowDialog();
        }
        private void Clear3_Click(object sender, RoutedEventArgs e)
        {
            FreeSlot.SelectedItem = null;
        }
        private void FreeSlot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterByCombiBoxOfBaseStation();
        }
        #endregion
        #region Parcel
        public void FilterByCombiBoxOfParcel()
        {
            if (DATEcombobox.SelectedDate == null && StatusParcelSelector.SelectedItem == null)
            {
                listOfParcel.ItemsSource = bl.GetAllParcels();
            }
            if (DATEcombobox.SelectedDate != null && StatusParcelSelector.SelectedIndex != -1)
                listOfParcel.ItemsSource = bl.GetAllParcels(dro =>dro.ParcelCondition == (BO.Situations)StatusParcelSelector.SelectedIndex,DATEcombobox.SelectedDate);
            else if (DATEcombobox.SelectedDate != null && StatusParcelSelector.SelectedIndex == -1)
                listOfParcel.ItemsSource = bl.GetAllParcels(null,DATEcombobox.SelectedDate);
            else if (DATEcombobox.SelectedDate == null && StatusParcelSelector.SelectedIndex != -1)
                listOfParcel.ItemsSource = bl.GetAllParcels(dro => dro.ParcelCondition == (BO.Situations)StatusParcelSelector.SelectedIndex);
        }
        private void ClearP1_Click(object sender, RoutedEventArgs e)
        {
            StatusParcelSelector.SelectedItem = null;
        }

        private void ClearP2_Click(object sender, RoutedEventArgs e)
        {
            DATEcombobox.SelectedDate = null;
        }
        private void StatusParcelSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterByCombiBoxOfParcel();
        }
        private void DATEcombobox_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterByCombiBoxOfParcel();
        }
        private void listOfParcel_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ParcelToList p = (ParcelToList)listOfParcel.SelectedItem;
            new ParcelWindow(p.ID, bl).ShowDialog();
        }
        #endregion
        private void Badd_Click(object sender, RoutedEventArgs e)
        {
            switch (TCview.SelectedIndex)
            {
                case 0:
                    new Drone(bl).ShowDialog();
                    break;
                case 1:
                   new BaseStationwindow(bl).ShowDialog();
                   break;
                case 2:
                   new CostumerWindow(bl).ShowDialog();
                   break;
                case 3:
                    new ParcelWindow(bl).ShowDialog();
                    break;
                default:
                    MessageBox.Show("Choose One of The following Possibilities", "ERROR", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    break;
            }
        }

        private void listViewWindow_Activated(object sender, EventArgs e)
        {
            FilterByCombiBoxOfDrone();
            FilterByCombiBoxOfBaseStation();
            FilterByCombiBoxOfParcel();
            listOfCostumer.ItemsSource = bl.GetAllCustomer();
        }
        private void listOfDrones_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void listOfBaseStation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
        private void TCview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void TCview_ColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            IEnumerable<IGrouping<BO.DroneConditions, DroneToList>> droneGroup = from drone in bl.GetAllDrones() group drone by drone.Conditions;
            List<DroneToList> droneList = new();

            foreach (var group in droneGroup)
            {
                foreach (var drone in group)
                {
                    droneList.Add(drone);
                }
            }
            listOfDrones.ItemsSource = droneList;
        }

        private void groupParcels_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<IGrouping<int, ParcelToList>> parcelGroup = from parcel in bl.GetAllParcels() group parcel by parcel.SenderID;
            List<ParcelToList> parcels = new();

            foreach (var group in parcelGroup)
            {
                foreach (var parcel in group)
                {
                    parcels.Add(parcel);
                }
            }
            listOfParcel.ItemsSource = parcels;
        }

        private void groupStation_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<IGrouping<int, BaseStationToList>> StationGroup = from station in bl.GetAllBaseStation() group station by station.FreeChargingSlots;
            List<BaseStationToList> BaseStationList = new();

            foreach (var group in StationGroup)
            {
                foreach (var station in group)
                {
                    BaseStationList.Add(station);
                }
            }
            listOfBaseStation.ItemsSource = BaseStationList;
        }
        private void groupParcels1_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<IGrouping<int, ParcelToList>> parcelGroup = from parcel in bl.GetAllParcels() group parcel by parcel.RecieverID;
            List<ParcelToList> parcels = new();

            foreach (var group in parcelGroup)
            {
                foreach (var parcel in group)
                {
                    parcels.Add(parcel);
                }
            }
            listOfParcel.ItemsSource = parcels;
        }
        private void listOfCostumer_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.CustomerToList customer = (BO.CustomerToList)listOfCostumer.SelectedItem;
            new CostumerWindow(customer,bl).ShowDialog();
        }
    }
}
