using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Services.Dialogs;
using UsePrismApp.Views; // nameof(ViewControlSecond)とキーに画面を指定するときに必要
using UsePrismApp.Services;

namespace UsePrismApp.ViewModels
{
    // 画面遷移時にパラメータを渡すために INavigationAware と using Prism.Regions を追加
    public class ViewControlFirstViewModel : BindableBase, INavigationAware
    {

        private IDialogService _dialogService;

        private IMessageService _messageService;

        private string _labelA = string.Empty;
        public string LabelA
        {
            get { return _labelA; }
            set { SetProperty(ref _labelA, value); }
        }

        // dialogService だけ受けて、thisで2つ目のコンストラクタの ViewControlFirstViewModel に dialogService と
        // インスタンス化した MessageService を渡している
        public ViewControlFirstViewModel(IDialogService dialogService) : this(dialogService, new MessageService())
        {
            // thisを指定することで複数のコンストラクタを呼び出すことができる
        }

        // 自分で作ったインターフェースをコンストラクタに渡した場合に、Prism側で理解できないのでエラーになる
        // IMessageService を実装した MessageService をインスタンス化したものを、1つ目のコンストラクタで指定して
        // こっちの（2つ目の）コンストラクタで受け取るように実装している
        public ViewControlFirstViewModel(
            IDialogService dialogService,
            IMessageService messageService)
        {
            ViewFirstControlOKButton = new DelegateCommand(ViewFirstControlOKButtonExecute);
            ViewFirstControlOKButton2 = new DelegateCommand(ViewFirstControlOKButtonExecute2);

            _dialogService = dialogService;

            _messageService = messageService;
        }

        public DelegateCommand ViewFirstControlOKButton { get; }
        public DelegateCommand ViewFirstControlOKButton2 { get; }

        // 画面遷移で通過するときの処理
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            // メイン画面から投げたパラメータで、String型でキーがLabelAのパラメータを受け取る
            LabelA = navigationContext.Parameters.GetValue<string>(nameof(LabelA));
        }

        // ViewControlFirstViewModelのインスタンスを画面非表示・表示時に使いまわすかどうか設定する
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            // 毎回新しくする場合はfalseにする
            // 使いまわしている場合は、前回のパラメーターの値が残っている
            return false;
        }

        // 画面が終了した際に処理を書きたい場合はここに記述する
        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }

        private void ViewFirstControlOKButtonExecute()
        {
            // ViewModelでメッセージボックスを表示してしまうとテストを実行してメッセージボックスが表示されたところで止まってしまう
            // OKボタンを押さない限りテストが自動で実行されないので、ViewModelにメッセージボックスを記述してはダメ
            // MessageBox.Show("Saveします");

            var p = new DialogParameters();
            p.Add(nameof(ViewControlSecondViewModel.ViewControlSecondTextBox), "Saveします");
            _dialogService.ShowDialog(nameof(ViewControlSecond), p, null);
        }

        // 標準のメッセージボックスを表示させてテストする方法
        private void ViewFirstControlOKButtonExecute2()
        {
            // インターフェース越しに純正のメッセージボックスを出すパターン
            if (_messageService.Question("Saveしますか？") == MessageBoxResult.OK)
            {
                _messageService.ShowDialog("Saveしました");
            }
        }
    }
}
