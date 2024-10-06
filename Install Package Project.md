<h1 align="center">
    Setting Project
</h1>

## Install Dotnet Tool by Version
- Check `dotnet --version`
- Install version 6 `dotnet tool install --global dotnet-ef --version 6.*`
- Or install version 8 `dotnet tool install --global dotnet-ef --version 8.*`
- Verify the installation `dotnet ef --version`

## Install NuGet Package in Terminal
- __BusinessObjects Layer__
    - `dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0.5`
    - `dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 8.0.5`
    - `dotnet add package Microsoft.EntityFrameworkCore.Tools --version 8.0.5`
    - `dotnet add package Microsoft.Extensions.Configuration --version 8.0.0`
    - `dotnet add package Microsoft.Extensions.Configuration.Json --version 8.0.0`

- __API Layer__
    - `dotnet add package Microsoft.AspNetCore.OData --version 8.2.5`
    - `dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 8.0.6`

## Database scaffold at _BusinessObjects Layer_
    dotnet ef dbcontext scaffold "Server=(local);Uid=sa;Pwd=myPassw0rd;Database=EnglishPremierLeague2024DB;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models

## appsettings.json at _BusinessObjects Layer_
- Get the IP address of your host machine, by running a command prompt and typing `ipconfig`
- Enable for TCP port: 1433 in 'SQL Server Configuration Manager'
```json
{
	"ConnectionStrings": {
		"DefaultConnection": "Server=192.168.XX.XX,1433;Database=MotelManagement2024DB;Uid=sa;Pwd=myPassw0rd;TrustServerCertificate=true;MultipleActiveResultSets=true;"
	}
}
```

- __Add this to project file: *BusinessObjects.csproj*__

```xml
<ItemGroup>
	<None Update="appsettings.json">
		<CopyToOutputDirectory>Always</CopyToOutputDirectory>
	</None>
</ItemGroup>
```

## Add this to DbContext
```cs
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(GetConnectionString());

private static string GetConnectionString() {
	IConfiguration config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", true, true)
        .Build();
	return config.GetConnectionString("DefaultConnection")
        ?? throw new Exception("Connection String 'DefaultConnection' not found in appsettings.json");
}
```

## appsettings.json at _API Layer_
```json
{
	"ConnectionStrings": {
		"DefaultConnection": "Server=192.168.XX.XX,1433;Database=MotelManagement2024DB;Uid=sa;Pwd=myPassw0rd;TrustServerCertificate=true;MultipleActiveResultSets=true;"
	},
    "Jwt": {
        "Key": "2C2C322D2C322F2D2D2C",
        "Issuer": "XOR_Brute_Force_1D[,,2-,2/--,]",
        "Audience": "11/01/2001"
    }
}
```

## Program.cs at *API Layer*
```cs
static IEdmModel GetEdmModel()
{
    ODataConventionModelBuilder builder = new();
    builder.EntitySet<Item1>("");
    return builder.GetEdmModel();
}

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers().AddOData(opt => opt.Select().Filter().Count().OrderBy().Expand().SetMaxTop(100).AddRouteComponents("odata", GetEdmModel()));

builder.Services.AddCors(o =>
{
    o.AddPolicy("AllowAnyOrigin", corsPolicyBuilder =>
    {
        corsPolicyBuilder.SetIsOriginAllowed(x => _ = true).AllowAnyMethod().AllowAnyHeader().AllowCredentials();
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.DescribeAllParametersInCamelCase();
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Please enter a valid token",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddScoped<Item1Repository>();
builder.Services.AddScoped<Item2Repository>();
builder.Services.AddScoped<Item3Repository>();
builder.Services.AddScoped<Item1Service>();
builder.Services.AddScoped<Item2Service>();
builder.Services.AddScoped<Item3Service>();
```

- Add this to after ***`var app = builder.Build()`***
```cs
app.UseHttpsRedirection();
app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
```

## Create "LoginRequest.cs" in Models folder for *API Layer*
```cs
public sealed record LoginRequest(string UserEmail, string UserPassword);
```

## Authorize for *API Layer*
```cs
[ApiController]
[Route("api/[controller]")]
public class AuthorController : ControllerBase
{
    private readonly Item1Service _service;
    private readonly IConfiguration _config;

    public AuthorController(Item1Service service, IConfiguration config)
    {
        _service = service;
        _config = config;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest loginRequest)
    {
        var account = _service.CheckLogin(loginRequest.UserEmail, loginRequest.UserPassword);
        if (account == null || account.Role != 2 && account.Role != 3)
            return Unauthorized();

        var tokenString = GenerateJSONWebToken(account);
        //return Ok(new { token = tokenString });
        return Ok(tokenString);
    }

    private string GenerateJSONWebToken(Item1 userInfo)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"],
        [
            new("UserId", userInfo.UserAccountId.ToString()),
            new(ClaimTypes.Email, userInfo.UserEmail!),
            new(ClaimTypes.Role, userInfo.Role.ToString()!)
        ], expires: DateTime.Now.AddMinutes(120), signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
```

## GET, POST, PUT, DELETE for *API Layer*
```cs
[ApiController]
[Route("api/[controller]")]
public class YourObjectsController : ControllerBase
{
    private readonly YourObjectService _service;

    public YourObjectsController(YourObjectService service)
    {
        _service = service;
    }

    // GET: YourObjects
    [HttpGet]
    [Authorize(Roles = "2, 3")]
    [EnableQuery]
    public IActionResult GetAll()
    {
        return Ok(_service.Get());
    }

    // GET: YourObjects/Details/WP00111
    [HttpGet("{id}")]
    [Authorize(Roles = "2, 3")]
    public async Task<IActionResult> Details(string id)
    {
        return Ok(await _service.GetYourObjectById(id));
    }

    // POST: YourObjects/Create
    [HttpPost]
    [Authorize(Roles = "3")]
    public async Task<IActionResult> Create([FromBody] YourObjectRequest YourObject)
    {
        var newYourObject = await _service.CreateYourObject(ConvertObject(YourObject));
        if (newYourObject > 0)
            return Ok("Successfully created!");

        return BadRequest("Your painting Id is already exist");
    }

    // Put: YourObjects/Edit/WP00115
    [HttpPut]
    [Authorize(Roles = "3")]
    public async Task<IActionResult> Edit(string id, [FromBody] YourObjectRequest YourObject)
    {
        YourObject.PaintingId = id;
        var modYourObject = await _service.UpdateYourObject(ConvertObject(YourObject));
        if (modYourObject > 0)
            return Ok("Successfully updated!");

        return BadRequest("This painting Id does not exist");
    }

    // Delete: YourObjects/Delete/WP00115
    [HttpDelete("{id}")]
    [Authorize(Roles = "3")]
    public async Task<IActionResult> Delete(string id)
    {
        var revYourObject = await _service.RemoveYourObject(id);
        if (revYourObject > 0)
            return Ok("Successfully deleted!");

        return BadRequest("Oops!! Something went wrong. Please try again later");
    }

    private static YourObject ConvertObject(YourObjectRequest request) {
        YourObject YourObjectConvert = new()
        {
            
        };

        return YourObjectConvert;
    }
}
```

## Data annotation validation
- Regex:
    - Each word of first letters is capitalize and allow (@, #, $, &, (, )) special characters.
    - `[RegularExpression("^(?:\\b[A-Z]{1}[a-z]*\\b[\\@\\#\\$\\&\\(\\)]?\\s?)+$", ErrorMessage = "The cartoon name must be in range[15,120] and match case: \"This Is An Example\" or \"This Is( An Example)\"")]`
- Range:
    - `[Range(1, int.MaxValue, ErrorMessage = "The duration must be greater than 0")]`
