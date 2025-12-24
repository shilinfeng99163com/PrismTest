using Prism.Commands;
using Prism.Mvvm;
using Prism.Events;
using System.Windows.Input;

namespace PrismTest.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private readonly IMessageService _messageService;
        private readonly IEventAggregator _eventAggregator;

        private string _title = "Prism Demo Application";
        private string _message = "Hello, Prism!";
        private int _counter = 0;

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        public int Counter
        {
            get => _counter;
            set => SetProperty(ref _counter, value);
        }

        // 命令
        public ICommand ClickCommand { get; }
        public ICommand SendMessageCommand { get; }

        public MainViewModel(IMessageService messageService, IEventAggregator eventAggregator)
        {
            _messageService = messageService;
            _eventAggregator = eventAggregator;

            // 初始化命令
            ClickCommand = new DelegateCommand(OnClick);
            SendMessageCommand = new DelegateCommand(OnSendMessage);

            // 订阅事件
            _eventAggregator.GetEvent<MessageSentEvent>().Subscribe(OnMessageReceived);
        }

        private void OnClick()
        {
            Counter++;
            Message = $"按钮被点击了 {Counter} 次";
        }

        private void OnSendMessage()
        {
            var result = _messageService.ShowMessage("这是来自服务的消息");
            _eventAggregator.GetEvent<MessageSentEvent>().Publish($"消息已发送: {result}");
        }

        private void OnMessageReceived(string message)
        {
            Message = message;
        }
    }
}
