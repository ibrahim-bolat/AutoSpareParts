namespace AutoSpareParts.MVC.Extensions
{
    public static class LocalizationExtension
    {

        public static void ConfigureLocalization(this WebApplication app)
        {
            app.UseRequestLocalization(opt =>
            {
                opt.AddSupportedCultures("tr-TR")
                    .AddSupportedUICultures("tr-TR")
                    .SetDefaultCulture("tr-TR");
            });
        }

    }
}
