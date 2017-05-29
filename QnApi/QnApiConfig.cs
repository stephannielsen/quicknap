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

    public static Dictionary<MethodGroup, IEnumerable<Method>> ApiMethods => new Dictionary<MethodGroup, IEnumerable<Method>>
    {
      { MethodGroup.Misc, new List<Method> { Method.Dir, Method.Env, Method.Login, Method.Logout, Method.Socks_5 } },
      { MethodGroup.Task, new List<Method> { Method.Status, Method.Query, Method.Detail, Method.Add_Url, Method.Add_Torrent, Method.Start, Method.Stop, Method.Pause, Method.Remove, Method.Priority, Method.Get_File, Method.Set_File } },
      { MethodGroup.Rss, new List<Method> { Method.Add, Method.Query, Method.Update, Method.Remove, Method.Query_Feed, Method.Update_Feed, Method.Add_Job, Method.Query_Job, Method.Update_Job, Method.Remove_Job } },
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
      Socks_5,
      Status,
      Query,
      Detail,
      Add_Url,
      Add_Torrent,
      Start,
      Stop,
      Pause,
      Remove,
      Priority,
      Get_File,
      Set_File,
      Add,
      Update,
      Query_Feed,
      Update_Feed,
      Add_Job,
      Query_Job,
      Update_Job,
      Remove_Job,
      Get,
      Set,
      Enable,
      Verify,
      Install,
      Uninstall,
      Search
    }
  }
}
