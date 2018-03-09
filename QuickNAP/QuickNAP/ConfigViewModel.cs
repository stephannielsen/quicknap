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
  public class ConfigViewModel : INotifyPropertyChanged
  {
    private readonly ICredentialsService _credentialsService;

    private string _ipAddress;
    private string _macAddress;
    private string _username;
    private string _password;
    private bool _configSaved;

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

    public bool ConfigSaved
    {
      get { return _configSaved; }
      set
      {
        if (_configSaved != value)
        {
          _configSaved = value;
          OnPropertyChanged(nameof(ConfigSaved));
        }
      }
    }
    
    public ICommand SaveConfigCommand { get; set; }
    public ICommand ClearConfigCommand { get; set; }

    public ConfigViewModel()
    {
      SaveConfigCommand = new Command(() => SaveConfig());
      ClearConfigCommand = new Command(() => ClearConfig());

      _credentialsService = DependencyService.Get<ICredentialsService>();
      _configSaved = _credentialsService.DoCredentialsExist();

      Username = _credentialsService.UserName;
      Password = _credentialsService.Password;
      IpAddress = _credentialsService.IpAddress;
      MacAddress = _credentialsService.MacAddress;
    }

    protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    private void SaveConfig()
    {

      if (!string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password) && !string.IsNullOrEmpty(IpAddress) && !string.IsNullOrEmpty(MacAddress))
      {
        _credentialsService.SaveCredentials(Username, Password, IpAddress, MacAddress);
        ConfigSaved = _credentialsService.DoCredentialsExist();
      }
    }

    private void ClearConfig()
    {
      _credentialsService.DeleteCredentials();
      ConfigSaved = _credentialsService.DoCredentialsExist();
      IpAddress = string.Empty;
      MacAddress = string.Empty;
      Password = string.Empty;
      Username = string.Empty;
    }


  }
}
