// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var root = FindRootDirectory(new DirectoryInfo(Directory.GetCurrentDirectory()));
if (root == null)
{
	Console.WriteLine($"Could not find root directory containing {Paket.References}");
	return 1;
}

Paket.FormatDependencies(root);

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
