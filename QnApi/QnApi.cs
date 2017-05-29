using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static QnApi.QnApiConfig;

namespace QnApi
{
  public class QnApi
  {
    private readonly string _host;
    private readonly string _username;
    private readonly string _password;

    private string _sessionId = string.Empty;

    public string BasePath => $"{Protocol}://{_host}/{AppName}/{ApiVersion}";

    public QnApi(string host, string username, string password)
    {
      _host = host;
      _username = username;
      _password = password;
    }

    private async Task<string> GetSessionId()
    {
      if (string.IsNullOrEmpty(_sessionId))
        await LoadSessionId();
      return _sessionId;
    }

    private async Task LoadSessionId()
    {
      var payload = new FormUrlEncodedContent(new[]
      {
        new KeyValuePair<string, string>("user", _username),
        new KeyValuePair<string, string>("pass", Convert.ToBase64String(Encoding.UTF8.GetBytes(_password)))
      });
      var client = new HttpClient();
      client.BaseAddress = new Uri(BasePath);
      var response = await client.PostAsync(GetUrlForPath(MethodGroup.Misc, Method.Login), payload);
      response.EnsureSuccessStatusCode();
      var json = await response.Content.ReadAsStringAsync()

    }

    private string GetUrlForPath(MethodGroup group, Method method)
    {
      return $"{BasePath}/{group.ToString()}/{method.ToString()}";
    }
  }
}
