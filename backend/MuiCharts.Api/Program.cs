using System.Reflection;
using LettuceEncrypt;
using MuiCharts.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
{
	// Add services to the container.
	// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
	builder.Services.AddLogging(options =>
	{
		options.AddConfiguration(builder.Configuration.GetSection("Logging"));
		options.AddConsole();
		options.AddDebug();
		options.AddEventSourceLogger();
	});

	builder.Services.AddControllers(options =>
	{
		options.SuppressAsyncSuffixInActionNames = false;
	});

	builder.AddInfrastructure();

	builder.Services.AddEndpointsApiExplorer();
	builder.Services.AddSwaggerGen(options =>
	{
		string xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
		string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFileName);
		options.IncludeXmlComments(xmlPath);
	});

	if (builder.Configuration.GetSection("LettuceEncrypt").Exists())
		builder.Services.AddLettuceEncrypt()
			.PersistDataToDirectory(new DirectoryInfo("/persistence"), null);
}

WebApplication app = builder.Build();
{
	// Configure the HTTP request pipeline.
	app.UseExceptionHandler("/error");
	app.UseSwagger();
	app.UseSwaggerUI();

	if (app.Configuration.GetSection("LettuceEncrypt").Exists())
		app.UseHttpsRedirection();

	app.MapControllers();

	app.Run();
}
