using Sandbox;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TagGame
{
	public partial class Tag : Sandbox.Game
	{
		public static Tag Instance => (Current as Tag);
		[Net] public List<Team> teams { get; set; } = new List<Team>();
		public TaggerTeam TagTeam = new TaggerTeam();
		public RunnerTeam RunTeam = new RunnerTeam();
		[Net] public Round currentRound { get; private set; }

		public Tag()
		{
			teams.Add( TagTeam );
			teams.Add( RunTeam );
			if ( !IsServer ) return;
			SetRound( new WaitRound() );
			_ = Tick();
			new TagUI();
		}
		public override void ClientJoined( Client cl )
		{
			TagPlayer player = new TagPlayer();
			cl.Pawn = player;
			player.Respawn();
			base.ClientJoined( cl );
		}
		public void SetRound( Round round )
		{
			currentRound?.End();
			currentRound = round;
			currentRound.Start();
		}
		private async Task Tick()
		{
			while ( true )
			{
				await Task.DelaySeconds( 1 );
				currentRound?.OnTick();
			}
		}
	}
}
