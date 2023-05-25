using Microsoft.EntityFrameworkCore;
using Share.Infracstructure.Extensions;
using ITfoxtec.Identity.Saml2.MvcCore.Configuration;
using ITfoxtec.Identity.Saml2.Schemas.Metadata;
using ITfoxtec.Identity.Saml2;
using Authentication.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#pragma warning disable CS8604 // Possible null reference argument.
builder.Services.AddSqlServerPersistence(builder.Configuration.GetConnectionString("Karrot"));
#pragma warning restore CS8604 // Possible null reference argument.

builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
    .AllowAnyHeader();
}));
builder.Services.AddAuthorization();

builder.Services.Configure<Saml2Configuration>(builder.Configuration.GetSection("Saml2"));

builder.Services.Configure<Saml2Configuration>(saml2Configuration =>
{
    saml2Configuration.AllowedAudienceUris.Add(saml2Configuration.Issuer);

    var entityDescriptor = new EntityDescriptor();
    entityDescriptor.ReadIdPSsoDescriptorFromUrl(new Uri(builder.Configuration["Saml2:IdPMetadata"]));
    if (entityDescriptor.IdPSsoDescriptor != null)
    {
        saml2Configuration.SingleSignOnDestination = entityDescriptor.IdPSsoDescriptor.SingleSignOnServices.First().Location;
        saml2Configuration.SignatureValidationCertificates.AddRange(entityDescriptor.IdPSsoDescriptor.SigningCertificates);
    }
    else
    {
        throw new Exception("IdPSsoDescriptor not loaded from metadata.");
    }
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Authorize", policy =>
    {
        policy.RequireAssertion(context =>
        {
            return context.User.Identity == null || !context.User.Identity.IsAuthenticated;
        });
    });
});

builder.Services.AddSaml2();

var app = builder.Build();

app.UseDatabaseMigration();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("MyPolicy");

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseSaml2();

app.UseMiddleware<TenantMiddleware>();

app.UseMiddleware<JwtMiddleware>();

app.MapControllers();

app.Run();
