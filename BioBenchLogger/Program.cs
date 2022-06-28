using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BioBenchLogger;

public class TestLogger
{
	ILogger<TestLogger> _logger;

	[GlobalSetup]
	public void Init()
	{
		IHost host = Host.CreateDefaultBuilder(Array.Empty<string>())
			.ConfigureLogging(logging =>
				logging.SetMinimumLevel(LogLevel.Information))
			.Build();

		_logger = host.Services.GetRequiredService<ILogger<TestLogger>>();
	}

	[Benchmark(Baseline = true)]
	public void LoggerInformation()
	{
		_logger.LogInformation("some info {data}", 17);
	}

	[Benchmark]
	public void LoggerDebug()
	{
		_logger.LogDebug("some info {data}", 17);
	}

	[Benchmark]
	public void LoggerDebugCheck()
	{
		if (_logger.IsEnabled(LogLevel.Debug))
			_logger.LogDebug("some info {data}", 17);
	}
}

internal class Program
{
	static void Main(string[] args)
	{
		var summary = BenchmarkRunner.Run(
			typeof(TestLogger),
			ManualConfig
				.Create(DefaultConfig.Instance)
				.WithOption(ConfigOptions.JoinSummary, true));
	}
}

public sealed class LoggerAdapter<T>
{
	ILogger<T> _logger;

	public LoggerAdapter(ILogger<T> logger)
	{
		_logger = logger;
	}

	public void LogDebug<T0>(string? message, T0 p0)
	{
			if (_logger.IsEnabled(LogLevel.Debug))
			_logger.LogDebug(message, p0);
	}
	public void LogDebug<T0, T1>(string? message, T0 p0, T1 p1) => _logger.LogDebug(message, p0, p1);
	public void LogDebug<T0, T1, T2>(string? message, T0 p0, T1 p1, T2 p2) => _logger.LogDebug(message, p0, p1, p2);
}
