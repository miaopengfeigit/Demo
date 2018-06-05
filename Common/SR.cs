using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Common
{
    public class SR
    {
        /// <summary>
        /// 当语言变更时发生
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public static event EventHandler LanguageChanged;

        public static void ChangeLanguageFile(string languagefileName)
        {
            if (Application.Current.Resources.MergedDictionaries.Count > 0)
            {
                if (!Uri.IsWellFormedUriString(languagefileName, UriKind.RelativeOrAbsolute)) return;
                Application.Current.Resources.MergedDictionaries[0] = new ResourceDictionary()
                {
                    Source = new Uri(languagefileName, UriKind.RelativeOrAbsolute)
                };
            }
            else
            {
                LoadLanguageFile(languagefileName);
            }
            LanguageChanged(null, EventArgs.Empty);
        }

        public static void LoadLanguageFile(string languagefileName)
        {
            if (!Uri.IsWellFormedUriString(languagefileName, UriKind.RelativeOrAbsolute)) return;
            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source = new Uri(languagefileName, UriKind.RelativeOrAbsolute)
            });
        }
        
        public static string GetString(string strKey)
        {
            object str = Application.Current.Resources[strKey];
            return str == null ? strKey : str.ToString();
        }

        public static string GetString(string strKey, params object[] args)
        {
            string msg = GetString(strKey);
            msg = string.Format(msg, args);
            return msg;
        }

        public static Dictionary<string, string> GetLanguages(string directoryPath = @".\Resources\Language\")
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string[] files = FileHelper.GetFileNames(directoryPath);
            foreach (var file in files)
            {
                ResourceDictionary rd = new ResourceDictionary();
                string m = string.Format("pack://siteoforigin:,,,{0}", file.Remove(0, 1).Replace('\\', '/')) ;
                rd.Source = new Uri(m, UriKind.RelativeOrAbsolute);
                dic.Add(rd["Language"].ToString(), m);
            }
            return dic;
        }
    }
}
