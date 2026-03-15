using ABI.System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Diapp
{
    public static class DiaryManager
    {
        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            WriteIndented = true
        };
        private static readonly string DiaryFilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), 
            "Diapp", 
            "diaries.json"
        );

        private static void EnsureDirectoryExists()
        {
            var directory = Path.GetDirectoryName( DiaryFilePath );
            if (!Directory.Exists( directory ) && directory != null)
            {
                Directory.CreateDirectory( directory );
            }
        }

        private static async Task SaveDiariesAsync(List<Diary> diaries)
        {
            try
            {
                EnsureDirectoryExists();

                var jsonString = JsonSerializer.Serialize(diaries, _jsonOptions);
                await File.WriteAllTextAsync(DiaryFilePath, jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static List<Diary> LoadDiaries()
        {
            try
            {
                EnsureDirectoryExists() ;
                if (!File.Exists(DiaryFilePath))
                {
                    return [];
                }

                string jsonString = File.ReadAllText(DiaryFilePath);
                var diaries = JsonSerializer.Deserialize<List<Diary>>(jsonString);

                return diaries ?? [];
            } 
            catch ( Exception ex )
            {
                Console.WriteLine(ex.ToString());
                return [];
            }
        }

        public static void AddDiary(Diary diary)
        {
            try
            {
                var diaries = LoadDiaries();
                diaries.Add(diary);
                using var _ = SaveDiariesAsync(diaries);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString()); 
                return ;
            }
        }

        public static void EditDiary(Diary diary, Diary oldDiary)
        {
            try
            {
                var diaries = LoadDiaries();
                bool flag = true;
                for (int i = 0; i < diaries.Count; i++)
                {
                    if (diaries[i].Name == oldDiary.Name && diaries[i].CreateDate == oldDiary.CreateDate)
                    {
                        diaries[i] = diary;
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    diaries.Add(diary);
                }
                _ = SaveDiariesAsync(diaries);
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return;
            }
        }
        public static void RemoveDiary(Diary oldDiary)
        {
            try
            {
                var diaries = LoadDiaries();
                for (int i = 0; i < diaries.Count; i++)
                {
                    if (diaries[i].Name == oldDiary.Name && diaries[i].CreateDate == oldDiary.CreateDate)
                    {
                        diaries.Remove(diaries[i]);
                    }
                }
                _ = SaveDiariesAsync(diaries);
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return;
            }
        }
    }
}
