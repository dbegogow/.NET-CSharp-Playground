namespace PrimaryConstructors;

public class SavingsAccount(string accountID, string owner, decimal interestRate)
    : BankAccount(accountID, owner)
{
    public SavingsAccount() : this("default", "default", 0.01m)
    {
    }

    public decimal CurrentBalance { get; private set; } = 0;

    public void Deposit(decimal amount)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Deposit amount must be positive");
        }

        CurrentBalance += amount;
    }

    public void Withdrawal(decimal amount)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Withdrawal amount must be positive");
        }

        if (CurrentBalance - amount < 0)
        {
            throw new InvalidOperationException("Insufficient funds for withdrawal");
        }

        CurrentBalance -= amount;
    }

    public void ApplyInterest()
        => CurrentBalance *= 1 + interestRate;

    public override string ToString() => $"Account ID: {accountID}, Owner: {owner}, Balance: {CurrentBalance}";
}
