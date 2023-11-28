using System.Text;

namespace PaketTools.Format;

public class DependencyGroup
{
	public const string Framework = "framework:";
	public const string Source = "source";
	public const string Group = "group";

	public List<string> FrameworkRestrictions { get; init; } = [];
	public List<string> Dependencies { get; init; } = [];
	public List<string> Sources { get; init; } = [];
	public required string Name { get; set; }

	public override string ToString()
	{
		var builder = new StringBuilder();
		if (!string.IsNullOrEmpty(Name))
			builder.AppendLine($"{Group} {Name}");
		builder.AppendLine($"{Framework} {string.Join(", ", FrameworkRestrictions)}");

		builder.AppendLine();
		builder.AppendLine(string.Join("\r\n", Sources.Order().Select(s => $"{Source} {s}")));
		builder.AppendLine();

		foreach (var dependency in Dependencies.Order())
			builder.AppendLine(dependency);

		return builder.ToString();
	}
}
