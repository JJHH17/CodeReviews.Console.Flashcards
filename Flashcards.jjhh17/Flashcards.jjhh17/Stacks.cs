using System;

public class Stacks
{
	public string StackName { get; set; }
	public string Description { get; set; }

    public Stacks(string name, string description)
	{
		StackName = name;
		Description = description;
    }

	// For Dapper usage
	public Stacks() { }
}
