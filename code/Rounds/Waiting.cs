using Sandbox;
using System;

namespace TagGame
{
	public class WaitRound : Round
	{
		public override string name => "Waiting for players";
		public override int length => -1;

		public WaitRound()
		{
			if ( !Host.IsServer ) return;
			Log.Info( "wait round" );
			foreach (Client client in Client.All )
			{
				TagPlayer player = (TagPlayer)client.Pawn;
				Tag.Instance.MoveToSpawnpoint( player );
				player.team = null;
			}
		}
		public override void OnTick()
		{
			if (Client.All.Count >= 2 )
			{
				Log.Info( "moving to tagging" );
				Tag.Instance.SetRound(new TagRound());
			}
		}
	}
}
