var cts = new CancellationTokenSource();
Console.CancelKeyPress += (_, _) => { cts.Cancel(); };

var simulation = new DrivingToDisneyLand(cts.Token);
await simulation.Simulate();

public class DrivingToDisneyLand
{
    private readonly CancellationToken _cancelationToken;
    private readonly Road _road;
    private readonly Driver _driver;
    private readonly Passenger _passenger;

    public DrivingToDisneyLand(CancellationToken cancellationToken)
    {
        this._cancelationToken = cancellationToken;
        this._driver = new Driver(this);
        this._road = new Road(this);
        this._passenger = new Passenger(this);
    }

    class Road(DrivingToDisneyLand context)
    {
        public async Task Drive()
        {
            while (!context._cancelationToken.IsCancellationRequested)
            {
                var danger = new[]
                {
                    "passing truck",
                    "cat on the road",
                    "sun shining in the eye",
                    "drunk driver",
                    "ambulance"
                };

                await context._driver.Alert(
                    danger[Random.Shared.Next(0, danger.Length)],
                    Random.Shared.Next(3, 7));

                await Task.Delay(TimeSpan.FromSeconds(Random.Shared.Next(1, 5)), context._cancelationToken);
            }
        }
    }

    class Driver(DrivingToDisneyLand context)
    {
        public async Task Alert(string danger, int durationSeconds)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Paying attention to {danger}");

            await Task.Delay(TimeSpan.FromSeconds(durationSeconds), context._cancelationToken);

            Console.WriteLine($"Finished paying attention to {danger}");
        }

        public async Task<string> AskQuestion(string question)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Processing \"{question}\"");

            await Task.Delay(TimeSpan.FromSeconds(Random.Shared.Next(1, 3)), context._cancelationToken);

            Console.WriteLine($"Answering {question}");

            return "Pffft";
        }
    }

    class Passenger(DrivingToDisneyLand context)
    {
        public async Task BeBored()
        {
            while (!context._cancelationToken.IsCancellationRequested)
            {
                var questions = new[]
                {
                    "are we there yet?",
                    "can we stop at McDonalds?",
                    "I'm hungry",
                    "I'm bored"
                };

                var answer = await context._driver.AskQuestion(
                    questions[Random.Shared.Next(0, questions.Length)]);

                await Task.Delay(TimeSpan.FromSeconds(Random.Shared.Next(1, 5)), context._cancelationToken);
            }
        }
    }

    public Task Simulate() => Task.WhenAll(this._road.Drive(), this._passenger.BeBored());
}