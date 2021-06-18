using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UsePrismApp.Services
{
    public interface IMessageService
    {
        // OKボタンだけのメッセージボックス
        void ShowDialog(string message);

        // 質問系のメッセージボックス
        MessageBoxResult Question(string message);
    }
}
