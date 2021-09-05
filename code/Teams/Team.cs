using Sandbox;
using System;
using System.Collections.Generic;

namespace TagGame {
	public abstract partial class Team : NetworkComponent
	{
		public List<TagPlayer> players { get; } = new List<TagPlayer>();
		[Net] public abstract string teamName { get; }
		public abstract Color teamColor { get; }

		public virtual void OnBecome( TagPlayer player )
		{
			Log.Info( $"Called on become for {player.GetClientOwner().Name}" );
			players.Add( player );
			player.RenderColor = teamColor;
		}

		public virtual void OnLeave( TagPlayer player )
		{
			players.Remove( player );
		}
	}
}
