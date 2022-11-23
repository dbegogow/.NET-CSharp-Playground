using System.Text;

ReadOnlySpan<byte> text = "Dzhulio Begogov"u8;

ReadOnlySpan<byte> u16A = Encoding.Unicode.GetBytes("A");
ReadOnlySpan<byte> u8A = "A"u8;