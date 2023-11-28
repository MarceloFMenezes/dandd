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
    internal partial class RaceViewModel: ObservableObject, IDisposable
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
        public ObservableCollection<Race> _races;


        public RaceViewModel() 
        { 
            client = new HttpClient();
            Races = new ObservableCollection<Race>();
            _serializerOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };            
        }
        public ICommand GetRacesCommand => new Command(async () => await LoadRacesAsync());

        private async Task LoadRacesAsync()
        {
            var url = $"{baseUrl}/races";
            try
            {
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Races = JsonSerializer.Deserialize<ObservableCollection<Race>>(content, _serializerOptions);
                }
            }
            catch(Exception e)
            {
                
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
