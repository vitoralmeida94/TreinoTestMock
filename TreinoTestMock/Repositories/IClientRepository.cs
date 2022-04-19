using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreinoTestMock.Entities;
using TreinoTestMock.Filters;

namespace TreinoTestMock.Repository
{
    public interface IClientRepository
    {
        List<Client> GetAll();

        bool CheckCode(string code);

        void Save(Client client);

    }
}
