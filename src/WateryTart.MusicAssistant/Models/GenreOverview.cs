using System.Text.Json.Serialization;
using WateryTart.MusicAssistant.Generators.Attributes;

namespace WateryTart.MusicAssistant.Models
{
    [NotifyPropertyChanged]
    public partial class GenreOverview : MediaItemBase
    {
        [JsonPropertyName("path")]
        public string Path { get; set; }

        [JsonPropertyName("icon")]
        public object Icon { get; set; }

        [JsonPropertyName("items")]
        public List<Item> Items { get; set; }

        [JsonPropertyName("subtitle")]
        public object Subtitle { get; set; }
    }
}