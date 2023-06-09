using Microsoft.EntityFrameworkCore;
using SafeLearn.Data;
using System.Security.Cryptography;
using System.Text;

namespace SafeLearn.Usuario
{
    public class UsuarioService : IUsuarioService
    {
        private readonly SafeLearnContext safeLearnContext;
        private readonly ILogger<UsuarioService> logger;
        public UsuarioService(SafeLearnContext safeLearnContext, ILogger<UsuarioService> logger)
        {
            this.safeLearnContext = safeLearnContext;
            this.logger = logger;
        }

        public async Task<UsuarioEntity> GetUsuario(int id)
        {
            try
            {
                return await safeLearnContext.Usuarios.AsNoTracking().FirstOrDefaultAsync(u => u.ID == id);
            }
            catch (Exception ex)
            {
                logger.LogInformation("Exception while getting user: id: {id} - Ex: {ex}", id, ex);
                throw;
            }
        }

        public async Task UpdateUsuario(UsuarioEntity usuario)
        {
            try
            {
                safeLearnContext.Usuarios.Update(usuario);
                await safeLearnContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogInformation("Exception while updating user: id: {id} - Ex: {ex}", usuario.ID, ex);
                throw;
            }
        }

        public async Task<UsuarioEntity> CreateUsuario(UsuarioEntity usuario)
        {
            try
            {
                var lastUser = await safeLearnContext.Usuarios.LastOrDefaultAsync();
                usuario.ID = lastUser.ID + 1;
                var newUser = await safeLearnContext.Usuarios.AddAsync(usuario);
                await safeLearnContext.SaveChangesAsync();
                await UsuarioSuspeito(usuario.Nome);
                return newUser.Entity;
            }
            catch (Exception ex)
            {
                logger.LogInformation("Exception while deleting user: id: {id} - Ex: {ex}", usuario.ID, ex);
                throw;
            }
        }

        public async Task DeleteUsuario(UsuarioEntity usuario)
        {
            try
            {
                safeLearnContext.Usuarios.Remove(usuario);
                await safeLearnContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogInformation("Exception while deleting user: id: {id} - Ex: {ex}", usuario.ID, ex);
                throw;
            }
        }

        public async Task<UsuarioEntity> Login(string cpf, string senha)
        {
            // Procurar usuário pelo CPF
            var usuario = await safeLearnContext.Usuarios.AsNoTracking().FirstOrDefaultAsync(u => u.CPF == cpf);

            if (usuario == null)
            {
                // Usuário não encontrado
                return null;
            }

            // Verificar a senha
            if (VerificarSenha(senha, usuario.Senha))
            {
                // Senha correta, retorno o usuário
                return usuario;
            }

            // Senha incorreta
            return null;
        }

        public async Task<bool> UsuarioSuspeito(string nome)
        {
            if (nome.ToLower().Equals("suspeito"))
            {
                //add notificacao
                return true;
            }

            return false;
        }

        private bool VerificarSenha(string senhaDigitada, string senhaArmazenada)
        {
            // Criar instância do algoritmo de criptografia SHA256
            using (SHA256 sha256 = SHA256.Create())
            {
                // Converter a senha digitada em bytes
                byte[] senhaBytes = Encoding.UTF8.GetBytes(senhaDigitada);

                // Calcular o hash da senha digitada
                byte[] hashBytes = sha256.ComputeHash(senhaBytes);

                // Converter o hash calculado em uma string hexadecimal
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }
                string senhaHash = builder.ToString();

                // Comparar a senha hasheada com a senha armazenada no banco de dados
                return senhaHash.Equals(senhaArmazenada);
            }
        }
    }
}
