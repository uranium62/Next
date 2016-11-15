using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NextApi.Models;
using Microsoft.Extensions.Options;

namespace NextApi.Services
{
    public class MdService : IService<MdModel>
    {
        private readonly BusinessModel[] _clients;
        private static int _nextClient;
        private readonly BusinessModel[] _products;
        private static int _nextProduct;
        private readonly BusinessModel[] _suppliers;
        private static int _nextSupplier;
        private readonly BusinessModel[] _companies;
        private static int _nextCompany;
        private readonly BusinessModel[] _smbcs;
        private static int _nextSmbc;
        public MdService(IOptions<FileStorageSettings> storageSettings)
        {
            var settings = storageSettings.Value;

            _clients = LoadFromFile(settings.ClientsPath, 8, 12);
            _products = LoadFromFile(settings.ProductsPath, 7, 9);
            _suppliers = LoadFromFile(settings.SuppliersPath, 8, 15);
            _companies = LoadFromFile(settings.CompaniesPath, 7, 14);
            _smbcs = LoadFromFile(settings.SmbcsPath, 8, 11);
        }

        private BusinessModel[] LoadFromFile(string path, int columnBusinessId, int columnSourceSystem)
        {
            if (!File.Exists(Path.Combine(Directory.GetCurrentDirectory(), path)))
            {
                throw new FileNotFoundException(String.Format("Cannot find file {0}", path));
            }

            var result = File.ReadLines(Path.Combine(Directory.GetCurrentDirectory(), path))
                .Skip(1)
                .Select(line => line.Split(','))
                .Select(item => new BusinessModel
                {
                    BusinessId = item[columnBusinessId],
                    SourceSystem = item[columnSourceSystem]
                })
                .ToArray();

            return result;
        }

        private BusinessModel GetNextBusinessModel(ref int next, BusinessModel[] collection)
        {
            var i = (uint)Interlocked.Increment(ref next) % collection.Length;
            return collection[i];
        }

        public MdModel GetNext()
        {
            var client = GetNextBusinessModel(ref _nextClient, _clients);
            var product = GetNextBusinessModel(ref _nextProduct, _products);
            var supplier = GetNextBusinessModel(ref _nextSupplier, _suppliers);
            var company = GetNextBusinessModel(ref _nextCompany, _companies);
            var smbc = GetNextBusinessModel(ref _nextSmbc, _smbcs);

            return new MdModel()
            {
                ClientBusinessId = client.BusinessId,
                ClientSourceSystem = client.SourceSystem,
                ProductBusinessId = product.BusinessId,
                ProductSourceSystem = product.SourceSystem,
                SupplierBusinessId = supplier.BusinessId,
                SupplierSourceSystem = supplier.SourceSystem,
                CompanyBusinessId = company.BusinessId,
                CompanySourceSystem = company.SourceSystem,
                SmbcBusinessId = smbc.BusinessId,
                SmbcSourceSystem = smbc.SourceSystem
            };
        }
    }
}
