using System.Collections.Concurrent;

namespace nobel.console.test.services;

public class SampleAPIService
{
   private IConfiguration configuration; 
   private List<SampleAPIResponse> ResponseAPI { get; set; }
   public List<string> Topics { get; private set; }

   public SampleAPIService(IConfiguration configuration)
   {
      this.configuration = configuration; 
      this.Topics = new List<string>();
      this.ResponseAPI = new List<SampleAPIResponse>();
   }

   public async Task GetDistinctTopicsAsync()
   {
      using (var client = new HttpClient())
      {
         var response = await client.GetAsync(configuration.GetSection("API")["base"]);

         if (response == null || !response.IsSuccessStatusCode)
         {
            throw new Exception($"Error! Integration with sample api. \ncode: {response?.StatusCode}\nmessage:{response?.ReasonPhrase ?? "internal error"}");
         }
         else
         {
            ResponseAPI = JsonConvert.DeserializeObject<List<SampleAPIResponse>>(
               await response.Content.ReadAsStringAsync()
            ) ?? new List<SampleAPIResponse>();
         }
      }
   }

   public void FillTopics()
   {
      var options =  new ParallelOptions { MaxDegreeOfParallelism = int.Parse(configuration.GetSection("Pararell")["MaxThread"] ?? "0") }; 
      var concurrenceBag = new ConcurrentBag<string>(); 

      Parallel.ForEach(this.ResponseAPI,options, (x, cancel) => 
      {
         concurrenceBag.AddUnicRange(x.Topics);
      });

      this.Topics = new List<string>(concurrenceBag);
   }
}

public class SampleAPIResponse
{
   public SampleAPIResponse()
   {
      Topics = new List<string>();
   }

   public int Id { get; set; }
   public List<string> Topics { get; set; }
}