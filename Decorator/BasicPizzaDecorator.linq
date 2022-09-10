<Query Kind="Program" />

public abstract class Product //Class for product AND decorator
{
	public abstract decimal Price {get;}
	public abstract string Description {get;}
}

public class Pizza : Product	//Decoratee
{
	public override decimal Price {get;} = 19.99M;
	public override string Description {get;} = "Pizza with: tomato sauce, cheese";
}

public abstract class Ingredient : Product //Decorator base class (Can be ommited, if not a lot of decorators will be created);
{
	protected readonly Product pizza;
	
	public Ingredient(Product pizza)
	{
		this.pizza = pizza;
	}
}

public class Jalapeno : Ingredient		//Decorator
{
	public override decimal Price => pizza.Price + 0.5M;
	public override string Description => pizza.Description + ", jalapeno";
	
	public Jalapeno(Product pizza): base(pizza){}
}

public class Mushrooms : Ingredient //Decorator
{
	public override decimal Price => pizza.Price + 0.19M;
	public override string Description => pizza.Description + ", mushrooms";
	
	public Mushrooms(Product pizza): base(pizza){}
}

public class Ham : Ingredient //Decorator
{
	public override decimal Price => pizza.Price + 1.19M;
	public override string Description => pizza.Description + ", ham";
	
	public Ham(Product pizza): base(pizza){}
}

void Main()
{
	"SIMPLE PIZZA".Dump();
	var simplePizza = new Pizza();
	simplePizza.Price.Dump();
	simplePizza.Description.Dump();
	
	"\nMORE EXPENSIVE PIZZA".Dump();
	Product moreExpensivePizza = new Pizza();
	moreExpensivePizza = new Jalapeno(moreExpensivePizza);
	moreExpensivePizza.Price.Dump();
	moreExpensivePizza.Description.Dump();
	
	"\nTHE MOST EXPENSIVE PIZZA".Dump();
	Product totallyAwesomePizza = new Pizza();
	totallyAwesomePizza = new Jalapeno(totallyAwesomePizza);
	totallyAwesomePizza = new Mushrooms(totallyAwesomePizza);
	totallyAwesomePizza = new Ham(totallyAwesomePizza);
	totallyAwesomePizza.Price.Dump();
	totallyAwesomePizza.Description.Dump();
}