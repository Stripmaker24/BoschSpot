using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoschSpot.App.Utilities.ModelState
{
    public class ModelStateTransferValue
    {
        public string Key { get; set; }
        public string AttemptedValue { get; set; }
        public object RawValue { get; set; }
        public ICollection<string> ErrorMessages { get; set; } = new List<string>();
    }
}
