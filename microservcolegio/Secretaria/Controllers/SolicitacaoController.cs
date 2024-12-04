using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using microservcolegio.Secretaria.Entities;
using microservcolegio.Secretaria.Entities.Services;
using microservcolegio.Secretaria.Services;
using Microsoft.AspNetCore.Mvc;

namespace microservcolegio.Secretaria.Controllers
{

    [ApiController]
    [Route("/api/v1/[controller]")]
    public class SolicitacaoController
    {
        
    private ISolicitacaoService _service;
    public SolicitacaoController(ISolicitacaoService service){
        this._service = service;
    }
    [HttpGet]
    public async Task<IResult> Get(){
        var listaAlunos = await _service.GetAllAsync();
        return Results.Ok(listaAlunos);
    }

    [HttpPost]
    public async Task<IResult> Post(Solicitacao solicitacao){
        if(solicitacao == null)
        {
            return Results.BadRequest();
        }
        var solicitacaoSalvo = await _service.SaveAsync(solicitacao);
        return Results.Ok(solicitacaoSalvo);
    }
    [HttpPut("{id}")]
    public async Task<IResult> Put(string id, [FromBody] Solicitacao solicitacao)
    {
        if(solicitacao == null || id.Equals(String.Empty))
        {
            return Results.BadRequest();
        }
        solicitacao = await _service.UpdateAsync(id,solicitacao);
        if(solicitacao == null)
        {
            return Results.NotFound();
        }
        return Results.Ok(solicitacao);
    }
    [HttpDelete("{id}")]
    public async Task<IResult> Delete(string id)
    {
        if(id.Equals(String.Empty))
        {
            return Results.BadRequest();
        }
        var solicitacao = await this._service.DeleteAsync(id);
         if(solicitacao == null)
        {
            return Results.NotFound();
        }
        return Results.Ok(solicitacao);
    }
    
    }
}