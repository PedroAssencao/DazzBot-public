using Chatbot.Infrastructure.Meta.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Chatbot.Services.Meta.Services
{
    public class MetodoCheckServices
    {
        protected readonly IMetodoCheck _repository;

        public MetodoCheckServices(IMetodoCheck repository)
        {
            _repository = repository;
        }
        public dynamic ababa(dynamic Values)
        {
            try
            {
                return _repository.VerificarTipoDeRetorno(Values);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
