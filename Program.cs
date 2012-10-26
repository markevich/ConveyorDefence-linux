#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace ConveyorDefence
{
	static class Program
	{
		private static ConveyorDefence game;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main ()
		{
			game = new ConveyorDefence();
			game.Run ();
		}
	}
}
