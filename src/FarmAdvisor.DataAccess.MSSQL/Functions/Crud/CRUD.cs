using FarmAdvisor.DataAccess.MSSQL.DataContext;
using System;
using System.Collections.Generic;


using FarmAdvisor.DataAccess.MSSQL.Entities;
using FarmAdvisor.DataAccess.MSSQL.Functions.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FarmAdvisor.DataAccess.MSSQL.Functions.Crud
{
    public class CRUD:ICrud
    {
        public async Task<T> Create<T>(T ObjectForDB) where T : class
        {
            try
            {
                using var context = new DatabaseContext(DatabaseContext.Options.DatabaseOptions);
                await context.AddAsync<T>(ObjectForDB);
                await context.SaveChangesAsync();
                return ObjectForDB;
            }
            catch
            {
                throw;
            }

        }

        public async Task<T> Find<T>(Guid EntityID) where T : class
        {
            try
            {
                using (var context = new DatabaseContext(DatabaseContext.Options.DatabaseOptions))
                {

                var result = await context.FindAsync<T>(EntityID);
                    if (result != null)
                    {
                        return result;
                    }
                    else
                    {
                        return null;
                    }

                }
            }
            catch {
                throw;
            }

        }
        public async Task<List<T>> FindAll<T>() where T : class
        {
            try
            {
                using (var context = new DatabaseContext(DatabaseContext.Options.DatabaseOptions))
                {

                    var result = await context.Set<T>().ToListAsync();
                    return result;
              
                }
            }
            catch
            {
                throw;
            }

        }

        public async Task<T> Update<T>(T NewEntity,Guid EntityID) where T : class
        {
            try
            {
                using (var context = new DatabaseContext(DatabaseContext.Options.DatabaseOptions))
                {

                    var ObjFromDB = await context.FindAsync<T>(EntityID);
                    if (ObjFromDB!=null)
                    {
                        context.Entry(ObjFromDB).CurrentValues.SetValues(NewEntity);
                        await context.SaveChangesAsync();
                    }

                    return ObjFromDB;

                }
            }
            catch
            {
                throw;
            }

        }

        public async Task<bool> Delete<T>(Guid EntityID) where T : class
        {
            try
            {
                using (var context = new DatabaseContext(DatabaseContext.Options.DatabaseOptions))
                {
                    var RecordToDel = await context.FindAsync<T>(EntityID);
                    if (RecordToDel !=null)
                    {
                        context.Remove(RecordToDel);
                        await context.SaveChangesAsync();
                        return true;
                    }
                    return false;      

                }
            }
            catch
            {
                throw;
            }

        }
     
        ////ADD
        
        ////UPDATE
        //addItemResult.Grade_Name = "Super Grade 7";
        //    var updateResult = await crud.Update<Grade>(addItemResult, addItemResult.Grade_ID);
        ////READ
        //var readResult = await crud.Read<Grade>(5);
        ////READ ALL FOR SPECIFIED ENTITY
        //var readAllResult = await crud.ReadAll<Grade>();
        ////DELETE
        //var deleted = await crud.Delete<Grade>(4);

    }


}
