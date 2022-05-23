namespace SimpleEvents
{
    public class Person
    {
        private int health;

        public int Id { get; set; }

        public string Name { get; set; }

        public int Health
        {
            get => this.health;
            set
            {
                this.health = value;
                this.OnHealthChanged?.Invoke(this, this.health);
            }
        }

        public event EventHandler<int> OnHealthChanged;
    }
}
