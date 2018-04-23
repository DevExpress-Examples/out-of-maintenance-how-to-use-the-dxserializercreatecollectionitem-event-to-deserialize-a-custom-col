# How to use the DXSerializer.CreateCollectionItem event to deserialize a custom collection


<p>The DXSerializer.CreateCollectionItem event is used to recreate items in a collection when this collection is deserialized. In this event handler, it's necessary to create a new item and add it to the deserialized collection. To deserialize item properties, it's necessary to assign this item to the XtraCreateCollectionItemEventArgs.CollectionItem property. The DXSerializer.CreateCollectionItem event is raised only if the XtraSerializableProperty.UseCreateItem property is "True".  </p>

<br/>


