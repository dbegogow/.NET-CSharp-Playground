namespace LinkedListReversal;

public class Node<T>
{
    public Node(T value)
        => this.Value = value;

    public T Value { get; init; }

    public Node<T> Next { get; set; }
}
