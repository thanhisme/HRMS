using HRMS;
using HRMS.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.RegisterAppServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else if (app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "DocV1"); });
}

app.UseHttpsRedirection();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    .AllowCredentials());

app.UseRouting();

app.UseMiddleware<GlobalExceptionHandler>();

app.UseMiddleware<ProtectedMiddleware>();

app.UseAuthentication();

app.UseMiddleware<ClaimsTransformerMiddleware>();

app.UseAuthorization();

// authentication Token
app.MapControllers().RequireAuthorization();

app.Run();
