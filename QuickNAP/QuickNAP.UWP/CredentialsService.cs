﻿using QuickNAP.UWP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Auth;
using Xamarin.Forms;

[assembly: Dependency(typeof(CredentialsService))]
namespace QuickNAP.UWP
{
  public class CredentialsService : ICredentialsService
  {

    public string UserName
    {
      get
      {
        var account = AccountStore.Create().FindAccountsForService(QuickNAP.App.AppName).FirstOrDefault();
        return account?.Username;
      }
    }

    public string Password
    {
      get
      {
        var account = AccountStore.Create().FindAccountsForService(QuickNAP.App.AppName).FirstOrDefault();
        return account?.Properties[nameof(Password)];
      }
    }

    public string IpAddress
    {
      get
      {
        var account = AccountStore.Create().FindAccountsForService(QuickNAP.App.AppName).FirstOrDefault();
        return account?.Properties[nameof(IpAddress)];
      }
    }

    public string MacAddress
    {
      get
      {
        var account = AccountStore.Create().FindAccountsForService(QuickNAP.App.AppName).FirstOrDefault();
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
      AccountStore.Create().Save(account, QuickNAP.App.AppName);
    }

    public void DeleteCredentials()
    {
      var account = AccountStore.Create().FindAccountsForService(QuickNAP.App.AppName).FirstOrDefault();
      if (account != null)
      {
        AccountStore.Create().Delete(account, QuickNAP.App.AppName);
      }
    }


    public bool DoCredentialsExist()
    {
      return AccountStore.Create().FindAccountsForService(QuickNAP.App.AppName).Any() ? true : false;
    }
  }
}