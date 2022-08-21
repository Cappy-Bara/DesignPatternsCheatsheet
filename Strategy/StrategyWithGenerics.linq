<Query Kind="Program" />

void Main()
{
	new Expression<Sum>(6,1).Execute().Dump();
	new Expression<Difference>(6,1).Execute().Dump();
	new Expression<Multiply>(6,2).Execute().Dump();
	new Expression<Divide>(6,2).Execute().Dump();
}


public interface OperationType{
	public float Execute(float a, float b);
}

public class Sum : OperationType{
	public float Execute(float a, float b){
		return a+b;	
	}
}

public class Difference : OperationType{
	public float Execute(float a, float b){
		return a-b;	
	}
}

public class Multiply : OperationType{
	public float Execute(float a, float b){
		return a*b;	
	}
}

public class Divide : OperationType{
	public float Execute(float a, float b){
		return a/b;	
	}
}

public class Expression<T> where T : OperationType, new(){
	private readonly float a;
	private readonly float b;
	private readonly OperationType operation;
	
	public Expression(float a, float b){
		operation = new T();
		this.a = a;
		this.b = b;
	}
	
	public float Execute(){
		return operation.Execute(a,b);
	}
}