using System;

public class Rule
{
	private Func<Scenario, bool> _test;
	public string Description { get; private set; }

	public Rule(Func<Scenario, bool> test, string description)
	{
		_test = test;
		Description = description;
	}

	public bool Test(Scenario scene)
	{
		return _test(scene);
	}
}