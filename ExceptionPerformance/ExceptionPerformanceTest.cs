using NBench;
using System;

/// <summary>
/// Test to see if we can achieve max throughput on a <see cref="AtomicCounter"/>
/// </summary>
public class ExceptionPerformanceTest
{
    const string number = "123devcom";
    const int iterations = 100000;
    private Counter counter;

    [PerfSetup]
    public void Setup(BenchmarkContext context)
    {
        counter = context.GetCounter("TestCounter");
    }

    [PerfBenchmark(Description = "Test number validation with Exceptions.", NumberOfIterations = 3, RunMode = RunMode.Iterations, TestMode = TestMode.Measurement)]
    [CounterMeasurement("TestCounter")]
    [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
    [GcMeasurement(GcMetric.TotalCollections, GcGeneration.AllGc)]
    public void BenchmarkValidateNumberWithException()
    {
        for (int i = 0; i < iterations; i++)
        {
            ValidateNumberWithException(number);
            counter.Increment();
        }
    }

    [PerfBenchmark(Description = "Test number validation without Exceptions.", NumberOfIterations = 3, RunMode = RunMode.Iterations, TestMode = TestMode.Measurement)]
    [CounterMeasurement("TestCounter")]
    [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
    [GcMeasurement(GcMetric.TotalCollections, GcGeneration.AllGc)]
    public void BenchmarkValidateNumberWithoutException()
    {
        for (int i = 0; i < iterations; i++)
        {
            ValidateNumberWithoutException(number);
            counter.Increment();
        }
    }

    private static bool ValidateNumberWithException(string number)
    {
        try
        {
            int.Parse(number);
        }
        catch (FormatException)
        {
            return false;
        }

        return true;
    }

    private static bool ValidateNumberWithoutException(string number)
    {
        int value;
        return int.TryParse(number, out value);
    }
}