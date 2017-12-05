using System;
using System.Collections.Generic;
using System.Text;

namespace QnApi
{
  public class QnApiConfig
  {
    public const string Protocol = "https";
    public const string AppName = "downloadstation";
    public const string ApiVersion = "V4";

    //curl "https://192.168.189.26/cgi-bin/sys/sysRequest.cgi?sid=lsmyj0pd" -H "Host: 192.168.189.26" -H "User-Agent: Mozilla/5.0 (Windows NT 10.0; WOW64; rv:54.0) Gecko/20100101 Firefox/54.0" -H "Accept: */*" -H "Accept-Language: en-US,en;q=0.5" --compressed -H "X-Requested-With: XMLHttpRequest" -H "Content-Type: application/x-www-form-urlencoded; charset=UTF-8" -H "Referer: https://192.168.189.26/cgi-bin/" -H "Cookie: DESKTOP=1; nas_wfm_tree_x=200; treeRootPathadmin=/Multimedia; WINDOW_MODE=1; remeber=1; qtoken_account=YWRtaW4=; qtoken=2333290de908e5425de7cbea509f6594; csrftoken=C6ryT0hcD2QVgcPQ6m7qBErhIMAXOX3T; sid_last_valid_time=""2|1:0|10:1502219868|19:sid_last_valid_time|16:MTUwMjIxOTg2OA==|a613f6a46f3952bb9c1932492e12a2a37254c468bffd63816fd96c3010a9aebd""; user=""2|1:0|10:1502219868|4:user|8:YWRtaW4=|facbcf6b0f22e6237963c84350885e43c632b22b94c4f952693c7a9a39a627ac""; sid=""2|1:0|10:1502219868|3:sid|12:Zjk1N3pzYzU=|f75ccbd4ad2aa80fe5f32a0fed248bca39eb809cc1aa5343465b141f5f61b1b0""; nas_1_u=YWRtaW4=; QT=1503699922731; NAS_USER=admin; NAS_SID=lsmyj0pd; home=1" -H "DNT: 1" -H "Connection: keep-alive" -H "Pragma: no-cache" -H "Cache-Control: no-cache" --data
    
    
    //"subfunc=power_mgmt&apply=s3"

    public static Dictionary<MethodGroup, IEnumerable<Method>> ApiMethods => new Dictionary<MethodGroup, IEnumerable<Method>>
    {
      { MethodGroup.Misc, new List<Method> { Method.Dir, Method.Env, Method.Login, Method.Logout, Method.Socks5, Method.Power_Mgmt } },
      { MethodGroup.Task, new List<Method> { Method.Status, Method.Query, Method.Detail, Method.AddUrl, Method.AddTorrent, Method.Start, Method.Stop, Method.Pause, Method.Remove, Method.Priority, Method.GetFile, Method.SetFile } },
      { MethodGroup.Rss, new List<Method> { Method.Add, Method.Query, Method.Update, Method.Remove, Method.QueryFeed, Method.UpdateFeed, Method.AddJob, Method.QueryJob, Method.UpdateJob, Method.RemoveJob } },
      { MethodGroup.Config, new List<Method> { Method.Get, Method.Set } },
      { MethodGroup.Account, new List<Method> { Method.Add, Method.Query, Method.Update, Method.Remove } },
      { MethodGroup.Addon, new List<Method> { Method.Query, Method.Enable, Method.Verify, Method.Install, Method.Uninstall, Method.Search } }
    };

    public enum MethodGroup
    {
      Misc,
      Task,
      Rss,
      Config,
      Account,
      Addon
    }

    public enum Method
    {
      Dir,
      Env,
      Login,
      Logout,
      Socks5,
      Status,
      Query,
      Detail,
      AddUrl,
      AddTorrent,
      Start,
      Stop,
      Pause,
      Remove,
      Priority,
      GetFile,
      SetFile,
      Add,
      Update,
      QueryFeed,
      UpdateFeed,
      AddJob,
      QueryJob,
      UpdateJob,
      RemoveJob,
      Get,
      Set,
      Enable,
      Verify,
      Install,
      Uninstall,
      Search,
      Power_Mgmt
    }
  }
}
