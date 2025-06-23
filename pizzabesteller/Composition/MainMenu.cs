namespace pizzabesteller.Composition
{
    public class MainMenu : CMenuComponent
    {
        private List<CMenuComponent> menuComponents = new List<CMenuComponent>();
        private string name;

        public MainMenu(string name)
        {
            this.name = name;
        }

        public override void Add(CMenuComponent menuComponent)
        {
            menuComponents.Add(menuComponent);
        }

        public override void Remove(CMenuComponent menuComponent)
        {
            menuComponents.Remove(menuComponent);
        }

        public override string PrintInfo()
        {
            Console.WriteLine($"Menu: {name}");
            Console.WriteLine("---------------------");
            foreach (CMenuComponent component in menuComponents)
            {
                Console.WriteLine(component.PrintInfo());
            }
            return "done";
        }
    }
}