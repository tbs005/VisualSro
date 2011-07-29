﻿Namespace GameServer.Functions
    Module ItemMall
        Public Sub OnSendSilks(ByVal Index_ As Integer)
            Dim UserIndex As Integer = GameServer.GameDB.GetUserWithAccID(PlayerData(Index_).AccountID)
            Dim writer As New PacketWriter
            writer.Create(ServerOpcodes.Silk)
            writer.DWord(GameDB.Users(UserIndex).Silk)
            writer.DWord(GameDB.Users(UserIndex).Silk_Bonus)
            writer.DWord(GameDB.Users(UserIndex).Silk_Points)
            Server.Send(writer.GetBytes, Index_)
        End Sub

        Public Sub OnBuyItemFromMall(ByVal packet As PacketReader, ByVal index_ As Integer)
            Dim type1 As Byte = packet.Byte
            Dim type2 As Byte = packet.Byte
            Dim type3 As Byte = packet.Byte
            Dim type4 As Byte = packet.Byte
            Dim type5 As Byte = packet.Byte
            Dim LongName As String = packet.String(packet.Word)
            Dim RefObject As MallPackage_ = GetItemMallItemByName(LongName)
            Dim amout As UShort = packet.Word
            Dim writer As New PacketWriter

            Dim UserIndex As Integer = GameDB.GetUserWithAccID(PlayerData(index_).AccountID)

            If LongName = RefObject.Name_Package Then
                If GameDB.Users(UserIndex).Silk - (RefObject.Price * amout) >= 0 And GetFreeItemSlot(index_) <> -1 Then
                    Dim ItemSlots As New List(Of Byte)

                    For i = 1 To amout
                        Dim _Refitem As cItem = GetItemByName(RefObject.Name_Normal)
                        Dim slot As Byte = GetFreeItemSlot(index_)
                        Dim item As cInvItem = Inventorys(index_).UserItems(slot)
                        item.Pk2Id = _Refitem.ITEM_TYPE

                        Select Case _Refitem.CLASS_A
                            Case 1
                                item.Durability = 30
                                item.Amount = 0
                            Case 2
                                item.Durability = 30
                                item.Amount = 10
                            Case 3
                                item.Durability = 30
                                item.Amount = RefObject.Amout
                        End Select
                        UpdateItem(item)
                        ItemSlots.Add(slot)

                        GameDB.Users(UserIndex).Silk -= RefObject.Price
                        DataBase.SaveQuery(String.Format("UPDATE users SET silk='{0}' where id='{1}'", GameDB.Users(UserIndex).Silk, PlayerData(index_).AccountID))
                        OnSendSilks(index_)
                    Next

                    writer.Create(ServerOpcodes.ItemMove)
                    writer.Byte(1)
                    writer.Byte(24)
                    writer.Byte(type1)
                    writer.Byte(type2)
                    writer.Byte(type3)
                    writer.Byte(type4)
                    writer.Byte(type5)
                    writer.Byte(ItemSlots.Count)
                    For Each slot In ItemSlots
                        writer.Byte(slot)
                    Next
                    writer.Word(amout)
                    Server.Send(writer.GetBytes, index_)

                    Log.WriteGameLog(index_, "Item_Mall", "Buy", String.Format("Item: {0}, Amout {1}, Payed: {2}", LongName, amout, RefObject.Price))
                Else
                    writer.Create(ServerOpcodes.ItemMove)
                    writer.Byte(2)
                    writer.Byte(24)
                    writer.Byte(1)
                    Server.Send(writer.GetBytes, index_)
                End If
            End If
        End Sub
    End Module
End Namespace