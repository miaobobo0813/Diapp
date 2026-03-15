using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
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
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ExtendsContentIntoTitleBar = true;

            ContentFrame.Navigate(typeof(Key));
        }

        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.InvokedItemContainer != null)
            {
                string navItemTag = args.InvokedItemContainer.Tag.ToString() ?? "default";

                switch (navItemTag)
                {
                    case "keyPage":
                        ContentFrame.Navigate(typeof(Key));
                        break;
                    case "newDiary":
                        if (Encryption.ShareUserName == "" || Encryption.SharePassword == "")
                        {
                            ContentFrame.Navigate(typeof(Warning));
                        } 
                        else
                        {
                            ContentFrame.Navigate(typeof(EditDiary));
                        }
                        break;
                    case "showAllDiaries":
                        if (Encryption.ShareUserName == "" || Encryption.SharePassword == "")
                        {
                            ContentFrame.Navigate(typeof(Warning));
                        }
                        else
                        {
                            ContentFrame.Navigate(typeof(DiaryList));
                        }
                        break;
                    default:
                        ContentFrame.Navigate(typeof(Key));
                        break;
                }
            }
        }

        private void LockButton_Click(object sender, RoutedEventArgs e)
        {
            Encryption.ClearKey();
            var KeyPage = ContentFrame.Content as Page;
            if (KeyPage != null && KeyPage.XamlRoot != null)
            {
                ContentDialog success = new()
                {
                    Title = "锁定成功",
                    Content = "已清除内存中的密钥",
                    CloseButtonText = "确定",
                    XamlRoot = KeyPage.XamlRoot
                };
                _ = success.ShowAsync();
                if (KeyPage is not Key)
                {
                    ContentFrame.Navigate(typeof(Warning));
                }
            } 
            else
            {
                return;
            }
        }
    }
}
