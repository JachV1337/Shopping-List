using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shopping_List.Model;
using System.Collections.ObjectModel;
using System.Text.Json;
using CommunityToolkit.Maui.Storage;
using System.Text;

namespace Shopping_List.ViewModel
{
    public partial class ListViewModel : BaseViewModel
    {
        public ObservableCollection<ShoppingItemModel> ShoppingItems { get; set; }

        [ObservableProperty]
        private int checkedCount;

        public double CheckedProgress => ShoppingItems.Count == 0 ? 0 : (double)CheckedCount / ShoppingItems.Count;
        public ListViewModel()
        {
            ShoppingItems = new ObservableCollection<ShoppingItemModel>();
            LoadItems();
        }
        
        private void LoadItems()
        {
            ShoppingItems.Add(new ShoppingItemModel { Name = "Milk", IsChecked = false });
            ShoppingItems.Add(new ShoppingItemModel { Name = "Bread", IsChecked = false });
            ShoppingItems.Add(new ShoppingItemModel { Name = "Eggs", IsChecked = false });
        }

        [RelayCommand]
        public void ClearItems()
        {
            ShoppingItems.Clear();
            UpdateCheckedCount();
        }

        [RelayCommand]
        public void RemoveItem(ShoppingItemModel item)
        {
            if (item != null)
            {
                ShoppingItems.Remove(item);
                UpdateCheckedCount();
            }
        }

        [RelayCommand]
        public void AddItem(string itemName)
        {
            if (!string.IsNullOrWhiteSpace(itemName))
            {
                ShoppingItems.Add(new ShoppingItemModel { Name = itemName, IsChecked = false });
                UpdateCheckedCount();
            }
        }
        [RelayCommand]
        public async Task ToggleItemChecked(ShoppingItemModel item)
        {
            if (item != null)
            {

                if (item != null)
                {
                    // Tworzymy listę posortowaną (referencje te same)
                    var sorted = ShoppingItems.OrderBy(i => i.IsChecked).ToList();

                    // Przesuwamy elementy w istniejącej kolekcji
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        for (int i = 0; i < sorted.Count; i++)
                        {
                            var currentIndex = ShoppingItems.IndexOf(sorted[i]);
                            if (currentIndex != i)
                                ShoppingItems.Move(currentIndex, i);
                        }
                    });
                }
                    UpdateCheckedCount();
            }
        }

        [RelayCommand]
        public async Task ExportItems()
        {
            string json = JsonSerializer.Serialize(ShoppingItems, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(json));

            var result = await FileSaver.Default.SaveAsync("zakupy.json", stream);

            if (!result.IsSuccessful)
            {
                await Application.Current.MainPage.DisplayAlertAsync("Błąd", "Nie udało się zapisać pliku", "OK");
            }
        }
        [RelayCommand]
        public async Task Importitems()
        {

            try
            {
                // Definiujemy własny typ pliku JSON
                var jsonFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
        {
            { DevicePlatform.iOS, new[] { "public.json" } },
            { DevicePlatform.Android, new[] { "application/json" } },
            { DevicePlatform.WinUI, new[] { ".json" } },
            { DevicePlatform.MacCatalyst, new[] { "public.json" } }
        });

                var result = await FilePicker.Default.PickAsync(new PickOptions
                {
                    PickerTitle = "Wybierz plik JSON z listą zakupów",
                    FileTypes = jsonFileType
                });

                if (result is null)
                    return; // użytkownik anulował wybór

                using var stream = await result.OpenReadAsync();
                using var reader = new StreamReader(stream);
                string json = await reader.ReadToEndAsync();

                var items = JsonSerializer.Deserialize<List<ShoppingItemModel>>(json);

                if (items != null)
                {
                    ShoppingItems.Clear();
                    foreach (var item in items)
                        ShoppingItems.Add(item);

                    UpdateCheckedCount();
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlertAsync("Błąd", $"Nie udało się wczytać pliku: {ex.Message}", "OK");
            }
        }
        

        public void UpdateCheckedCount()
        {
            CheckedCount = ShoppingItems.Count(item => item.IsChecked);
            OnPropertyChanged(nameof(CheckedProgress));
        }
    }
}