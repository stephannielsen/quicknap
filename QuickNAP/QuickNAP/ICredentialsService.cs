﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickNAP
{
  public interface ICredentialsService
  {
    string UserName { get; }

    string Password { get; }

    string IpAddress { get; }
    
    string MacAddress { get; }

    void SaveCredentials(string userName, string password, string ipAddress, string macAddress);

    void DeleteCredentials();

    bool DoCredentialsExist();
  }
}
