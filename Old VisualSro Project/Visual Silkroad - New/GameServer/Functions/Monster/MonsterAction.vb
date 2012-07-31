﻿Imports SRFramework

Namespace Functions
    Module MonsterAction
        Public Sub KillMob(ByVal UniqueID As Integer)
            If MobList.ContainsKey(UniqueID) = False Then
                Exit Sub
            End If

            MobList(UniqueID).HP_Cur = 0
            MobList(UniqueID).Death = True
            MobList(UniqueID).DeathRemoveTime = Date.Now.AddSeconds(5)
            UpdateState(0, 2, MobList(UniqueID))

            DropMonsterItems(UniqueID)

            Dim tmp_ As Integer = MobGetPlayerWithMostDamage(UniqueID)
            If tmp_ >= 0 Then
                If MobList(UniqueID).Mob_Type = 3 Then
                    SendUniqueKill(MobList(UniqueID).Pk2ID, PlayerData(tmp_).CharacterName)
                End If

                If Settings.ModGeneral And Settings.ModDamage Then
                    GameServer.GameMod.Damage.SendDamageInfo(UniqueID)
                End If
            End If
        End Sub

        Public Function MobGetPlayerWithMostDamage(ByVal UniqueID As Integer)
            Dim MostIndex As Integer = -1
            Dim MostDamage As UInteger
            Dim Mob_ As cMonster = MobList(UniqueID)

            For i = 0 To Mob_.DamageFromPlayer.Count - 1
                If Mob_.DamageFromPlayer(i).Damage > MostDamage Then
                    MostDamage = Mob_.DamageFromPlayer(i).Damage
                    MostIndex = Mob_.DamageFromPlayer(i).PlayerIndex
                End If
            Next
            Return MostIndex
        End Function


        Public Function MobGetPlayerWithMostDamage(ByVal UniqueID As Integer, ByVal Attacking As Boolean)
            Dim MostIndex As Integer = -1
            Dim MostDamage As Long = -1
            Dim Mob_ As cMonster = MobList(UniqueID)

            For i = 0 To Mob_.DamageFromPlayer.Count - 1
                If Mob_.DamageFromPlayer(i).Damage > MostDamage Then
                    MostDamage = Mob_.DamageFromPlayer(i).Damage
                    MostIndex = Mob_.DamageFromPlayer(i).PlayerIndex
                End If
            Next
            Return MostIndex
        End Function

        Public Sub MoveMob(ByVal UniqueID As Integer, ByVal ToPos As Position)
            Dim Obj As SilkroadObject = GetObject(MobList(UniqueID).Pk2ID)

            Dim WalkTime As Single
            Dim distance As Single = CalculateDistance(MobList(UniqueID).Position, ToPos)
            Select Case MobList(UniqueID).Pos_Tracker.SpeedMode
                Case cPositionTracker.enumSpeedMode.Walking
                    WalkTime = (distance / Obj.WalkSpeed) * 10000
                Case cPositionTracker.enumSpeedMode.Running
                    WalkTime = (distance / Obj.RunSpeed) * 10000
                Case cPositionTracker.enumSpeedMode.Zerking
                    WalkTime = (distance / Obj.BerserkSpeed) * 10000
            End Select


            Dim writer As New PacketWriter
            writer.Create(ServerOpcodes.GAME_MOVEMENT)
            writer.DWord(UniqueID)
            writer.Byte(1)
            'destination
            writer.Byte(ToPos.XSector)
            writer.Byte(ToPos.YSector)

            If IsInCave(ToPos) = False Then
                writer.Byte(BitConverter.GetBytes(CShort(ToPos.X)))
                writer.Byte(BitConverter.GetBytes(CShort(ToPos.Z)))
                writer.Byte(BitConverter.GetBytes(CShort(ToPos.Y)))
            Else
                'In Cave
                writer.Byte(BitConverter.GetBytes(CInt(ToPos.X)))
                writer.Byte(BitConverter.GetBytes(CInt(ToPos.Z)))
                writer.Byte(BitConverter.GetBytes(CInt(ToPos.Y)))
            End If

            writer.Byte(0)
            '1= source

            Server.SendIfMobIsSpawned(writer.GetBytes, MobList(UniqueID).UniqueID)
            MobList(UniqueID).Pos_Tracker.Move(ToPos)


            If MobList(UniqueID).IsAttacking = True Then
                If WalkTime > 0 Then
                    MobList(UniqueID).AttackTimer_Start(WalkTime)
                End If
            End If
        End Sub

        Public Sub MoveMobToUser(ByVal uniqueID As Integer, ByVal toPos As Position, ByVal range As Integer)
            Dim obj As SilkroadObject = GetObject(MobList(uniqueID).Pk2ID)

            Dim distance_x As Double = MobList(uniqueID).Position.ToGameX - toPos.ToGameX
            Dim distance_y As Double = MobList(uniqueID).Position.ToGameY - toPos.ToGameY
            Dim distance As Double = Math.Sqrt((distance_x * distance_x) + (distance_y * distance_y))

            If distance > range Then
                Dim Cosinus As Double = Math.Cos(distance_x / distance)
                Dim Sinus As Double = Math.Sin(distance_y / distance)

                Dim distance_x_new As Double = range * Cosinus
                Dim distance_y_new As Double = range * Sinus

                Dim new_x As Single = toPos.ToGameX + distance_x_new
                Dim new_y As Single = toPos.ToGameY + distance_y_new
                toPos.X = GetXOffset(new_x)
                toPos.Y = GetYOffset(new_y)
                toPos.XSector = GetXSecFromGameX(new_x)
                toPos.YSector = GetYSecFromGameY(new_y)
            End If

            Dim WalkTime As Single
            Select Case MobList(uniqueID).Pos_Tracker.SpeedMode
                Case cPositionTracker.enumSpeedMode.Walking
                    WalkTime = (distance / obj.WalkSpeed) * 10000
                Case cPositionTracker.enumSpeedMode.Running
                    WalkTime = (distance / obj.RunSpeed) * 10000
                Case cPositionTracker.enumSpeedMode.Zerking
                    WalkTime = (distance / obj.BerserkSpeed) * 10000
            End Select


            Dim writer As New PacketWriter
            writer.Create(ServerOpcodes.GAME_MOVEMENT)
            writer.DWord(uniqueID)
            writer.Byte(1)
            'destination
            writer.Byte(toPos.XSector)
            writer.Byte(toPos.YSector)

            If IsInCave(toPos) = False Then
                writer.Byte(BitConverter.GetBytes(CShort(toPos.X)))
                writer.Byte(BitConverter.GetBytes(CShort(toPos.Z)))
                writer.Byte(BitConverter.GetBytes(CShort(toPos.Y)))
            Else
                'In Cave
                writer.Byte(BitConverter.GetBytes(CInt(toPos.X)))
                writer.Byte(BitConverter.GetBytes(CInt(toPos.Z)))
                writer.Byte(BitConverter.GetBytes(CInt(toPos.Y)))
            End If

            writer.Byte(0)
            '1= source

            Server.SendIfMobIsSpawned(writer.GetBytes, MobList(uniqueID).UniqueID)
            MobList(uniqueID).Pos_Tracker.Move(toPos)


            If MobList(uniqueID).IsAttacking = True Then
                If WalkTime > 0 Then
                    MobList(uniqueID).AttackTimer_Start(WalkTime)
                End If
            End If
        End Sub


        Public Sub GetEXPFromMob(ByVal mob_ As cMonster)
            Dim ref_ As SilkroadObject = GetObject(mob_.Pk2ID)
            For i = 0 To mob_.DamageFromPlayer.Count - 1
                Dim Index_ As Integer = mob_.DamageFromPlayer(i).PlayerIndex
                Dim DmgPercent As Double = mob_.DamageFromPlayer(i).Damage / mob_.HP_Max

                If PlayerData(Index_) IsNot Nothing Then
                    Dim Balance As Double
                    'The Level factor...
                    If CSng(ref_.Level) - PlayerData(Index_).Level > 0 Then
                        'Mob is higher then you
                        Balance = (1 + ((CSng(ref_.Level) - CSng(PlayerData(Index_).Level)) / 10))
                    Else
                        'Mob is lower then you
                        If PlayerData(Index_).Level - CSng(ref_.Level) < 100 Then
                            Balance = (1 + ((CSng(ref_.Level) - CSng(PlayerData(Index_).Level)) / 100))
                        Else
                            Balance = 0.01
                        End If
                    End If

                    Dim HigehstMastery As cMastery = GetHighestPlayerMastery(Index_)
                    Dim Gap As Integer = (CInt(PlayerData(Index_).Level) - CInt(HigehstMastery.Level))
                    Dim GapFactorXP As Double
                    Dim GapFactorSP As Double
                    'Gap 0 = 100 EXP; 100 SP
                    'Gap 1 = 90 EXP; 110 SP
                    'Gap 2 = 80 EXP; 120 SP
                    'Gap 3 = 70 EXP; 130 SP
                    'Gap 4 = 60 EXP; 140 SP
                    'Gap 5 = 50 EXP; 150 SP
                    'Gap 6 = 40 EXP; 160 SP
                    'Gap 7 = 30 EXP; 170 SP
                    'Gap 8 = 20 EXP; 180 SP
                    'Gap 9 = 10 EXP; 190 SP

                    If Gap > 9 Then
                        GapFactorXP = 0.1
                        GapFactorSP = 1.9
                    ElseIf Gap <= 0 Then
                        GapFactorXP = 1
                        GapFactorSP = 1
                    Else
                        'Gap 1-9
                        GapFactorXP = 1 - (Gap / 10)
                        GapFactorSP = 1 + (Gap / 10)
                    End If


                    Dim EXP As Long = (((ref_.Exp * GetMobExpMultiplier(mob_.Mob_Type)) * DmgPercent * Balance * GapFactorXP) * Settings.Server_XPRate)
                    Dim SP As Long = ((ref_.Exp * DmgPercent * Balance * GapFactorSP) * Settings.Server_SPRate)


                    GetXP(EXP, SP, Index_, mob_.UniqueID)
                End If
            Next
        End Sub

        Public Function GetMobExpMultiplier(ByVal type As Byte) As Byte
            Select Case type
                Case 0
                    Return MobMultiplierExp.Normal
                Case 1
                    Return MobMultiplierExp.Champion
                Case 3
                    Return MobMultiplierExp.Unique
                Case 4
                    Return MobMultiplierExp.Giant
                Case 5
                    Return MobMultiplierExp.Titan
                Case 6
                    Return MobMultiplierExp.Elite
                Case 16
                    Return MobMultiplierExp.Party_Normal
                Case 17
                    Return MobMultiplierExp.Party_Champ
                Case 20
                    Return MobMultiplierExp.Party_Giant
                Case Else
                    Return 1
            End Select
        End Function
    End Module
End Namespace
