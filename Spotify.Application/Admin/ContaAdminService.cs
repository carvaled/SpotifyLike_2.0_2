using AutoMapper;
using Domain.Admin.Agreggates;
using Domain.Admin.ValueObject;
using Spotify.Application.Admin.Dto;
using Spotify.Application.Admin.Interfaces;
using Spotify.Application.Shared.Dto;
using SpotifyLike.Repository.Admin;

namespace Spotify.Application.Admin
{
    public class ContaAdminService : IContaAdminAuthService
    {
        private IMapper Mapper { get; set; }
        private ContaAdminRepository Repository { get; set; }
        public ContaAdminService(IMapper mapper, ContaAdminRepository contaAdminRepository) 
        {
            Mapper = mapper;
            Repository = contaAdminRepository;
        }

        public ContaAdminDto Create(ContaAdminDto dto)
        {
            IsValidPerfilUsuario(dto);

            if (this.Repository.Exists(x => x.Email != null && x.Email == dto.Email))
                throw new ArgumentException("Usuário já cadastrado.");

            var account = this.Mapper.Map<ContaAdmin>(dto);
            this.Repository.Save(account);
            var result = this.Mapper.Map<ContaAdminDto>(account);
            return result;
        }

        public ContaAdminDto FindById(Guid id)
        {
            var account = this.Repository.GetById(id);
            var result = this.Mapper.Map<ContaAdminDto>(account);
            return result;
        }

        public IEnumerable<ContaAdminDto> FindAll(Guid userId)
        {
            var accounts = this.Repository.GetAll().Where(c => c.Id == userId).ToList();
            var result = this.Mapper.Map<IEnumerable<ContaAdminDto>>(accounts);
            return result;
        }

        public IEnumerable<ContaAdminDto> FindAll()
        {
            var result = this.Mapper.Map<IEnumerable<ContaAdminDto>>(this.Repository.GetAll());
            return result;
        }


        public ContaAdminDto Update(ContaAdminDto dto)
        {
            IsValidPerfilUsuario(dto);
            var account = this.Mapper.Map<ContaAdmin>(dto);
            this.Repository.Update(account);
            return this.Mapper.Map<ContaAdminDto>(account);
        }

        public bool Delete(ContaAdminDto dto)
        {
            IsValidPerfilUsuario(dto);
            var account = this.Mapper.Map<ContaAdmin>(dto);
            this.Repository.Delete(account);
            return true;
        }

        public ContaAdminDto Authentication(LoginDto dto)
        {
            bool credentialsValid = false;
            var account = this.Repository.Find(u => u.Email.Equals(dto.Email)).FirstOrDefault();
            if (account == null)
                throw new ArgumentException("Usuário inexistente!");
            else
            {
                credentialsValid = account is not null && !String.IsNullOrEmpty(account.Senha) && !String.IsNullOrEmpty(account.Email) && dto.Senha.Equals(account.Senha);
            }

            if (credentialsValid)
                return this.Mapper.Map<ContaAdminDto>(account);

            throw new ArgumentException("Usuário Inválido!");
        }
        private void IsValidPerfilUsuario(ContaAdminDto dto)
        {
            var administrador = this.Repository.Find(account => account.Id == dto.Id && account.PerfilType.Id == (int)Perfil.TipoUsuario.Admin).FirstOrDefault();
            if (administrador == null)
                throw new ArgumentException("Usuário não permitido a realizar operação.");

        }
    }
}