<Query Kind="Program" />

//Swimming Interface
public interface ISwimmingStrategy
{
	public void Swim();
}

//Swimming strategies

public class FloatingStrategy : ISwimmingStrategy 
{
	public void Swim()
	{
		"I'm floating!".Dump();
	}
}

public class DrowningStrategy : ISwimmingStrategy 
{
	public void Swim()
	{
		"Help! I'm drowning!".Dump();
	}
}

//Flying Interface
public interface IFlyingStrategy
{
	public void Fly();
}
//Flying Strategies

public class HasWingsStrategy : IFlyingStrategy 
{
	public void Fly()
	{
		"I'm flying because I have wings!".Dump();
	}
}

public class NoWingsStrategy : IFlyingStrategy 
{
	public void Fly()
	{
		"I can't go up, unfortunately.".Dump();
	}
}



public class Duck		//strategy context
{
	internal IFlyingStrategy flyingStrategy;
	internal ISwimmingStrategy swimmingStrategy;
	
	public void StartSwimming(){
		swimmingStrategy.Swim();
	}

	public void StartFlying(){
		flyingStrategy.Fly();
	}
}


public class WoodenDuck : Duck{
	public WoodenDuck()
	{
		flyingStrategy = new NoWingsStrategy();
		swimmingStrategy = new DrowningStrategy();
	}
}

public class WildDuck : Duck{
	public WildDuck()
	{
		flyingStrategy = new HasWingsStrategy();
		swimmingStrategy = new FloatingStrategy();
	}
}

void Main()
{
	Duck duck = new WildDuck();
	Duck gustaw = new WoodenDuck();
	
	
	gustaw.StartSwimming();
	gustaw.StartFlying();
	duck.StartSwimming();
	duck.StartFlying();
}
