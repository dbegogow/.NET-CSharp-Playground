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

    Because Of = () => this.car.Start();

    It ShouldBeRunning = () => this.car.IsRunning.ShouldBe(true);

    It ShouldHaveCorrectModel = () => this.car.Model.ShouldBe("BMW");
}

[Subject("Car")]
public class WhenCarIsStopped
{
    private static Car car;

    Given Context = () => car = new Car();

    Because Of = () => car.Stop();

    It ShouldBeRunning = () => car.IsRunning.ShouldBe(false);
}

