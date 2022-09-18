<Query Kind="Program">
  <IncludeUncapsulator>false</IncludeUncapsulator>
</Query>

public class WeatherData		
{
	public float Temperature {get;}
	public int Humidity {get;}
	
	public WeatherData(float temperature, int humidity)
	{
		Temperature = temperature;
		Humidity = humidity;
	}
}

public abstract class IWeatherDataProvider
{
	protected readonly int cityHash;
	public abstract WeatherData GetData();
	
	public IWeatherDataProvider(int cityHash)
	{
		this.cityHash = cityHash;
	}
}


public class APIWeatherDataProvider : IWeatherDataProvider 			//CONCRETE PRODUCT
{
	public override WeatherData GetData()
	{
		"MAKING API CALL FOR DATA USING CITY HASH...".Dump();
		return new WeatherData(3.21f,74);			//some random data from 'api'
	}
	
	public APIWeatherDataProvider(int cityHash) : base(cityHash){}
}

public class SensorWeatherDataProvider : IWeatherDataProvider 		//CONCRETE PRODUCT
{
	public override WeatherData GetData()
	{
		"GETTING VALUES FROM THE SENSORS USING CITY HASH...".Dump();
		return new WeatherData(7.12f,66);							//some random data from 'sensor'
	}
	
	public SensorWeatherDataProvider (int cityHash) : base(cityHash){}
}


public abstract class WeatherDataProviderCreator	//Creator INTERFACE. It doesn't have to create the provider during every call.
{
	protected abstract IWeatherDataProvider CreateProvider(int hash);
	
	public IWeatherDataProvider GetWeatherDataProvider(string cityName)
	{
		"========SHARED LOGICS========".Dump();
		"Validating city name...".Dump();
		"Getting city location hash...".Dump();
		int hash = cityName.Length;
		"=====END OF SHARED LOGICS=====".Dump();
		
		var provider = CreateProvider(hash);
		return provider;
	}
}


public class APIWeatherDataProviderCreator : WeatherDataProviderCreator	//Concrete Creator
{
	protected override IWeatherDataProvider CreateProvider(int hash)
	{
		return new APIWeatherDataProvider(hash);
	}
}

public class SensorWeatherDataProviderCreator : WeatherDataProviderCreator //Concrete Creator
{
	protected override IWeatherDataProvider CreateProvider(int hash)
	{
		return new SensorWeatherDataProvider(hash);
	}
}


void Main()
{
	"Get data from API".Dump();
	var apiProvider = new APIWeatherDataProviderCreator().GetWeatherDataProvider("Cracow");
	apiProvider.GetData().Dump();
	
	"\nGet data from Sensors".Dump();
	var sensorProvider = new SensorWeatherDataProviderCreator().GetWeatherDataProvider("Cracow");
	sensorProvider.GetData().Dump();
}