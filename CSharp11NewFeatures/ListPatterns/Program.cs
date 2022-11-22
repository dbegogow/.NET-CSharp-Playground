int[] numbers = { 1, 2, 3 };

Console.WriteLine(numbers is [1, 2, 3]);
Console.WriteLine(numbers is [1, 2, 4]);
Console.WriteLine(numbers is [1, 2, 3, 4]);

Console.WriteLine(new string('-', 60));

Console.WriteLine(numbers is [0 or 1, <= 2, >= 3]);

Console.WriteLine(new string('-', 60));

//if (numbers is [var first, _, _])
//{
//    Console.WriteLine(first);
//}

if (numbers is [var first, .. var rest])
{
    Console.WriteLine(first);
}

Console.WriteLine(new string('-', 60));

var emptyName = Array.Empty<string>();
var myName = new[] { "Dzhulio Begogov" };
var myNameBrokenDown = new[] { "Dzhulio", "Begogov" };
var myNameBrokenDownFurther = new[] { "Dzhulio", "Andon", "Begogov" };
var otherNameBrokenDownFurther = new[] { "Ivana", "Tomova", "Tagareva" };

var text = otherNameBrokenDownFurther switch
{
    [] => "Name was empty",
    [var fullName] => $"My first name is: {fullName}",
    [var firstName, var lastName] => $"My first and last names are: {firstName} {lastName}",
    ["Dzhulio", "Andon", "Begogov"] => $"This array contains my full name",
    ["Dzhuliano", "Andon", "Begogov"] => $"This array not contains my full name",
    _ => "Nothing was matched"
};

Console.WriteLine(text);
