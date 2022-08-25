<Query Kind="Program" />

public interface IObserver<T> //subscriber,listener
{		
	public void UpdateState(T data);
}

public interface IObservable<T> //publisher
{		

	public void Publish();
	
	public void Register(IObserver<T> observer);
	public void Unregister(IObserver<T> observer);
}

public class PromotionGenerator : IObservable<double>		//using generics as result for more flexibility. Observable can also return itself (for instance - when a lot of values needed).
{
	private readonly IList<IObserver<double>> subscribers = new List<IObserver<double>>();
	private double promotion = 0;
	private Random random = new Random();
	
	public void Register(IObserver<double> observer)
	{
		subscribers.Add(observer);
	}
	
	public void Unregister(IObserver<double> observer)
	{
		subscribers.Remove(observer);
	}
	
	public void Publish()
	{
		foreach(var subscriber in subscribers)
		{
			subscriber.UpdateState(promotion);
		}
	}
	
	public void ModifyState()
	{
		promotion = random.NextDouble();
	}
}


public class PromotionConsumer : IObserver<double>{
	private double promotionVal = 0;
	public string Name {get;}
	
	public PromotionConsumer(PromotionGenerator generator, string name){
		Name = name;
		generator.Register(this);
	}

	public void UpdateState(double data){
		promotionVal = data;
		$"{Name} promotion value has been updated to: {promotionVal}".Dump();
	}
}

void Main()
{
	var generator = new PromotionGenerator();
	var observer1 = new PromotionConsumer(generator,"Web app");				//consumers don't have to be same type - only interface implementation needed.
	var observer2 = new PromotionConsumer(generator,"Mobile app");
	
	for(int i = 0; i<5;i++)
	{
		generator.ModifyState();		//those methods can be in one method
		generator.Publish();
	}
}

