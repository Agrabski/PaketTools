
namespace PaketTools.Format;

public class PaketDependencies
{
	public List<DependencyGroup> Groups { get; init; } = [new() { Name = string.Empty }];

	public static PaketDependencies Parse(string content)
	{
		var result = new PaketDependencies();
		var currentGroup = result.Groups.First();
		foreach (var line in content.Replace("\r\n", "\n").Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
		{
			if (line.StartsWith(DependencyGroup.Framework))
				currentGroup.FrameworkRestrictions.AddRange(line[DependencyGroup.Framework.Length..].Split(',', StringSplitOptions.TrimEntries));
			else
			{
				if (line.StartsWith(DependencyGroup.Source))
					currentGroup.Sources.Add(line[DependencyGroup.Source.Length..].Trim());
				else
					currentGroup.Dependencies.Add(line);
			}
		}

		return result;
	}
	public override string ToString() => string.Join("\r\n\r\n", Groups.OrderBy(g => g.Name));
}
