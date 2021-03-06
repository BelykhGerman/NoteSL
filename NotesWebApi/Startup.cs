using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NotesApplication;
using NotesApplication.Common.Mappings;
using NotesApplication.Interfaces;
using NotesPersistence;
using System.Reflection;

namespace NotesWebApi {
    public class Startup {

        public IConfiguration Configuration { get; set; }

        public Startup ( IConfiguration configuration ) {
            Configuration = configuration;
        }

        public void ConfigureServices ( IServiceCollection services ) {
            services.AddAutoMapper ( config => {
                config.AddProfile ( new AssemblyMappingProfile ( Assembly.GetExecutingAssembly () ) );
                config.AddProfile ( new AssemblyMappingProfile ( typeof ( INotesDbContext ).Assembly ) );
            } );

            services.AddApplication ();
            services.AddPersistence ( Configuration );
            services.AddControllers ();

            services.AddCors ( options => {
                options.AddPolicy ( "AllowAll" , policy => {
                    policy.AllowAnyHeader ();
                    policy.AllowAnyMethod ();
                    policy.AllowAnyOrigin ();
                } );
            } );
        }

        public void Configure ( IApplicationBuilder app , IWebHostEnvironment env ) {
            if ( env.IsDevelopment () ) {
                app.UseDeveloperExceptionPage ();
            }

            app.UseRouting ();
            app.UseHttpsRedirection ();
            app.UseCors ( "AllowAll" );

            app.UseEndpoints ( endpoints => {
                endpoints.MapControllers ();
            } );
        }
    }
}
