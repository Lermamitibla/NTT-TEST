using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NTT_TEST_server.ViewModel
{
        abstract class BaseVM : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;
            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
}
