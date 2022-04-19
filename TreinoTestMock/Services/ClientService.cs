using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreinoTestMock.Entities;
using TreinoTestMock.Filters;
using TreinoTestMock.Repository;

namespace TreinoTestMock.Services
{
    public class ClientService
    {
        private readonly IClientRepository _repository;
        public ClientService(IClientRepository repository)
        {
            _repository = repository;
        }

        public List<Client> Search(ClientFilter filter)
        {
            if (string.IsNullOrEmpty(filter.Code) && string.IsNullOrEmpty(filter.Name) && filter.Year < 0)
                return Enumerable.Empty<Client>().ToList();

            var clients = _repository.GetAll();

            if (!string.IsNullOrEmpty(filter.Code))
                clients = clients.Where(x => x.Code == filter.Code).ToList();
            if (!string.IsNullOrEmpty(filter.Name))
                clients = clients.Where(x => x.Name.Contains(filter.Name)).ToList();
            if (filter.Year > 0)
                clients = clients.Where(x => x.Birthday.Year == filter.Year).ToList();
               
            return clients;
        }

        public bool Add(Client client)
        {
            if (_repository.CheckCode(client.Code))
                return false;

            _repository.Save(client);
            return true;
        }

        
    }
}
