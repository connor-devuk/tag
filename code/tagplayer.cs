using Sandbox;
using System;

namespace TagGame
{
	public partial class TagPlayer : Player
	{
		public float nextTouch = 0;
		[Net, OnChangedCallback] public Team team { get; set; }
		private void OnteamChanged()
		{
			team?.OnBecome( this );
		}
		public override void Respawn()
		{
			SetModel( "models/citizen/citizen.vmdl" );
			Animator = new StandardPlayerAnimator();
			Controller = new TagController();
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
		public override void Touch( Entity other )
		{
			base.StartTouch( other );
			if ( other is not TagPlayer ) return;
			Tag.Instance.currentRound?.PlayerTouch( this, other as TagPlayer );
		}
	}
}
