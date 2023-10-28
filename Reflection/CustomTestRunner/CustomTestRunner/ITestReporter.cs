namespace CustomTestRunner;

public interface ITestReporter
{
    void Report(string message = null);

    void ReportLine(string message = null);
}
