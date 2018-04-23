// Developer Express Code Central Example:
// How to serialize custom properties with a custom type using the CreateContentPropertyValue event
// 
// This example demonstrates how to serialize and deserialize custom properties
// with a custom type. If a custom property is null when the deserialization
// process is invoked, it's necessary to handle the
// DXSerializer.CreateContentPropertyValue event. In the CreateContentPropertyValue
// event handler, create a new instance of a custom type and assign it to the
// XtraCreateContentPropertyValueEventArgs.SomeCustomProperty property.
// 
// You can find sample updates and versions for different programming languages here:
// http://www.devexpress.com/example=T159099

using DevExpress.Data;
using DevExpress.Utils.Serializing;
using DevExpress.Utils.Serializing.Helpers;
using DevExpress.Xpf.Core.Serialization;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace WpfApplication58
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ObservableCollection<Customer> customers = new ObservableCollection<Customer>();
            for (int i = 1; i < 30; i++)
            {
                customers.Add(new Customer() { ID = i, Name = "Name" + i });
            }
            grid.ItemsSource = customers;
            grid.Columns["Name"].AddHandler(DXSerializer.CreateCollectionItemEvent, new XtraCreateCollectionItemEventHandler(OnCreateCollectionItem));
            grid.Columns["Name"].AddHandler(DXSerializer.ClearCollectionEvent, new XtraItemRoutedEventHandler(OnClearCollectionItem));
            

            nameColumn.SomeCollection = new ObservableCollection<CustomObject>();
        }
        void OnClearCollectionItem(object sender, XtraItemRoutedEventArgs e)
        {
            ((ObservableCollection<CustomObject>)e.Collection).Clear();
        }

        void OnCreateCollectionItem(object sender, XtraCreateCollectionItemEventArgs e)
        {
            CustomObject item = new CustomObject();
            ((ObservableCollection<CustomObject>)e.Collection).Add(item);
            e.CollectionItem = item;
        }
        private void loadButton_Click(object sender, RoutedEventArgs e)
        {
            grid.RestoreLayoutFromXml("..\\..\\layout.xml");
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            grid.SaveLayoutToXml("..\\..\\layout.xml");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            grid.RestoreLayoutFromXml("..\\..\\layout.xml");
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            CustomObject item = new CustomObject() { ItemID = propATextBox.Text, ItemValue = propBTextBox.Text };
            nameColumn.SomeCollection.Add(item);
        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            nameColumn.SomeCollection.Clear();
        }
    }

    public class MyGridColumn : GridColumn
    {
        [XtraSerializableProperty(XtraSerializationVisibility.Collection, true, false, true)]
        public ObservableCollection<CustomObject> SomeCollection
        {
            get { return (ObservableCollection<CustomObject>)GetValue(SomeCollectionProperty); }
            set { SetValue(SomeCollectionProperty, value); }
        }

        public static readonly DependencyProperty SomeCollectionProperty =
DependencyProperty.Register("SomeCollection", typeof(ObservableCollection<CustomObject>), typeof(MyGridColumn), null);


    }
    public class CustomObject : INotifyPropertyChanged
    {
        string itemID;
        string itemValue;
        [XtraSerializableProperty]
        public string ItemID
        {
            get
            {
                return itemID;
            }
            set
            {
                itemID = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("ItemID"));
            }
        }
        [XtraSerializableProperty]
        public string ItemValue
        {
            get
            {
                return itemValue;
            }
            set
            {
                itemValue = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("PropertyB"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }


    public class Customer
    {
        public int ID
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }
    }
}
