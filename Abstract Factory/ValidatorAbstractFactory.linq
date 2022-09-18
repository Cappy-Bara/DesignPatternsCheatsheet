<Query Kind="Program" />

public class Account
{
	public int Age {get;}
	public string Email {get;}
	
	public Account(int age, string email)
	{
		Age = age;
		Email = email;
	}
}

public interface IEmailValidator
{
	public bool Validate(string email);
}

public class StudentEmailValidator : IEmailValidator
{
	public bool Validate(string email)
	{
		return email.Contains("@student");		
	}
}

public class PresidentEmailValidator : IEmailValidator
{
	public bool Validate(string email)
	{
		return email.EndsWith(".gov");
	}
}

public interface IAgeValidator
{
	public bool Validate(int age);
}

public class StudentAgeValidator : IAgeValidator
{
	public bool Validate(int age)
	{
		return age >= 19;	
	}
}

public class PresidentAgeValidator : IAgeValidator
{
	public bool Validate(int age)
	{
		return age == 35;
	}
}


public interface IValidatorFactory
{
	public IEmailValidator GetEmailValidator();
	public IAgeValidator GetAgeValidator();
}

public abstract class Validator
{
	protected readonly IValidatorFactory validatorFactory;

	public bool Validate(Account account)
	{
		return validatorFactory.GetEmailValidator().Validate(account.Email) &&
		validatorFactory.GetAgeValidator().Validate(account.Age);
	}

	protected Validator(IValidatorFactory factory)
	{
		validatorFactory = factory;
	}
}


public class StudentValidatorFactory : IValidatorFactory
{
	public IEmailValidator GetEmailValidator() => new StudentEmailValidator();
	public IAgeValidator GetAgeValidator() => new StudentAgeValidator();
}

public class PresidentValidatorFactory : IValidatorFactory
{
	public IEmailValidator GetEmailValidator() => new PresidentEmailValidator();
	public IAgeValidator GetAgeValidator() => new PresidentAgeValidator();
}


public class PresidentValidator : Validator
{
	public PresidentValidator() : base(new PresidentValidatorFactory()){}
}

public class StudentValidator : Validator
{
	public StudentValidator() : base(new StudentValidatorFactory()){}
}


void Main()
{
	var student = new Account(26,"email@student.com");

	student.Dump();
	"Is valid for student:".Dump();
	new StudentValidator().Validate(student).Dump();
	
	"Is valid for president:".Dump();
	new PresidentValidator().Validate(student).Dump();
}