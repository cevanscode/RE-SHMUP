using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGameLibrary;
using MonoGameLibrary.Input;
using RE_SHMUP.Scenes;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RE_SHMUP.Scenes
{
    public class StoryModeScene : Scene, IParticleEmitter
    {
        #region Important fields for making stuff
        private SpriteFont _spriteFont;
        private BasicTilemap _tilemap;
        #endregion

        #region Particle Stuff
        ExplosionParticleSystem _explosions;
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        #endregion

        #region Larger Screen Transforms
        private bool _shaking;
        private float _shakeTime;
        #endregion

        #region Sound Stuff
        private SoundEffect _shootSoundEffect;
        private SoundEffect _explodeSoundEffect;
        private SoundEffect _beamShotSoundEffect;
        #endregion

        #region Bomb fields

        private int bombCount = 3;

        private bool bombActive = false;

        private float bombDuration = 2f; // iframes

        private float bombTimer = 0f;

        private bool bombWaveActive = false;

        private float bombWaveRadius = 0f;

        private float bombWaveMaxRadius;

        private Vector2 bombWaveCenter;

        private float bombWaveSpeed = 800f;

        #endregion

        #region ScoreSaving

        public int score = 0;

        private static int _bestScore = 0;

        private string _scorerName;

        private bool _playerDead = false;
        #endregion

        private bool _paused;

        #region Sprites/Objects
        private List<MeteorSprite> meteors;
        private int meteorCount = 10;

        private PlayerSprite player;

        private List<BulletSprite> bullets;

        private List<MissileSprite> missiles;

        private List<DummyBalloonSprite> balloons;

        private Texture2D basicStar;
        private List<Vector2> starPlacements;
        #endregion

        private Random _rand = new Random();

        public StoryModeScene()
        {

        }
    }
}
