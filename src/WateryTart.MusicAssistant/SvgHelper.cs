using System.Xml.Linq;

namespace WateryTart.MusicAssistant;

public static class SvgHelper
{
    /// <summary>
    /// Extracts all SVG path data ("d" attributes) from an SVG string for use in XAML.
    /// </summary>
    /// <param name="svgContent">The SVG XML as a string.</param>
    /// <returns>A list of path data strings compatible with XAML's Path.Data.</returns>
    public static List<string> ExtractPathDataForXaml(string svgContent)
    {
        var result = new List<string>();
        var doc = XDocument.Parse(svgContent);
        XNamespace ns = doc.Root?.Name.Namespace ?? "";

        foreach (var path in doc.Descendants(ns + "path"))
        {
            var d = path.Attribute("d")?.Value;
            if (!string.IsNullOrWhiteSpace(d))
                result.Add(d);
        }

        return result;
    }
}