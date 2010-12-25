﻿Namespace GameServer.Functions
    Module Guild

        Public Sub SendGuildInfo(ByVal Index_ As Integer, ByVal update As Boolean)
            Dim writer As New PacketWriter
            Dim guild As cGuild = GameDB.GetGuildWithGuildID(PlayerData(Index_).GuildID)

            If update = False Then
                writer.Create(ServerOpcodes.Guild_Info)
                writer.DWord(guild.GuildID)
                writer.Word(guild.Name.Length)
                writer.String(guild.Name)
                writer.Byte(guild.Level)
                writer.DWord(guild.Points)

                writer.Word(guild.NoticeTitle.Length)
                writer.String(guild.NoticeTitle)
                writer.Word(guild.Notice.Length)
                writer.String(guild.Notice)
                writer.DWord(0)
                writer.Byte(0)

                writer.Byte(guild.Member.Count)
                For i = 0 To guild.Member.Count - 1
                    Dim char_ As [cChar] = GameDB.GetCharWithCharID(guild.Member(i).CharacterID)

                    writer.DWord(char_.CharacterId)
                    writer.Word(char_.CharacterName.Length)
                    writer.String(char_.CharacterName)

                    'Rights
                    If guild.Member(i).Rights.Master = True Then
                        writer.Byte(0)
                        writer.Byte(char_.Level)
                        writer.DWord(guild.Member(i).DonantedGP)
                        writer.DWord(UInteger.MaxValue)
                    Else
                        writer.Byte(&HA)
                        writer.Byte(char_.Level)
                        writer.DWord(guild.Member(i).DonantedGP)
                        writer.DWord(1)
                    End If

                    writer.DWord(0)
                    writer.DWord(0)
                    writer.DWord(0)

                    writer.Word(guild.Member(i).GrantName.Length)
                    writer.String(guild.Member(i).GrantName)
                    writer.DWord(char_.Model)
                    writer.Word(2)
                Next


                Server.Send(writer.GetBytes, Index_)
            End If
        End Sub

        Public Sub LinkPlayerToGuild(ByVal Index_ As Integer)
            If PlayerData(Index_).GuildID <> -1 Then
                Dim guild As cGuild = GameDB.GetGuildWithGuildID(PlayerData(Index_).GuildID)
                Dim member As cGuild.GuildMember_ = GetMember(PlayerData(Index_).GuildID, PlayerData(Index_).CharacterId)

                Dim writer As New PacketWriter
                writer.Create(ServerOpcodes.Guild_Link)
                writer.DWord(PlayerData(Index_).UniqueId)
                writer.DWord(PlayerData(Index_).GuildID)
                writer.Word(guild.Name.Length)
                writer.String(guild.Name)
                writer.Word(member.GrantName.Length)
                writer.String(member.GrantName)
                writer.DWord(1)
                writer.DWord(1)
                writer.DWord(0)
                writer.Byte(1)
                writer.Byte(1)
                Server.SendIfPlayerIsSpawned(writer.GetBytes, Index_)
            End If
        End Sub

        Private Function GetMember(ByVal GuildID As UInteger, ByVal CharID As UInteger) As cGuild.GuildMember_
            For i = 0 To GameDB.Guilds.Count - 1
                If GameDB.Guilds(i).GuildID = GuildID Then
                    For m = 0 To GameDB.Guilds(i).Member.Count - 1
                        If GameDB.Guilds(i).Member(m).CharacterID = CharID Then
                            Return GameDB.Guilds(i).Member(m)
                        End If
                    Next
                End If
            Next
        End Function


    End Module
End Namespace
