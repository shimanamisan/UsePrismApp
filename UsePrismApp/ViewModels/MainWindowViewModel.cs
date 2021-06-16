﻿using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs; // 画面遷移時にポップアップさせる時に使う
using System;
using UsePrismApp.Views;

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

        // MainWindow側のボタン（Command で ShowViewControlFirst と名付けたやつ）とデータバインドさせる
        public DelegateCommand ShowViewControlFirst { get; }

        public DelegateCommand ShowViewControlFirstParam { get; }

        public DelegateCommand ShowViewControlSecond { get; }

        // 画面遷移させる処理
        private readonly IRegionManager _regionManager;

        // 画面遷移時にポップアップさせるための処理
        private readonly IDialogService _daialogService;

        // ナビゲーション方式で画面を切り替えるために必要な機能
        // ViewModelでIRegionManagerを受け取るように記述することで自動的に受け取ることができる
        public MainWindowViewModel(IRegionManager regionManager, IDialogService daialogService)
        {
            // DelegateCommandの引数にボタンがクリックされて時の処理を渡してインスタンスを生成する
            SystemDateUpdateButton = new DelegateCommand(SystemDateUpdateButtonExecute);

            _regionManager = regionManager;

            ShowViewControlFirst = new DelegateCommand(ShowViewControlFirstExecute);

            ShowViewControlFirstParam = new DelegateCommand(ShowViewControlFirstParamExecute);

            ShowViewControlSecond = new DelegateCommand(ShowViewControlSecondExecute);

            _daialogService = daialogService;

        }

        // ボタンがクリックされたときの処理を実装する
        private void SystemDateUpdateButtonExecute()
        {
            // データバインドされているLabelの値（プロパティ）を変更する
            SystemDateLabel = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        }

        private void ShowViewControlFirstExecute()
        {
            // 第一引数：reigionName（MainWindowのContentControlタグの属性にある）
            // 第二引数：Viewの名前（文字列ではコンパイルエラーにならないので、nameofにViewsファイル内のファイル名を指定して、
            // 名前が変わった際にコンパイルエラーになるようにする）
            _regionManager.RequestNavigate("ContentRegion", nameof(ViewControlFirst));

            // 最後に画面遷移させたいViewをApp.xamlのRegisterTypesメソッドに登録しておく必要がある
        }
        private void ShowViewControlFirstParamExecute()
        {
            var p = new NavigationParameters();

            // ViewControlFirstViewModelのプロパティLabelAに、システム日時をパラメーターとして渡す
            p.Add(nameof(ViewControlFirstViewModel.LabelA), SystemDateLabel);

            // 第三引数にインスタンス化したNavigationParametersを渡す
            _regionManager.RequestNavigate("ContentRegion", nameof(ViewControlFirst), p);
        }

        private void ShowViewControlSecondExecute()
        {
            _daialogService.ShowDialog(nameof(ViewControlSecond) ,null ,null);

            // ポップアップで表示させる処理もApp.xamlのRegisterTypesに登録する
        }
    }
}
