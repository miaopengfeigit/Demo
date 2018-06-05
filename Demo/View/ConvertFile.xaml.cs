
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.WindowsAPICodePack.Dialogs;
using MvvmLight1.ViewModel;

namespace MvvmLight1.View
{
    /// <summary>
    /// ConvertFile.xaml 的交互逻辑
    /// </summary>
    public partial class ConvertFile : UserControl
    {
        public ConvertFile()
        {
            InitializeComponent();
            DataContext = new ConvertFileViewModel();
        }

        /// <summary>
        /// 接收到消息后的后续工作：根据返回来的信息弹出消息框
        /// </summary>
        /// <param name="msg"></param>
        private void ShowReceiveInfo(string msg)
        {
            MessageBox.Show(msg);
        }
    }

}
