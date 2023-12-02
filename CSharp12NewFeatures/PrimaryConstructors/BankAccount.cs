namespace PrimaryConstructors;

public class BankAccount(string accountId, string owner)
{
    public string AccountID { get; } = ValidAccountNumber(accountId)
        ? accountId
        : throw new ArgumentException("Invalid account number", nameof(accountId));

    public string Owner { get; } = string.IsNullOrWhiteSpace(owner)
        ? throw new ArgumentException("Owner name cannot be empty", nameof(owner))
        : owner;

    public override string ToString() => $"Account ID: {AccountID}, Owner: {Owner}";

    public static bool ValidAccountNumber(string accountID) =>
    accountID?.Length == 10 && accountID.All(c => char.IsDigit(c));
}
