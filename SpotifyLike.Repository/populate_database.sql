-- Create Usuarios
INSERT INTO [SpotifyDatabase].[dbo].[Usuario] ([Id], [Nome], [Email], [Senha], [DtNascimento])
VALUES
    (NEWID(), 'João Silva', 'joao.silva@example.com', '123', '1990-01-15'),
    (NEWID(), 'Maria Oliveira', 'maria.oliveira@example.com', '123', '1985-05-22');

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
INSERT INTO [SpotifyDatabase].[dbo].[Album] ([Id], [Nome], [BandaId])
VALUES
    (NEWID(), 'Rock Greatest Hits', @BandaId1),
    (NEWID(), 'Pop Sensations', @BandaId2),
    (NEWID(), 'Jazz Classics', @BandaId3),
    (NEWID(), 'Electronic Vibes', @BandaId4);

DECLARE @AlbumId1 UNIQUEIDENTIFIER = NEWID();
DECLARE @AlbumId2 UNIQUEIDENTIFIER = NEWID();
DECLARE @AlbumId3 UNIQUEIDENTIFIER = NEWID();
DECLARE @AlbumId4 UNIQUEIDENTIFIER = NEWID();

INSERT INTO [SpotifyDatabase].[dbo].[Album] ([Id], [Nome])
VALUES
    (@AlbumId1, 'Rock Album'),
    (@AlbumId2, 'Pop Album'),
    (@AlbumId3, 'Jazz Album'),
    (@AlbumId4, 'Electronic Album');

-- Create Musicas
INSERT INTO [SpotifyDatabase].[dbo].[Musica] ([Id], [Nome], [Duracao_Valor], [AlbumId])
VALUES
    (NEWID(), 'Rock Anthem', 45, @AlbumId1),
    (NEWID(), 'Pop Hit', 40, @AlbumId2),
    (NEWID(), 'Smooth Jazz', 55, @AlbumId3),
    (NEWID(), 'Electronic Groove', 30, @AlbumId4);
