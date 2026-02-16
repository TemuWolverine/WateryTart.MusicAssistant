using System.Text.Json.Serialization;

namespace WateryTart.MusicAssistant.Models;

public class ProviderManifest
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("domain")]
    public string Domain { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("codeowners")]
    public List<string>? Codeowners { get; set; }

    [JsonPropertyName("stage")]
    public string Stage { get; set; } = string.Empty;

    [JsonPropertyName("requirements")]
    public List<string>? Requirements { get; set; }

    [JsonPropertyName("documentation")]
    public string Documentation { get; set; } = string.Empty;

    [JsonPropertyName("multi_instance")]
    public bool? MultiInstance { get; set; }

    [JsonPropertyName("builtin")]
    public bool? Builtin { get; set; }

    [JsonPropertyName("allow_disable")]
    public bool? AllowDisable { get; set; }

    [JsonPropertyName("depends_on")]
    public string DependsOn { get; set; } = string.Empty;

    [JsonPropertyName("icon")]
    public string Icon { get; set; } = string.Empty;

    [JsonPropertyName("icon_svg")]
    public string IconSvg { get; set; } = string.Empty;

    [JsonPropertyName("icon_svg_dark")]
    public string IconSvgDark { get; set; } = string.Empty;

    [JsonPropertyName("icon_svg_monochrome")]
    public string IconSvgMonochrome { get; set; } = string.Empty;

    [JsonPropertyName("mdns_discovery")]
    public List<string>? MdnsDiscovery { get; set; }

    [JsonPropertyName("credits")]
    public List<string>? Credits { get; set; }
}
