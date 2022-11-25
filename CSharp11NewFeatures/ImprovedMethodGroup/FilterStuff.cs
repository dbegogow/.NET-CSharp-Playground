namespace ImprovedMethodGroup;

public class FilterStuff
{
    private static readonly List<int> Ages = Enumerable.Range(0, 100).ToList();

    public int Sum()
        => Ages.Where(x => Filter(x)).Sum();

    public int SumMethodGroup()
        => Ages.Where(Filter).Sum();

    static bool Filter(int age)
        => age > 50;
}
