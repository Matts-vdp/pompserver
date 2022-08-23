namespace PompServer.Models;

public class RepeatedCommand
{
    public Guid Id { get; set; }
    public bool Action { get; set; }

    protected bool done = false;
    public int Amount { get; set; }
    public int OffTime { get; set; }
    public int OnTime { get; set; }
    public DateTime NextTime { get; set; } = DateTime.MinValue;

    public RepeatedCommand(
        int amount = 1,
        int offTime = 0,
        int onTime = 0
        ) : this(true, amount, offTime, onTime) { }


    public RepeatedCommand(
        bool action = true,
        int amount = 1,
        int offTime = 0,
        int onTime = 0
        )
    {
        Id = Guid.NewGuid();
        Action = action;
        Amount = amount;
        OffTime = offTime;
        OnTime = onTime;
        NextTime = DateTime.Now;
    }

    public virtual bool ShouldExecute(DateTime time)
    {
        return NextTime <= time;
    }
    public virtual bool Execute()
    {
        if (Action) // als eerste keer 
        {
            NextTime += TimeSpan.FromSeconds(OnTime);
            Action = false;
            return true;
        }
        else
        {
            NextTime += TimeSpan.FromSeconds(OffTime);
            Action = true;
            Amount--;
            if (Amount == 0) done = true;
            return false;
        }
    }

    public bool IsDone()
    {
        return done;
    }


    public override string ToString()
    {
        return $"id:{Id} action:{Action} amount:{Amount}";
    }
}