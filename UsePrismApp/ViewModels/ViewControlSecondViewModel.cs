using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using UsePrismApp.Views;

namespace UsePrismApp.ViewModels
{
    public class ViewControlSecondViewModel : BindableBase, IDialogAware
    {
      
        public ViewControlSecondViewModel()
        {
            OKButton = new DelegateCommand(OKButtonExecute);
     
        }

        public string Title => "タイトル";

        private string _viewSecondTextBox = "XXXXXX";
        public string ViewControlSecondTextBox
        {
            get { return _viewSecondTextBox; }
            set { SetProperty(ref _viewSecondTextBox, value); }
        }

        public event Action<IDialogResult> RequestClose;

        // OKButton
        public DelegateCommand OKButton { get; }

        // falseにすると閉じれない画面になる
        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            
        }

        // ポップアップ画面にパラメータを渡す
        public void OnDialogOpened(IDialogParameters parameters)
        {
            ViewControlSecondTextBox = parameters.GetValue<string>(nameof(ViewControlSecondTextBox));
        }

        private void OKButtonExecute()
        {
            var p = new DialogParameters();

            // ViewControlSecondTextBoxをそのまま返したので、Keyもそれを指定し、値もTextBoxに表示されているものをそのまま返す
            p.Add(nameof(ViewControlSecondTextBox), ViewControlSecondTextBox);

            // nullかもしれないので？マークをつける
            // RequestClose?.Invokeで画面を閉じる処理が行われ、DialogResultでOKが押されたという通知とパラメータが渡る
            RequestClose?.Invoke(new DialogResult(ButtonResult.OK, p));
        }
    }
}
