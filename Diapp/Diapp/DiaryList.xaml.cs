using Microsoft.UI.Input;
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
    /// 
    public class DiaryForDisplay
    {
        public required string ShowTitle { get; set; }
        public required string ShowContent { get; set; }
        public required string ShowDate { get; set; }
    }
    public sealed partial class DiaryList : Page
    {
        public List<DiaryForDisplay> DisplayDiaries = [];
        public DiaryList()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Init();
        }

        private void Init()
        {
            try
            {
                DisplayDiaries.Clear();
                var encryptedDiaries = DiaryManager.LoadDiaries();
                foreach (var diary in encryptedDiaries)
                {
                    string decryptedTitle = "";
                    string decryptedContent = "";
                    string key = Encryption.MakeKey(Encryption.ShareUserName, Encryption.ShareUUID, diary.CreateDate, Encryption.SharePassword);
                    decryptedTitle = Encryption.Decrypt(diary.Name, key);
                    decryptedContent = Encryption.Decrypt(diary.CipherContent, key);
                    string year = diary.CreateDate[..4];
                    string month = diary.CreateDate.Substring(4, 2);
                    string day = diary.CreateDate.Substring(6, 2);
                    string showDate = year + "/" + month + "/" + day;
                    var newDiary = new DiaryForDisplay
                    {
                        ShowContent = decryptedContent,
                        ShowDate = showDate,
                        ShowTitle = decryptedTitle
                    };
                    DisplayDiaries.Add(newDiary);
                }

                diariesStack.ItemsSource = DisplayDiaries;
                
            }
            catch (Exception ex)
            {
                var errorDialog = new ContentDialog
                {
                    Title = "加载失败",
                    Content = $"无法加载日记列表: {ex.Message}",
                    CloseButtonText = "确定",
                    XamlRoot = XamlRoot
                };
                _ = errorDialog.ShowAsync();
            }
        }
        
        private void Border_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (sender is FrameworkElement element)
            {
                if (element.DataContext is DiaryForDisplay clickedDiaryDisplay)
                {
                    Frame?.Navigate(typeof(ReadDiary), clickedDiaryDisplay);
                }
            }
        }

    }
}
