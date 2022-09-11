<Query Kind="Program" />

public interface IMessage 
{
	public string Content {get;}
}

public class Message : IMessage
{
	public string Content {get;}
	
	public Message(string content)
	{
		Content = content;
	}
}

//No base decorator class - use only when not many decorators predicted.

public class DateTimeMessageDecorator : IMessage
{
	private IMessage message;
	public string Content => "[" + DateTime.Now + "]" + "\t" + message.Content;
	
	public DateTimeMessageDecorator(IMessage message)
	{
		this.message = message;
	}
}

public class CapitalLettersMessageDecorator : IMessage
{
	private IMessage message;
	public string Content => message.Content.ToUpper();
	
	public CapitalLettersMessageDecorator(IMessage message)
	{
		this.message = message;
	}
}


void Main()
{
	IMessage simpleMessage = new Message("Sample message");
	simpleMessage.Content.Dump();
	
	IMessage dateTimeMessage = new Message("Message with dateTime prefix");
	dateTimeMessage = new DateTimeMessageDecorator(dateTimeMessage);
	dateTimeMessage.Content.Dump();
	
	IMessage capitalLettersAndDateTime = new Message("big letter message with dateTime prefix");
	capitalLettersAndDateTime = new DateTimeMessageDecorator(capitalLettersAndDateTime);
	capitalLettersAndDateTime = new CapitalLettersMessageDecorator(capitalLettersAndDateTime);
	capitalLettersAndDateTime.Content.Dump();
}
