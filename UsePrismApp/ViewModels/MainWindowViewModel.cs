using Prism.Commands;
using Prism.Mvvm;
using System;

namespace UsePrismApp.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Hello! Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string _systemDateLabel = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        public string SystemDateLabel
        {
            get { return _systemDateLabel; }
            set { SetProperty(ref _systemDateLabel, value);  }
        }

        // このDelegateCommandがXaml側のボタンとデータバインドされる
        public DelegateCommand SystemDateUpdateButton { get; }
        
        public MainWindowViewModel()
        {
            // DelegateCommandの引数にボタンがクリックされて時の処理を渡してインスタンスを生成する
            SystemDateUpdateButton = new DelegateCommand(SystemDateUpdateButtonExecute);

        }

        // ボタンがクリックされたときの処理を実装する
        private void SystemDateUpdateButtonExecute()
        {
            // データバインドされているLabelの値（プロパティ）を変更する
            SystemDateLabel = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        }
    }
}
