using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QnApi
{
  public class MockedQnApi : IQnApi
  {
    private NasStatus _nasStatus = NasStatus.Off;

    public string BasePath => "MockApi";

    public void Configure(string ipAddress, string macAddress, string username, string password)
    {
      //mocked api does not need these values, so nothing to do here
    }

    public Task<NasStatus> Connect()
    {
      _nasStatus = _nasStatus == NasStatus.Off ? NasStatus.On : NasStatus.Off;
      return Task.FromResult(_nasStatus);
    }

    public Task PutToSleep()
    {
      _nasStatus = NasStatus.Off;
      return Task.CompletedTask;
    }

    public Task WakeUp()
    {
      _nasStatus = NasStatus.On;
      return Task.CompletedTask;
    }

    public void Clear()
    {
      _nasStatus = NasStatus.Off;  
    }
  }
}
