using HandyControl.Tools;
using HandyControl.Tools.Command;
using ICSharpCode.AvalonEdit.Highlighting;
using JianXiEditor.Config;
using JianXiEditor.Model;
using JianXiEditor.Utility;
using System;
using System.Windows;

namespace JianXiEditor.ViewModel
{
    class SyntaxHighlightingViewModel : BindablePropertyBase
    {
        private SyntaxHighlightingModel syntax;
        public SyntaxHighlightingModel Syntax
        {
            get
            {
                if (syntax == null)//如果不是空的，就说明已经实例化了
                {
                    syntax = new SyntaxHighlightingModel();
                }
                return syntax;
            }
            set => Set(ref syntax, value);
        }
        public RelayCommand SyntaxSelectedCommand => new RelayCommand(obj =>
        {
            try
            {
                MainViewModel main = Application.Current.MainWindow.DataContext as MainViewModel;
                main.Main.FileSyntaxHighlighting = Syntax.SelectProgramLanguage;
                main.Editor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinitionByExtension(FileExtension.ToExtension(Syntax.SelectProgramLanguage));
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                HandyControl.Controls.Growl.Warning("切换高亮语法时出现异常，已将异常原因记录到如下目录：" + Environment.NewLine + Config_Temp.AppDataPath_Log);
            }
        });
    }
}
