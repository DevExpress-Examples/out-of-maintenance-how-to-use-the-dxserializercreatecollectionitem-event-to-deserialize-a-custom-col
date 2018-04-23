' Developer Express Code Central Example:
' How to serialize custom properties with a custom type using the CreateContentPropertyValue event
' 
' This example demonstrates how to serialize and deserialize custom properties
' with a custom type. If a custom property is null when the deserialization
' process is invoked, it's necessary to handle the
' DXSerializer.CreateContentPropertyValue event. In the CreateContentPropertyValue
' event handler, create a new instance of a custom type and assign it to the
' XtraCreateContentPropertyValueEventArgs.SomeCustomProperty property.
' 
' You can find sample updates and versions for different programming languages here:
' http://www.devexpress.com/example=T159099

Imports DevExpress.Data
Imports DevExpress.Utils.Serializing
Imports DevExpress.Utils.Serializing.Helpers
Imports DevExpress.Xpf.Core.Serialization
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.Grid
Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports System.Windows.Shapes

Namespace WpfApplication58
    ''' <summary>
    ''' Interaction logic for MainWindow.xaml
    ''' </summary>
    Partial Public Class MainWindow
        Inherits Window

        Public Sub New()
            InitializeComponent()

            Dim customers As New ObservableCollection(Of Customer)()
            For i As Integer = 1 To 29
                customers.Add(New Customer() With {.ID = i, .Name = "Name" & i})
            Next i
            grid.ItemsSource = customers
            grid.Columns("Name").AddHandler(DXSerializer.CreateCollectionItemEvent, New XtraCreateCollectionItemEventHandler(AddressOf OnCreateCollectionItem))
            grid.Columns("Name").AddHandler(DXSerializer.ClearCollectionEvent, New XtraItemRoutedEventHandler(AddressOf OnClearCollectionItem))


            nameColumn.SomeCollection = New ObservableCollection(Of CustomObject)()
        End Sub
        Private Sub OnClearCollectionItem(ByVal sender As Object, ByVal e As XtraItemRoutedEventArgs)
            CType(e.Collection, ObservableCollection(Of CustomObject)).Clear()
        End Sub

        Private Sub OnCreateCollectionItem(ByVal sender As Object, ByVal e As XtraCreateCollectionItemEventArgs)
            Dim item As New CustomObject()
            CType(e.Collection, ObservableCollection(Of CustomObject)).Add(item)
            e.CollectionItem = item
        End Sub
        Private Sub loadButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            grid.RestoreLayoutFromXml("..\..\layout.xml")
        End Sub

        Private Sub saveButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            grid.SaveLayoutToXml("..\..\layout.xml")
        End Sub

        Private Sub Window_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            grid.RestoreLayoutFromXml("..\..\layout.xml")
        End Sub

        Private Sub addButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim item As New CustomObject() With {.ItemID = propATextBox.Text, .ItemValue = propBTextBox.Text}
            nameColumn.SomeCollection.Add(item)
        End Sub

        Private Sub clearButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            nameColumn.SomeCollection.Clear()
        End Sub
    End Class

    Public Class MyGridColumn
        Inherits GridColumn

        <XtraSerializableProperty(XtraSerializationVisibility.Collection, True, False, True)> _
        Public Property SomeCollection() As ObservableCollection(Of CustomObject)
            Get
                Return CType(GetValue(SomeCollectionProperty), ObservableCollection(Of CustomObject))
            End Get
            Set(ByVal value As ObservableCollection(Of CustomObject))
                SetValue(SomeCollectionProperty, value)
            End Set
        End Property

        Public Shared ReadOnly SomeCollectionProperty As DependencyProperty = DependencyProperty.Register("SomeCollection", GetType(ObservableCollection(Of CustomObject)), GetType(MyGridColumn), Nothing)


    End Class
    Public Class CustomObject
        Implements INotifyPropertyChanged


        Private itemID_Renamed As String

        Private itemValue_Renamed As String
        <XtraSerializableProperty> _
        Public Property ItemID() As String
            Get
                Return itemID_Renamed
            End Get
            Set(ByVal value As String)
                itemID_Renamed = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("ItemID"))
            End Set
        End Property
        <XtraSerializableProperty> _
        Public Property ItemValue() As String
            Get
                Return itemValue_Renamed
            End Get
            Set(ByVal value As String)
                itemValue_Renamed = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("PropertyB"))
            End Set
        End Property

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
        Public Sub RaisePropertyChanged(ByVal propertyName As String)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub
    End Class


    Public Class Customer
        Public Property ID() As Integer

        Public Property Name() As String
    End Class
End Namespace
