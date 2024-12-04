using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using microservcolegio.Secretaria.Entities;

namespace microservcolegio.Secretaria.Services
{
    public interface ISolicitacaoService
    {
        Task<List<Solicitacao>> GetAllAsync();
        Task<Solicitacao> SaveAsync(Solicitacao solicitacao);
        Task<Solicitacao> UpdateAsync(String id, Solicitacao solicitacao);
        Task<Solicitacao> DeleteAsync(String id);
    }
}