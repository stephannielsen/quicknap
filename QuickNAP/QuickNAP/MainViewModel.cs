using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuickNAP
{
  public class MainViewModel : INotifyPropertyChanged
  {
    private const string _letNASNap = "Let NIPINAS nap!";
    private const string _wakeNASUp = "Wake NIPINAS up!";

    private string _buttonText;

    public event PropertyChangedEventHandler PropertyChanged;

    public ICommand PerformAction { get; set; }

    public string ButtonText
    {
      get { return _buttonText; }
      set
      {
        if (_buttonText != value)
        {
          _buttonText = value;
        }
        OnPropertyChanged(nameof(ButtonText));
      }
    }

    public MainViewModel()
    {
      PerformAction = new Command(() =>
      {
        ButtonText = ButtonText.Equals(_letNASNap, StringComparison.CurrentCultureIgnoreCase) ?
          _wakeNASUp : _letNASNap;
      });
      ButtonText = _letNASNap;
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
