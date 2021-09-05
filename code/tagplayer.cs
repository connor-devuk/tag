using Sandbox;
using System;

namespace TagGame
{
	public partial class TagPlayer : Player
	{
		[Net] public Team PlayerTeam { get; set; }
		public float nextTouch = 0;
		public Team team {
			get => PlayerTeam;
			set
			{
				Team previous = PlayerTeam;
				previous?.OnLeave( this );
				value?.OnBecome( this );
				PlayerTeam = value;
			}
		}
		public override void Respawn()
		{
			SetModel( "models/citizen/citizen.vmdl" );
			Animator = new StandardPlayerAnimator();
			Controller = new WalkController();
			Camera = new FirstPersonCamera();
			EnableAllCollisions = false;
			EnableTouch = true;
			EnableTouchPersists = true;
			EnableHideInFirstPerson = true;
			EnableShadowInFirstPerson = true;
			base.Respawn();
			Log.Info(Time.Now);
		}

		public override void Simulate( Client cl )
		{
			base.Simulate( cl );
			if ( Input.Pressed( InputButton.View ) )
			{
				if (Camera is FirstPersonCamera )
				{
					Camera = new ThirdPersonCamera();
				}
				else
				{
					Camera = new FirstPersonCamera();
				}
			}
		}
		public override void StartTouch( Entity other )
		{
			if ( other is not TagPlayer ) return;
			Tag.Instance.currentRound?.PlayerTouch( this, other as TagPlayer );
			base.StartTouch( other );
		}
	}
}
