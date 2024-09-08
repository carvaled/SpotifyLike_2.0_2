-- Create Usuarios
INSERT INTO [SpotifyDatabase].[dbo].[Usuario] ([Id], [Nome], [Email], [Senha], [DtNascimento])
VALUES
    (NEWID(), 'João Silva', 'joao@user.com', '123', '1990-01-15'),
    (NEWID(), 'Maria Oliveira', 'maria@user.com', '123', '1985-05-22');

-- Create Banda
DECLARE @BandaId1 UNIQUEIDENTIFIER = NEWID();
DECLARE @BandaId2 UNIQUEIDENTIFIER = NEWID();
DECLARE @BandaId3 UNIQUEIDENTIFIER = NEWID();
DECLARE @BandaId4 UNIQUEIDENTIFIER = NEWID();

INSERT INTO [SpotifyDatabase].[dbo].[Banda] ([Id], [Nome], [Descricao], [Backdrop])
VALUES
    (@BandaId1, 'The Rockers', 'Uma banda de rock clássico.', 'backdrop_rockers.jpg'),
    (@BandaId2, 'Pop Stars', 'Grupo de pop famoso.', 'backdrop_popstars.jpg'),
    (@BandaId3, 'Jazz Legends', 'Banda de jazz renomada.', 'backdrop_jazzlegends.jpg'),
    (@BandaId4, 'Electronic Beats', 'Música eletrônica inovadora.', 'backdrop_electronicbeats.jpg');

-- Create Albuns
DECLARE @AlbumId1 UNIQUEIDENTIFIER = NEWID();
DECLARE @AlbumId2 UNIQUEIDENTIFIER = NEWID();
DECLARE @AlbumId3 UNIQUEIDENTIFIER = NEWID();
DECLARE @AlbumId4 UNIQUEIDENTIFIER = NEWID();

INSERT INTO [SpotifyDatabase].[dbo].[Album] ([Id], [Nome], [BandaId], [Backdrop])
VALUES
    (@AlbumId1, 'Rock Greatest Hits', @BandaId1, 'backdrop_rock_greatest_hits.jpg'),
    (@AlbumId2, 'Pop Sensations', @BandaId2, 'backdrop_pop_sensations.jpg'),
    (@AlbumId3, 'Jazz Classics', @BandaId3, 'backdrop_jazz_classics.jpg'),
    (@AlbumId4, 'Electronic Vibes', @BandaId4, 'backdrop_electronic_vibes.jpg');

-- Create Musicas
INSERT INTO [SpotifyDatabase].[dbo].[Musica] ([Id], [Nome], [Duracao_Valor], [AlbumId], [URL])
VALUES
    (NEWID(), 'Rock Anthem', 45, @AlbumId1, 'http://example.com/rock_anthem.mp3'),
    (NEWID(), 'Pop Hit', 40, @AlbumId2, 'http://example.com/pop_hit.mp3'),
    (NEWID(), 'Smooth Jazz', 55, @AlbumId3, 'http://example.com/smooth_jazz.mp3'),
    (NEWID(), 'Electronic Groove', 30, @AlbumId4, 'http://example.com/electronic_groove.mp3');

-- Inserir conta com perfil Admin
INSERT INTO [SpotifyDatabase].[dbo].[ContaAdmin] ([Id], [Nome], [Email], [Senha], [PerfilTypeId], [DataCricao])
VALUES (NEWID(), 'Admin User', 'user@admin.com', '123', 1, GETDATE());

-- Inserir conta com perfil Normal
INSERT INTO [dbo].[ContaAdmin] ([Id], [Nome], [Email], [Senha], [PerfilTypeId], [DataCricao])
VALUES (NEWID(), 'Normal User', 'user@normal.com', '123', 2, GETDATE());
