using Core_Nile.Repo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Model.Nile;
using Model.Nile.Interface;
using Model.Nile.Options;
using Model.Nile.Scheduler;
internal class Program

{ private static string jobname="";

    private static async Task<int> Main(string[] args)
    {
        try
        {

       
        //Create 
        HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

        builder.Configuration.AddJsonFile("appsettings.json", optional: true);
        var connectionStringOptions = builder.Configuration.GetSection("ConnectionStrings").Get<ConnectionStringOptions>();
        var microsoftKeysOptions = builder.Configuration.GetSection("MicrosoftKeys").Get<MicrosoftKeysOptions>();
        if (connectionStringOptions is null)
        {
            throw new Exception("Error in reading app settings connectionStringOptions");
        }
        if(microsoftKeysOptions is null)
        {
            throw new Exception("Error in reading app settings microsoftKeysOptions");
        }
        builder.Services.AddSingleton<INileRepo>(x => new NileRepo(connectionStringOptions));
        builder.Services.AddSingleton<IMicrosoftCalender>(x => new MicrosoftCalender(microsoftKeysOptions));
        using IHost host = builder.Build();

        var microsoftCalenderService = host.Services.GetRequiredService<IMicrosoftCalender>();
       
        if (args.Length < 0)
        {

            return -1;
        }
        
        jobname = args[0].ToUpper();
        
        switch (jobname)
        {
            case "GETOUTLOOKCALENDER":
                string email = "AdeleV@0zgwy.onmicrosoft.com";
                await microsoftCalenderService.GetUserProfileDetails(email);
                break;
        }
        Console.WriteLine("Hey, World!");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            Console.ReadLine();
            return -1;
            
           
        }
        return 1;
    }
}