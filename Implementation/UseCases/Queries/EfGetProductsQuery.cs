using Application.DTO;
using Application.UseCases.Queries;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Queries
{
    public class EfGetProductsQuery : EfUseCase, IGetProductsQuery
    {
        public EfGetProductsQuery(Context context) : base(context)
        {
        }

        public int Id => 12;

        public string Name => "Search products";

        public PagedResponse<ProductDto> Execute(ProductSearch search)
        {
            var query = Context.Products.Where(x => x.IsActive == true).AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword))
            {
                query = query.Where(x => x.Name.Contains(search.Keyword));
            }


            int totalCount = query.Count();

            int perPage = search.PerPage.HasValue ? (int)Math.Abs((double)search.PerPage) : 10;
            int page = search.Page.HasValue ? (int)Math.Abs((double)search.Page) : 1;

            //16 PerPage = 5, Page = 2

            int skip = perPage * (page - 1);

            query = query.Skip(skip).Take(perPage);

            return new PagedResponse<ProductDto>
            {
                CurrentPage = page,
                Data = query.Select(x => new ProductDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Category = new CategoryDto()
                    {
                        Name = x.Category.Name
                    }
                }).ToList(),
                PerPage = perPage,
                TotalCount = totalCount,
            };
        }
    }
}
