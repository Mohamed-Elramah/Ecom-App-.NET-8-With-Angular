using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.Entites.Product
{
    public class Photo:BaseEntity<int>
    {
        public  String ImageName { get; set; }

        public int ProductId { get; set; }
        //[ForeignKey(nameof(ProductId))]
        //public virtual Product Product { get; set; }

    }
}
