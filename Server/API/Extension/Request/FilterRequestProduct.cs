using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.ComponentModel.DataAnnotations;

namespace API.Extension.Request
{
    public class FilterRequestProduct
    {
        public int page {get; set;}
        public List<Guid> filerByCategory {get; set;}
        public int filerByStar {get; set;}

    }
}