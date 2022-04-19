using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreinoTestMock.Entities
{
    public class Client
    {
        public Client(string name, string code, DateTime birthday)
        {
            Name = name;
            Code = code;
            Birthday = birthday;
            CreatedAt = DateTime.Now;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Code { get; private set; }
        public DateTime Birthday { get; private set; }
        public DateTime CreatedAt { get; private set; }
    }
}
