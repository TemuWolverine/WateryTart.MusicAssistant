using System.Text.Json.Serialization;

namespace WateryTart.MusicAssistant.Models;

public class ItemsListingArtistalbumsArtistalbums
{
    [JsonPropertyName("sortBy")]
    public string SortBy { get; set; }
}