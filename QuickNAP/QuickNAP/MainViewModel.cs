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

    private readonly QnApi.QnApi _qnapi;

    private string _wakeUpButtonText;

    public event PropertyChangedEventHandler PropertyChanged;

    public ICommand WakeUpAction { get; set; }
    public ICommand QueryApiAction { get; set; }

    public string WakeUpButtonText
    {
      get { return _wakeUpButtonText; }
      set
      {
        if (_wakeUpButtonText != value)
        {
          _wakeUpButtonText = value;
        }
        OnPropertyChanged(nameof(WakeUpButtonText));
      }
    }

    public MainViewModel()
    {
      _qnapi = new QnApi.QnApi("", "", "");

      WakeUpAction = new Command(() =>
      {
        WakeUpButtonText = WakeUpButtonText.Equals(_letNASNap, StringComparison.CurrentCultureIgnoreCase) ?
          _wakeNASUp : _letNASNap;
      });
      WakeUpButtonText = _letNASNap;

      QueryApiAction = new Command(async () => await QueryApi());
    }

    private async Task QueryApi()
    {
      var response = await _qnapi.LoadSessionId();
      Console.WriteLine(response);
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
