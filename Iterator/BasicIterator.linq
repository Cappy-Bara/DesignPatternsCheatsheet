<Query Kind="Program">
  <Namespace>System.Net</Namespace>
  <Namespace>System.Security.Principal</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

    public interface IMyIterator
    {
        public string GetNext();
        public bool HasNext();
    }

    public class SomeCollectionIterator : IMyIterator
    {
        private int _currentNumber = 0;
        private SomeCollection _collection = null;
        public SomeCollectionIterator(SomeCollection col)
        {
            _collection = col;
        }

    public string GetNext()
    {
        var type = typeof(SomeCollection);

        _currentNumber++;
        var prop = type.GetProperty($"Prop{_currentNumber}");

        if (prop is null)
            throw new Exception("Collection boundaries exceeded.");

        return prop.GetValue(_collection).ToString();
    }
    public bool HasNext()
    {
        var type = typeof(SomeCollection);

        var prop = type.GetProperty($"Prop{_currentNumber+1}");
        return prop != null;
    }
}
public class DifferentCollectionIterator : IMyIterator
{
    private int _currentNumber = 0;
    private string[] _collection = null;
    public DifferentCollectionIterator(string[] col)
    {
        _collection = col;
    }

    public string GetNext()
    {
        var value = _collection[_currentNumber];
        _currentNumber++;
        return value;
    }

    public bool HasNext()
    {
        return _currentNumber < _collection.Length;
    }
}

public class SomeCollection
{
    public SomeCollection(string prop1, string prop2, string prop3, string prop4, string prop5, string prop6)
    {
        Prop1 = prop1;
        Prop2 = prop2;
        Prop3 = prop3;
        Prop4 = prop4;
        Prop5 = prop5;
        Prop6 = prop6;
    }

    public string Prop1 { get; set; }
    public string Prop2 { get; set; }
    public string Prop3 { get; set; }
    public string Prop4 { get; set; }
    public string Prop5 { get; set; }
    public string Prop6 { get; set; }

    public IMyIterator GetIterator()
    {
        return new SomeCollectionIterator(this);
    }
}

public class DifferentCollection
{
    public string[] values = new string[6];

    public DifferentCollection(string prop1, string prop2, string prop3, string prop4, string prop5, string prop6)
    {
        values[0] = prop1;
        values[1] = prop2;
        values[2] = prop3;
        values[3] = prop4;
        values[4] = prop5;
        values[5] = prop6;
    }

    public IMyIterator GetIterator()
    {
        return new DifferentCollectionIterator(values);
    }
}



void Main()
{
"First Collection".Dump();
var t = new SomeCollection("test1", "field 2", "field 3", "4 test", "field 5", "6 test");
var i = t.GetIterator();
Iterate(i);

"\nSecond Collection".Dump();
var t2 = new SomeCollection("col2 test1", "col2 field 2", "col2 field 3", "col2 4 test", "col 2 field 5", "col 2 6 test");
var i2 = t2.GetIterator();
Iterate(i2);
}

void Iterate(IMyIterator iterator)
{
    while (iterator.HasNext())
    {
        iterator.GetNext().Dump();
    }
}
	

