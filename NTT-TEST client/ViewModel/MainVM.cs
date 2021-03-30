using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NTT_TEST_server.Commands;
using System.ServiceModel;
using System.Configuration;
using System.Windows;

namespace NTT_TEST_server.ViewModel
{
    class MainVM : BaseVM
    {
        private int _Port;
        private string _Status = "Stopped";
        private bool isRunning;
        private ServiceHost host;

        public string Status
        {
            get { return _Status; }
            set { if (!_Status.Equals(value)) _Status = value; OnPropertyChanged(); }
        }
        public int Port
        {
            get { return _Port; }
            set { if (_Port != value) _Port = value; OnPropertyChanged(); }
        }

        public MainVM()
        {
            CreateCommands();
        }


        public ICommand RunServerButtonCommand { get; private set; }
        private void OnRunServerButtonCommandExecuted(object p)
        {
            string hostURL = ConfigurationManager.AppSettings.Get("HostURL");
            Uri connAddress = new Uri(hostURL + _Port + "/");
            try
            {
                using (host = new ServiceHost(typeof(WCFChat.ChatService), connAddress))
                {
                    host.Open();
                    isRunning = true;
                    Status = "Running";
                }

            } catch (CommunicationObjectFaultedException ex)
            {
                MessageBox.Show(ex.Message);
            } catch (AddressAlreadyInUseException ex1)
            {
                MessageBox.Show(ex1.Message);
            }
        }
        private bool CanRunServerButtonCommandExecute(object p)
        {
            if (isRunning) return false;
            if (_Port <= 0 || Port > 65535) return false;
            return true;
        }


        public ICommand StopServerButtonCommand { get; private set; }
        private void OnStopServerButtonCommandExecuted(object p)
        {
            if(host != null)
            {
                host.Close();
                isRunning = false;
                Status = "Stopped";
            }
            
        }
        private bool CanStopServerButtonCommandExecute(object p)
        {
            if (isRunning) return true;
            else return false;
        }


        private void CreateCommands()
        {
            RunServerButtonCommand = new LambdaCommand(OnRunServerButtonCommandExecuted, CanRunServerButtonCommandExecute);
            StopServerButtonCommand = new LambdaCommand(OnStopServerButtonCommandExecuted, CanStopServerButtonCommandExecute);
        }

    }


}

   
