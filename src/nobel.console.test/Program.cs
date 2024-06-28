Console.WriteLine("Hello, Nobel!");

var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);


var services = new SampleAPIService(builder.Build());

try {
   
   await services.GetDistinctTopicsAsync();
   services.FillTopics();
   
   foreach (var topic in services.Topics)
   {
      Console.WriteLine(topic);
   }

   Console.WriteLine($"End process, find: {services?.Topics.Count} Topics");

} catch (Exception e)
{
   Console.WriteLine(e.Message);
}