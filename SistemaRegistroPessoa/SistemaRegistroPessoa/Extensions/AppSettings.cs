using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaRegistroPessoa.Extensions
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public int ExpireHours { get; set; }
        public string Issuer { get; set; }
        public string ValidOn { get; set; }
    }
}
