using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using HandyControl.Tools;
using HandyControl.Tools.Command;
using ICSharpCode.AvalonEdit;
using JianXiEditor.Model;
using JianXiEditor.Service;
using JianXiEditor.View;

namespace JianXiEditor.ViewModel
{
    class TextCountViewModel : BindablePropertyBase
    {
        private TextCountModel textCountM;
        public TextCountModel TextCountM
        {
            get
            {
                if (textCountM == null)//如果不是空的，就说明已经实例化了
                {
                    textCountM = new TextCountModel();
                }
                return textCountM;
            }
            set => Set(ref textCountM, value);
        }

        private string content = null;
        /// <summary>
        /// 初始化
        /// </summary>
        public RelayCommand TextCountLoadedCommand => new RelayCommand(obj =>
        {
            Task task = new Task(() =>
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle,
                    new Action(() =>
                    {
                        TextEditor textEditor = (Application.Current.MainWindow as MainWindow).CodeEditor;
                        if (TextCountM.IsSelect == true)//如果是true说明是查看选中文字的详细字数统计
                            {
                            content = textEditor.SelectedText.Replace("　?", null).Replace(" ", null).Replace("\t", null).Replace("\n", null).Replace("\r", null);
                        }
                        else
                        {
                            content = textEditor.Document.Text.Replace("　?", null).Replace(" ", null).Replace("\t", null).Replace("\n", null).Replace("\r", null);
                        }
                    }));
            });
            //线程开启
            task.Start();
            //线程结束后加载以下代码
            task.ContinueWith((a) =>
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle,
                    new Action(() =>
                    {
                        TextCount textCount = new TextCount();
                        if (string.IsNullOrEmpty(content))
                        {
                            return;
                        }
                        Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle,
                            new Action(() =>
                            {
                                TextCountM.TotalWord = content.Length;//总字数
                                }));
                        Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle,
                            new Action(() =>
                            {
                                TextCountM.ByteCount = textCount.GetByteLength(content);//字节数
                                }));
                        Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle,
                            new Action(() =>
                            {
                                TextCountM.ChineseCount = TextCountM.ByteCount - TextCountM.TotalWord;//汉字数量
                                }));
                        Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle,
                            new Action(() =>
                            {
                                TextCountM.NumberCount = textCount.GetNumberLength(content);//数字数量
                                }));
                        Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle,
                            new Action(() =>
                            {
                                TextCountM.LetterCount = textCount.GetLetterLength(content);//字母数量
                                }));
                        Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle,
                            new Action(() =>
                            {
                                TextCountM.SymbolCount = TextCountM.ByteCount - TextCountM.NumberCount - TextCountM.LetterCount - ((TextCountM.ByteCount - TextCountM.TotalWord) * 2);
                            }));
                    }));
            });
        });
        /// <summary>
        /// 关闭窗口后
        /// </summary>
        public RelayCommand TextCountDeactivatedCommand => new RelayCommand(obj =>
        {
            //关闭窗口以后，把content设为null，这样内存占用会少一点
            content = null;
        });
    }
}
