using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UsePrismApp.ViewModels
{
    public class ViewControlSecondViewModel : BindableBase, IDialogAware
    {
        public ViewControlSecondViewModel()
        {

        }

        public string Title => "タイトル";

        public event Action<IDialogResult> RequestClose;

        // falseにすると閉じれない画面になる
        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
           
        }
    }
}
