﻿using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UsePrismApp.Services;

namespace UsePrismApp.ViewModels
{
    public class ViewControlThirdViewModel : BindableBase, IConfirmNavigationRequest
    {
        private IMessageService _messageService;

        // ListBox
        private ObservableCollection<string> _viewThirdListBox = new ObservableCollection<string>();

        // リストボックスにバインディングさせるものは ObservableCollection を使用する
        // System.Collections.ObjectModel をusingしておく
        public ObservableCollection<string> ViewThirdListBox
        {
            get { return _viewThirdListBox; }
            set { SetProperty(ref _viewThirdListBox, value); }
        }

        // ComboBox
        private ObservableCollection<ComboBoxViewModel> _viewThirdComboBox = new ObservableCollection<ComboBoxViewModel>();

        // ComboBox
        public ObservableCollection<ComboBoxViewModel> ViewThirdComboBox
        {
            get { return _viewThirdComboBox; }
            set { SetProperty(ref _viewThirdComboBox, value); }
        }

        // SelectedItem Binding コンボボックスの中のデータ1つを選択していることを受ける
        private ComboBoxViewModel _selectedAreas;

        // Selected
        public ComboBoxViewModel SelectedAreas
        {
            get { return _selectedAreas; }
            set { SetProperty(ref _selectedAreas, value); }
        }

        // SelectedAreaLabelの文字列をバインディングさせる
        private string _selectedAreaLabel = "デフォルトラベル： イベントを検知したら値が変わります";

        public string SelectedAreaLabel
        {
            get { return _selectedAreaLabel; }
            set { SetProperty(ref _selectedAreaLabel, value); }
        }

        // AreaSelectedChanged
        public DelegateCommand AreaSelectedChanged { get; }

        public ViewControlThirdViewModel() : this(new MessageService())
        {

        }

        // コンストラクターが2つあるがこちらにロジックを集中させる
        // 引数なしから呼ばれてもかならずこっちも呼ばれるので
        public ViewControlThirdViewModel(IMessageService messageService)
        {
            _messageService = messageService;

            ViewThirdListBox.Add("Sample");
            ViewThirdListBox.Add("TeatData");
            ViewThirdListBox.Add("Hello! World");

            ViewThirdComboBox.Add(new ComboBoxViewModel(1, "大阪"));
            ViewThirdComboBox.Add(new ComboBoxViewModel(2, "東京"));
            ViewThirdComboBox.Add(new ComboBoxViewModel(3, "名古屋"));

            // ComboBoxのデフォルト値も指定できる
            SelectedAreas = ViewThirdComboBox[1];

            // AreaSelectedChanged
            AreaSelectedChanged = new DelegateCommand(AreaSelectedChangedExecute);

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

        // SlectionChangeが発生したときに通知を受け取る
        public void AreaSelectedChangedExecute()
        {
            SelectedAreaLabel = SelectedAreas.Value + "： " + SelectedAreas.DisplayValue;
        }
    }
}
