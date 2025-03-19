// <copyright file="Obstacle.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Project1.Entities
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using static System.Net.Mime.MediaTypeNames;

    /// <summary>
    /// Represents an obstacle in the game.
    /// </summary>
    public class Obstacle
    {
        private readonly Texture2D spikeLongTexture;
        private readonly Texture2D spikeShortTexture;
        private readonly List<Rectangle> obstacles;
        private bool isInvulnerable = false;
        private float invulnerabilityTimer = 0.0f;
        private float invulnerabilityDuration = 2.0f;
        private float blinkTimer = 0.0f;
        private bool isVisible = true;
        private float blinkInterval = 0.15f;
        private bool isActive = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="Obstacle"/> class.
        /// </summary>
        /// <param name="content">The content manager used to load textures.</param>
        public Obstacle(ContentManager content)
        {
            this.spikeLongTexture = content.Load<Texture2D>("spikeLong");
            this.spikeShortTexture = content.Load<Texture2D>("spikeShort");
            this.obstacles = new List<Rectangle>();
        }

        /// <summary>
        /// Gets or sets a value indicating whether the obstacle is active.
        /// </summary>
        public bool IsActive
        {
            get => this.isActive;
            set => this.isActive = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the obstacle is invulnerable.
        /// </summary>
        public bool IsInvulnerable
        {
            get
            {
                return this.isInvulnerable;
            }

            set
            {
                this.isInvulnerable = value;
            }
        }

        /// <summary>
        /// Gets or sets the invulnerability timer.
        /// </summary>
        public float InvulnerabilityTimer
        {
            get
            {
                return this.invulnerabilityTimer;
            }

            set
            {
                this.invulnerabilityTimer = value;
            }
        }

        /// <summary>
        /// Gets or sets the invulnerability duration.
        /// </summary>
        public float InvulnerabilityDuration
        {
            get
            {
                return this.invulnerabilityDuration;
            }

            set
            {
                this.invulnerabilityDuration = value;
            }
        }

        /// <summary>
        /// Gets or sets the blink timer.
        /// </summary>
        public float BlinkTimer
        {
            get
            {
                return this.blinkTimer;
            }

            set
            {
                this.blinkTimer = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the obstacle is visible.
        /// </summary>
        public bool IsVisible
        {
            get
            {
                return this.isVisible;
            }

            set
            {
                this.isVisible = value;
            }
        }

        /// <summary>
        /// Gets or sets the blink interval.
        /// </summary>
        public float BlinkInterval
        {
            get
            {
                return this.blinkInterval;
            }

            set
            {
                this.blinkInterval = value;
            }
        }

        /// <summary>
        /// Checks if the obstacle collides with any other boundaries or entities.
        /// </summary>
        /// <param name="bounds">The boundaries with which the collision is tested.</param>
        /// <returns>The list of the obstacles that encounter a collision.</returns>
        public List<Rectangle> CheckCollision(Rectangle bounds)
        {
            if (!this.isActive)
            {
                return new List<Rectangle>();
            }

            var result = new List<Rectangle>();

            foreach (var obstacle in this.obstacles)
            {
                if (bounds.Intersects(obstacle))
                {
                    result.Add(obstacle);
                }
            }

            return result;
        }

        /// <summary>
        /// Creates a new obstacle and adds it to the list of obstacles.
        /// </summary>
        /// <param name="position">The position of the new obstacle.</param>
        /// <param name="type">The type of obstacle to create.</param>
        public void CreateObstacle(Vector2 position, string type)
        {
            // Use spikeLong or spikeShort based on the type!
            Texture2D texture = type == "spikeLong" ? this.spikeLongTexture : this.spikeShortTexture;
            this.obstacles.Add(new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height));
        }

        /// <summary>
        /// Updates the obstacle and handles invulnerability.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void Update(GameTime gameTime)
        {
            this.HandleInvulnerability(gameTime);
        }

        /// <summary>
        /// Draws the obstacle.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch used for drawing.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var obstacle in this.obstacles)
            {
                // Uses height to determine which texture to draw.
                var texture = obstacle.Height == this.spikeLongTexture.Height ? this.spikeLongTexture : this.spikeShortTexture;
                spriteBatch.Draw(texture, obstacle, Color.White);
            }
        }

        /// <summary>
        /// Handles the invulnerability logic.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void HandleInvulnerability(GameTime gameTime)
        {
            // Update invulnerability timer if the player is invulnerable
            if (this.isInvulnerable)
            {
                this.invulnerabilityTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (this.invulnerabilityTimer <= 0)
                {
                    this.isInvulnerable = false;
                    this.isVisible = true;
                    Debug.WriteLine("You're no longer invulnerable!");
                }

                this.blinkTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (this.blinkTimer <= 0)
                {
                    this.isVisible = !this.isVisible;
                    this.blinkTimer = this.blinkInterval;
                }
            }
        }
    }
}