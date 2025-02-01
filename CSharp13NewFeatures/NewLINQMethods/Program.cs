// ---------------------------CountBy---------------------------

(string firstName, string lastName)[] people =
[
   ("John", "Doe"),
   ("Jane", "Peterson"),
   ("John", "Smith"),
   ("Mary", "Johnson"),
   ("Nick", "Carson"),
   ("Mary", "Morgan")
];

// Before
var firstNameCounts = people
   .GroupBy(p => p.firstName)
   .ToDictionary(group => group.Key, group => group.Count())
   .AsEnumerable();

// .NET 9
firstNameCounts = people
   .CountBy(p => p.firstName);

foreach (var entry in firstNameCounts)
{
    Console.WriteLine($"First Name {entry.Key} appears {entry.Value} times");
}

// ---------------------------AggregateBy---------------------------

(string name, string department, int vacationDaysLeft)[] employees =
[
   ("John Doe", "IT", 12),
   ("Jane Peterson", "Marketing", 18),
   ("John Smith", "IT", 28),
   ("Mary Johnson", "HR", 17),
   ("Nick Carson", "Marketing", 5),
   ("Mary Morgan", "HR", 9)
];

// Before
var departmentVacationDaysLeft = employees
   .GroupBy(emp => emp.department)
   .ToDictionary(group => group.Key, group => group.Sum(emp => emp.vacationDaysLeft))
   .AsEnumerable();

// .NET 9
departmentVacationDaysLeft = employees
   .AggregateBy(emp => emp.department, 0, (acc, emp) => acc + emp.vacationDaysLeft);

foreach (var entry in departmentVacationDaysLeft)
    Console.WriteLine($"Department {entry.Key} has a total of {entry.Value} vacation days left.");

// ---------------------------Index---------------------------

var managers = new[]
{
   "John Doe",
   "Jane Peterson",
   "John Smith"
};

// Before
foreach (var (index, manager) in managers.Select((m, i) => (i, m)))
    Console.WriteLine($"Manager {index}: {manager}");

// .NET 9
foreach (var (index, manager) in managers.Index())
    Console.WriteLine($"Manager {index}: {manager}");