using System.Linq;
using Xamarin.Auth;
using Xamarin.Forms;
using QuickNAP.Droid;
using Android.Content;
using Plugin.CurrentActivity;

[assembly: Dependency(typeof(CredentialsService))]
namespace QuickNAP.Droid
{
  public class CredentialsService : ICredentialsService
  {
    public string UserName
    {
      get
      {
        var account = AccountStore.Create(CrossCurrentActivity.Current.Activity).FindAccountsForService(App.AppName).FirstOrDefault();
        return account?.Username;
      }
    }

    public string Password
    {
      get
      {
        var account = AccountStore.Create(CrossCurrentActivity.Current.Activity).FindAccountsForService(App.AppName).FirstOrDefault();
        return account?.Properties[nameof(Password)];
      }
    }

    public string IpAddress
    {
      get
      {
        var account = AccountStore.Create(CrossCurrentActivity.Current.Activity).FindAccountsForService(App.AppName).FirstOrDefault();
        return account?.Properties[nameof(IpAddress)];
      }
    }

    public string MacAddress
    {
      get
      {
        var account = AccountStore.Create(CrossCurrentActivity.Current.Activity).FindAccountsForService(App.AppName).FirstOrDefault();
        return account?.Properties[nameof(MacAddress)];
      }
    }

    public void SaveCredentials(string userName, string password, string ipAddress, string macAddress)
    {
      Account account = new Account
      {
        Username = userName
      };
      account.Properties.Add(nameof(Password), password);
      account.Properties.Add(nameof(IpAddress), ipAddress);
      account.Properties.Add(nameof(MacAddress), macAddress);
      AccountStore.Create(CrossCurrentActivity.Current.Activity).Save(account, App.AppName);
    }

    public void DeleteCredentials()
    {
      var account = AccountStore.Create(CrossCurrentActivity.Current.Activity).FindAccountsForService(App.AppName).FirstOrDefault();
      if (account != null)
      {
        AccountStore.Create(CrossCurrentActivity.Current.Activity).Delete(account, App.AppName);
      }
    }


    public bool DoCredentialsExist()
    {
      return AccountStore.Create(CrossCurrentActivity.Current.Activity).FindAccountsForService(App.AppName).Any() ? true : false;
    }
  }
}