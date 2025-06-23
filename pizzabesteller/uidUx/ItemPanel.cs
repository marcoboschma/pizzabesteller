using pizzabesteller.uidUx;

namespace pizzabesteller.uidUx
{
}

public class MsgParser
{
    public string ParseMsg(MenuPanel menuPanel, string message)
    {
        if (message.Contains("your order is processed"))
        {
            return message;
        }
        else
        {
            menuPanel.LoadMenuItems(new MenuParser().ParseToMenu(message));
        }
        return "";
    }
}

public class MenuParser
{
    public List<MenuItemModel> ParseToMenu(string Msg)
    {
        List<MenuItemModel> menuItemModels = new List<MenuItemModel>();

        List<string> list = new List<string>();

        list.AddRange(Msg.Split('\n'));

        for (int i = 0; i < list.Count(); i += 2)
        {
            if (i + 1 < list.Count) // Ensure there's a next element
            {
                MenuItemModel itemModel = new MenuItemModel(list[i]);
                Console.WriteLine(list[i + 1] + "-= 1");
                itemModel.BaseIngredients = new List<string>();
                itemModel.BaseIngredients.AddRange(list[i + 1].Split(","));
                menuItemModels.Add(itemModel);
            }
        }
        return menuItemModels;
    }

    public List<string> GetAllIngrediants(List<MenuItemModel> menuItems)
    {
        List<string> extraOptions = new List<string>();
        foreach (MenuItemModel menuItem in menuItems)
        {
            foreach (string ingrediant in menuItem.BaseIngredients)
            {
                if (!extraOptions.Contains(ingrediant))
                {
                    extraOptions.Add(ingrediant);
                }
            }
        }
        return extraOptions;
    }
}

public class MenuItemModel
{
    public string Name
    {
        get; set;
    }

    public List<string> BaseIngredients { get; set; } = new List<string>();
    public List<string> ExtraIngredients { get; set; } = new List<string>();
    public int Quantity { get; set; } = 0;

    //overload
    public MenuItemModel(string name, params string[] baseIngredients)
    {
        Name = name;
        BaseIngredients.AddRange(baseIngredients);
    }

    public void AddExtraIngredient(string ingredient)
    {
        ExtraIngredients.Add(ingredient);
    }

    public void AddToBasket()
    {
        this.Quantity++;
    }

    public void RemoveFromBasket()
    {
        this.Quantity = Quantity > 0 ? Quantity-- : 0;
    }

    public string printAllextraToppingsAndCount()
    {
        string toppings = "";

        toppings += ExtraIngredients.Count + "\n";

        if (ExtraIngredients.Count > 0)
        {
            foreach (var topping in ExtraIngredients)
            {
                toppings += topping.ToString() + "\n";
            }
        }
        else
        {
            return toppings;
        }
        return toppings;
    }

    public override string ToString()
    {
        return $"{Name} (x{Quantity})";
    }

    public string GetSandersEisen()
    {
        return $"{Name}\n" + $"{Quantity}\n" + $"{printAllextraToppingsAndCount()}";
    }

    public string GetFullDescription()
    {
        var baseList = BaseIngredients.Count > 0 ? string.Join(", ", BaseIngredients) : "None";
        var extraList = ExtraIngredients.Count > 0 ? string.Join(", ", ExtraIngredients) : "None";

        return $"{Name}\n" +
               $"Base: {baseList}\n" +
               $"Extras: {extraList}\n" +
               $"Quantity: {Quantity}";
    }
}