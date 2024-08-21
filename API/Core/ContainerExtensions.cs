using Application;
using Application.DTO;
using Application.UseCases.Commands.Categories;
using Application.UseCases.Commands.Users;
using Implementation;
using Implementation.Logging.UseCase;
using Implementation.UseCases.Commands.Users;
using Implementation.UseCases.Commands.Categories;
using Implementation.Validators;
using System.IdentityModel.Tokens.Jwt;
using Application.UseCases.Commands.Containers;
using Implementation.UseCases.Commands.Containers;
using Application.UseCases.Queries;
using Implementation.UseCases.Queries;
using Application.UseCases.Commands.Products;
using Implementation.UseCases.Commands.Products;

namespace API.Core

{
    public static class ContainerExtensions
    {
        public static void AddUseCases(this IServiceCollection services)
        {
            
            services.AddTransient<UseCaseHandler>();
            services.AddTransient<IUseCaseLogger, DBUseCaseLogger>();

            //Users
            services.AddTransient<IRegisterUserCommand, EfRegisterUserCommand>();
            services.AddTransient<RegisterUserDtoValidator>();
            services.AddTransient<IGetUsersQuery, EfGetUsersQuery>();
            services.AddTransient<UpdateUserDtoValidator>();
            services.AddTransient<IUpdateUserCommand, EfUpdateUserCommand>();
            services.AddTransient<UpdateUserAccessDto>();
            services.AddTransient<IUpdateUserAccessCommand, EfUpdateUserAccessCommand>();

            //Categories
            services.AddTransient<ICreateCategoryCommand, EfCreateCategoryCommand>();
            services.AddTransient<CreateCategoryValidator>();
            services.AddTransient<IUpdateCategoryCommand, EfUpdateCategoryCommand>();
            services.AddTransient<UpdateCategoryValidator>();
            services.AddTransient<IGetCategoriesQuery, EfGetCategoriesQuery>();

            //Containers
            services.AddTransient<ICreateContainerCommand, EfCreateContainerCommand>();
            services.AddTransient<CreateContainerValidator>();
            services.AddTransient<IUpdateContainerCommand, EfUpdateContainerCommand>();
            services.AddTransient<UpdateContainerValidator>();
            services.AddTransient<IGetContainersQuery, EfGetContainersQuery>();

            //Products
            services.AddTransient<ICreateProductCommand, EfCreateProductCommand>();
            services.AddTransient<CreateProductValidator>();
            services.AddTransient<IUpdateProductsCommand, EfUpdateProductCommand>();
            services.AddTransient<UpdateProductValidator>();
            services.AddTransient<IGetProductsQuery, EfGetProductsQuery>();
        }

        public static Guid? GetTokenId(this HttpRequest request)
        {
            if (request == null || !request.Headers.ContainsKey("Authorization"))
            {
                return null;
            }

            string authHeader = request.Headers["Authorization"].ToString();

            if (authHeader.Split("Bearer ").Length != 2)
            {
                return null;
            }

            string token = authHeader.Split("Bearer ")[1];

            var handler = new JwtSecurityTokenHandler();

            var tokenObj = handler.ReadJwtToken(token);

            var claims = tokenObj.Claims;

            var claim = claims.First(x => x.Type == "jti").Value;

            var tokenGuid = Guid.Parse(claim);

            return tokenGuid;
        }
    }
}
