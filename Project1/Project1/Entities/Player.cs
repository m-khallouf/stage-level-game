// <copyright file="Player.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Project1.Entities
{
    using System;
    using System.Diagnostics;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using Project1;

    /// <summary>
    /// Represents a player in the game.
    /// </summary>
    public class Player
    {
        private readonly Texture2D characterTexture;
        private readonly float gravity = 0.3f;
        private readonly float jumpForce = -10f;
        private readonly float playerSpeed = 300f;

        private readonly Obstacle obstacle;
        private readonly CollisionManager collisionManager;

        private Texture2D currentSkin;
        private Vector2 characterPosition;
        private int currentLives;
        private bool moveLeft = false;
        private bool moveRight = false;
        private bool jump = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="content">The content manager used to load assets.</param>
        /// <param name="playerPosition">The position of the player character.</param>
        /// <param name="inputManager">The input manager used for handling input.</param>
        /// <param name="respawnPosition">The position where the player will respawn.</param>
        /// <param name="obstacle">The obstacle object for collision detection.</param>
        public Player(ContentManager content, Vector2 playerPosition, InputManager inputManager, Vector2 respawnPosition, Obstacle obstacle)
        {
            this.characterTexture = content.Load<Texture2D>("character");
            this.currentSkin = this.characterTexture;
            this.currentLives = this.MaxLives;
            this.characterPosition = playerPosition;
            this.RespawnPosition = respawnPosition;
            this.obstacle = obstacle;
            this.HealthManager = new HealthManager(this);
            this.collisionManager = new CollisionManager(this.obstacle, this, this.HealthManager);

            inputManager.OnMoveLeftClicked += () => this.moveLeft = true;
            inputManager.OnMoveRightClicked += () => this.moveRight = true;
            inputManager.OnSpaceClicked += () => this.jump = true;
        }

        /// <summary>
        /// Gets a value indicating whether the player is dead.
        /// </summary>
        public event Action OnGameOver;

        /// <summary>
        /// Gets the bounding rectangle of the player.
        /// </summary>
        public Rectangle Bounds { get; private set; }

        /// <summary>
        /// Gets or sets the character position.
        /// </summary>
        public Vector2 CharacterPosition
        {
            get => this.characterPosition;
            set => this.characterPosition = value;
        }

        /// <summary>
        /// Gets the maximum number of lives the player can have.
        /// </summary>
        public int MaxLives { get; } = 3;

        /// <summary>
        /// Gets or sets the player's velocity.
        /// </summary>
        public Vector2 playerVelocity = Vector2.Zero;

        /// <summary>
        /// Gets or sets the respawn position of the player.
        /// </summary>
        public Vector2 RespawnPosition { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the player is grounded.
        /// </summary>
        public bool IsGrounded { get; set; } = false;

        /// <summary>
        /// Gets the health manager for the player.
        /// </summary>
        public HealthManager HealthManager { get; private set; }

        /// <summary>
        /// Clamps the character position within the screen bounds.
        /// </summary>
        /// <param name="characterPosition">The position of the character.</param>
        public void Clamp()
        {
            this.characterPosition.X = MathHelper.Clamp(this.characterPosition.X, 0, 1280 - 45);
            this.characterPosition.Y = MathHelper.Clamp(this.characterPosition.Y, 0, 720 - 100);
        }

        /// <summary>
        /// Resets the player traits for a new level.
        /// </summary>
        public void ResetForNewLevel()
        {
            this.ResetPlayerPosition();
            this.ResetPlayerVelocity();
            this.HealthManager.ResetHealth();

            if (this.obstacle != null)
            {
                this.obstacle.IsInvulnerable = false;
                this.obstacle.IsVisible = true;
            }
        }

        /// <summary>
        /// Changes the skin of the player character.
        /// </summary>
        /// <param name="skin">The new skin of the player.</param>
        public void ChangeSkin(Texture2D skin)
        {
            this.currentSkin = skin;
        }

        /// <summary>
        /// Draws the player character at the specified position.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch used for drawing.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (!this.obstacle.IsVisible && this.obstacle.IsInvulnerable)
            {
                return; // Do not draw if invulnerable and invisible
            }

            spriteBatch.Draw(this.currentSkin, this.characterPosition, Color.White);
        }

        /// <summary>
        /// Updates the player state based on game time, platform, and lava.
        /// </summary>
        /// <param name="gameTime">The current game time.</param>
        /// <param name="platform">The platform object for collision detection.</param>
        /// <param name="lava">The lava object for collision detection.</param>
        /// <param name="obstacle">The obstacle object for collision detection.</param>
        public void Update(GameTime gameTime, Platform platform, Lava lava, Obstacle obstacle)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            this.HandleMovement(deltaTime);
            this.Bounds = new Rectangle((int)this.characterPosition.X, (int)this.characterPosition.Y, this.characterTexture.Width, this.characterTexture.Height);
            this.collisionManager.HandleCollisions(platform, lava, obstacle, gameTime);
        }

        /// <summary>
        /// Respawns the player at the specified position.
        /// </summary>
        /// <param name="respawnPosition">The position where the player will respawn.</param>
        public void Respawn(Vector2 respawnPosition) => this.characterPosition = respawnPosition;

        /// <summary>
        /// Reduces the player's life by one.
        /// </summary>
        public void ReduceLife() => this.HealthManager.ReduceLife();

        /// <summary>
        /// Triggers the game over event.
        /// </summary>
        public void GameOver() => this.OnGameOver?.Invoke();

        /// <summary>
        /// Resets the player's velocity to zero.
        /// </summary>
        public void ResetPlayerVelocity() => this.playerVelocity = Vector2.Zero;

        /// <summary>
        /// Resets the player's position to the starting position.
        /// </summary>
        public void ResetPlayerPosition() => this.characterPosition = new Vector2(0, 566);

        /// <summary>
        /// Handles the obstacle blinker logic.
        /// </summary>
        public void HandleObstacleBlinker()
        {
            // Check invulnerability
            if (!this.obstacle.IsInvulnerable)
            {
                this.currentLives--;
                this.obstacle.IsInvulnerable = true; // Invulnerability handled in Update method due to needing gameTime
                this.obstacle.InvulnerabilityTimer = this.obstacle.InvulnerabilityDuration; // Reset invulnerability timer
                this.obstacle.BlinkTimer = this.obstacle.BlinkInterval; // Reset blink timer
            }
        }

        /// <summary>
        /// Handles the movement of the player character based on input.
        /// </summary>
        /// <param name="deltaTime">The time elapsed since the last update.</param>
        private Vector2 HandleMovement(float deltaTime)
        {
            if (this.moveLeft)
            {
                this.moveLeft = false;
                this.characterPosition += new Vector2(-this.playerSpeed * deltaTime, 0);
            }

            if (this.moveRight)
            {
                this.moveRight = false;
                this.characterPosition += new Vector2(this.playerSpeed * deltaTime, 0);
            }

            if (this.jump)
            {
                if (this.IsGrounded)
                {
                    this.playerVelocity.Y = this.jumpForce;
                    this.IsGrounded = false;
                }

                this.jump = false;
            }

            this.playerVelocity.Y += this.gravity;
            this.characterPosition += this.playerVelocity;

            return this.characterPosition;
        }
    }
}