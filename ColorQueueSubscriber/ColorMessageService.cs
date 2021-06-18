using System;
using Color.MessageLib;
using Color.MessageLib.Implementations;
using Color.MessagesLib;

namespace ColorQueueSubscriber
{
    public class ColorMessageService : MessageService
    {
        public static int redColorCnt = 0;
        public static int blueColorCnt = 0;
        public static int greenColorCnt = 0;

        public ColorMessageService(MessageServiceConfiguration configuration) : base(configuration)
        {

        }

        public bool EnqueueColorMessage(string message)
        {            
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentNullException(nameof(message));

            switch (message)
            {
                case MessageTypes.MessageRed:
                    if (HasTenMessage(redColorCnt++))
                    {
                        Enqueue(message);
                        redColorCnt = 0;
                    }
                    break;

                case MessageTypes.MessageBlue:
                    if (HasTenMessage(blueColorCnt++))
                    {
                        Enqueue(message);
                        blueColorCnt = 0;
                    }
                    break;

                case MessageTypes.MessageGreen:
                    if (HasTenMessage(greenColorCnt++))
                    {
                        Enqueue(message);
                        greenColorCnt = 0;
                    }
                    break;

                default:
                    throw new InvalidOperationException($"Invalid message type: {message}");
            }

            return true;
        }

        private static bool HasTenMessage(int cnt)
        {
            return cnt >= 10;
        }
    }
}
