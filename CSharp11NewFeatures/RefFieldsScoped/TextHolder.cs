using System.Buffers;

namespace RefFieldsScoped;

public ref struct TextHolder
{
    private readonly Span<char> _chars;
    private int _pos;

    public TextHolder(int size)
    {
        this._chars = ArrayPool<char>.Shared.Rent(size);
        this._pos = 0;
    }

    public void Append(scoped ReadOnlySpan<char> value)
    {
        if (value.TryCopyTo(this._chars.Slice(this._pos)))
        {
            this._pos += value.Length;
        }
    }

    public override string ToString() => new(Text);

    private ReadOnlySpan<char> Text => this._chars[..this._pos];
}
