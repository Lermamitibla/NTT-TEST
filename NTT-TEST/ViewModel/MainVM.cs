using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NTT_TEST.ViewModel
{

    class MainVM : BaseVM
    {
        private string _IPAddress = String.Empty;
        private int _Port;

        public string IPAddress
        {
            get { return _IPAddress; }
            set { if (!_IPAddress.Equals(value)) _IPAddress = value; OnPropertyChanged(); }
        }
        public int Port
        {
            get { return _Port; }
            set { if (!_Port.Equals(value)) _Port = value; OnPropertyChanged(); }
        }


        public ICommand ChangePassButtonCommand { get; private set; }
        private void OnChangePassButtonCommandExecuted(object p)
        {

        }
        private bool CanChangePassCommandExecute(object p) => true;


    }
}
