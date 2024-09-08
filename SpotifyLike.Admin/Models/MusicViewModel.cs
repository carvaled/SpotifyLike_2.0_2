using Spotify.Application.Streaming.Dto;

namespace Spotify.Admin.Models;

public class MusicViewModel
{
    public Guid UsuarioId { get; set; }
    public MusicaDto? Music { get; set; }
    public IEnumerable<AlbumDto>? Albums { get; set; }
}