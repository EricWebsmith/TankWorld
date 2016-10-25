Imports System.Runtime.InteropServices

''' <summary>
''' Supports cloning, which creates a new instance of a class with the same value
'''     as an existing instance.
''' </summary>
''' <typeparam name="T">The type of elements to be cloned.</typeparam>
<ComVisible(True)>
Public Interface ICloneable(Of T)

    ''' <summary>
    ''' Creates a new object that is a copy of the current instance.
    ''' </summary>
    ''' <returns>A new object that is a copy of this instance.</returns>
    Function Clone() As T
End Interface

