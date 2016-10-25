Public Module MathHelper
    ''Function to get random number

    Private syncLockObj = New Object()
    Public Function GetRandomNumber(min As Integer, max As Integer) As Integer
        Dim getrandom As Random = New Random()
        ''Lock(syncLockObj)
        Dim result = getrandom.Next(min, max)
        Return result
    End Function
End Module
