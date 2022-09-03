using System.Collections;

namespace LinkedListReversal;

public class MyLinkedList<T> : IEnumerable<T>
{
    private Node<T> _head;

    public void Add(T value)
    {
        if (this._head == null)
        {
            this._head = new Node<T>(value);
            return;
        }

        var currentNode = this._head;

        while (currentNode.Next != null)
        {
            currentNode = currentNode.Next;
        }

        currentNode.Next = new Node<T>(value);
    }

    public void Reverse()
        => this._head = this.Reverse(this._head);

    public IEnumerator<T> GetEnumerator()
    {
        var currentNode = this._head;

        while (currentNode != null)
        {
            yield return currentNode.Value;

            currentNode = currentNode.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
        => this.GetEnumerator();

    private Node<T> Reverse(Node<T> node)
    {
        if (node.Next == null)
        {
            return node;
        }

        var reversedNode = this.Reverse(node.Next);

        node.Next.Next = node;
        node.Next = null;

        return reversedNode;
    }
}
