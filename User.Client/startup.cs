using Microsoft.Extensions.DependencyInjection;
using OfficeOpenXml;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        // Set EPPlus license context
        ExcelPackage.LicenseContext = LicenseContext.Commercial; // or LicenseContext.NonCommercial

        // Other service configurations...
    }

    // Other methods in the Startup class...
}
