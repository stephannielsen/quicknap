using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace QnApi.Test
{
  [TestClass]
  public class QnApiTests
  {
    [TestMethod]
    public async Task LoadSessionIdReturnsSuccess()
    {
      var config = Config.Create();

      var qnapi = new QnApi();
      qnapi.Configure(config.IpAddress, config.MacAddress, config.UserName, config.Password);
      var response = await qnapi.Connect();
      Assert.IsNotNull(response);
    }
  }
}