using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace Ouzdev.JwtWebToken.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication();
            services.AddControllers();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {

                //Https protokolunu iptal et.
                opt.RequireHttpsMetadata = false;
                // Tokenın özelliklerini belirleme. 
                opt.TokenValidationParameters = new TokenValidationParameters()
                {


                    //Tokenı kim oluşturdu ?
                    ValidIssuer = "http://localhost",

                    //Tokenı kim kullanacak ?
                    ValidAudience = "http://localhost",

                    //Token hangi key değerine göre çözülecek ?
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("OguzOguzOguzOguz123")),

                    //Token yukardaki keye göre çözülebilsinmi ?
                    ValidateIssuerSigningKey = true,

                    /*Bu token ne kadar süre yaşayacak ? Buradaki işlemi tokenı oluştururken tanımlayacağız. Yani token oluştururken ne kadar süre yaşayacağını belirteceğiz. */
                    //Eğer tokenın süresi dolmuşsa sen o tokenı invalid yani geçersiz olarak kabul et. 
                    ValidateLifetime = true,

                    //ClockSkew => Serverlar arasındaki zaman farklılıklarını göz önünde bulundurarak süre arttırımı ve azaltımı yapmak için kullanılır.
                    //Biz kullanmayacağımız için "TimeSpan.Zero" tanımlamasını yapıyoruz.
                    ClockSkew = TimeSpan.Zero

                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            //Oturum Kontrolu
            app.UseAuthentication();

            //Yetki Kontrolu
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
