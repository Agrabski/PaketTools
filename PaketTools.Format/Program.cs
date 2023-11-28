// See https://aka.ms/new-console-template for more information
using PaketTools.Format;

var root = FindRootDirectory(new DirectoryInfo(Directory.GetCurrentDirectory()));
if (root == null)
{
	Console.WriteLine($"Could not find root directory containing {Paket.References}");
	Console.WriteLine($"This tool must be run from a directory containing a {Paket.References} file, or a child directory");
	return 1;
}

var dependenciesFile = Path.Combine(root.FullName, Paket.Dependencies);
await File.WriteAllTextAsync(dependenciesFile, Paket.FormatDependencies(await File.ReadAllTextAsync(dependenciesFile)));

foreach (var file in root.GetFiles(Paket.References, SearchOption.AllDirectories))
{
	await File.WriteAllTextAsync(file.FullName, Paket.FormatReferences(await File.ReadAllTextAsync(file.FullName)));
	Console.WriteLine($"Formatted {file.FullName}");
}
return 0;

static DirectoryInfo? FindRootDirectory(DirectoryInfo current)
{
	while (current.Parent != null)
	{
		if (current.GetFiles(Paket.References).Length != 0)
			return current;

		current = current.Parent;
	}
	return null;
}
