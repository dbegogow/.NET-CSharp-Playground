using Grpc.Core;

namespace gRPCServer.Services;

public class GreeterService : Greeter.GreeterBase
{
    private readonly List<GreetingRequest> requests = new List<GreetingRequest>();

    public override Task<StatusResponse> SaveGreeting(GreetingRequest request, ServerCallContext context)
    {
        var isNameExist = this.IsNameExist(request.Name);

        var statusResponse = new StatusResponse();

        if (isNameExist)
        {
            statusResponse.Summary = $"Error: {request.Name} already exists.";
        }
        else
        {
            this.requests.Add(request);
            statusResponse.Summary = $"Success: {request.Name} is added successfully.";
        }

        return Task.FromResult(statusResponse);
    }

    public override Task<GreetingReplies> GetGreetings(Empty request, ServerCallContext context)
    {
        var greetings = this.requests
            .Select(r => new GreetingReply
            {
                Message = $"Hello from {r.Country}. My name is {r.Name}."
            });

        var greetingsResponse = new GreetingReplies();
        greetingsResponse.Replies.AddRange(greetings);

        return Task.FromResult(greetingsResponse);
    }

    private bool IsNameExist(string name)
        => this.requests.Any(r => r.Name == name);
}
