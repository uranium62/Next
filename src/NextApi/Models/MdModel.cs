using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NextApi.Models
{
    public class MdModel
    {
        public string ClientBusinessId { get; set; }
        public string ClientSourceSystem { get; set; }
        public string ProductBusinessId { get; set; }
        public string ProductSourceSystem { get; set; }
        public string SupplierBusinessId { get; set; }
        public string SupplierSourceSystem { get; set; }
        public string CompanyBusinessId { get; set; }
        public string CompanySourceSystem { get; set; }
        public string SmbcBusinessId { get; set; }
        public string SmbcSourceSystem { get; set; }
    }
}
