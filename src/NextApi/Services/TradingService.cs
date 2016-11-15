using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NextApi.Models;
using Microsoft.Extensions.Options;
using System.IO;
using System.Threading;

namespace NextApi.Services
{
    public class TradingService : IService<Trading>
    {
        private readonly Trading[] _collection;
        private static int _next;
        public TradingService(IOptions<FileStorageSettings> storageSettings)
        {
            var settings = storageSettings.Value;
            _collection = LoadFromFile(settings.UsersPath);
        }

        private Trading[] LoadFromFile(string path)
        {
            if (!File.Exists(Path.Combine(Directory.GetCurrentDirectory(), path)))
            {
                throw new FileNotFoundException(String.Format("Cannot find file {0}", path));
            }

            var result = File.ReadLines(Path.Combine(Directory.GetCurrentDirectory(), path))
                .Skip(1)
                .Select(line => line.Split(','))
                .Select(item => new Trading
                {
                    UserName = item[0],
                    AgencyId = item[1],
                    SaleHouseId = item[2]
                })
                .ToArray();

            return result;
        }

        public Trading GetNext()
        {
            var i = (uint)Interlocked.Increment(ref _next) % _collection.Length;
            return _collection[i];
        }
    }
}
