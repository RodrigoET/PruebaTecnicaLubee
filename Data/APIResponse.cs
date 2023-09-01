using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class APIResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool Success { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public object Result { get; set; }
    }
}
