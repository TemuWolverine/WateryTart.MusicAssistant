using WateryTart.MusicAssistant.Generators.Attributes;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace WateryTart.MusicAssistant.Models;

[NotifyPropertyChanged]
public partial class MediaItem : Item
{
    [JsonPropertyName("elapsed_time")]
    [NotifyingProperty]
    public partial double ElapsedTime { get; set; }
    public double Progress
    {
        get
        {
            if (Duration == null || Duration == 0)
                    return 0;
            return (ElapsedTime / Duration.Value) * 100;
        }
    }
}