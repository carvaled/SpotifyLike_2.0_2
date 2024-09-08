using Spotify.Application.Admin.Dto;
using Spotify.Application.Shared.Dto;

namespace Spotify.Application.Admin.Interfaces;
public interface IContaAdminAuthService
{
    ContaAdminDto Authentication(LoginDto dto);
}
