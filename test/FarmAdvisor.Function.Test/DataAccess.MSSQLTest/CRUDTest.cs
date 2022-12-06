using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using FarmAdvisor.DataAccess.MSSQL.DataContext;
using FarmAdvisor.DataAccess.MSSQL.Entities;
using FarmAdvisor.DataAccess.MSSQL.Functions.Crud;

namespace FarmAdvisor.Function.Test.DataAcess.MSSQLTest
{
    public class CRUDTest
    {
        Mock<DatabaseContext> MockDbContext = new Mock<DatabaseContext>();



        [Fact]
        public async Task Positive_CreateUser()
        {
            var UserMock = new Mock<DbSet<User>>();
            var User = new User
            {
                Name = "Test",
                Email = "Test@test.com",
                AuthId = "admin",
                Phone = "1234567890"
            };

            UserMock.Setup(us => us.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                .Callback((User user, CancellationToken token) => { })
                .Returns((User user, CancellationToken token) => { ValueTask.FromResult<User>(User); });
            MockDbContext.Setup(us=>us.Set<User>()).Returns(UserMock.Object);
            MockDbContext.Setup(us => us.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(() => Task.FromResult(1));

            var UserCrud = new CRUD();
            var result = await UserCrud.Create(User);

            Assert.NotNull(result);
        }

    }
}
