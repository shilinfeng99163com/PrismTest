using System.Windows;

namespace PrismTest
{
    public class MessageService : IMessageService
    {
        public string ShowMessage(string message)
        {
            MessageBox.Show(message, "Prism Demo", MessageBoxButton.OK, MessageBoxImage.Information);
            return $"已显示消息: {message}";
        }
    }
}