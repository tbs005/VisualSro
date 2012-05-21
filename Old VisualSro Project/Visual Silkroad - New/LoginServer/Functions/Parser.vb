﻿Imports Framework
Imports LoginServer.Framework

Namespace Functions

    Public Module Parser
        Public Sub Parse(ByVal packet As PacketReader, ByVal Index_ As Integer)
            Dim length As UInteger = packet.Word
            Dim opcode As UInteger = packet.Word
            Dim security As UInteger = packet.Word

            ClientList.LastPingTime(Index_) = Date.Now

            Select Case opcode
                Case ClientOpcodes.Ping

                Case ClientOpcodes.Handshake_Confirm  'Client accepts

                Case ClientOpcodes.LOGIN_WHO_AM_I  'GateWay
                    GateWay(packet, Index_)

                Case ClientOpcodes.LOGIN_PATCH_REQ  'Client sends Patch Info
                    ClientInfo(packet, Index_)
                    SendPatchInfo(Index_)

                Case ClientOpcodes.LOGIN_LAUNCHER_REQ
                    SendLauncherInfo(Index_)

                Case ClientOpcodes.LOGIN_SERVER_LIST_REQ
                    SendServerList(Index_)

                Case ClientOpcodes.LOGIN_LOGIN_REQ
                    HandleLogin(packet, Index_)

                Case Else
                    Log.WriteSystemLog("opCode: " & opcode) '& " Packet : " & packet.Byte)
            End Select
        End Sub

        Public Sub ParseGlobalManager(ByVal packet As PacketReader)
            Dim length As UInteger = packet.Word
            Dim opcode As UInteger = packet.Word
            Dim security As UInteger = packet.Word

            Select Case opcode
                Case ClientOpcodes.Handshake_Confirm  'Client accepts
                    GlobalManager.OnHandshake(packet)

     

                Case Else
                    Log.WriteSystemLog("opCode: " & opcode) '& " Packet : " & packet.Byte)
            End Select
        End Sub

    End Module
End Namespace

