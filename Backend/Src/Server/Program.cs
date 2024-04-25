var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    var kestrelSection = configuration.GetSection("Kestrel");
    serverOptions.Configure(kestrelSection);
}).UseKestrel();

builder.Services.AddAntiforgery();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<JwtOptions>(
    configuration.GetSection(nameof(JwtOptions)));

builder.Services.AddDatabase(configuration);
builder.Services.AddHelpers();
builder.Services.AddRepositories();
builder.Services.AddServices();

builder.Services.AddJwtAuthentication(configuration);
builder.Services.AddSwagger();

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

builder.Services.AddCors();

var app = builder.Build();

#if DEBUG
app.ConfigureSwagger();
#else

#endif
app.UseAntiforgery();


app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) 
    .AllowCredentials()); 

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();