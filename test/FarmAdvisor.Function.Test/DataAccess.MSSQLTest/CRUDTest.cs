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
                .Returns((User user, CancellationToken token) =>  ValueTask.FromResult((EntityEntry<User>)null!) );
            MockDbContext.Setup(us=>us.Set<User>()).Returns(UserMock.Object);
            MockDbContext.Setup(us => us.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(() => Task.FromResult(1));

            var UserCrud = new Crud();
            var result = await UserCrud.Create(User);

            Assert.NotNull(result);
            // Assert.Equal(User, result);

        }

        [Fact]
        public async Task Negative_CreateUser()
        {
            var UserMock = new Mock<DbSet<User>>();

            var User = new User
            {};

            UserMock.Setup(us => us.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                .Callback((User user, CancellationToken token) => { })
                .Returns((User user, CancellationToken token) => ValueTask.FromResult((EntityEntry<User>)null!));
            MockDbContext.Setup(us => us.Set<User>()).Returns(UserMock.Object);
            MockDbContext.Setup(us => us.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(() => Task.FromResult(1));

            var UserCrud = new Crud();


            await Assert.ThrowsAnyAsync<DbUpdateException>(async () => await UserCrud.Create(User));

        }

        [Fact]
        public async Task Positive_FindAll()
        {
            var UserMock = new Mock<DbSet<User>>();

            UserMock.Setup(us => us.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                .Callback((User user, CancellationToken token) => { })
                .Returns((User user, CancellationToken token) => ValueTask.FromResult((EntityEntry<User>)null!));
            MockDbContext.Setup(us => us.Set<User>()).Returns(UserMock.Object);
            MockDbContext.Setup(us => us.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(() => Task.FromResult(1));

            var UserCrud = new Crud();
            var result = await UserCrud.FindAll<User>();

            Assert.NotNull(result);

        }
        //negative find

        [Fact]
        public async Task Positive_FindOne()
        {
            var UserMock = new Mock<DbSet<User>>();


            UserMock.Setup(us => us.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                .Callback((User user, CancellationToken token) => { })
                .Returns((User user, CancellationToken token) => ValueTask.FromResult((EntityEntry<User>)null!));
            MockDbContext.Setup(us => us.Set<User>()).Returns(UserMock.Object);
            MockDbContext.Setup(us => us.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(() => Task.FromResult(1));

            var UserCrud = new Crud();
            var TestUser = await UserCrud.Create(new User
            {
                Name = "Test",
                Phone = "0000000000",
                Email = "old@old.com",
                AuthId = "user"

            });
            var result = await UserCrud.Find<User>(TestUser.UserID);

            Assert.NotNull(result);
            Assert.Contains("Test", result.Name);
        }

        [Fact]
        public async Task Negative_FindOne()
        {
            var UserMock = new Mock<DbSet<User>>();


            UserMock.Setup(us => us.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                .Callback((User user, CancellationToken token) => { })
                .Returns((User user, CancellationToken token) => ValueTask.FromResult((EntityEntry<User>)null!));
            MockDbContext.Setup(us => us.Set<User>()).Returns(UserMock.Object);
            MockDbContext.Setup(us => us.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(() => Task.FromResult(1));

            var UserCrud = new Crud();
            await Assert.ThrowsAnyAsync<KeyNotFoundException>( async ()=> await UserCrud.Find<User>(new Guid("D5367A3A-D98C-4A24-11F4-05DAD77C01C2")));

           // Assert.Throws<ArgumentException>( ()=> result);
           // Assert.NotNull(result);
           // Assert.True(true);


        }

        [Fact]
        public async Task Positive_Update()
        {

            var UserMock = new Mock<DbSet<User>>();


            UserMock.Setup(us => us.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                .Callback((User user, CancellationToken token) => { })
                .Returns((User user, CancellationToken token) => ValueTask.FromResult((EntityEntry<User>)null!));
            MockDbContext.Setup(us => us.Set<User>()).Returns(UserMock.Object);
            MockDbContext.Setup(us => us.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(() => Task.FromResult(1));

            var UserCrud = new Crud();

            var oldUser = await UserCrud.Create(new User
            {
                Name = "test",
                Phone = "0000000000",
                Email = "old@old.com",
                AuthId = "user"

            });

            var NewUser = new User
            {
                UserID = oldUser.UserID,
                Name = "updates",
                Phone = "111111111111",
                Email = "updated@email.com",
                AuthId = "updated"
            };
            string guid = oldUser.UserID.ToString();

            var result = await UserCrud.Update<User>(new Guid(guid), NewUser);


            Assert.Contains("updates", result.Name);
            Assert.Contains("1111111111", result.Phone);
            Assert.Contains("updated@email.com", result.Email);
            Assert.Contains("updated", result.AuthId);

            var newResult = await UserCrud.Find<User>(new Guid(guid));

            Assert.Equal(newResult.Name, result.Name);

        }
        //negative update

        [Fact]
        public async Task Positive_Delete()
        {
            var UserMock = new Mock<DbSet<User>>();


            UserMock.Setup(us => us.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                .Callback((User user, CancellationToken token) => { })
                .Returns((User user, CancellationToken token) => ValueTask.FromResult((EntityEntry<User>)null!));
            MockDbContext.Setup(us => us.Set<User>()).Returns(UserMock.Object);
            MockDbContext.Setup(us => us.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(() => Task.FromResult(1));

            var UserCrud = new Crud();

            var allUsers = await UserCrud.FindAll<User>();

            Guid sacId = allUsers[0].UserID;

            var result = await UserCrud.Delete<User>(sacId);
            var AfterDeleteUsers = await UserCrud.FindAll<User>();

            Assert.Equal(AfterDeleteUsers.Count, allUsers.Count - 1);
            Assert.True(result);


        }

        [Fact]
        public async Task Negative_Delete()
        {
            var UserMock = new Mock<DbSet<User>>();


            UserMock.Setup(us => us.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                .Callback((User user, CancellationToken token) => { })
                .Returns((User user, CancellationToken token) => ValueTask.FromResult((EntityEntry<User>)null!));
            MockDbContext.Setup(us => us.Set<User>()).Returns(UserMock.Object);
            MockDbContext.Setup(us => us.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(() => Task.FromResult(1));

            var UserCrud = new Crud();

            var result = await UserCrud.Delete<User>(new Guid());


            Assert.False(result);




        }

    }
}
