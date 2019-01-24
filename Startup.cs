using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ProdutCatalog.Data;
using ProdutCatalog.Repositories;
using Swashbuckle.AspNetCore.Swagger;

namespace ProdutCatalog
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddResponseCompression();
            services.AddScoped<StoreDataContext,StoreDataContext>();
            services.AddTransient<ProductRepository, ProductRepository>();

            services.AddSwaggerGen(x =>{
                x.SwaggerDoc("v1",new Info{Title ="API de Produtos e Categorias",Version="v1"});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseMvc();
            app.UseResponseCompression();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json","API de Produtos e Categorias");
            });
        }
    }
}
