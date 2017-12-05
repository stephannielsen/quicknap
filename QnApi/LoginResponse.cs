using System;
using System.Collections.Generic;
using System.Text;

namespace QnApi
{
  internal class LoginResponse
  {

    //{ "admin":1,"error":0,"privilege":{ "file":1,"music":1,"photo":1,"video":1},"sid":"qr71lbx8","token":"2333290de908e5425de7cbea509f6594","user":"admin"}
    public bool Admin { get; set; }
    public bool Error { get; set; }
    public UserPrivileges Privilege { get; set; }
    public string Sid { get; set; }
    public string Token { get; set; }
    public string User { get; set; }
  }
}
