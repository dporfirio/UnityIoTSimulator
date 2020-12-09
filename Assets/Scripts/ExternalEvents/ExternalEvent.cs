public abstract class ExternalEvent {
	
	// receive updates from the clock
	public abstract void ReceiveTimeUpdate(int day, int hour, int min, int sec);

	// execute the event
	public abstract void Execute();

}