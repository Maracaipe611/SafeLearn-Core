
namespace SafeLearn.Usuario
{
    public interface IUsuarioService
    {
        public Task<UsuarioEntity> GetUsuario(int id);
        public Task UpdateUsuario(UsuarioEntity usuario);
        public Task<UsuarioEntity> CreateUsuario(UsuarioEntity usuario);
        public Task DeleteUsuario(UsuarioEntity usuario);
        public Task<bool> UsuarioSuspeito(string nome);
    }
}
