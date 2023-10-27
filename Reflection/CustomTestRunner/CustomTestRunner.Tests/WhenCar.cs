using CustomTestRunner.Framework;
using Shouldly;

namespace CustomTestRunner.Tests;

[Subject("Car")]
public class WhenCarIsStarted
{
    private static Car car;

    Given Context = () =>
    {
        car = new Car();
        car.Produce("BMW");
    };

    Because Of = () => car.Start();

    It ShouldBeRunning = () => car.IsRunning.ShouldBe(true);

    It ShouldHaveCorrectModel = () => car.Model.ShouldBe("BMW");
}

[Subject("Car")]
public class WhenCarIsStopped
{
    private static Car car;

    Given Context = () => car = new Car();

    Because Of = () => car.Stop();

    It ShouldBeRunning = () => car.IsRunning.ShouldBe(false);
}

