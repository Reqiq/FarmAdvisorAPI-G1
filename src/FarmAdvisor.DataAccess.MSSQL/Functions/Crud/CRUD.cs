using FarmAdvisor.DataAccess.MSSQL.DataContext;
using System;
using System.Collections.Generic;



using FarmAdvisor.DataAccess.MSSQL.Functions.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FarmAdvisor.DataAccess.MSSQL.Functions.Crud
{

    public class Param<P> where P:unmanaged
    {
        public P param { get; set; }

    }
    
    public class Crud:ICrud

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
            catch (Exception error)
            {
                throw new DbUpdateException("error creating with the specified data", error);
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
                        throw new KeyNotFoundException(" not found");
                    }

                }
            }
            catch (Exception error) { 

                throw new KeyNotFoundException("error finding with the specified Id", error); 
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
            catch (Exception error)
            {
                throw new KeyNotFoundException("error finding the specified spec", error);
            }

        }

        public async Task<T> Update<T>(Guid EntityID, T NewEntity) where T : class
        {
            try
            {
                using (var context = new DatabaseContext(DatabaseContext.Options.DatabaseOptions))
                {

                    var ObjFromDB = await context.FindAsync<T>(EntityID);
                    if (ObjFromDB !=null)
                    {
                        context.Entry(ObjFromDB).CurrentValues.SetValues(NewEntity);
                        await context.SaveChangesAsync();
                    }

                    return ObjFromDB!;

                }
            }
            catch(Exception error)
            {
                throw new KeyNotFoundException("error updating ", error );
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
            catch(Exception error)
            {
                throw new KeyNotFoundException("error deleting with the specified Id", error);
            }

        }
        
    }

}
