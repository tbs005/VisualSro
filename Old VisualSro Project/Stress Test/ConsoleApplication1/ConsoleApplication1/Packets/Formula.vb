﻿Module Formula
    Public Function CalculateDistance(ByVal Pos_1 As Position, ByVal Pos_2 As Position) As Double
        'Get Real Cords
        Dim Pos1X As Double = GetRealX(Pos_1.XSector, Pos_1.X)
        Dim Pos1Y As Double = GetRealY(Pos_1.YSector, Pos_1.Y)
        Dim Pos2X As Double = GetRealX(Pos_2.XSector, Pos_2.X)
        Dim Pos2Y As Double = GetRealY(Pos_2.YSector, Pos_2.Y)

        Dim distance_x As Double = Pos1X - Pos2X
        Dim distance_y As Double = Pos1Y - Pos2Y

        Dim x As Double = 0
        If distance_x < 0 And distance_x <> 0 Then
            x = distance_x * -1
        Else
            x = distance_x
        End If
        If distance_y < 0 And distance_y <> 0 Then
            Return ((distance_y * -1) + x)
        End If

        Return x + distance_y
    End Function

#Region "Pos Help Functions"
    Public Function GetRealX(ByVal XSec As Byte, ByVal XPos As Single) As Single
        Return (XSec - 135) * 192 + XPos / 10
    End Function

    Public Function GetRealY(ByVal YSec As Byte, ByVal YPos As Single) As Single
        Return (YSec - 92) * 192 + YPos / 10
    End Function

    Public Function GetXSec(ByVal X As Single) As Single
        Return CSng(Math.Floor(CDbl(((X / 192.0!) + 135.0!))))
    End Function
    Public Function GetYSec(ByVal Y As Single) As Single
        Return CSng(Math.Floor(CDbl(((Y / 192.0!) + 92.0!))))
    End Function

    Public Function GetXOffset(ByVal X As Single) As Integer
        Return CInt(Math.Round(CDbl((((((X / 192.0!) - GetXSec(X)) + 135.0!) * 192.0!) * 10.0!))))
    End Function

    Public Function GetYOffset(ByVal Y As Single) As Integer
        Return CInt(Math.Round(CDbl((((((Y / 192.0!) - GetYSec(Y)) + 92.0!) * 192.0!) * 10.0!))))
    End Function
#End Region
End Module
