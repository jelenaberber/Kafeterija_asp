﻿using Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Queries
{
    public interface IGetCategoriesQuery : IQuery<PagedResponse<CategoryDto>, CategorySearch>
    {
    }
}
