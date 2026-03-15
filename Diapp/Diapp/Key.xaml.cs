using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Diapp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Key : Page
    {
        public Key()
        {
            InitializeComponent();
        }
        
        private void SavePassword_Click(object sender, RoutedEventArgs e)
        {
            if (UserName.Password == "")
            {
                ContentDialog noneUserNameDialog = new()
                {
                    Title = "保存错误",
                    Content = "未输入用户名。请输入用户名后继续。",
                    CloseButtonText = "确定",
                    XamlRoot = this.XamlRoot
                };

                _ = noneUserNameDialog.ShowAsync();
                return;
            }
            Encryption.ShareUserName = UserName.Password;
            if (Password.Password.Length < 10)
            {
                ContentDialog tooShortPasswordDialog = new()
                {
                    Title = "保存错误",
                    Content = "密码太短。请使用安全性更高的密码。",
                    CloseButtonText = "确定",
                    XamlRoot = this.XamlRoot
                };

                _ = tooShortPasswordDialog.ShowAsync();
                return ;
            }
            Encryption.SharePassword = Password.Password;
            Encryption.ShareUUID = Encryption.MakeUUID(Encryption.ShareUserName);
            ContentDialog successDialog = new()
            {
                Title = "保存成功",
                Content = "已成功保存你的密钥。",
                CloseButtonText = "确定",
                XamlRoot = this.XamlRoot
            };

            _ = successDialog.ShowAsync();

            return;
        }

        private void Border_Loaded(object sender, RoutedEventArgs e)
        {
            shadow.Receivers.Add(shadowCard);
        }
    }
}
