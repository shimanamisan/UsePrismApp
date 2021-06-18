using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Prism.Services.Dialogs;
using System;
using UsePrismApp.Services;
using UsePrismApp.ViewModels;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ボタン1のテスト()
        {
            // ポップアップ画面をテストする際にDialogServiceを渡さないとエラーになる
            // IDialogServiceを実装したダミーのクラスを用意してインスタンス化すればよいが
            // 面倒なのでMoqを使用する
            var moq = new Mock<IDialogService>();

            var vm = new ViewControlFirstViewModel(moq.Object);

            moq.Setup(x => x.ShowDialog(
                    // ShowDialogしたときに以下のパラメータが投げられて値が入っているかというテストができる
                    It.IsAny<string>(),
                    It.IsAny<IDialogParameters>(),
                    It.IsAny<Action<IDialogResult>>()
                )).Callback<string,
                            IDialogParameters,
                            Action<IDialogResult>>
                            ((viewName, p, result) =>
                            {
                                Assert.AreEqual("ViewControlSecond", viewName);
                            });

            vm.ViewFirstControlOKButton.Execute();
        }

        [TestMethod]
        public void ボタン2のテスト()
        {
            var dialogService = new Mock<IDialogService>();
            var messageService = new Mock<IMessageService>();

            // 表示されているメッセージまでテストする方法
            // 本番コードのMessageBoxで「Saveしますか？」と表示されたらOKを返すという意味のコード
            messageService.Setup(x => x.Question("Saveしますか？")).Returns(System.Windows.MessageBoxResult.OK);

            var vm = new ViewControlFirstViewModel(dialogService.Object, messageService.Object);

            messageService.Setup(x => x.ShowDialog(
                   // ShowDialogが呼ばれたときに以下のパラメータが投げられて値が入っているかというテストができる
                   It.IsAny<string>()
                   )).Callback<string>(message =>
                   {
                       // OKボタンが押された後のメッセージが「Saveしました」となっているかどうかテストを実行している
                       // 引数のmessageに実際にボタンが押されたときに表示されるメッセージが入ってくる
                       Assert.AreEqual("Saveしました", message);
                   });

            vm.ViewFirstControlOKButton2.Execute();

            // 通ってないテストがあれば検知する
            messageService.VerifyAll();
        }
    }
}
