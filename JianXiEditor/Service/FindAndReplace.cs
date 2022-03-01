using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Document;
using JianXiEditor.View;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.IO;
using JianXiEditor.Utility;
using System;

namespace JianXiEditor.Service
{
    class FindAndReplace
    {
        /// <summary>
        /// 执行查找
        /// </summary>
        /// <param name="findContent">要查找的内容</param>
        /// <param name="searchUp">是否向上搜索</param>
        /// <param name="matchCase">是否区分大小写</param>
        /// <param name="regExp">是否开启正则表达式</param>
        /// <param name="wildCards">是否开启通配符</param>
        /// <param name="matchWholeWord">是否开启全字匹配</param>
        /// <returns>有没有查到</returns>
        public static bool FindNext(string findContent, bool searchUp, bool matchCase, bool regExp, bool wildCards, bool matchWholeWord)
        {
            TextEditor textEditor = (Application.Current.MainWindow as MainWindow).CodeEditor;
            Regex regex = GetRegEx(findContent, searchUp, matchCase, regExp, wildCards, matchWholeWord, false);
            int start = regex.Options.HasFlag(RegexOptions.RightToLeft) ?
            textEditor.SelectionStart : textEditor.SelectionStart + textEditor.SelectionLength;
            Match match = regex.Match(textEditor.Document.Text, start);

            if (!match.Success)//如果没查到内容，就从头开始重新查
            {
                if (regex.Options.HasFlag(RegexOptions.RightToLeft))
                {
                    match = regex.Match(textEditor.Document.Text, textEditor.Document.TextLength);
                }
                else
                {
                    match = regex.Match(textEditor.Document.Text, 0);
                }
            }
            if (match.Success)
            {
                textEditor.Select(match.Index, match.Length);
                TextLocation loc = textEditor.Document.GetLocation(match.Index);
                textEditor.ScrollTo(loc.Line, loc.Column);
            }
            return match.Success;
        }
        /// <summary>
        /// 正则表达式
        /// </summary>
        /// <param name="findContent">要查找的内容</param>
        /// <param name="searchUp">是否向上搜索</param>
        /// <param name="matchCase">是否区分大小写</param>
        /// <param name="regExp">是否开启正则表达式</param>
        /// <param name="wildCards">是否开启通配符</param>
        /// <param name="matchWholeWord">是否开启全字匹配</param>
        /// <param name="leftToRight">查找循序，left向左，true向右</param>
        /// <returns>正则</returns>
        public static Regex GetRegEx(string findContent, bool searchUp, bool matchCase, bool regExp, bool wildCards, bool matchWholeWord, bool leftToRight = false)
        {
            RegexOptions options = RegexOptions.None;
            if (searchUp == true && !leftToRight)
            {
                options |= RegexOptions.RightToLeft;
            }
            if (matchCase == false)
            {
                options |= RegexOptions.IgnoreCase;
            }
            switch (regExp)
            {
                case true:
                    return new Regex(findContent, options);
                default:
                    {
                        string pattern = Regex.Escape(findContent);
                        if (wildCards == true)
                        {
                            pattern = pattern.Replace("\\*", ".*").Replace("\\?", ".");
                        }

                        if (matchWholeWord == true)
                        {
                            pattern = "\\b" + pattern + "\\b";
                        }

                        return new Regex(pattern, options);
                    }
            }
        }
        /// <summary>
        /// 获取所有文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="findFileType">要查找的文件类型</param>
        /// <returns>查找到的文件</returns>
        public string[] GetFiles(string filePath, string findFileType)
        {
            string direPath = Path.GetDirectoryName(filePath);
            if (string.IsNullOrEmpty(findFileType))//如果没有填写，就默认*.*
            {
                findFileType = "*.*";
            }
            string[] files = Directory.GetFiles(direPath, findFileType, SearchOption.AllDirectories);
            GC.Collect();
            return files;
        }
        /// <summary>
        /// 查找文件中的内容
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="findContent">要查找的内容</param>
        /// <returns>查找到的结果</returns>
        public Dictionary<int, string> FindFileContent(string filePath, string findContent)
        {
            Dictionary<int, string> list = new Dictionary<int, string>();
            string[] s = File.ReadAllLines(filePath);
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i].Contains(findContent))
                {
                    list.Add(i + 1, s[i]);
                }
            }
            GC.Collect();
            return list;
        }
        /// <summary>
        /// 替换文件中的内容
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="findContent">要查找的内容</param>
        /// <param name="replaceContent">要替换的内容</param>
        public void ReplaceFileContent(string filePath, string findContent, string replaceContent)
        {
            string encoding = FileBasicInfo.GetEncodingName(filePath);
            string[] s = File.ReadAllLines(filePath);
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i].Contains(findContent))
                {
                    s[i] = s[i].Replace(findContent, replaceContent);
                }
            }
            File.WriteAllLines(filePath, s, FileBasicInfo.GetEncoding(encoding));
            GC.Collect();
        }
    }
}