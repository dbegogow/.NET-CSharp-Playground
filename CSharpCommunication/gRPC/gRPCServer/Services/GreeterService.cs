using Grpc.Core;

namespace gRPCServer.Services;

public class GreeterService : Greeter.GreeterBase
{
    private readonly HashSet<GreetingRequest> requests;

    public GreeterService()
    {
        this.requests = new HashSet<GreetingRequest>();
    }

    public override Task<StatusResponse> SaveGreeting(GreetingRequest request, ServerCallContext context)
    {
        var isSuccessful = this.requests.Add(request); ;

        var statusResponse = new StatusResponse();

        if (isSuccessful)
        {
            statusResponse.Summary = "Error: This name already exists.";
        }

        statusResponse.Summary = "Success: Name is added successfully.";

        return Task.FromResult(statusResponse);
    }

    public override Task<IEnumerable<GreetingReply>> GetGreetings(Empty request, ServerCallContext context)
        => Task.FromResult(this.requests.Select(r => new GreetingReply
        {
            Message = $"Hello from {r.Country}. My name is {r.Name}"
        }));
}
