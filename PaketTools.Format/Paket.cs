using System.Text;
using System.Text.RegularExpressions;

namespace PaketTools.Format;

public static class Paket
{
	public const string References = "paket.references";
	public const string Lock = "paket.lock";
	public const string Dependencies = "paket.dependencies";

	public static string FormatReferences(string content)
	{
		var lines = content.Replace("\r\n", "\n").Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
		var sb = new StringBuilder();
		var groups = new Dictionary<string, List<string>>();
		var currentGroup = new List<string>();
		groups[""] = currentGroup;
		foreach (var line in lines)
			if (line.StartsWith("group"))
				if (groups.TryGetValue(line, out var group))
					currentGroup = group;
				else
					groups[line] = currentGroup = [];
			else
				currentGroup.Add(line);
		return string.Join("\r\n", groups
			.OrderBy(kv => kv.Key)
			.Select(kv => $"{kv.Key}\r\n{string.Join("\r\n", kv.Value.Order())}")
		);
	}
	public static string FormatDependencies(string content)
	{
		var dependencies = PaketDependencies.Parse(content);
		return dependencies.ToString();

	}
}
