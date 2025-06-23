namespace pizzabesteller.Composition
{
    public abstract class CMenuComponent
    {
        public virtual void Add(CMenuComponent menuComponent)
        {
        }

        public virtual void Remove(CMenuComponent menuComponent)
        {
        }

        public virtual string GetName()
        {
            return string.Empty;
        }

        public virtual string GetDescription()
        {
            return string.Empty;
        }

        public virtual decimal GetPrice()
        {
            return 0;
        }

        public virtual string PrintInfo()
        {
            return string.Empty;
        }
    }
}