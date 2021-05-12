using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class AudioPlayer : GameObject
{
    private SoundChannel _musicChannel;
    readonly private Sound _menuMusic;
    readonly private Sound _graveyardMusic;
    readonly private Sound _caveMusic;
    readonly private Sound _hellMusic;

    private SoundChannel _walkingChannel;
    readonly private Sound _walkingSound;

    /// <summary>
    /// audio playing class
    /// </summary>
    public AudioPlayer()
    {
        _menuMusic = new Sound("Menu_music.mp3", true, true);
        _graveyardMusic = new Sound("Graveyard_level_music.mp3", true, true);
        _caveMusic = new Sound("Cave_level_music.mp3", true, true);
        _hellMusic = new Sound("Hell_level_music.mp3", true, true);
        _walkingSound = new Sound("Walking.wav", true, true);
        EventSubscriptions();
    }

    private void EventSubscriptions()
    {
        MyGame.OnScreenSwitch += PlayMusic;

        Skull.OnWalkingStart += PlayWalkingSound;
        Skull.OnWalkingStop += StopWalkingSound;

        PlayerInteractionHitbox.OnDeath += PlayDeathSound;
        Button.OnButtonClicked += PlayClickSound;
        PlayerInteractionHitbox.OnGoalReached += PlayLevelFinishSound;
        MyGame.OnGravitySwitch += PlayGravitySound;
        PlayerInteractionHitbox.OnLegsPickup += PlayBonesPickupSound;

        PlayerInteractionHitbox.OnPortalInHit += PlayPortalSound;
        Skull.OnTeleport += PlayPortalSound;
        MovingWall.OnTeleport += PlayPortalSound;
        MovingSpike.OnTeleport += PlayPortalSound;
    }

    protected override void OnDestroy()
    {
        MyGame.OnScreenSwitch -= PlayMusic;

        Skull.OnWalkingStart -= PlayWalkingSound;
        Skull.OnWalkingStop -= StopWalkingSound;

        PlayerInteractionHitbox.OnDeath -= PlayDeathSound;
        Button.OnButtonClicked -= PlayClickSound;
        PlayerInteractionHitbox.OnGoalReached -= PlayLevelFinishSound;
        MyGame.OnGravitySwitch -= PlayGravitySound;
        PlayerInteractionHitbox.OnLegsPickup -= PlayBonesPickupSound;

        Skull.OnTeleport -= PlayPortalSound;
        MovingWall.OnTeleport -= PlayPortalSound;
        MovingSpike.OnTeleport -= PlayPortalSound;
    }

    private void PlayMusic(MyGame.ScreenState currentScreen)
    {
        switch (currentScreen)
        {
            case MyGame.ScreenState.MENU:
                {
                    PlayNewMusic(_menuMusic);
                    break;
                }
            case MyGame.ScreenState.TUTORIAL:
                {
                    PlayNewMusic(_graveyardMusic);
                    break;
                }
            case MyGame.ScreenState.LEVEL3:
                {
                    PlayNewMusic(_caveMusic);
                    break;
                }
            case MyGame.ScreenState.LEVEL5:
                {
                    PlayNewMusic(_hellMusic);
                    break;
                }
        }
    }

    /// <summary>
    /// stops old music and plays the new music
    /// </summary>
    /// <param name="music">new music to play</param>
    private void PlayNewMusic(Sound music)
    {
        if (_musicChannel != null)
        {
            _musicChannel.Stop();
        }
        _musicChannel = music.Play();
    }

    /// <summary>
    /// plays the walking sound while the player is walking
    /// </summary>
    private void PlayWalkingSound()
    {
        _walkingChannel = _walkingSound.Play();
    }
    private void StopWalkingSound()
    {
        if (_walkingChannel != null)
        {
            _walkingChannel.Stop();
        }
    }

    private void PlayDeathSound()
    {
        new Sound("Fail.wav").Play();
    }

    private void PlayClickSound(MyGame.ScreenState leaveEmpty)
    {
        new Sound("Clicking_the_button.wav").Play();
    }

    private void PlayLevelFinishSound()
    {
        new Sound("Pass-the-level.wav").Play();
    }

    private void PlayGravitySound(MyGame.GravityDirection leaveEmpty)
    {
        new Sound("Moving_gravity.wav").Play();
    }

     private void PlayBonesPickupSound()
    {
        new Sound("Collecting.wav").Play();
    }
    private void PlayPortalSound()
    {
        new Sound("Tiny_portals.wav").Play();
    }
}

