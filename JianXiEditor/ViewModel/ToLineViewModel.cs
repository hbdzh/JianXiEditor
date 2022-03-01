using HandyControl.Tools;
using HandyControl.Tools.Command;
using ICSharpCode.AvalonEdit;
using JianXiEditor.Model;
using JianXiEditor.View;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace JianXiEditor.ViewModel
{
    class ToLineViewModel : BindablePropertyBase
    {
        private ToLineModel toLineM;
        public ToLineModel ToLineM
        {
            get
            {
                if (toLineM == null)//如果不是空的，就说明已经实例化了
                {
                    toLineM = new ToLineModel();
                }
                return toLineM;
            }
            set => Set(ref toLineM, value);
        }

        /// <summary>
        /// 跳转行的定时器
        /// </summary>
        private DispatcherTimer timer_Line;
        private TextEditor textEditor;

        /// <summary>
        /// 初始化
        /// </summary>
        public RelayCommand ToLineLoadedCommand => new RelayCommand(obj =>
        {
            timer_Line = new DispatcherTimer();
            textEditor = (Application.Current.MainWindow as MainWindow).CodeEditor;
            timer_Line.Tick += Timer_Line_Tick;
            timer_Line.Interval = TimeSpan.FromSeconds(0.2);
        });
        /// <summary>
        /// 数值变更后
        /// </summary>
        public RelayCommand ValueChangedCommand => new RelayCommand(obj =>
        {
            timer_Line.Stop();
            if (ToLineM.ToLineNumber <= textEditor.Document.LineCount)
            {
                timer_Line.Start();
            }
        });
        /// <summary>
        /// 0.2秒后跳转行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Line_Tick(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                    new Action(() =>
                    {
                        textEditor.ScrollTo(ToLineM.ToLineNumber, 0);
                    }));
            });
            timer_Line.Stop();
        }
        /// <summary>
        /// 窗口非激活后
        /// </summary>
        public RelayCommand DeactivatedCommand => new RelayCommand(obj =>
        {
            timer_Line.Stop();
        });
    }
}
