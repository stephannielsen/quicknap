using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using QnApi;

namespace QuickNAP
{
  public class ControlViewModel : INotifyPropertyChanged
  {
    private const string _letNASNap = "Let NIPINAS nap!";
    private const string _wakeNASUp = "Wake NIPINAS up!";

    private readonly IQnApi _qnapi;
    private readonly ICredentialsService _credentialsService;

    private NasStatus _nasStatus = NasStatus.Off;

    public event PropertyChangedEventHandler PropertyChanged;

    public string WakeUpButtonText => _wakeNASUp;
    public string SleepButtonText => _letNASNap;
    public bool IsConnected => _nasStatus == NasStatus.On;
    public bool IsNotConnected => _nasStatus == NasStatus.Off;
    public bool ConfigSaved => _credentialsService.DoCredentialsExist();


    public ICommand ConnectCommand { get; set; }
    public ICommand WakeUpCommand { get; set; }
    public ICommand SleepCommand { get; set; }
    public ICommand QueryCommand { get; set; }

    public ControlViewModel()
    {
      _qnapi = new QnApi.QnApi();
      _credentialsService = DependencyService.Get<ICredentialsService>();

      ConnectCommand = new Command(async () => await ConnectQnApi());
      WakeUpCommand = new Command(async () => await WakeNasUp());
      SleepCommand = new Command(async () => await PutNasToSleep());
      QueryCommand = new Command(async () => await QueryNas());
    }

    protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    private async Task ConnectQnApi()
    {
      // custom property changed handler to react on completely entered user data
      if (ConfigSaved)
      {
        _qnapi.Configure(_credentialsService.IpAddress, _credentialsService.MacAddress, _credentialsService.UserName, _credentialsService.Password);
        var status = await _qnapi.Connect();
        SetNasStatus(status);
      }
    }
    
    private async Task WakeNasUp()
    {
      await _qnapi.WakeUp();
      SetNasStatus(NasStatus.On);
    }

    private async Task PutNasToSleep()
    {
      await _qnapi.PutToSleep();
      SetNasStatus(NasStatus.Off);
    }

    private async Task QueryNas()
    {
      var response = await _qnapi.Connect();
      SetNasStatus(response);
    }

    private void SetNasStatus(NasStatus status)
    {
      if (_nasStatus != status)
      {
        _nasStatus = status;
        OnPropertyChanged(nameof(IsConnected));
        OnPropertyChanged(nameof(IsNotConnected));
      }
    }
  }
}
