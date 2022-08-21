<Query Kind="Program" />

//Flying Interface
public interface IFlyingStrategy
{
	public void Fly(Duck duck);
}
//Flying Strategies

public class HasWingsStrategy : IFlyingStrategy 
{
	public void Fly(Duck duck)
	{
		$"Majestic, {duck.Color} duck flies over our heads.".Dump();
	}
}

public class NoWingsStrategy : IFlyingStrategy 
{
	//Have to pass duck, even if it's not used in this strategy.
	public void Fly(Duck duck)
	{
		"I can't go up, unfortunately.".Dump();
	}
}

public class Duck	//strategy context
{
	public Duck(string color)
	{
		Color = color;
	}

	public string Color {get; init;}

	internal IFlyingStrategy flyingStrategy;

	public void StartFlying(){
		flyingStrategy.Fly(this);
	}
}

public class WoodenDuck : Duck{
	public WoodenDuck(string color) : base(color)
	{
		flyingStrategy = new NoWingsStrategy();
	}
}

public class WildDuck : Duck{
	public WildDuck(string color) : base(color)
	{
		flyingStrategy = new HasWingsStrategy();
	}
}

void Main()
{
	Duck duck = new WildDuck("brown");
	Duck gustaw = new WoodenDuck("wooden");
	
	gustaw.StartFlying();
	duck.StartFlying();
}
