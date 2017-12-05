using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static QnApi.QnApiConfig;
using System.Net;
using Newtonsoft.Json;

namespace QnApi
{
  public interface IQnApi
  {
    string BasePath { get; }
    void Configure(string ipAddress, string macAddress, string username, string password);
    Task<NasStatus> Connect();
    Task PutToSleep();
    Task WakeUp();
    void Clear();
  }

  public class QnApi : IQnApi
  {
    private HttpClient _httpClient;
    private string _ipAddress;
    private string _macAddress;
    private string _username;
    private string _password;
    private string _sessionId;
    private string _token;

    public string BasePath => $"{Protocol}://{_ipAddress}/{AppName}/{ApiVersion}";

    public void Configure(string ipAddress, string macAddress, string username, string password)
    {
      Clear();
      _ipAddress = ipAddress ?? string.Empty;
      _macAddress = macAddress ?? string.Empty;
      _username = username ?? string.Empty;
      _password = password ?? string.Empty;
      _sessionId = string.Empty;
      _token = string.Empty;
      _httpClient = new HttpClient
      {
        BaseAddress = new Uri(BasePath)
      };
    }

    public async Task<NasStatus> Connect()
    {

      var payload = new FormUrlEncodedContent(new[]
      {
        new KeyValuePair<string, string>("user", _username),
        new KeyValuePair<string, string>("pass", Convert.ToBase64String(Encoding.UTF8.GetBytes(_password)))
      });
      var response = await _httpClient.PostAsync(GetUrlForPath(MethodGroup.Misc, Method.Login), payload);
      response.EnsureSuccessStatusCode();
      var json = await response.Content.ReadAsStringAsync();
      LoginResponse loginResponse = JsonConvert.DeserializeObject<LoginResponse>(json);
      _sessionId = loginResponse.Sid;
      _token = loginResponse.Token;
      Console.WriteLine(json);
      return string.IsNullOrEmpty(loginResponse.Token) ? NasStatus.Off : NasStatus.On;
    }

    public Task PutToSleep()
    {
      //throw new NotImplementedException();
      return Task.CompletedTask;
    }

    public async Task WakeUp()
    {
      if (!string.IsNullOrEmpty(_macAddress))
      {
        using (MagicPacketSender magicPacketSender = new MagicPacketSender())
        {
          var returnValue = await magicPacketSender.WakeUp(_macAddress);
          Console.WriteLine(returnValue);
        }
      }
    }

    private async Task<string> GetSessionId()
    {
      if (string.IsNullOrEmpty(_sessionId))
        await Connect();
      return _sessionId;
    }

    private string GetUrlForPath(MethodGroup group, Method method)
    {
      return $"{BasePath}/{group.ToString()}/{method.ToString()}";
    }

    public void Clear()
    {
      if (_httpClient != null) _httpClient.Dispose();
      _httpClient = null;
      _username = string.Empty;
      _password = string.Empty;
      _ipAddress = string.Empty;
      _macAddress = string.Empty;
      _sessionId = string.Empty;
    }
  }
}
