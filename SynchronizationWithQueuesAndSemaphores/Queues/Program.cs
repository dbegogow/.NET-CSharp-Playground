using System.Threading.Channels;

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

                await context._driver.Send(
                    new Driver.Alert(
                        danger[Random.Shared.Next(0, danger.Length)],
                        Random.Shared.Next(3, 7)));

                await Task.Delay(TimeSpan.FromSeconds(Random.Shared.Next(1, 5)), context._cancelationToken);
            }
        }
    }

    class Driver(DrivingToDisneyLand context)
    {
        private Channel<IMessage> _channel = Channel.CreateUnbounded<IMessage>();

        public async Task Listen()
        {
            while (!context._cancelationToken.IsCancellationRequested)
            {
                var msg = await _channel.Reader.ReadAsync();

                if (msg is Alert alert)
                {
                    await ProcessAlert(alert);
                }
                else if (msg is AskQuestion askQuestion)
                {
                    await ProcessAskQuestion(askQuestion);
                }
            }
        }

        public ValueTask Send<T>(T msg) where T : IMessage
            => _channel.Writer.WriteAsync(msg);

        public interface IMessage { }
        public abstract record AwaitableMessage<TReturn>
        {
            private TaskCompletionSource<TReturn> _resolve = new();

            public Task<TReturn> Result() => _resolve.Task;

            public void Complete(TReturn result) => _resolve.SetResult(result);
        }

        public record Alert(string Danger, int DurationSeconds) : IMessage;

        public record AskQuestion(string Question) : AwaitableMessage<string>, IMessage;

        private async Task ProcessAlert(Alert alert)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Paying attention to {alert.Danger}");

            await Task.Delay(TimeSpan.FromSeconds(alert.DurationSeconds), context._cancelationToken);

            Console.WriteLine($"Finished paying attention to {alert.Danger}");
        }

        private async Task ProcessAskQuestion(AskQuestion question)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Processing \"{question.Question}\"");

            await Task.Delay(TimeSpan.FromSeconds(Random.Shared.Next(1, 3)), context._cancelationToken);

            Console.WriteLine($"Answering {question.Question}");

            question.Complete("Pffft");
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

                var msg = new Driver.AskQuestion(
                    questions[Random.Shared.Next(0, questions.Length)]);

                await context._driver.Send(msg);

                var answer = await msg.Result();

                await Task.Delay(TimeSpan.FromSeconds(Random.Shared.Next(1, 5)), context._cancelationToken);
            }
        }
    }

    public Task Simulate()
        => Task.WhenAll(
            this._road.Drive(),
            this._driver.Listen(),
            this._passenger.BeBored());
}