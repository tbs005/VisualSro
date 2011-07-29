﻿Namespace GameServer.Functions
    Module GameMaster
        Public Sub OnGM(ByVal Packet As GameServer.PacketReader, ByVal Index_ As Integer)

            Dim tag As Byte = Packet.Word
            'Debug.Print("[GM][Tag:" & tag & "]")

            If PlayerData(Index_).GM = True Then
                Select Case tag
                    Case GmTypes.MakeMonster
                        OnCreateObject(Packet, Index_)

                    Case GmTypes.MakeItem  ' Create Item
                        OnGmCreateItem(Packet, Index_)

                    Case GmTypes.WayPoints 'Teleport
                        OnGmTeleport(Packet, Index_)

                    Case GmTypes.MoveToUser
                        OnMoveToUser(Packet, Index_)

                    Case GmTypes.RecallUser
                        OnRecallUser(Packet, Index_)

                    Case GmTypes.Ban
                        OnBanUser(Packet, Index_)

                    Case GmTypes.GoTown
                        OnGoTown(Index_)

                    Case GmTypes.ToTown
                        OnMoveUserToTown(Index_, Packet)

                    Case GmTypes.KillMob
                        OnKillObject(Packet, Index_)

                    Case GmTypes.Invincible
                        OnInvincible(Index_)

                    Case GmTypes.Invisible
                        OnInvisible(Index_)
                End Select
            Else
                Server.Dissconnect(Index_)

                Log.WriteGameLog(Index_, "GM", "Unautorized", "Gm_Command_Try:" & tag)
                'Hack Versuch
            End If
        End Sub

        Public Sub OnGmCreateItem(ByVal packet As PacketReader, ByVal index_ As Integer)
            Dim pk2id As UInteger = packet.DWord
            Dim plus As Byte = packet.Byte  'Or Count
            Dim slot As Byte = GetFreeItemSlot(index_)

            If slot <> -1 Then
                Dim temp_item As cInvItem = Inventorys(index_).UserItems(slot)
                temp_item.Pk2Id = pk2id
                temp_item.OwnerCharID = PlayerData(index_).CharacterId


                Dim refitem As cItem = GetItemByID(pk2id)
                If refitem.CLASS_A = 1 Then
                    'Equip
                    temp_item.Plus = plus
                    temp_item.Durability = refitem.MAX_DURA
                    temp_item.Blues = New List(Of cInvItem.sBlue)
                ElseIf refitem.CLASS_A = 2 Then
                    'Pet

                ElseIf refitem.CLASS_A = 3 Then
                    'Etc
                    temp_item.Amount = plus
                End If

                Inventorys(index_).UserItems(slot) = temp_item
                UpdateItem(Inventorys(index_).UserItems(slot)) 'SAVE IT

                Dim writer As New PacketWriter
                writer.Create(ServerOpcodes.ItemMove)
                writer.Byte(1)
                writer.Byte(6) 'type = new item
                writer.Byte(slot)

                AddItemDataToPacket(Inventorys(index_).UserItems(slot), writer)

                Server.Send(writer.GetBytes, index_)

                Debug.Print("[ITEM CREATE][Info][Slot:{0}][ID:{1}][Dura:{2}][Amout:{3}][Plus:{4}]", temp_item.Slot, temp_item.Pk2Id, temp_item.Durability, temp_item.Amount, temp_item.Plus)

                If Settings.Log_GM Then
                    Log.WriteGameLog(index_, "GM", "Item_Create", String.Format("Slot:{0}, ID:{1}, Dura:{2}, Amout:{3}, Plus:{4}", temp_item.Slot, temp_item.Pk2Id, temp_item.Durability, temp_item.Amount, temp_item.Plus))
                End If
            End If
        End Sub

        Public Sub OnGmTeleport(ByVal packet As PacketReader, ByVal index_ As Integer)

            Dim to_pos As New Position
            to_pos.XSector = packet.Byte
            to_pos.YSector = packet.Byte
            to_pos.X = packet.Float
            to_pos.Z = packet.Float
            to_pos.Y = packet.Float
            Dim Angle As UInt16 = packet.Word 'Not sure 

            PlayerData(index_).Position = to_pos
            PlayerData(index_).TeleportType = TeleportType_.GM


            DataBase.SaveQuery(String.Format("UPDATE characters SET xsect='{0}', ysect='{1}', xpos='{2}', zpos='{3}', ypos='{4}' where id='{5}'", PlayerData(index_).Position.XSector, PlayerData(index_).Position.YSector, Math.Round(PlayerData(index_).Position.X), Math.Round(PlayerData(index_).Position.Z), Math.Round(PlayerData(index_).Position.Y), PlayerData(index_).CharacterId))

            Dim writer As New PacketWriter
            writer.Create(ServerOpcodes.Teleport_Annonce)
            writer.Byte(PlayerData(index_).Position.XSector)
            writer.Byte(PlayerData(index_).Position.YSector)
            Server.Send(writer.GetBytes, index_)

        End Sub
        Public Sub OnSetWeather(ByVal Type As Byte, ByVal Strength As Byte)
            Dim writer As New PacketWriter
            writer.Create(ServerOpcodes.Weather)
            writer.Byte(Type)
            writer.Byte(Strength)
            Server.SendToAllIngame(writer.GetBytes)
        End Sub

        Private Sub OnMoveToUser(ByVal packet As PacketReader, ByVal index_ As Integer)
            Dim NameLength As Byte = packet.Word
            Dim Name As String = packet.String(NameLength)

            For i As Integer = 0 To Server.MaxClients
                If PlayerData(i) IsNot Nothing Then
                    If PlayerData(i).CharacterName = Name Then

                        PlayerData(index_).Position = PlayerData(i).Position

                        DataBase.SaveQuery(String.Format("UPDATE characters SET xsect='{0}', ysect='{1}', xpos='{2}', zpos='{3}', ypos='{4}' where id='{5}'", PlayerData(index_).Position.XSector, PlayerData(index_).Position.YSector, Math.Round(PlayerData(index_).Position.X), Math.Round(PlayerData(index_).Position.Z), Math.Round(PlayerData(index_).Position.Y), PlayerData(index_).CharacterId))

                        OnTeleportUser(index_, PlayerData(index_).Position.XSector, PlayerData(index_).Position.YSector)

                        Exit For
                    End If
                End If
            Next

        End Sub

        Private Sub OnRecallUser(ByVal packet As PacketReader, ByVal index_ As Integer)
            Dim NameLength As UInt16 = packet.Word
            Dim Name As String = packet.String(NameLength)

            For i As Integer = 0 To Server.MaxClients
                If PlayerData(i) IsNot Nothing Then
                    If PlayerData(i).CharacterName = Name Then

                        PlayerData(i).Position = PlayerData(index_).Position

                        DataBase.SaveQuery(String.Format("UPDATE characters SET xsect='{0}', ysect='{1}', xpos='{2}', zpos='{3}', ypos='{4}' where id='{5}'", PlayerData(i).Position.XSector, PlayerData(i).Position.YSector, Math.Round(PlayerData(i).Position.X), Math.Round(PlayerData(i).Position.Z), Math.Round(PlayerData(i).Position.Y), PlayerData(i).CharacterId))

                        OnTeleportUser(i, PlayerData(i).Position.XSector, PlayerData(i).Position.YSector)
                        Exit For
                    End If
                End If
            Next
        End Sub

        Public Sub OnBanUser(ByVal Packet As PacketReader, ByVal index_ As Integer)
            Dim NameLength As UInt16 = Packet.Word
            Dim Name As String = Packet.String(NameLength)

            For i As Integer = 0 To GameDB.Chars.Length - 1
                If GameDB.Chars(i).CharacterName = Name Then
                    DataBase.InsertData(String.Format("UPDATE users SET banned='1', bantime = '3000-01-01 00:00:00', banreason = 'You got banned by: {0}' where id='{1}'", PlayerData(index_).CharacterName, GameDB.Chars(i).AccountID))

                    For U = 0 To GameDB.Users.Count - 1
                        If GameDB.Users(U).Id = GameDB.Chars(i).AccountID Then
                            GameDB.Users(U).Banned = True
                        End If
                    Next

                    SendPm(index_, "User got banned.", "[SERVER]")
                    Exit For
                End If
            Next

            For i As Integer = 0 To Server.OnlineClient
                If PlayerData(i) IsNot Nothing Then
                    If PlayerData(i).CharacterName = Name Then
                        Server.Dissconnect(i)
                    End If
                End If
            Next i

            If Settings.Log_GM Then
                Log.WriteGameLog(index_, "GM", "Ban", String.Format("Banned User:" & Name))
            End If

        End Sub

        Public Sub OnGoTown(ByVal Index_ As Integer)

            'Teleport the GM to Town
            PlayerData(Index_).Position = Functions.PlayerData(Index_).Position_Return 'Set new Pos
            DataBase.SaveQuery(String.Format("UPDATE characters SET xsect='{0}', ysect='{1}', xpos='{2}', zpos='{3}', ypos='{4}' where id='{5}'", PlayerData(Index_).Position.XSector, PlayerData(Index_).Position.YSector, Math.Round(PlayerData(Index_).Position.X), Math.Round(PlayerData(Index_).Position.Z), Math.Round(PlayerData(Index_).Position.Y), PlayerData(Index_).CharacterId))
            OnTeleportUser(Index_, PlayerData(Index_).Position.XSector, PlayerData(Index_).Position.YSector)

        End Sub

        Public Sub OnMoveUserToTown(ByVal Index_ As Integer, ByVal packet As PacketReader)

            Dim NameLength As UInt16 = packet.Word
            Dim Name As String = packet.String(NameLength)

            For i As Integer = 0 To Server.MaxClients
                If PlayerData(i) IsNot Nothing Then
                    If PlayerData(i).CharacterName = Name Then
                        OnGoTown(i)
                    End If
                End If
            Next
        End Sub

        Public Sub OnCreateObject(ByVal Packet As PacketReader, ByVal Index_ As Integer)
            Dim objectid As UInteger = Packet.DWord
            Dim type As Byte = Packet.Byte
            Dim refobject As Object_ = GetObjectById(objectid)

            Dim selector As String = refobject.TypeName.Substring(0, 3)

            Select Case selector
                Case "MOB"
                    SpawnMob(objectid, type, PlayerData(Index_).Position, 0, -1)
                Case "NPC"
                    SpawnNPC(objectid, PlayerData(Index_).Position, 0)
            End Select

            If Settings.Log_GM Then
                Log.WriteGameLog(Index_, "GM", "Monster_Spawn", String.Format("PK2ID: {0}, Monster_Name: {1} Type: {2}", objectid, refobject.TypeName, type))
            End If
        End Sub

        Public Sub OnKillObject(ByVal Packet As PacketReader, ByVal Index_ As Integer)
            Dim unique_Id As UInteger = Packet.DWord


            For Each key In MobList.Keys.ToList
                If MobList.ContainsKey(key) Then
                    Dim Mob_ As cMonster = MobList.Item(key)
                    If Mob_.UniqueID = unique_Id Then
                        MobAddDamageFromPlayer(Mob_.HP_Cur, Index_, Mob_.UniqueID, False)
                        GetEXPFromMob(Mob_)
                        KillMob(Mob_.UniqueID)
                    End If
                End If
            Next
        End Sub

        Public Sub OnInvincible(ByVal Index_ As Integer)
            If PlayerData(Index_).Invincible = False Then
                PlayerData(Index_).Invincible = True
            Else
                PlayerData(Index_).Invincible = False
            End If
        End Sub

        Public Sub OnInvisible(ByVal Index_ As Integer)
            If PlayerData(Index_).Invisible = False Then
                PlayerData(Index_).Invisible = True
                DespawnPlayer(Index_)
                UpdateState(4, 4, Index_)
            Else
                PlayerData(Index_).Invisible = False
                UpdateState(4, 13, Index_)
            End If
        End Sub

        Enum GmTypes
            FindUser = 1
            GoTown = 2
            ToTown = 3
            MakeMonster = 6
            MakeItem = 7
            MoveToUser = 8
            Ban = 13
            Invisible = 14
            Invincible = 15
            WayPoints = 16
            RecallUser = 17
            KillMob = 20
            MoveToNpc = 49
        End Enum

    End Module
End Namespace