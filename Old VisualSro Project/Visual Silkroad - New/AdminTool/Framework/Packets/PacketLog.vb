﻿Namespace Log
    Module PacketLog

        Public Sub LogPacket(ByVal buffer As Byte(), ByVal FromServer As Boolean)
            Try
                Dim length As Integer = BitConverter.ToUInt16(buffer, 0)
                Dim op As UInteger = BitConverter.ToUInt16(buffer, 2)


                If FromServer = False Then
                    Log.WriteSystemLog("C --> S (" & (op) & ")" & BitConverter.ToString(buffer, 6, length))
                ElseIf FromServer = True Then
                    Log.WriteSystemLog("S --> C (" & (op) & ")" & BitConverter.ToString(buffer, 6, length))
                End If
            Catch ex As Exception

            End Try



        End Sub
    End Module
End Namespace
