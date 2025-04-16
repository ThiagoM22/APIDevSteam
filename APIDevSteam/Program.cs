
using APIDevSteam.Data;
using APIDevSteam.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Adicionar Servico de banco de dados
builder.Services.AddDbContext<APIDevSteamContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adicionar o serviço de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});
// Adicionar o serviço de autenticação
// Serviço de EndPoints do Identity Framework
builder.Services.AddIdentityApiEndpoints<Usuario>(options =>
{
    options.SignIn.RequireConfirmedEmail = false; // Exige confirmação de email
    options.SignIn.RequireConfirmedAccount = false; // Exige confirmação de conta
    options.User.RequireUniqueEmail = true; // Exige email único
    options.Password.RequireNonAlphanumeric = false; // Exige caracteres não alfanuméricos
    options.Password.RequireLowercase = false; // Exige letras minúsculas
    options.Password.RequireUppercase = false; // Exige letras maiúsculas
    options.Password.RequireDigit = false; // Exige dígitos numéricos
    options.Password.RequiredLength = 4; // Exige comprimento mínimo da senha
})

.AddRoles<IdentityRole>() // Adicionando o serviço de roles
.AddEntityFrameworkStores<APIDevSteamContext>() // Adicionando o serviço de EntityFramework
.AddDefaultTokenProviders(); // Adiocionando o provedor de tokens padrão


// Swagger com Autenticação JWT Bearer
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "APIEscola", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


// Adicionar os Serviços de Autenticação e Autorização
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build(); // Cria o aplicativo

app.UseSwagger(); // Habilita o Swagger

app.UseSwaggerUI(); // Habilita a interface do Swagger

app.UseHttpsRedirection(); // Redireciona requisições HTTP para HTTPS

app.UseCors("AllowAll"); // Habilita o CORS

app.UseAuthentication(); // Habilita a autenticação

app.UseAuthorization(); // Habilita a autorização

app.MapControllers(); // Mapeia os controladores

app.MapGroup("/Usuario").MapIdentityApi<Usuario>(); // Mapeia o grupo de endpoints de autenticação

app.Run();// Executa o aplicativo

