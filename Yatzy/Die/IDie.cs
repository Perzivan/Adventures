using System;

namespace Yatzy
{
	public interface IDie
	{
		int roll();
		int GetMaxNumber ();
		int GetMinNumber ();
		int GetLastRolledNumber ();
	}
}

