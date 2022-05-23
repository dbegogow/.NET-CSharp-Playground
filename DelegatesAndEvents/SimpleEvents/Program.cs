namespace SimpleEvents;

public class Program
{
    public static void Main()
    {
        var person = new Person
        {
            Id = 1,
            Name = "Dzhulio",
            Health = 100
        };

        person.OnHealthChanged += PersonOnHealthChanged;
        person.OnHealthChanged += PersonIsDead;

        person.Health = 200;

        for (int i = 0; i < 10; i++)
        {
            person.Health -= 20;
        }
    }

    private static void PersonIsDead(object sender, int health)
    {
        var person = (Person)sender;
        if (person.Health <= 0)
        {
            Console.WriteLine($"{person.Name} is no longer alive");
        }
    }

    private static void PersonOnHealthChanged(object sender, int health)
    {
        var person = (Person)sender;
        Console.WriteLine($"{person.Name} has new health: {health}");
    }
}