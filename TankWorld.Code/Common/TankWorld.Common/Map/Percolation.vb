Imports TankWorld.UnionFind

<Obsolete("Please use the F# code", True)>
Public Class Percolation_VB
    Dim uf As WeightedQuickUnion
    Dim width As Integer
    Dim height As Integer
    Dim openSites() As Boolean

    ''' <summary>
    ''' Creates a rectangular with width * height, the first block is  (0,0)
    ''' </summary>
    ''' <param name="width">the width of the blocks, starting from 0</param>
    ''' <param name="height">the height of the blocks, starting from 0</param>
    Public Sub New(width As Integer, height As Integer)
        If (width <= 0 Or height <= 0) Then
            Throw New ArgumentOutOfRangeException()
        End If

        Me.width = width
        Me.height = height
        uf = New WeightedQuickUnion(width * height)

        openSites = New Boolean(width * height) {}
    End Sub

    ''' <summary>
    ''' true if the site is open
    ''' </summary>
    ''' <param name="x">the x value</param>
    ''' <param name="y">the y value</param>
    ''' <returns>true if the site is open</returns>
    Public Function isOpen(x As Integer, y As Integer) As Boolean
        Return openSites(x + y * Me.width)
    End Function

    Private Function GetOneDimensionIndex(x As Integer, y As Integer) As Integer
        Return x + y * Me.width
    End Function

    ''' <summary>
    ''' Open site x, y
    ''' </summary>
    ''' <param name="x">x</param>
    ''' <param name="y">y</param>
    Public Sub Open(x As Integer, y As Integer)
        ValidateHeight(y)
        ValidateWidth(x)

        If openSites(x + y * Me.width) Then
            Return
        End If
        openSites(x + y * Me.width) = True

        If x <> 0 Then
            UnionIfOpen(x + y * Me.width, x - 1 + y * Me.width)
        End If
        If x <> Me.width - 1 Then
            UnionIfOpen(x + y * Me.width, x + 1 + y * Me.width)
        End If

        If y <> 0 Then
            UnionIfOpen(x + y * Me.width, x + (y - 1) * Me.width)
        End If

        If y <> Me.height - 1 Then
            UnionIfOpen(x + y * Me.width, x + (y + 1) * Me.width)
        End If

    End Sub

    Private Sub UnionIfOpen(p As Integer, q As Integer)
        If Not openSites(q) Then
            Return
        End If
        uf.Union(p, q)
    End Sub

    Private Sub ValidateWidth(index As Integer)
        If (index < 0 Or index >= Me.width) Then
            Throw New ArgumentOutOfRangeException("index", "index " & index & " is not between 0 and " & (Me.width - 1))
        End If
    End Sub

    Private Sub ValidateHeight(index As Integer)
        If (index < 0 Or index >= Me.height) Then
            Throw New ArgumentOutOfRangeException("index", "index " & index & " is not between 0 and " & (Me.height - 1))
        End If
    End Sub

    ''' <summary>
    ''' true if point(x1, y1) and point(x2, y2) are connected
    ''' </summary>
    ''' <param name="x1">x value of the first point</param>
    ''' <param name="y1">y value of the first point</param>
    ''' <param name="x2">x value of the second point</param>
    ''' <param name="y2">y value of the second point</param>
    ''' <returns>true if point(x1, y1) and point(x2, y2) are connected</returns>
    Public Function Connected(x1 As Integer, y1 As Integer, x2 As Integer, y2 As Integer) As Boolean
        Return uf.Connected(x1 + y1 * width, x2 + y2 * width)
    End Function

End Class
