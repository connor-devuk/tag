using Sandbox;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TagGame
{
	public class TagRound : Round
	{
		public override string name => "Tagging";

		public override int length => 120;

		public override void Start()
		{
			if ( !Host.IsServer ) return;
			int taggerNum = Math.Clamp( (int)Math.Ceiling( (decimal)(Client.All.Count / 3) ), 1, 4 );
			List<Client> players = new List<Client>( Client.All );
			for ( int i = 0; i < taggerNum; i++ )
			{
				Client tagger = players[Rand.Int( players.Count - 1 )];
				players.Remove( tagger );
				TagPlayer tagPawn = (TagPlayer)tagger.Pawn;
				tagPawn.team = Tag.Instance.TagTeam;
				Log.Info( $"{tagger} -> {tagPawn.team.teamName}" );
			}
			foreach (Client player in players )
			{
				TagPlayer pawn = (TagPlayer)player.Pawn;
				pawn.team = Tag.Instance.RunTeam;
			}
		}

		public override void PlayerTouch( TagPlayer player , TagPlayer other )
		{
			float now = Time.Now;
			if ( player.team is not TaggerTeam || now < other.nextTouch ) return;
			player.nextTouch = now + 1;
			player.team = Tag.Instance.RunTeam;
			other.team = Tag.Instance.TagTeam;
		}
		public override void OnTick()
		{
			if ( TimeLeft <= 0 )
			{
			}
		}
	}
}
