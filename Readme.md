<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128642457/13.1.7%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T163234)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [MainWindow.xaml](./CS/WpfApplication58/MainWindow.xaml) (VB: [MainWindow.xaml](./VB/WpfApplication58/MainWindow.xaml))
* [MainWindow.xaml.cs](./CS/WpfApplication58/MainWindow.xaml.cs) (VB: [MainWindow.xaml.vb](./VB/WpfApplication58/MainWindow.xaml.vb))
<!-- default file list end -->
# How to use the DXSerializer.CreateCollectionItem event to deserialize a custom collection


<p>The DXSerializer.CreateCollectionItem event is used to recreate items in a collection when this collection is deserialized. In this event handler, it's necessary to create a new item and add it to the deserialized collection. To deserialize item properties, it's necessary to assign this item to the XtraCreateCollectionItemEventArgs.CollectionItem property. The DXSerializer.CreateCollectionItem event is raised only if the XtraSerializableProperty.UseCreateItem property is "True".  </p>

<br/>


