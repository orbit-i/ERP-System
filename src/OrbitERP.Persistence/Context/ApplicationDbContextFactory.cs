using System;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace OrbitERP.Persistence.Context;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var apiPath = ResolveApiPath();

        var builder = new ConfigurationBuilder()
            .SetBasePath(apiPath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        try
        {
            var apiAssembly = System.Reflection.Assembly.Load("OrbitERP.API");
            builder.AddUserSecrets(apiAssembly, optional: true);
        }
        catch
        {
            // Ignore if we can't load the API assembly for user secrets
        }

        IConfigurationRoot configuration = builder.Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("Could not find a connection string named 'DefaultConnection' in appsettings.json or User Secrets.");
        }

        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new ApplicationDbContext(optionsBuilder.Options);
    }

    private static string ResolveApiPath()
    {
        var currentDir = Directory.GetCurrentDirectory();
        var directory = new DirectoryInfo(currentDir);

        while (directory != null && !directory.GetFiles("*.sln").Any() && !directory.GetFiles("*.slnx").Any())
        {
            directory = directory.Parent;
        }

        if (directory != null)
        {
            var expectedApiPath = Path.Combine(directory.FullName, "src", "OrbitERP.API");
            if (Directory.Exists(expectedApiPath) && File.Exists(Path.Combine(expectedApiPath, "appsettings.json")))
            {
                return expectedApiPath;
            }
        }

        if (File.Exists(Path.Combine(currentDir, "appsettings.json")))
        {
            return currentDir;
        }

        throw new DirectoryNotFoundException(
            "Could not reliably locate the OrbitERP.API project to load appsettings.json. " +
            "Ensure you are running EF Core tools within the OrbitERP repository structure.");
    }
}
