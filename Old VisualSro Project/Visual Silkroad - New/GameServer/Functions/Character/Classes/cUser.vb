﻿Namespace Functions
    Public Class cUser
        Public Id As Integer = 0
        Public Name As String = ""
        Public Pw As String = ""
        Public FailedLogins As Byte = 0
        Public Banned As Boolean = False
        Public LoggedIn As Boolean = False
        Public Permission As Byte = 0 '0x00 = normal user, 0x01 = prefered access to the server (premium), 0x02 = gm, 0x03 = admin

        Public Silk As UInteger = 0
        Public Silk_Bonus As UInteger = 0
        Public Silk_Points As UInteger = 0
    End Class
End Namespace