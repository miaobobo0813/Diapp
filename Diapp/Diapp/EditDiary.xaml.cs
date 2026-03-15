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
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditDiary : Page
    {
        public EditDiary()
        {
            InitializeComponent();
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PlainContent.Document.GetText(Microsoft.UI.Text.TextGetOptions.None, out string plainText);
                string title = Title.Text;
                title = title.TrimEnd(' ');
                if (title == "")
                {
                    ContentDialog noneTitleDialog = new()
                    {
                        Title = "保存错误",
                        Content = "不能创建无标题的日记。",
                        CloseButtonText = "确定",
                        XamlRoot = this.XamlRoot
                    };
                    await noneTitleDialog.ShowAsync();
                    return;
                }
                var newDiary = new Diary(title, plainText, Encryption.ShareUserName, Encryption.ShareUUID, Encryption.SharePassword);
                DiaryManager.AddDiary(newDiary);
                ContentDialog successDialog = new()
                {
                    Title = "保存成功",
                    Content = "日记已保存。",
                    CloseButtonText = "确定",
                    XamlRoot = this.XamlRoot
                };
                await successDialog.ShowAsync();
            }
            catch (Exception ex)
            {
                ContentDialog dialog = new()
                {
                    Title = "保存错误",
                    Content = $"意外错误: {ex.Message}。",
                    CloseButtonText = "确定",
                    XamlRoot = this.XamlRoot
                };
                await dialog.ShowAsync();
            }
        }

        private void Border_Loaded(object sender, RoutedEventArgs e)
        {
            cardShadow.Receivers.Add(gridShadow);
        }
    }
}
