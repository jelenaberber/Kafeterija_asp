using Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Queries
{
    public interface IGetProductsQuery : IQuery<PagedResponse<ProductDto>, ProductSearch>
    {
    }
}
