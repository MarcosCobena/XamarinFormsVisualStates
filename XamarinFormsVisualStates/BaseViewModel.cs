using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace XamarinFormsVisualStates
{
    public abstract class BaseViewModel : IStateNotifier
    {
        string _state;

        public event PropertyChangedEventHandler PropertyChanged;

        public string State
        {
            get => _state;
            set => SetAndRaisePropertyChanged(ref _state, value);
        }

        protected void SetAndRaisePropertyChanged<TRef>(ref TRef field, TRef value, 
                                                        [CallerMemberName] string propertyName = null)
        {
            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
