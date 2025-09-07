using Ecom.Core.DTO;
using Ecom.Core.Entites.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.interfaces
{
    public interface IProductRepositry:IGenericRepositry<Product>
    {
        // for future methods

        Task<bool> AddAsync(AddProductDTO productDTO);
        Task<bool> UpdateAsync( UpdateProductDTO updateproductDTO);
    }
}
