using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreinoTestMock.Entities;
using TreinoTestMock.Filters;
using TreinoTestMock.Repository;
using TreinoTestMock.Services;
using Xunit;

namespace Tests
{
    public class ClientServiceTest
    {

        [Theory]
        [InlineData("Anna",1)]
        [InlineData("Vitor",1)]
        [InlineData("Teste",3)]
        public void Search_FilterWithName_ReturnClients(string name, int countResult)
        {
            //Arrange
            var clients = new List<Client>
            {
                new Client("Vitor Teste","123456",new DateTime(1994,7,4)),
                new Client("Anna Teste","888855",new DateTime(1996,9,20)),
                new Client("Johny Teste","852364",new DateTime(1965,8,8)),
            };
            
            var mock = new Mock<IClientRepository>();
            mock.Setup(x => x.GetAll()).Returns(clients);

            var service = new ClientService(mock.Object);

            //Act
            var filter = new ClientFilter { Name = name };
            var result = service.Search(filter);

            //Asserts
            Assert.NotEmpty(result);
            Assert.Equal(countResult, result.Count());
            var client = result.FirstOrDefault();
            Assert.NotNull(client);
            Assert.Contains(name, client.Name);
        }

        [Theory]
        [InlineData(1994, 2)]
        [InlineData(1996, 1)]
        public void Search_FilterWithYear_ReturnClients(int year, int countResult)
        {
            //Arrange
            var clients = new List<Client>
            {
                new Client("Vitor Teste","123456",new DateTime(1994,7,4)),
                new Client("Anna Teste","888855",new DateTime(1996,9,20)),
                new Client("Johny Teste","852364",new DateTime(1994,8,8)),
            };

            var mock = new Mock<IClientRepository>();
            mock.Setup(x => x.GetAll()).Returns(clients);

            var service = new ClientService(mock.Object);

            //Act
            var filter = new ClientFilter { Year = year };
            var result = service.Search(filter);

            //Asserts
            Assert.NotEmpty(result);
            Assert.Equal(countResult, result.Count());
        }
        [Theory]
        [InlineData(2033, 0)]
        [InlineData(1888, 0)]
        [InlineData(1898, 0)]
        public void Search_FilterWithYear_ReturnNoClients(int year, int countResult)
        {
            //Arrange
            var clients = new List<Client>
            {
                new Client("Vitor Teste","123456",new DateTime(1994,7,4)),
                new Client("Anna Teste","888855",new DateTime(1996,9,20)),
                new Client("Johny Teste","852364",new DateTime(1994,8,8)),
            };

            var mock = new Mock<IClientRepository>();
            mock.Setup(x => x.GetAll()).Returns(clients);

            var service = new ClientService(mock.Object);

            //Act
            var filter = new ClientFilter { Year = year };
            var result = service.Search(filter);

            //Asserts
            Assert.Empty(result);
            Assert.Equal(countResult, result.Count());
        }

        [Theory]
        [InlineData("Vitor","555")]
        [InlineData("Teste", "551")]
        public void Save_ClientValid_ReturnTrue(string name, string code)
        {
            //Arrange
            var mock = new Mock<IClientRepository>();
            mock.Setup(x => x.CheckCode(It.IsAny<string>())).Returns(false);
            //Act
            var service = new ClientService(mock.Object);
            var client = new Client(name, code, DateTime.Now);
            //Assert
            Assert.True(service.Add(client));
        }
        [Theory]
        [InlineData("Vitor", "8586")]
        [InlineData("Teste", "84853")]
        public void Save_ClientValid_ReturnFalse(string name, string code)
        {
            //Arrange
            var mock = new Mock<IClientRepository>();
            mock.Setup(x => x.CheckCode(It.IsAny<string>())).Returns(true);
            //Act
            var service = new ClientService(mock.Object);
            var client = new Client(name, code, DateTime.Now);
            //Assert
            Assert.False(service.Add(client));
        }
    }
}
