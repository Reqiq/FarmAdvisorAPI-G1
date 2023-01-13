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
using FarmAdvisor.DataAccess.MSSQL.Functions.Crud;
using FarmAdvisor.Models.Models;

namespace FarmAdvisor.Function.Test.DataAcess.MSSQLTest
{
    public class CRUDTest
    {
        Mock<DatabaseContext> MockDbContext = new Mock<DatabaseContext>();




        [Fact]
        public async Task Positive_CreateUser()
        {
            var UserMock = new Mock<DbSet<UserModel>>();
            var User = new UserModel
            {
                Name = "Test",
                Email = "Test@test.com",
                AuthId = "admin",
                Phone = "1234567890"
            };

            UserMock.Setup(us => us.AddAsync(It.IsAny<UserModel>(), It.IsAny<CancellationToken>()))
                .Callback((UserModel user, CancellationToken token) => { })
                .Returns((UserModel user, CancellationToken token) =>  ValueTask.FromResult((EntityEntry<UserModel>)null!) );
            MockDbContext.Setup(us=>us.Set<UserModel>()).Returns(UserMock.Object);
            MockDbContext.Setup(us => us.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(() => Task.FromResult(1));

            var UserCrud = new Crud();
            var result = await UserCrud.Create(User);

            Assert.NotNull(result);
            // Assert.Equal(User, result);

        }

        [Fact]
        public async Task Negative_CreateUser()
        {
            var UserMock = new Mock<DbSet<UserModel>>();

            var User = new UserModel
            {};

            UserMock.Setup(us => us.AddAsync(It.IsAny<UserModel>(), It.IsAny<CancellationToken>()))
                .Callback((UserModel user, CancellationToken token) => { })
                .Returns((UserModel user, CancellationToken token) => ValueTask.FromResult((EntityEntry<UserModel>)null!));
            MockDbContext.Setup(us => us.Set<UserModel>()).Returns(UserMock.Object);
            MockDbContext.Setup(us => us.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(() => Task.FromResult(1));

            var UserCrud = new Crud();


            await Assert.ThrowsAnyAsync<DbUpdateException>(async () => await UserCrud.Create(User));

        }

        [Fact]
        public async Task Positive_FindAll()
        {
            var UserMock = new Mock<DbSet<UserModel>>();

            UserMock.Setup(us => us.AddAsync(It.IsAny<UserModel>(), It.IsAny<CancellationToken>()))
                .Callback((UserModel user, CancellationToken token) => { })
                .Returns((UserModel user, CancellationToken token) => ValueTask.FromResult((EntityEntry<UserModel>)null!));
            MockDbContext.Setup(us => us.Set<UserModel>()).Returns(UserMock.Object);
            MockDbContext.Setup(us => us.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(() => Task.FromResult(1));

            var UserCrud = new Crud();
            var result = await UserCrud.FindAll<UserModel>();

            Assert.NotNull(result);

        }
        //negative find

        [Fact]
        public async Task Positive_FindOne()
        {
            var UserMock = new Mock<DbSet<UserModel>>();


            UserMock.Setup(us => us.AddAsync(It.IsAny<UserModel>(), It.IsAny<CancellationToken>()))
                .Callback((UserModel user, CancellationToken token) => { })
                .Returns((UserModel user, CancellationToken token) => ValueTask.FromResult((EntityEntry<UserModel>)null!));
            MockDbContext.Setup(us => us.Set<UserModel>()).Returns(UserMock.Object);
            MockDbContext.Setup(us => us.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(() => Task.FromResult(1));

            var UserCrud = new Crud();
            var TestUser = await UserCrud.Create(new UserModel
            {
                Name = "Test",
                Phone = "0000000000",
                Email = "old@old.com",
                AuthId = "user"

            });
            var result = await UserCrud.Find<UserModel>(TestUser.UserID);

            Assert.NotNull(result);
            Assert.Contains("Test", result.Name);
        }

        [Fact]
        public async Task Negative_FindOne()
        {
            var UserMock = new Mock<DbSet<UserModel>>();


            UserMock.Setup(us => us.AddAsync(It.IsAny<UserModel>(), It.IsAny<CancellationToken>()))
                .Callback((UserModel user, CancellationToken token) => { })
                .Returns((UserModel user, CancellationToken token) => ValueTask.FromResult((EntityEntry<UserModel>)null!));
            MockDbContext.Setup(us => us.Set<UserModel>()).Returns(UserMock.Object);
            MockDbContext.Setup(us => us.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(() => Task.FromResult(1));

            var UserCrud = new Crud();
            await Assert.ThrowsAnyAsync<KeyNotFoundException>( async ()=> await UserCrud.Find<UserModel>(new Guid("D5367A3A-D98C-4A24-11F4-05DAD77C01C2")));

           // Assert.Throws<ArgumentException>( ()=> result);
           // Assert.NotNull(result);
           // Assert.True(true);


        }

        [Fact]
        public async Task Positive_Update()
        {

            var UserMock = new Mock<DbSet<UserModel>>();


            UserMock.Setup(us => us.AddAsync(It.IsAny<UserModel>(), It.IsAny<CancellationToken>()))
                .Callback((UserModel user, CancellationToken token) => { })
                .Returns((UserModel user, CancellationToken token) => ValueTask.FromResult((EntityEntry<UserModel>)null!));
            MockDbContext.Setup(us => us.Set<UserModel>()).Returns(UserMock.Object);
            MockDbContext.Setup(us => us.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(() => Task.FromResult(1));

            var UserCrud = new Crud();

            var oldUser = await UserCrud.Create(new UserModel
            {
                Name = "test",
                Phone = "0000000000",
                Email = "old@old.com",
                AuthId = "user"

            });

            var NewUser = new UserModel
            {
                UserID = oldUser.UserID,
                Name = "updates",
                Phone = "111111111111",
                Email = "updated@email.com",
                AuthId = "updated"
            };
            string guid = oldUser.UserID.ToString();

            var result = await UserCrud.Update<UserModel>(new Guid(guid), NewUser);


            Assert.Contains("updates", result.Name);
            Assert.Contains("1111111111", result.Phone);
            Assert.Contains("updated@email.com", result.Email);
            Assert.Contains("updated", result.AuthId);

            var newResult = await UserCrud.Find<UserModel>(new Guid(guid));

            Assert.Equal(newResult.Name, result.Name);

        }
        //negative update

        [Fact]
        public async Task Positive_Delete()
        {
            var UserMock = new Mock<DbSet<UserModel>>();


            UserMock.Setup(us => us.AddAsync(It.IsAny<UserModel>(), It.IsAny<CancellationToken>()))
                .Callback((UserModel user, CancellationToken token) => { })
                .Returns((UserModel user, CancellationToken token) => ValueTask.FromResult((EntityEntry<UserModel>)null!));
            MockDbContext.Setup(us => us.Set<UserModel>()).Returns(UserMock.Object);
            MockDbContext.Setup(us => us.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(() => Task.FromResult(1));

            var UserCrud = new Crud();

            var allUsers = await UserCrud.FindAll<UserModel>();

            Guid sacId = allUsers[0].UserID;

            var result = await UserCrud.Delete<UserModel>(sacId);
            var AfterDeleteUsers = await UserCrud.FindAll<UserModel>();

            Assert.Equal(AfterDeleteUsers.Count, allUsers.Count - 1);
            Assert.True(result);


        }

        [Fact]
        public async Task Negative_Delete()
        {
            var UserMock = new Mock<DbSet<UserModel>>();


            UserMock.Setup(us => us.AddAsync(It.IsAny<UserModel>(), It.IsAny<CancellationToken>()))
                .Callback((UserModel user, CancellationToken token) => { })
                .Returns((UserModel user, CancellationToken token) => ValueTask.FromResult((EntityEntry<UserModel>)null!));
            MockDbContext.Setup(us => us.Set<UserModel>()).Returns(UserMock.Object);
            MockDbContext.Setup(us => us.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(() => Task.FromResult(1));

            var UserCrud = new Crud();

            var result = await UserCrud.Delete<UserModel>(new Guid());


            Assert.False(result);




        }

    }
}
