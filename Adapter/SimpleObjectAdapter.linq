<Query Kind="Program" />

public interface ILocationProvider {
	public uint GetLocationHash();
}

public class OldLocationProvider : ILocationProvider
{
	private string postCode;
	
	public OldLocationProvider(string postCode)
	{
		this.postCode = postCode;
	}
	
	public uint GetLocationHash()
	{
		var random = new Random(postCode.Length);
		"Getting x and y position from post code...".Dump();
		var x = random.Next();
		var y = random.Next();
		"Calculating hash...".Dump();
		return (uint)(x*y);
	}
}

public class NewLocationProvider	//outsourced class
{
	private float posX;
	private float posY;
	
	public NewLocationProvider(float x, float y)
	{
		posX = x;
		posY = y;
	}
	
	public (float,float) GetLocation()
	{
		"Returning position from new location provider".Dump();
		return (posX,posY);
	}
}


public class LocationProviderAdapter : ILocationProvider
{
	private NewLocationProvider provider;

	public LocationProviderAdapter(NewLocationProvider provider)
	{
		this.provider = provider;
	}

	public uint GetLocationHash()
	{
		var location = provider.GetLocation();
		return (uint)(location.Item1*location.Item2);
	}
}



static void Main()
{
	var provider = new OldLocationProvider("301-24");
	provider.GetLocationHash().Dump();
	
	var newProvider = new NewLocationProvider(3176544.145f,263144.122f);
	newProvider.GetLocation().Dump();
	var adapter = new LocationProviderAdapter(newProvider);
	
	GetWeather(provider).Dump();
	GetWeather(adapter).Dump();
}
	
public static string GetWeather(ILocationProvider locationProvider)
{
	var locationHash = locationProvider.GetLocationHash();

	if(locationHash < 2768315232)
	{
		return "It's sunny";
	}
	
	return "It's cold";
}