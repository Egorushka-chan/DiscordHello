using System.IO;
using System.IO.Pipes;
using System.Text.Json;

using Microsoft.AspNetCore.SignalR.Protocol;

namespace DiscordHello.Models
{
    public interface IDiscordSender
    {
        public void Send(string serverID, string content);
        public void Receive();
    }

    public class FileBetterDiscordSender : IDiscordSender
    {
        List<string> messages = new List<string>();
        Task? fileAppendingTask;
        readonly int taskTimeSeconds = 10;

        public void Receive()
        {
            throw new NotImplementedException();
        }

        public void Send(string serverID, string content)
        {
            if(string.IsNullOrEmpty(Settings.BufferPath))
            {
                throw new FileNotFoundException("Путь к буферу пустой");
            }

            if (string.IsNullOrEmpty(content))
            {
                throw new ArgumentException("content пустой");
            }

            if(!File.Exists(Settings.InputJson))
            {
                File.Create(Settings.InputJson);
            }
            
            Message message = new Message();
            message.Server = serverID; message.Content = content;

            string jsonMessage = JsonSerializer.Serialize(message);

            lock(messages)
            {
                messages.Add(jsonMessage);
            }

            if (fileAppendingTask == null)
            {
                StartSenderTask();
            }
        }

        private void StartSenderTask()
        {
            fileAppendingTask = Task.Run(()=>
            {
                DateTime start = DateTime.Now;
                bool notOver = true;
                while (notOver)
                {
                    if (!NotEvenSecond())
                    {
                        lock (messages)
                        {
                            foreach(string message in messages)
                            {
                                File.AppendAllText(Settings.InputJson, message);
                            }
                        }
                    }

                    // notOver = !NotEvenSecond() && (DateTime.Now - start).TotalSeconds > taskTimeSeconds;
                }
            });
        }
        private bool NotEvenSecond()
        {
            int seconds = DateTime.Now.Second;
            return !seconds.IsEven();
        }
    }
}
