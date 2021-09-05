using Sandbox;
using System;

namespace TagGame
{
	public abstract partial class Round : NetworkComponent
	{
		[Net] public TimeSince elapsed { get; set; } = 0;
		public abstract string name { get; }
		public abstract int length { get; }
		public int TimeLeft
		{
			get
			{
				return Math.Max((int)(length - elapsed), 0);
			}
		}

		public virtual void Start() { }
		public virtual void End() { }
		public virtual void PlayerTouch( TagPlayer player , TagPlayer other ) { }
		public abstract void OnTick();
	}
}
