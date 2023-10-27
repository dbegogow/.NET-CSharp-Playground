using CustomTestRunner.Framework;
using Shouldly;

namespace CustomTestRunner.Tests;

[Subject("Car")]
public class WhenCarIsStarted
{
    private Car car;

    Given Context = () =>
    {
        this.car = new Car();
        this.car.Produce("BMW");
    };

    Because Of = () => this.car.Start();

    It ShouldBeRunning = () => this.car.IsRunning.ShouldBe(true);

    It ShouldBeRunning = () => this.car.Model.ShouldBe("BMW");
}

[Subject("Car")]
public class WhenCarIsStopped
{
    private Car car;

    Given Context = () => this.car = new Car();

    Because Of = () => this.car.Stop();

    It ShouldBeRunning = () => this.car.IsRunning.ShouldBe(false);
}

