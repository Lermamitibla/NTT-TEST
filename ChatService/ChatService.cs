
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFChat
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "ChatService" в коде и файле конфигурации.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ChatService : IChatService
    {
        List<ServerUser> users = new List<ServerUser>();
        int nextID = 1;


        public int Connect(string name)
        {
            var user = new ServerUser()
            {
                ID = nextID,
                Name = name,
                opContext = OperationContext.Current
            };
            nextID++;
            SendMessage(user.Name + " has entered the chat", 0);
            users.Add(user);
            return user.ID;
        }

        public void Disconnect(int id)
        {
            var user = users.FirstOrDefault(i => i.ID == id);
            if(user != null)
            {
                users.Remove(user);
                SendMessage(user.Name + " has left the chat", 0);
            }
            
        }

        public void SendMessage(string message, int id)
        {
            foreach (var user in users)
            {
                var answer = new StringBuilder( DateTime.Now.ToShortTimeString());
                if (user != null)
                {
                    answer.Append(": ").Append(user.Name).Append(" ");
                }
                answer.Append(message);

                user.opContext.GetCallbackChannel<IServerChatCallback>()
                    .CallBackMessage(answer.ToString());
            }
        }
    }
}
