namespace pizzabesteller.Composition
{
    internal class Product : CMenuComponent
    {
        private string name;
        private string description;
        private decimal price;
        private List<string> ingredients;

        public Product(string name, string description, decimal price, List<string> ingredients)
        {
            this.name = name;
            this.description = description;
            this.price = price;
            this.ingredients = ingredients;
        }

        public override string GetName() => name;

        public override string GetDescription() => description;

        public override decimal GetPrice() => price;

        public override string PrintInfo()
        {
            return ($"Pizza: {GetName()}");
            //Console.WriteLine($"Pizza: {GetName()}");
            //Console.WriteLine($"Description: {GetDescription()}");
            //Console.WriteLine($"Price: {GetPrice():C}");
            //Console.WriteLine("Ingredients: " + string.Join(", ", ingredients));
        }
    }
}