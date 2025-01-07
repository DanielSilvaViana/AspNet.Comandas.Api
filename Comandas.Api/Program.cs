using Comandas.Api;
using Comandas.Api.Data;
using Comandas.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//Adicionar o contexto do banco de dados, no caso SqlServer

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var chaveSecretaHexaDecimal = "1ec2d3ace73de4d656f76a1727fa957757fdae32b9a22176480a0c8d52149ffb";

var chaveSecretaBytes = new byte[chaveSecretaHexaDecimal.Length / 2];
for (int i = 0; i < chaveSecretaBytes.Length; i++)
{
    chaveSecretaBytes[i] = Convert.ToByte(chaveSecretaHexaDecimal.Substring(i * 2, 2), 16);
}


var chaveSecreta = new SymmetricSecurityKey(chaveSecretaBytes);

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = false,

        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("3e8acfc238f45a314fd4b2bde272678ad30bd1774743a11dbc5c53ac71ca494b"))
    };
});

// Add services to the container.
//builder.Services.AddScoped<IMesasServices>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(b =>
    {
        b.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
        {

            Title = "ApiDeComandas",
            Version = "v1",
            Description = "Api em .Net com autenticação em JWT",
            Contact = new OpenApiContact
            {
                Name = "Daniel",
                Email = "danielviana212@gmail.com",
                Url = new Uri("https://github.com/DanielSilvaViana/AspNet.Comandas.Api")
            }
        });
        b.EnableAnnotations();

        var xml = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

        b.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xml));

        b.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
        {
            Description =
            "JWT Authorization Header - utilizado com Bearer Authentication.\r\n\r\n" +
            "Digite 'Bearer' [espaço] e então seu token no campo abaixo.\r\n\r\n" +
            "Exemplo (informar sem as aspas): 'Bearer 12345abcdef'",
            Name = "Authorization",
            In = Microsoft.OpenApi.Models.ParameterLocation.Header,
            Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT"
        });
        b.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                         Type = ReferenceType.SecurityScheme,
                         Id = "Bearer"
                    }
                }, Array.Empty<string>()
            }
        });
    }
    );

var app = builder.Build();

using (var scopo = app.Services.CreateScope())
{
    var contexto = scopo.ServiceProvider.GetRequiredService<AppDbContext>();
    contexto.Database.Migrate();
    InicializarDados.Semear(contexto);
}

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
