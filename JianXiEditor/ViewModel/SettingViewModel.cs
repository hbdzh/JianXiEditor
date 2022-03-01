using HandyControl.Tools.Command;
using JianXiEditor.Config;
using JianXiEditor.Service;
using JianXiEditor.View;
using System;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;

namespace JianXiEditor.ViewModel
{
    class SettingViewModel
    {
        MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
        public SettingViewModel()
        {
            MainViewModel main = mainWindow.DataContext as MainViewModel;
            foreach (FontFamily fontfamily in Fonts.SystemFontFamilies)
            {
                LanguageSpecificStringDictionary fontdics = fontfamily.FamilyNames;
                //判断该字体是不是中文字体
                if (fontdics.ContainsKey(XmlLanguage.GetLanguage("zh-cn")))
                {
                    if (fontdics.TryGetValue(XmlLanguage.GetLanguage("zh-cn"), out string fontfamilyname))
                    {
                        main.Setting.FontList.Add(fontfamilyname);
                    }
                }
                //英文字体
                else
                {
                    if (fontdics.TryGetValue(XmlLanguage.GetLanguage("en-us"), out string fontfamilyname))
                    {
                        main.Setting.FontList.Add(fontfamilyname);
                    }
                }
            }
            main.Setting.SelectFont = Config_App.Config_FontFamily;
        }
        /// <summary>
        /// 退出时写入配置文件
        /// </summary>
        public RelayCommand ClosingCommand => new RelayCommand(obj =>
        {
            MainViewModel main = mainWindow.DataContext as MainViewModel;
            if (main != null)
            {
                //当关闭设置窗口时再写入配置文件，这样性能也可以最大化
                try
                {
                    Service.Initialize.Initialize_Config initConfig = new Service.Initialize.Initialize_Config();
                    Config_App.Config_UseTheme = main.Setting.SelectAppTheme;
                    initConfig.Initialize_Config_Theme();//重新加载主题
                    Config_App.Config_EditorLocation = main.Setting.SelectEditorLocation;
                    initConfig.Initialize_Config_EditorLocation(mainWindow.CodeEditor);
                    Config_App.Config_FontFamily = main.Setting.SelectFont;
                    Config_App.Config_FastMoveText = main.Setting.FastMoveText;
                    Config_App.Config_FastMoveLine = main.Setting.FastMoveLine;
                    Config_App.Config_QuickCopyLine = main.Setting.QuickCopyLine;
                    Config_App.Config_AutoUpdate = main.Setting.AutoUpdate;
                    Config_App.Config_RealDisplayState = main.Setting.RealDisplayState;
                    Config_App.Config_FileCache = main.Setting.FileCache;
                    Config_App.Config_HighlightSameWord = main.Setting.HighlightSameWord;
                    Config_App.Config_TagAutoClose = main.Setting.TagAutoClose;
                    Config_App.Config_AutoSaveFile = main.Setting.AutoSaveFile;
                    new AutoSaveFile().Initialize(main);//开启或关闭自动保存文件
                    /*------------------------------------------分界线--------------------------------------------*/
                    Config_Editor.Config_FontSize = main.Editor.SelelctFontSize;
                    Config_Editor.Config_ShowLineNumbers = main.Editor.ShowLineNumbers;
                    Config_Editor.Config_LineHeight = main.Setting.SelectLineHeight;
                    mainWindow.CodeEditor.TextArea.TextView.LineSpacing = main.Setting.SelectLineHeight;
                    Config_Editor.Config_WordWarp = main.Editor.WordWrap;
                    Config_Editor.Config_VirtualSpace = main.Editor.TextOptions.EnableVirtualSpace;
                    Config_Editor.Config_HighlightCurrentLine = main.Editor.TextOptions.HighlightCurrentLine;
                    Config_Editor.Config_ConvertTabsToSpaces = main.Editor.TextOptions.ConvertTabsToSpaces;
                    Config_Editor.Config_EnableEmailHyperlinks = main.Editor.TextOptions.EnableEmailHyperlinks;
                    Config_Editor.Config_EnableHyperlinks = main.Editor.TextOptions.EnableHyperlinks;
                    Config_Editor.Config_ShowEndOfLine = main.Editor.TextOptions.ShowEndOfLine;
                    Config_Editor.Config_ShowSpaces = main.Editor.TextOptions.ShowSpaces;
                    Config_Editor.Config_AllowScrollBelowDocument = main.Editor.TextOptions.AllowScrollBelowDocument;
                    Config_Editor.Config_CutCopyWholeLine = main.Editor.TextOptions.CutCopyWholeLine;
                    Config_Editor.Config_ShowTabs = main.Editor.TextOptions.ShowTabs;
                    Config_Editor.Config_IndentationSize = main.Editor.TextOptions.IndentationSize;
                    Config_Editor.Config_HideCursorWhileTyping = main.Editor.TextOptions.HideCursorWhileTyping;
                    Config_Editor.Config_ShowColumnRuler = main.Editor.TextOptions.ShowColumnRuler;
                    Config_Editor.Config_ColumnRulerPosition = main.Editor.TextOptions.ColumnRulerPosition;
                }
                catch (Exception ex)
                {
                    HandyControl.Tools.Logger.Log(ex);
                    HandyControl.Controls.Growl.Warning("保存设置时出现异常，已将异常原因记录到如下目录：" + Environment.NewLine + Config_Temp.AppDataPath_Log);
                }
            }
        });
    }
}
