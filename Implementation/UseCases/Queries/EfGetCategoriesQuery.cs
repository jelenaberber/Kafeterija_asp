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
    public class EfGetCategoriesQuery : EfUseCase, IGetCategoriesQuery
    {
        public EfGetCategoriesQuery(Context context) : base(context)
        {
        }

        public int Id => 9;

        public string Name => "Search Categories";

        public PagedResponse<CategoryDto> Execute(CategorySearch search)
        {
            var query = Context.Categories.Where(x => x.IsActive == true).AsQueryable();

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

            return new PagedResponse<CategoryDto>
            {
                CurrentPage = page,
                Data = query.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                }).ToList(),
                PerPage = perPage,
                TotalCount = totalCount,
            };
        }
    }
}
