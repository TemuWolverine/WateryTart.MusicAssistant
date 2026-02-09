using System.Text.Json.Serialization;

namespace WateryTart.MusicAssistant.Models;

public class UserPreferences
{
    [JsonPropertyName("globalSearch")]
    public string GlobalSearch { get; set; }

    [JsonPropertyName("activePlayerId")]
    public string ActivePlayerId { get; set; }

    [JsonPropertyName("recentlySelectedPlayerIds")]
    public List<string> RecentlySelectedPlayerIds { get; set; }

    [JsonPropertyName("settings.players.viewMode")]
    public string SettingsPlayersViewMode { get; set; }

    [JsonPropertyName("allPlayersExpanded")]
    public bool AllPlayersExpanded { get; set; }

    [JsonPropertyName("itemsListing.artistalbums.artistalbums")]
    public ItemsListingArtistalbumsArtistalbums ItemsListingArtistalbumsArtistalbums { get; set; }
}