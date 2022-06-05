using RefitCommon;

using Refit;

namespace RefitClient;

public interface IPersonService
{
    [Get("/api/person")]
    Task GetAll();

    [Post("/api/person")]
    Task<IEnumerable<Person>> AddPerson([Body] Person person);
}
