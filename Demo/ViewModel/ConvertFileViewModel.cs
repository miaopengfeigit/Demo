
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MaterialDesignThemes.Wpf;
using MaterialDesignColors;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Common;
using System.Windows.Input;

namespace MvvmLight1.ViewModel
{
    public class ConvertFileViewModel : ViewModelBase
    {
        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, StringBuilder lParam);
        [DllImport("User32.DLL")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        public const int WM_SETTEXT = 0x000C;
        public const int WM_GETTEXT = 0x000D;

        public ConvertFileViewModel()
        {

        }

        #region 属性
        private string sourcePath;
        /// <summary>
        /// SourcePath
        /// </summary>
        public string SourcePath
        {
            get { return sourcePath; }
            set { SetProperty(ref sourcePath ,value); }
        }

        private string destPath;
        /// <summary>
        /// SourcePath
        /// </summary>
        public string DestPath
        {
            get { return destPath; }
            set { SetProperty(ref destPath, value); }
        }

        #endregion

        #region 命令

        
        //private RelayCommand sourcePathCmd;
        ///// <summary>
        ///// SourcePathCmd
        ///// </summary>
        //public RelayCommand SourcePathCmd
        //{
        //    get
        //    {
        //        if (sourcePathCmd == null)
        //            sourcePathCmd = new RelayCommand(() => ExcuteSourcePathCmd());
        //        return sourcePathCmd;

        //    }
        //    set { sourcePathCmd = value; }
        //}
        public ICommand SourcePathCmd
        {
            get { return new DelegateCommand(e => ExcuteSourcePathCmd()); }
        }

        private void ExcuteSourcePathCmd()
        {
            string path = OpenFileDialog();
            if (!string.IsNullOrEmpty(path)) SourcePath = path;
        }

        //private RelayCommand destPathCmd;
        ///// <summary>
        ///// DestPathCmd
        ///// </summary>
        //public RelayCommand DestPathCmd
        //{
        //    get
        //    {
        //        if (destPathCmd == null)
        //            destPathCmd = new RelayCommand(() => ExcuteDestPathCmd());
        //        return destPathCmd;

        //    }
        //    set { destPathCmd = value; }
        //}
        public ICommand DestPathCmd
        {
            get { return new DelegateCommand(e => ExcuteDestPathCmd()); }
        }

        private void ExcuteDestPathCmd()
        {
            string path = OpenFileDialog();
            if (!string.IsNullOrEmpty(path)) DestPath = path;
        }

        //private RelayCommand convertCmd;
        ///// <summary>
        ///// ConvertCmd
        ///// </summary>
        //public RelayCommand ConvertCmd
        //{
        //    get
        //    {
        //        if (convertCmd == null)
        //            convertCmd = new RelayCommand(() => ExcuteConvertCmd());
        //        return convertCmd;

        //    }
        //    set { convertCmd = value; }
        //}
        public ICommand ConvertCmd
        {
            get { return new DelegateCommand(e => ExcuteConvertCmd()); }
        }

        private void ExcuteConvertCmd()
        {
            var func = new Func< bool>(Convert);
            func.BeginInvoke((ar =>
             {
                 if (func.EndInvoke(ar))
                     ;
                 //Messenger.Default.Send<String>("ConvertSuccess", "ViewAlert");
                 else
                     ;
                    // Messenger.Default.Send<String>("ConvertFail", "ViewAlert");
             }), null);
        }
        #endregion

        #region 方法
        private string OpenFileDialog()
        {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            if (CommonFileDialogResult.Ok == dialog.ShowDialog())
                return dialog.FileName;
            else
                return string.Empty;
        }

        private string ReWrite(string path, string newType = ".miao")
        {
            Process process = new Process();
            process.StartInfo.FileName = "notepad.exe";
            process.StartInfo.Arguments = path;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.EnableRaisingEvents = true;
            process.Start();
            while (process.MainWindowHandle == IntPtr.Zero)
            {
                process.Refresh();
            }

            IntPtr vHandle = FindWindowEx(process.MainWindowHandle, IntPtr.Zero, "Edit", null);
            StringBuilder s = new StringBuilder(655650);
            SendMessage(vHandle, WM_GETTEXT, 655650, s);
            
            process.Kill();
            return s.ToString();
            //StreamWriter FileWriter = new StreamWriter(path.Replace(".miao", "") + newType, true); //创建日志文件
            //FileWriter.Write(s.ToString());
            //FileWriter.Close();
        }

        private bool Convert()
        {
            try
            {
                string[] sourcePaths = FileHelper.GetFileNames(SourcePath,"*",true);

                foreach (var sourceName in sourcePaths)
                {
                    string endPath = sourceName.Replace(sourcePath, string.Empty);
                    string extension = FileHelper.GetExtension(sourceName).ToLower();
                    if (extension == ".cs" || extension == ".cpp" || extension == ".h")
                    {
                        string fileName = destPath + endPath + ".miao";
                        string directoryName = FileHelper.GetDirectoryName(fileName);
                        if (!FileHelper.IsExistDirectory(directoryName))
                        {
                            FileHelper.CreateDirectory(directoryName);
                        }
                        FileHelper.WriteText(fileName, ReWrite(sourceName));
                    }
                    else
                    {
                        string fileName = destPath + endPath;
                        if (extension == ".miao") fileName = fileName.Replace(".miao", string.Empty);
                        string directoryName = FileHelper.GetDirectoryName(fileName);
                        if (!FileHelper.IsExistDirectory(directoryName))
                        {
                            FileHelper.CreateDirectory(directoryName);
                        }
                        FileHelper.Copy(sourceName, fileName);
                    }


                    //FileHelper.DeleteFile(processPath);

                }
                return true;
            }
            catch (Exception ex)
            {
                //Messenger.Default.Send<String>(ex.ToString(), "ViewAlert");
                return false;
            }
        }

        #endregion
    }
}
