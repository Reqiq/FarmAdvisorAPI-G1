using System;
using FarmAdvisor.DataAccess.MSSQL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAdvisor.DataAccess.MSSQL.Functions.Crud
{
    public class Test
    {
        CRUD Crud = new CRUD();
        public async void createUser()
        {
            var user = new User();
            user.Name = "yoseph";
            user.Email = "yosephhabtamu3@gmail.com";
            user.AuthId = "Auth";
            await Crud.Create(user);
            Console.WriteLine("create success");

        }
    }
}
