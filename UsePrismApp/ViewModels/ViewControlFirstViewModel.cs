using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Services.Dialogs;
using UsePrismApp.Views; // nameof(ViewControlSecond)とキーに画面を指定するときに必要

namespace UsePrismApp.ViewModels
{
    // 画面遷移時にパラメータを渡すために INavigationAware と using Prism.Regions を追加
    public class ViewControlFirstViewModel : BindableBase, INavigationAware
    {

        private IDialogService _dialogService;

        private string _labelA = string.Empty;
        public string LabelA
        {
            get { return _labelA; }
            set { SetProperty(ref _labelA, value); }
        }

        public ViewControlFirstViewModel(IDialogService dialogService)
        {
            ViewFirstControlOKButton = new DelegateCommand(ViewFirstControlOKButtonExecute);

            _dialogService = dialogService;
        }

        public DelegateCommand ViewFirstControlOKButton { get; }

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
    }
}
