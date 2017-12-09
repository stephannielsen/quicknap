using Newtonsoft.Json;
using System.IO;
using System.Reflection;

namespace QnApi.Test
{
  public class Config
  {
    public string UserName { get; set; }
    public string Password { get; set; }
    public string MacAddress { get; set; }
    public string IpAddress { get; set; }

    private Config()
    {
    }

    public static Config Create()
    {
      var file = GetConfigFilePath("config.json");
      using (StreamReader r = new StreamReader(file))
      {
        string json = r.ReadToEnd();
        return JsonConvert.DeserializeObject<Config>(json);

      }
    }

    private static string GetConfigFilePath(string filename)
    {
      return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), filename);
    }
  }
}