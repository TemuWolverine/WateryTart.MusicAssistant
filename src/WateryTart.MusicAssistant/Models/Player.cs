using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using WateryTart.MusicAssistant.Models.Enums;

namespace WateryTart.MusicAssistant.Models;

[ObservableObject]
#pragma warning disable MVVMTK0033 // Inherit from ObservableObject instead of using [ObservableObject]
public partial class Player : IResult
#pragma warning restore MVVMTK0033 // Inherit from ObservableObject instead of using [ObservableObject]
{
    [JsonPropertyName("active_group")] public object? ActiveGroup { get; set; }
    [JsonPropertyName("active_source")] public string? ActiveSource { get; set; }
    public bool Available { get; set; }
    [JsonPropertyName("can_group_with")] public List<string>? CanGroupWith { get; set; }

    [JsonPropertyName("current_media")]
    [ObservableProperty] public partial CurrentMedia? CurrentMedia { get; set; }
    [JsonPropertyName("device_info")] public DeviceInfo? DeviceInfo { get; set; }
    [JsonPropertyName("display_name")] public string? DisplayName { get; set; }
    [JsonPropertyName("elapsed_time")] public double? ElapsedTime { get; set; }
    [JsonPropertyName("elapsed_time_last_updated")] public double? ElapsedTimeLastUpdated { get; set; }
    public bool Enabled { get; set; }
    [JsonPropertyName("expose_to_ha")] public bool ExposedToHA { get; set; }
    [JsonPropertyName("extra_attributes")] public ExtraAttributes? ExtraAttributes { get; set; }
    [JsonPropertyName("extra_data")] public ExtraData? ExtraData { get; set; }
    [JsonPropertyName("group_childs")] public List<object>? GroupChilds { get; set; }
    [JsonPropertyName("group_volume")] public int? GroupVolume { get; set; }
    [JsonPropertyName("hide_player_in_ui")] public List<string>? HidePlayerInUI { get; set; }
    public string? Icon { get; set; }
    [JsonPropertyName("mute_control")] public string? MuteControl { get; set; }
    public string? Name { get; set; }

    [JsonPropertyName("playback_state")]
    [ObservableProperty]
    public partial PlaybackState PlaybackState { get; set; }

    [JsonPropertyName("player_id")] public string? PlayerId { get; set; }
    [JsonPropertyName("power_control")] public string? PowerControl { get; set; }
    public bool Powered { get; set; }
    public string? Provider { get; set; }
    [JsonPropertyName("source_list")] public List<SourceList>? SourceList { get; set; }
    public string? state { get; set; }
    [JsonPropertyName("supported_features")] public List<string>? SupportedFeatures { get; set; }
    [JsonPropertyName("synced_to")] public object? SyncedTo { get; set; }
    public string? Type { get; set; }
    [JsonPropertyName("volume_control")] public string? VolumeControl { get; set; }

    [JsonPropertyName("volume_level")]
    [ObservableProperty]
    public partial int? VolumeLevel { get; set; }

    [JsonPropertyName("volume_muted")] public bool? VolumeMuted { get; set; }
}