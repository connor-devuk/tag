using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace TagGame
{
	public partial class UITeam : Panel
	{
		public Label label;
		public UITeam()
		{
			label = Add.Label( "Undecided", "value" );
		}
		public override void Tick()
		{
			TagPlayer player = Local.Pawn as TagPlayer;
			if ( player is null ) return;
			label.Text = $"{(player.team is not null ? player.team.teamName : "Undecided")}";
			SetClass( "runner", player.team is RunnerTeam );
		}
	}

	public partial class UICountdown : Panel
	{
		public Label label;
		public UICountdown()
		{
			label = Add.Label( "0:00", "value" );
		}
		public override void Tick()
		{
			label.Text = $"{Tag.Instance?.currentRound?.TimeLeft}";
		}
	}
}
