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
  public class MainViewModel : INotifyPropertyChanged
  {
    private const string _letNASNap = "Let NIPINAS nap!";
    private const string _wakeNASUp = "Wake NIPINAS up!";

    private readonly ICredentialsService _credentialsService;
    private readonly IQnApi _qnapi;

    private string _ipAddress;
    private string _macAddress;
    private string _username;
    private string _password;
    private bool _credentialsSaved;
    private NasStatus _nasStatus = NasStatus.Off;

    public event PropertyChangedEventHandler PropertyChanged;

    public string IpAddress
    {
      get { return _ipAddress; }
      set
      {
        if (_ipAddress != value)
        {
          _ipAddress = value;
          OnPropertyChanged(nameof(IpAddress));
        }
      }
    }

    public string MacAddress
    {
      get { return _macAddress; }
      set
      {
        if (_macAddress != value)
        {
          _macAddress = value;
          OnPropertyChanged(nameof(MacAddress));
        }
      }
    }

    public string Username
    {
      get { return _username; }
      set
      {
        if (_username != value)
        {
          _username = value;
          OnPropertyChanged(nameof(Username));
        }
      }
    }

    public string Password
    {
      get { return _password; }
      set
      {
        if (_password != value)
        {
          _password = value;
          OnPropertyChanged(nameof(Password));
        }
      }
    }

    public bool CredentialsSaved
    {
      get { return _credentialsSaved; }
      set
      {
        if (_credentialsSaved != value)
        {
          _credentialsSaved = value;
          OnPropertyChanged(nameof(CredentialsSaved));
        }
      }
    }

    public string WakeUpButtonText => _wakeNASUp;
    public string SleepButtonText => _letNASNap;
    public bool IsConnected => _nasStatus == NasStatus.On;
    public bool IsNotConnected => _nasStatus == NasStatus.Off;

    public ICommand ConfigureCommand { get; set; }
    public ICommand SaveCredentialsCommand { get; set; }
    public ICommand DeleteCredentialsCommand { get; set; }
    public ICommand WakeUpCommand { get; set; }
    public ICommand SleepCommand { get; set; }
    public ICommand QueryCommand { get; set; }

    public MainViewModel()
    {
      ConfigureCommand = new Command(async () => await ConfigureAndConnectQnApi());
      SaveCredentialsCommand = new Command(() => SaveCredentials());
      DeleteCredentialsCommand = new Command(() => DeleteCredentials());
      WakeUpCommand = new Command(async () => await WakeNasUp());
      SleepCommand = new Command(async () => await PutNasToSleep());
      QueryCommand = new Command(async () => await QueryNas());

      _qnapi = new QnApi.QnApi();


      _credentialsService = DependencyService.Get<ICredentialsService>();
      _credentialsSaved = _credentialsService.DoCredentialsExist();

      Username = _credentialsService.UserName;
      Password = _credentialsService.Password;
      IpAddress = _credentialsService.IpAddress;
      MacAddress = _credentialsService.MacAddress;
    }

    private async Task ConfigureAndConnectQnApi()
    {
      // custom property changed handler to react on completely entered user data
      if (!string.IsNullOrEmpty(IpAddress) && !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(MacAddress))
      {
        _qnapi.Configure(IpAddress, MacAddress, Username, Password);
        var status = await _qnapi.Connect();
        SetNasStatus(status);
      }
    }

    private void SaveCredentials()
    {

      if (!string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password) && !string.IsNullOrEmpty(IpAddress) && !string.IsNullOrEmpty(MacAddress))
      {
        _credentialsService.SaveCredentials(Username, Password, IpAddress, MacAddress);
        CredentialsSaved = _credentialsService.DoCredentialsExist();
      }
    }

    private void DeleteCredentials()
    {
      _credentialsService.DeleteCredentials();
      CredentialsSaved = _credentialsService.DoCredentialsExist();
      IpAddress = string.Empty;
      MacAddress = string.Empty;
      Password = string.Empty;
      Username = string.Empty;
      _qnapi.Clear();
      SetNasStatus(NasStatus.Off);
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

    protected virtual void OnPropertyChanged(string propertyName)
    {
      if (PropertyChanged != null)
      {
        PropertyChanged(this,
            new PropertyChangedEventArgs(propertyName));
      }
    }


  }
}
