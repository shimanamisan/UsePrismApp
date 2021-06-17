using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Prism.Services.Dialogs;
using System;
using UsePrismApp.ViewModels;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            // ポップアップ画面をテストする際にDialogServiceを渡さないとエラーになる
            // IDialogServiceを実装したダミーのクラスを用意してインスタンス化すればよいが
            // 面倒なのでMoqを使用する
            var moq = new Mock<IDialogService>();

            var vm = new ViewControlFirstViewModel(moq.Object);

            vm.ViewFirstControlOKButton.Execute();
        }
    }
}
