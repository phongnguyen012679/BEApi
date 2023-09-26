using BEApi.Data;
using BEApi.Dtos;
using OpenAI.Extensions;
using BEApi.Servies;
using BEApi.Extensions;
using BEApi.MIddleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerServices();
builder.Services.AddApplicationServices(builder.Configuration, builder.Environment.IsProduction());
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMemoryCache();
builder.Services.AddOpenAIService(settings => { settings.ApiKey = builder.Configuration["OpenAI:ApiKey"]; });
builder.Services.AddAuthenticationServices(builder.Configuration);
builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .SetIsOriginAllowed((host) => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

builder.Services.Configure<AppSettingDto>(builder.Configuration.GetSection("AppSettings"));
builder.Services.Configure<OpenAIDto>(builder.Configuration.GetSection("OpenAI"));
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IDiaryRepo, DiaryRepo>();
builder.Services.AddTransient<IMailService, MailService>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
