
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace microservcolegio.Secretaria.Entities;

public class RepositoryDbContext : DbContext
{
    private IConfiguration _configuration;
    public DbSet<Aluno> Alunos {get;set;}
    //Eu sei que se escreve "solicitações"
    public DbSet<Solicitacao> Solicitacaos {get;set;}

    public RepositoryDbContext(IConfiguration configuration){
        this._configuration = configuration;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){

        optionsBuilder.UseCosmos(
            connectionString: this._configuration["CosmosDBURL"],
            databaseName: this._configuration["CosmosDBDBName"],
            cosmosOptionsAction: options =>
            {
                options.ConnectionMode(ConnectionMode.Gateway);
                options.HttpClientFactory(() => new HttpClient(new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                }));
            }

        );
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aluno>()
            .ToContainer("aluno")
            .HasPartitionKey(p => p.id)
            .HasAutoscaleThroughput(400)
            .HasNoDiscriminator()
            .Property(p => p.id)
            .HasValueGenerator<GuidValueGenerator>()
            .IsRequired(true)
            ;
        modelBuilder.Entity<Solicitacao>()
            .ToContainer("solicitacao")
            .HasPartitionKey(p => p.id)
            .HasAutoscaleThroughput(400)
            .HasNoDiscriminator()
            .Property(p => p.id)
            .HasValueGenerator<GuidValueGenerator>()
            .IsRequired(true)
            ;
    }

}