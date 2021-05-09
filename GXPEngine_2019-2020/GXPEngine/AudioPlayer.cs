using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class AudioPlayer : GameObject
{
    SoundChannel _musicChannel;
    Sound _music;

    /// <summary>
    /// audio playing class
    /// </summary>
    public AudioPlayer()
    {
        _music = new Sound("placeholder_background_music.mp3", true, true);
        EventSubscriptions();
    }

    private void EventSubscriptions()
    {
        MyGame.OnScreenSwitch += PlayMusic;

        InteractionHitbox.OnDeath += PlayDeathSound;
    }

    protected override void OnDestroy()
    {
        MyGame.OnScreenSwitch -= PlayMusic;

        InteractionHitbox.OnDeath -= PlayDeathSound;
    }

    private void PlayMusic(MyGame.ScreenState currentScreen)
    {
        switch (currentScreen)
        {
            case MyGame.ScreenState.MENU:
                {
                    if (_musicChannel != null)
                    {
                        _musicChannel.Stop();
                    }
                    _musicChannel = _music.Play();
                    break;
                }
        }
    }

    private void PlayDeathSound()
    {
        new Sound("placeholder_death_sound.wav").Play();
    }
}

