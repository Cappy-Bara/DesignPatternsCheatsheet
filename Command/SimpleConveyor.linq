<Query Kind="Program" />

public class Box			//marker
{
	public int Position = 0;

	public void Move(int value)
	{
		Position += value;
	}
}

public class Conveyor		//receiver class
{
	private readonly Box box = new Box();
	private int speed = 0;
	
	public void MoveBoxLeft()
	{
		box.Move(-speed);	
	}
	
	public void MoveBoxRight()
	{
		box.Move(speed);
	}

	public void SetSpeed(int speed)
	{
		this.speed = speed;	
	}
	
	public void GetBlockPosition()
	{
		box.Position.Dump();
	}
}


public interface ICommand		//command interface
{
	public void Handle();		//Important - no parameters here. Parameters should be as command fields
	public void Rollback();
}

public class MoveSlowForwardCommand : ICommand			//real command
{
	private readonly Conveyor conveyor;

	public MoveSlowForwardCommand(Conveyor conveyor)
	{
		this.conveyor = conveyor;
	}

	public void Handle()
	{
		conveyor.SetSpeed(1);
		conveyor.MoveBoxRight();
		conveyor.SetSpeed(0);
	}
	
	public void Rollback()
	{
		conveyor.SetSpeed(1);
		conveyor.MoveBoxLeft();
		conveyor.SetSpeed(0);
	}
}

public class MoveFastForwardCommand : ICommand
{
	private readonly Conveyor conveyor;

	public MoveFastForwardCommand(Conveyor conveyor)
	{
		this.conveyor = conveyor;
	}

	public void Handle()
	{
		conveyor.SetSpeed(4);
		conveyor.MoveBoxRight();
		conveyor.SetSpeed(0);
	}
	
	public void Rollback()
	{
		conveyor.SetSpeed(4);
		conveyor.MoveBoxLeft();
		conveyor.SetSpeed(0);
	}
}

public class MoveSlowBackwardsCommand : ICommand
{
	private readonly Conveyor conveyor;

	public MoveSlowBackwardsCommand(Conveyor conveyor)
	{
		this.conveyor = conveyor;
	}

	public void Handle()
	{
		conveyor.SetSpeed(1);
		conveyor.MoveBoxLeft();
		conveyor.SetSpeed(0);
	}
	
	public void Rollback()
	{
		conveyor.SetSpeed(1);
		conveyor.MoveBoxRight();
		conveyor.SetSpeed(0);
	}
}

public class MoveFastBackwardsCommand : ICommand
{
	private readonly Conveyor conveyor;

	public MoveFastBackwardsCommand(Conveyor conveyor)
	{
		this.conveyor = conveyor;
	}

	public void Handle()
	{
		conveyor.SetSpeed(4);
		conveyor.MoveBoxLeft();
		conveyor.SetSpeed(0);
	}
	
	public void Rollback()
	{
		conveyor.SetSpeed(4);
		conveyor.MoveBoxRight();
		conveyor.SetSpeed(0);
	}
}


public class Pilot					//Invoker class
{
	private List<ICommand> commands = new List<ICommand>();

	private ICommand leftButtonAction;
	private ICommand leftButtonTwiceAction;
	private ICommand rightButtonAction;
	private ICommand rightButtonTwiceAction;

	public Pilot(ICommand leftButtonAction, 
				ICommand leftButtonTwiceAction, 
				ICommand rightButtonAction, 
				ICommand rightButtonTwiceAction)		//Can have methods to assign command to 'button'
	{
		this.leftButtonAction = leftButtonAction;
		this.leftButtonTwiceAction = leftButtonTwiceAction; 
		this.rightButtonAction = rightButtonAction;
		this.rightButtonTwiceAction = rightButtonTwiceAction;
	}

	public void PressRightButton() => HandleAction(rightButtonAction);
	
	public void PressRightButtonTwice() => HandleAction(rightButtonTwiceAction);
	
	public void PressLeftButton() => HandleAction(leftButtonAction);
	
	public void PressLeftButtonTwice() => HandleAction(leftButtonTwiceAction);

	private void HandleAction(ICommand command)
	{
		command.Handle();
		commands.Add(command);
	}
	
	public void BackToDefaultPosition()
	{
		commands.Reverse();
		commands.ForEach(x => x.Rollback());	
	}
	
}

void Main()		//client
{
	var conveyor = new Conveyor();		//receiver
	var moveFastForwardCommand = new MoveFastForwardCommand(conveyor);	//concrete commands (can have parameters ex. in constructor)
	var moveSlowForwardCommand = new MoveSlowForwardCommand(conveyor);
	var moveFastBackwardsCommand = new MoveFastBackwardsCommand(conveyor);
	var moveSlowBackwardsCommand  = new MoveSlowBackwardsCommand(conveyor);
	
	//invoker
	var pilot = new Pilot(moveSlowBackwardsCommand, moveFastBackwardsCommand, moveSlowForwardCommand, moveFastForwardCommand);
	
	//some actions
	pilot.PressRightButton();
	pilot.PressRightButton();
	pilot.PressRightButtonTwice();
	conveyor.GetBlockPosition();
	
	pilot.PressLeftButton();
	
	conveyor.GetBlockPosition();
	
	pilot.BackToDefaultPosition();
	
	conveyor.GetBlockPosition();
}

