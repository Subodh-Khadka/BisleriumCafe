using Microsoft.Extensions.Logging;

namespace BisleriumCafe
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            Utils.BisleriumUtils.ApplicationDirectoryPath();
            Utils.BisleriumUtils.ApplicationFilePath();
            BisleriumCafe.Data.Services.AddInsService.InjectSampleAddInsData();
            Utils.BisleriumUtils.AddInsFilePath();
            Utils.BisleriumUtils.CustomerFilePath();    
            Utils.BisleriumUtils.OrderFilePath();    
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
    		builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();

            builder.Services.AddScoped<BisleriumCafe.Data.Services.CofeeTypesService>();
            builder.Services.AddScoped<BisleriumCafe.Data.Services.AddInsService>();
            builder.Services.AddScoped<BisleriumCafe.Data.Services.CartService>();                                    
            builder.Services.AddScoped<BisleriumCafe.Data.Services.OrderService>();                                    
            builder.Services.AddScoped<BisleriumCafe.Data.Services.CustomerMembershipService>();                                    
            builder.Services.AddScoped<BisleriumCafe.Data.Services.ReportService>();                                    


#endif

            return builder.Build();
        }
    }
}
