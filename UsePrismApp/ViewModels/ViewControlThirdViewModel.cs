using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using UsePrismApp.Services;

namespace UsePrismApp.ViewModels
{
    public class ViewControlThirdViewModel : BindableBase, IConfirmNavigationRequest
    {
        private IMessageService _messageService;

        public ViewControlThirdViewModel() : this(new MessageService())
        {

        }

        public ViewControlThirdViewModel(IMessageService messageService)
        {
            _messageService = messageService;
        }

        // 画面が閉じる直前に通知されるメソッド
        // 引数のActionを通知してあげないと画面が閉じない
        public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            if (_messageService.Question("閉じますか？") == System.Windows.MessageBoxResult.OK)
            {
                continuationCallback(true);
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
           
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            
        }
    }
}
