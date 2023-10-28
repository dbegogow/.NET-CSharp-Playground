namespace CustomTestRunner;

public class ConsoleReporter : ITestReporter
{
    public void Report(string message)
        => Console.Write(message);

    public void ReportLine(string message)
        => Console.WriteLine(message);
}
