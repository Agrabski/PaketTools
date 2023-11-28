using PaketTools.Format;

namespace PaketTools.Tests;

public class FormatTests
{
	[Fact]
	public void PaketReferencesWithTwoGroupsAreSortedAlphabetically()
	{
		var content =
			"""
			System.Text.Json
			Castle.Core
			group A
			Newtonsoft.Json
			""";
		var expected =
			"""

			Castle.Core
			System.Text.Json
			group A
			Newtonsoft.Json
			""";
		Assert.Equal(expected, Paket.FormatReferences(content));
	}


	[Fact]
	public void PaketReferencesWithTwoNamedGroupsAreSortedAlphabetically()
	{
		var content =
			"""
			System.Text.Json
			group B
			Nlog
			Urbanite
			Castle.Core
			group A
			Newtonsoft.Json
			""";
		var expected =
			"""

			System.Text.Json
			group A
			Newtonsoft.Json
			group B
			Castle.Core
			Nlog
			Urbanite
			""";
		Assert.Equal(expected, Paket.FormatReferences(content));
	}

	[Fact]
	public void PaketReferenceContainingOnlyMainGroupIsSortedAlphabetically()
	{
		var content =
			"""
			System.Text.Json
			Castle.Core
			Newtonsoft.Json
			""";
		var expected =
			"""

			Castle.Core
			Newtonsoft.Json
			System.Text.Json
			""";
		Assert.Equal(expected, Paket.FormatReferences(content));
	}

	[Fact]
	public void PaketDependenciesAreSortedAlphabetically()
	{
		var content =
			"""
			framework: net5.0
			nuget System.Text.Json
			nuget Castle.Core
			nuget Newtonsoft.Json
			source https://api.nuget.org/v3/index.json
			""";
		var expected =
			"""
			framework: net5.0

			source https://api.nuget.org/v3/index.json
			
			nuget Castle.Core
			nuget Newtonsoft.Json
			nuget System.Text.Json

			""";
		var actual = Paket.FormatDependencies(content);
		Assert.Equal(expected, actual);
	}
}

