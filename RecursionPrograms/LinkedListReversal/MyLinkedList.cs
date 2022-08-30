namespace LinkedListReversal;

public class MyLinkedList<T>
{
    private Node<T> _startNode;

    public void Add(T value)
    {
        if (this._startNode == null)
        {
            this._startNode = new Node<T>(value);
            return;
        }

        var currentNode = this._startNode;

        while (currentNode.Next != null)
        {
            currentNode = currentNode.Next;
        }

        currentNode.Next = new Node<T>(value);
    }
}
