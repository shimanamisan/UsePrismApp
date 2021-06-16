using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UsePrismApp.ViewModels
{
    // 画面遷移時にパラメータを渡すために INavigationAware と using Prism.Regions を追加
    public class ViewControlFirstViewModel : BindableBase, INavigationAware
    {
        private string _labelA = string.Empty;
        public string LabelA
        {
            get { return _labelA; }
            set { SetProperty(ref _labelA, value); }
        }
        public ViewControlFirstViewModel()
        {

        }

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
    }
}
