using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace microservcolegio.Secretaria.Entities
{
    public class Solicitacao
    {
        public Guid id {get;set;}
        public DateTime data {get;set;}
        public string cliente {get;set;}
        public string funcionario {get;set;}
    }
}