<Query Kind="Program">
  <IncludeUncapsulator>false</IncludeUncapsulator>
</Query>

//Not great example, because we don't really see the product. PROVIDER is the product

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

public interface IWeatherDataProvider
{
	public WeatherData GetData(int LocationHash);
}


public class APIWeatherDataProvider : IWeatherDataProvider 			//CONCRETE PRODUCT
{
	public WeatherData GetData(int LocationHash)
	{
		"MAKING API CALL FOR DATA...".Dump();
		return new WeatherData(3.21f,74);
	}
}

public class SensorWeatherDataProvider : IWeatherDataProvider 		//CONCRETE PRODUCT
{
	public WeatherData GetData(int LocationHash)
	{
		"GETTING VALUES FROM THE SENSORS...".Dump();
		return new WeatherData(7.12f,66);
	}
}


public abstract class WeatherDataGetter	//Creator INTERFACE. It doesn't have to create the provider during every call.
{
	protected abstract IWeatherDataProvider CreateProvider();
	
	public WeatherData GetWeatherData(string cityName)
	{
		"========SHARED LOGICS========".Dump();
		"Validating city name...".Dump();
		"Getting city location hash...".Dump();
		int hash = cityName.Length;
		"====END OF SHARED LOGICS====".Dump();
		
		var provider = CreateProvider();
		return provider.GetData(hash);
	}
}


public class APIWeatherDataGetter : WeatherDataGetter	//Concrete Creator
{
	protected override IWeatherDataProvider CreateProvider()
	{
		return new APIWeatherDataProvider();
	}
}

public class SensorWeatherDataGetter : WeatherDataGetter //Concrete Creator
{
	protected override IWeatherDataProvider CreateProvider()
	{
		return new SensorWeatherDataProvider();
	}
}


void Main()
{
	"Get data from API".Dump();
	new APIWeatherDataGetter().GetWeatherData("Cracow").Dump();
	
	"Get data from Sensors".Dump();
	new SensorWeatherDataGetter().GetWeatherData("Cracow").Dump();
}