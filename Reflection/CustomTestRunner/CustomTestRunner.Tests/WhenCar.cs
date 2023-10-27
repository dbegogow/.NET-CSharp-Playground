using CustomTestRunner.Framework;
using Shouldly;

namespace CustomTestRunner.Tests;

[Subject("Car")]
public class WhenCarIsStarted
{
    const string Model = "BMW";

    static Car car;

    Given Context = () =>
    {
        car = new Car();
        car.Produce(Model);
    };

    Because Of = () => car.Start();

    It ShouldBeRunning = () => car.IsRunning.ShouldBe(true);

    It ShouldHaveCorrectModel = () => car.Model.ShouldBe(Model);
}

[Subject("Car")]
public class WhenCarIsStopped
{
    static Car car;

    Given Context = () => car = new Car();

    Because Of = () => car.Stop();

    It ShouldBeRunning = () => car.IsRunning.ShouldBe(false);
}

