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
