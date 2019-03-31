using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceServer.JSONModels
{
    public class RefreshJSON
    {
        public string grant_type { get; set; }
        public string refresh_token { get; set; }
    }
}
