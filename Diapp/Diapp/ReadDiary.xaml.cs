using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using WinRT;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Diapp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ReadDiary : Page
    {
        public ReadDiary()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is DiaryForDisplay diaryToDisplay)
            {
                titleTextBlock.Text = diaryToDisplay.ShowTitle;
                dateTextBlock.Text = diaryToDisplay.ShowDate;

                var para = new Paragraph();
                var run = new Run { Text = diaryToDisplay.ShowContent };
                para.Inlines.Add(run);
                contextBlock.Blocks.Add(para);
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            DiaryForDisplay diaryToEdit = new()
            {
                ShowTitle = titleTextBlock.Text,
                ShowDate = dateTextBlock.Text,
                ShowContent = "", 
            };
            foreach (var para in contextBlock.Blocks)
            {
                if (para is Paragraph showPara)
                {
                    foreach (var text in showPara.Inlines)
                    {
                        if (text is Run run)
                        {
                            diaryToEdit.ShowContent += run.Text;
                        }
                    }
                }
            }
            Diary encrypedDiaryToEdit = new(diaryToEdit.ShowTitle, diaryToEdit.ShowContent, Encryption.ShareUserName, Encryption.ShareUUID, Encryption.SharePassword);
            Frame?.Navigate(typeof(EditHadDiary), encrypedDiaryToEdit);
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var warningDialog = new ContentDialog
            {
                Title = "警告",
                Content = "此操作将永久删除当天所有相同标题的日记（真的很久！）",
                PrimaryButtonText = "确定",
                CloseButtonText = "取消", 
                XamlRoot = XamlRoot
            };
            var result = await warningDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                DiaryForDisplay diaryToDelete = new()
                {
                    ShowTitle = titleTextBlock.Text,
                    ShowDate = dateTextBlock.Text,
                    ShowContent = "",
                };
                Diary encrypedDiaryToDelete = new(diaryToDelete.ShowTitle, diaryToDelete.ShowContent, Encryption.ShareUserName, Encryption.ShareUUID, Encryption.SharePassword);
                DiaryManager.RemoveDiary(encrypedDiaryToDelete);
                Frame.Navigate(typeof(DiaryList));
            } 
            else
            {
                return;
            }
        }

        private void Border_Loaded(object sender, RoutedEventArgs e)
        {
            cardShadow.Receivers.Add(gridShadow);
        }
    }
}
