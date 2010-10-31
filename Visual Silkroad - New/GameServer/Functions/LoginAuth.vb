﻿Namespace GameServer.Functions


    Module LoginAuth

        Public Sub GateWay(ByVal Index_ As Integer)
            Dim writer As New PacketWriter
            Dim name As String = "AgentServer"
            writer.Create(ServerOpcodes.ServerInfo)
            writer.Word(name.Length)
            writer.HexString(name)
            writer.Byte(0)
            GameServer.Server.Send(writer.GetBytes, Index_)
        End Sub

        Public Sub SendPatchInfo(ByVal index_ As Integer)

            'Note: Patch Info for Rsro


            GameServer.Server.Send(New Byte() {5, 0, 13, 96, 0, 0, 1, 1, 0, 5, 32, _
                                                11, 0, 13, 96, 0, 0, 0, 1, 0, 1, 16, 10, 5, 0, 0, 0, 2, _
                                                5, 0, 13, 96, 0, 0, 1, 1, 0, 5, 96, _
                                                6, 0, 13, 96, 0, 0, 0, 3, 0, 2, 0, 2}, index_)


        End Sub

        Public Sub CheckLogin(ByVal index_ As Integer, ByVal packet As PacketReader)
            Dim key As UInteger = packet.DWord()
            Dim name As String = packet.String(packet.Word)
            Dim password As String = packet.String(packet.Word)

            'Checking
            Dim UserIndex As Integer = DatabaseCore.GetUserWithAccName(name)
            Dim sock As Net.Sockets.Socket = GameServer.ClientList.GetSocket(index_)
            Dim endpoint = sock.RemoteEndPoint
            Dim split1 As String() = endpoint.ToString.Split(":")
            Dim split2 As String() = split1(0).Split(".")
            Dim realkey As UInt32 = CUInt(split2(0)) + CUInt(split2(1)) + CUInt(split2(2)) + CUInt(split2(3))
            Dim loggedin As Boolean = False

            For i = 0 To Server.MaxClients
                If ClientList.OnCharListing(i) IsNot Nothing Then
                    If ClientList.OnCharListing(i).LoginInformation.Id = GameServer.DatabaseCore.Users(UserIndex).Id Then
                        loggedin = True
                    End If
                End If
            Next

            For i = 0 To Server.MaxClients
                If PlayerData(i) IsNot Nothing Then
                    If PlayerData(i).AccountID = GameServer.DatabaseCore.Users(UserIndex).Id Then
                        loggedin = True
                    End If
                End If
            Next

      
            Dim writer As New PacketWriter
            writer.Create(ServerOpcodes.LoginAuthInfo)

            If loggedin = True Or GameServer.DatabaseCore.Users(UserIndex).Banned = True Then
                writer.Byte(2)
                writer.Byte(2) 'Server Full aka User is already logged in
                GameServer.Server.Send(writer.GetBytes, index_)
                GameServer.Server.Dissconnect(index_)
            ElseIf GameServer.DatabaseCore.Users(UserIndex).Name = name And GameServer.DatabaseCore.Users(UserIndex).Pw = password And key = realkey Then
                writer.Byte(1)
                GameServer.Server.Send(writer.GetBytes, index_)
                GameServer.ClientList.OnCharListing(index_) = New cCharListing
                GameServer.ClientList.OnCharListing(index_).LoginInformation = New cCharListing.UserArray
                GameServer.ClientList.OnCharListing(index_).LoginInformation = GameServer.DatabaseCore.Users(UserIndex)
                GameServer.ClientList.OnCharListing(index_).LoginInformation.LoggedIn = True
            ElseIf GameServer.DatabaseCore.Users(UserIndex).Name <> name Or GameServer.DatabaseCore.Users(UserIndex).Pw <> password Or key <> realkey Then
                writer.Byte(2)
                writer.Byte(2)
                GameServer.Server.Send(writer.GetBytes, index_)
                GameServer.Server.Dissconnect(index_)
            End If



        End Sub
    End Module
End Namespace
