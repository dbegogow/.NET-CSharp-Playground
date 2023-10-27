namespace CustomTestRunner.Tests;

public class Car
{
    public string Model { get; private set; }

    public bool IsRunning { get; private set; }

    public void Produce(string model) => this.Model = model;

    public void Start() => this.IsRunning = true;

    public void Stop() => this.IsRunning = false;
}
