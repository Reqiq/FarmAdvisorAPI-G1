using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmAdvisor.DataAccess.AzureTableStorage.services;

namespace FarmAdvisor.DataAccess.AzureTableStorage.test
{
    public class TableStorageServicesTest
    {
        public ITableStorageService StorageService { get; }
        public TableStorageServicesTest(ITableStorageService storageService)
        {
            this.StorageService = storageService ?? throw new ArgumentNullException(nameof(storageService));
        }

    }
}