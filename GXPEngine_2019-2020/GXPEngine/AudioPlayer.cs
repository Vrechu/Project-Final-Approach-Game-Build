using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class AudioPlayer : GameObject
{
    private SoundChannel _musicChannel;
    readonly private Sound _menuMusic;
    readonly private Sound _levelMusic;

    private SoundChannel _walkingChannel;
    readonly private Sound _walkingSound;

    /// <summary>
    /// audio playing class
    /// </summary>
    public AudioPlayer()
    {
        _menuMusic = new Sound("placeholder_background_music.mp3", true, true);
        _levelMusic = new Sound("placeholder_background_music.mp3", true, true);
        _walkingSound = new Sound("placeholder_death_sound.wav", true, true);
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
        PlayerInteractionHitbox.OnPortalOutHit += PlayPortalSound;
        PortalHitbox.OnPortalInHit += PlayObjectPortalSound;
        PortalHitbox.OnPortalInHit += PlayObjectPortalSound;
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

        PlayerInteractionHitbox.OnPortalInHit -= PlayPortalSound;
        PlayerInteractionHitbox.OnPortalOutHit -= PlayPortalSound;
        PortalHitbox.OnPortalInHit -= PlayObjectPortalSound;
        PortalHitbox.OnPortalInHit -= PlayObjectPortalSound;
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
            case MyGame.ScreenState.LEVEL1:
                {
                    PlayNewMusic(_levelMusic);
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
        new Sound("placeholder_death_sound.wav").Play();
    }

    private void PlayClickSound(MyGame.ScreenState leaveEmpty)
    {
        new Sound("placeholder_death_sound.wav").Play();
    }

    private void PlayLevelFinishSound()
    {
        new Sound("placeholder_death_sound.wav").Play();
    }

    private void PlayGravitySound(MyGame.GravityDirection leaveEmpty)
    {
        new Sound("placeholder_death_sound.wav").Play();
    }

     private void PlayBonesPickupSound()
    {
        new Sound("placeholder_death_sound.wav").Play();
    }
    private void PlayPortalSound()
    {
        new Sound("placeholder_death_sound.wav").Play();
    }

    private void PlayObjectPortalSound(GameObject leaveEmpty)
    {
        new Sound("placeholder_death_sound.wav").Play();
    }
}

