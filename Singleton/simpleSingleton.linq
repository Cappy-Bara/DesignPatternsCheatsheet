<Query Kind="Program" />

public class DbConnectionProvider
{
	public static DbConnectionProvider singleton;
	
	private DbConnectionProvider(string connectionString)
	{
		"Estabilishing connection...".Dump();
	}
	
	public static DbConnectionProvider GetConnector()
	{
		if(singleton is null)
		{
			"Getting connection string from config".Dump();
			string connectionString = "randomConnectionString";
			singleton = new DbConnectionProvider(connectionString);
		}
		
		return singleton;		
	}
	
	public void SaveInDb(object value)
	{
		"Serializing object to json...".Dump();
		"Writing object to db...".Dump();
	}
	
	public object GetFromDb(int id)
	{
		"Getting json from db".Dump();
		"Deserializing from json".Dump();
		return new object();
	}
}

void Main()
{
	var connector = DbConnectionProvider.GetConnector();
	var testValue = connector.GetFromDb(231);
	testValue.Dump();
	connector.SaveInDb(new {name = "Peter", age = 31});
}