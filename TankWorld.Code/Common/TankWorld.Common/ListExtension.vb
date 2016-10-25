''' <summary>
''' Extends System.Collections.Generic.List`1.
''' </summary>
Public Module ListExtension
    ''' <summary>
    ''' Adds an object to the end of the System.Collections.Generic.List`1. If the object is null/nothing, do not add it.
    ''' </summary>
    ''' <typeparam name="T">The type of elements in the list.</typeparam>
    ''' <param name="aList">
    ''' Represents a strongly typed list of objects that can be accessed by index. Provides
    '''     methods to search, sort, and manipulate lists.
    ''' </param>
    ''' <param name="item">
    ''' The object to be added to the end of the System.Collections.Generic.List`1. 
    ''' The value cannot be null for reference types.
    '''</param>
    <Runtime.CompilerServices.Extension()>
    Public Sub AddWithoutNull(Of T)(aList As List(Of T), item As T)
        If item Is Nothing Then
            Return
        End If
        aList.Add(item)
    End Sub
End Module
