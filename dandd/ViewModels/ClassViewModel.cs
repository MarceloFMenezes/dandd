using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;

using CommunityToolkit.Mvvm.ComponentModel;
using dandd.Models;

namespace dandd.ViewModels
{
    internal partial class ClassViewModel : ObservableObject, IDisposable
    {
        HttpClient client;
        JsonSerializerOptions _serializerOptions;

        string baseUrl = "https://www.dnd5eapi.co/api";

        [ObservableProperty]
        public string index;
        [ObservableProperty]
        public string name;
        [ObservableProperty]
        public string url;
        [ObservableProperty]
        public ObservableCollection<Class> _classes;


        public ClassViewModel()
        {
            client = new HttpClient();
            Classes = new ObservableCollection<Class>();
            _serializerOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
        }
        public ICommand GetRacesCommand => new Command(async () => await LoadRacesAsync());

        private async Task LoadRacesAsync()
        {
            var url = $"{baseUrl}/classes";
            try
            {
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Classes = JsonSerializer.Deserialize<ObservableCollection<Class>>(content, _serializerOptions);
                }
            }
            catch (Exception e)
            {

            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
