using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using microservcolegio.Secretaria.Entities;
using Microsoft.EntityFrameworkCore;
using Dapr.Client;

namespace microservcolegio.Secretaria.Services
{
    public class SolicitacaoService : ISolicitacaoService
    {
    private IConfiguration _configuration;
    private DaprClient _daprClient;
    private RepositoryDbContext _dbContext;
    public SolicitacaoService(RepositoryDbContext dbContext)
    {
        this._dbContext = dbContext;
    }

    
    private async Task PublishUpdateAsync(Solicitacao solicitacao){
        await this._daprClient.PublishEventAsync(_configuration["AppComponentService"], 
                                                _configuration["AppComponentSolicitacao"], 
                                                solicitacao);
    }
    public async Task<List<Solicitacao>> GetAllAsync()
    {
        var listaSolicitacaos = await this._dbContext.Solicitacaos.ToListAsync();
        return listaSolicitacaos;
    }

    public async Task<Solicitacao> SaveAsync(Solicitacao solicitacao)
    {
        await this._dbContext.Solicitacaos.AddAsync(solicitacao);
        await this._dbContext.SaveChangesAsync();
        await PublishUpdateAsync(solicitacao);
        return solicitacao;
    }

    public async Task<Solicitacao> UpdateAsync(string id, Solicitacao solicitacao)
    {
        var solicitacaoAntiga = 
            await this._dbContext.Solicitacaos.Where(a => a.id.Equals(id))
                .FirstOrDefaultAsync();
        if(solicitacaoAntiga != null)
        {
            solicitacaoAntiga.data = solicitacao.data;
            solicitacaoAntiga.cliente = solicitacao.cliente;
            solicitacaoAntiga.funcionario = solicitacao.funcionario;
            await this._dbContext.SaveChangesAsync();
            await PublishUpdateAsync(solicitacao);            
            return solicitacaoAntiga;
        }
        return null;
    }
    public async Task<Solicitacao> DeleteAsync(string id)
    {
        var solicitacaoAntiga = 
            await this._dbContext.Solicitacaos.Where(a => a.id.Equals(id))
                .FirstOrDefaultAsync();
        if(solicitacaoAntiga != null)
        {
            this._dbContext.Remove(solicitacaoAntiga);
            await this._dbContext.SaveChangesAsync();
            return solicitacaoAntiga;
        }
        return null;
    }
        
    }
}