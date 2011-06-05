﻿Namespace GameServer.Functions
    Module ItemSpawn

        Public ItemList As New Dictionary(Of UInteger, cItemDrop)

        Public Function CreateItemSpawnPacket(ByVal Item_ As cItemDrop) As Byte()
            Dim refitem As cItem = GetItemByID(Item_.Item.Pk2Id)
            Dim writer As New PacketWriter
            writer.Create(ServerOpcodes.SingleSpawn)
            writer.DWord(Item_.Item.Pk2Id)
            Select Case refitem.CLASS_A
                Case 1 'Equipment
                    writer.Byte(Item_.Item.Plus)
                Case 3 'Etc
                    If refitem.CLASS_B = 5 And refitem.CLASS_C = 0 Then
                        'Gold...
                        writer.DWord(Item_.Item.Amount)
                    End If
            End Select
            writer.DWord(Item_.UniqueID)
            writer.Byte(Item_.Position.XSector)
            writer.Byte(Item_.Position.YSector)
            writer.Float(Item_.Position.X)
            writer.Float(Item_.Position.Z)
            writer.Float(Item_.Position.Y)
            writer.Word(0) 'angle

            writer.Byte(0)
            ' writer.DWord(UInt32.MaxValue)
            writer.Byte(0)
            writer.Byte(6)
            writer.DWord(Item_.DroppedBy)

            Return writer.GetBytes
        End Function

        Public Function DropItem(ByVal Item As cInvItem, ByVal Position As Position) As UInteger
            Dim tmp_ As New cItemDrop
            tmp_.UniqueID = Id_Gen.GetUnqiueId
            tmp_.DroppedBy = Item.OwnerCharID
            tmp_.Position = Position
            tmp_.Item = FillItem(Item)

            If tmp_.Item.Pk2Id = 1 Then
                'Gold...
                If tmp_.Item.Amount <= 1000 Then
                    tmp_.Item.Pk2Id = 1
                ElseIf tmp_.Item.Amount > 1000 And tmp_.Item.Amount <= 10000 Then
                    tmp_.Item.Pk2Id = 2
                ElseIf tmp_.Item.Amount > 10000 Then
                    tmp_.Item.Pk2Id = 3
                End If
            End If

            ItemList.Add(tmp_.UniqueID, tmp_)

            For refindex As Integer = 0 To Server.MaxClients
                Dim socket As Net.Sockets.Socket = ClientList.GetSocket(refindex)
                Dim player As [cChar] = PlayerData(refindex) 'Check if Player is ingame
                If (socket IsNot Nothing) AndAlso (player IsNot Nothing) AndAlso socket.Connected Then
                    If CheckRange(player.Position, Position) Then
                        If PlayerData(refindex).SpawnedItems.Contains(tmp_.UniqueID) = False Then
                            Server.Send(CreateItemSpawnPacket(tmp_), refindex)
                            PlayerData(refindex).SpawnedItems.Add(tmp_.UniqueID)
                        End If
                    End If
                End If
            Next refindex

            Return tmp_.UniqueID
        End Function

        Public Sub RemoveItem(ByVal UniqueId As UInteger)
            Dim _item As cItemDrop = ItemList(UniqueId)
            Server.SendIfItemIsSpawned(CreateDespawnPacket(_item.UniqueID), _item.UniqueID)
            ItemList.Remove(UniqueId)


            For i = 0 To Server.MaxClients
                If PlayerData(i) IsNot Nothing Then
                    If PlayerData(i).SpawnedItems.Contains(_item.UniqueID) = True Then
                        PlayerData(i).SpawnedItems.Remove(_item.UniqueID)
                    End If
                End If
            Next

        End Sub

        Public Sub PickUp(ByVal UniqueId As UInteger, ByVal Index_ As Integer)
            Dim _item As cItemDrop = ItemList(UniqueId)
            Dim distance As Double = CalculateDistance(PlayerData(Index_).Position, _item.Position)

            If distance >= 5 Then
                'Out Of Range
                Dim TravelTime As Single = MoveUserToObject(Index_, _item.Position, 5)
                PickUpTimer(Index_).Interval = Traveltime
                PickUpTimer(Index_).Start()

            Else
                UpdateState(1, 1, Index_)

                Dim writer As New PacketWriter
                writer.Create(ServerOpcodes.PickUp_Move)
                writer.DWord(PlayerData(Index_).UniqueId)
                writer.Byte(_item.Position.XSector)
                writer.Byte(_item.Position.YSector)
                writer.Float(_item.Position.X)
                writer.Float(_item.Position.Z)
                writer.Float(_item.Position.Y)
                writer.Word(0)
                Server.SendIfPlayerIsSpawned(writer.GetBytes, Index_)

                writer.Create(ServerOpcodes.PickUp_Item)
                writer.DWord(PlayerData(Index_).UniqueId)
                writer.Byte(0)
                Server.SendIfPlayerIsSpawned(writer.GetBytes, Index_)

                RemoveItem(UniqueId)

                If _item.Item.Pk2Id = 1 Or _item.Item.Pk2Id = 2 Or _item.Item.Pk2Id = 3 Then
                    If _item.Item.Amount > 0 Then
                        PlayerData(Index_).Gold += _item.Item.Amount
                        UpdateGold(Index_)
                    End If
                Else
                    Dim slot As Byte = GetFreeItemSlot(Index_)
                    If slot <> -1 Then
                        Dim ref As cItem = GetItemByID(_item.Item.Pk2Id)
                        Dim temp_item As cInvItem = Inventorys(Index_).UserItems(slot)

                        temp_item.Pk2Id = _item.Item.Pk2Id
                        temp_item.OwnerCharID = PlayerData(Index_).CharacterId
                        temp_item.Durability = _item.Item.Durability
                        temp_item.Plus = _item.Item.Plus
                        temp_item.Amount = _item.Item.Amount
                        temp_item.Blues = New List(Of cInvItem.sBlue)

                        UpdateItem(Inventorys(Index_).UserItems(slot)) 'SAVE IT

                        writer.Create(ServerOpcodes.ItemMove)
                        writer.Byte(1)
                        writer.Byte(6) 'type = new item
                        writer.Byte(Inventorys(Index_).UserItems(slot).Slot)

                        AddItemDataToPacket(Inventorys(Index_).UserItems(slot), writer)

                        Server.Send(writer.GetBytes, Index_)
                    End If
                End If
            End If
        End Sub
    End Module

    Class cItemDrop
        Public UniqueID As UInteger
        Public DroppedBy As UInteger
        Public Position As Position
        Public Item As cInvItem
    End Class
End Namespace
