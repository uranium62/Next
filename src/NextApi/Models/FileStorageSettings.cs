using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NextApi.Models
{
    public class FileStorageSettings
    {
        public string ClientsPath { get; set; }
        public string CompaniesPath { get; set; }
        public string ProductsPath { get; set; }
        public string SmbcsPath { get; set; }
        public string SuppliersPath { get; set; }
        public string UsersPath { get; set; }
    }
}
