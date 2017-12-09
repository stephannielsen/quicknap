using System;
using System.Collections.Generic;
using System.Text;

namespace QnApi
{
  internal class LoginResponse
  {
    public bool Admin { get; set; }
    public bool Error { get; set; }
    public UserPrivileges Privilege { get; set; }
    public string Sid { get; set; }
    public string Token { get; set; }
    public string User { get; set; }
  }
}
