''' <summary>
''' A path consists with blocks
''' </summary>
Public Class MapPath
    Implements ICloneable(Of MapPath)
    Public Property Path As List(Of IMapBlock)
    Public Property EndPointFound As Boolean
    Public Property DeadEnded As Boolean

    ''' <summary>
    ''' Clones the path, the blocks in the path are referenced.
    ''' </summary>
    ''' <returns>Cloned path</returns>
    Public Function Clone() As MapPath Implements ICloneable(Of MapPath).Clone
        Dim clonedPath = New MapPath()
        clonedPath.Path = New List(Of IMapBlock)(Path)
        Return clonedPath
    End Function
End Class
