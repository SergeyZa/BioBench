using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

namespace BioBench;

public class BaseClass
{
	virtual public string Name() => "Base";
}

public class Class : BaseClass
{
	public override string Name() => "Class";
}

public sealed class SealedClass : BaseClass
{
	public override string Name() => "Sealed";
}

public class Test
{
	BaseClass baseClass = new();
	Class rClass = new();
	SealedClass sealedClass = new();
	
	[Benchmark(Baseline = true)]
	public string BaseName()
	{
		return baseClass.Name();
	}

	[Benchmark]
	public string ClassName()
	{
		return rClass.Name();
	}

	[Benchmark]
	public string SealedName()
	{
		return sealedClass.Name();
	}
}

[RPlotExporter]
static public class Program
{

	static void Main()
	{
		var summary = BenchmarkRunner.Run(
			typeof(Test),
			ManualConfig
				.Create(DefaultConfig.Instance)
				.WithOption(ConfigOptions.JoinSummary, true));
	}
}
