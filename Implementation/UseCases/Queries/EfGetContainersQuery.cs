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

    public class EfGetContainersQuery : EfUseCase, IGetContainersQuery
    {
        public EfGetContainersQuery(Context context) : base(context)
        {
        }

        public int Id => 10;

        public string Name => "Search Containers";

        public PagedResponse<ContainterDto> Execute(ContainerSearch search)
        {
            var query = Context.Containers.Where(x => x.IsActive == true).AsQueryable();

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

            return new PagedResponse<ContainterDto>
            {
                CurrentPage = page,
                Data = query.Select(x => new ContainterDto
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
