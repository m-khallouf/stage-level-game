// <copyright file="CollisionManager.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Project1.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Manages the collisions between the player and the game objects.
    /// </summary>
    public class CollisionManager
    {
        private readonly Player player;
        private readonly HealthManager healthManager;
        private readonly Obstacle obstacle;

        private readonly int characterTextureWidth = 45;
        private readonly int characterTextureHeight = 100;

        /// <summary>
        /// Initializes a new instance of the <see cref="CollisionManager"/> class.
        /// </summary>
        /// <param name="obstacle">obstacle param.</param>
        /// <param name="player">player param.</param>
        /// <param name="healthManager">healmanager param.</param>
        public CollisionManager(Obstacle obstacle, Player player, HealthManager healthManager)
        {
            this.obstacle = obstacle;
            this.player = player;
            this.healthManager = healthManager;
        }

        /// <summary>
        /// Handles the collisions between the player and the game objects.
        /// </summary>
        /// <param name="platform">platform.</param>
        /// <param name="lava">lava.</param>
        /// <param name="obstacle">obstacle.</param>
        /// <param name="gameTime">gameTime.</param>
        public void HandleCollisions(Platform platform, Lava lava, Obstacle obstacle, GameTime gameTime)
        {
            this.HandlePlatformCollision(platform);
            this.HandleLavaCollision(lava, gameTime);
            this.HandleObstacleCollision(obstacle);
        }

        private void HandlePlatformCollision(Platform pl)
        {
            var collisions = pl.CheckCollision(this.player.Bounds);
            if (collisions.Count > 0)
            {
                foreach (var platform in collisions)
                {
                    this.ProcessPlatformCollision(platform);
                }
            }
        }

        private void ProcessPlatformCollision(Rectangle platform)
        {
            // bottom
            if (this.player.Bounds.Bottom < (platform.Bottom + platform.Top) / 2f)
            {
                this.ResolvePlatformBottomCollision(platform);
            } // top
            else if (this.player.Bounds.Top > (platform.Bottom + platform.Top) / 2f)
            {
                this.ResolvePlatformTopCollision(platform);
            } // left
            else if (this.player.Bounds.Left < platform.Left)
            {
                this.ResolvePlatformLeftCollision(platform);
            } // right
            else if (this.player.Bounds.Right > platform.Right)
            {
                this.ResolvePlatformRightCollision(platform);
            }
        }

        private void ResolvePlatformRightCollision(Rectangle platform)
        {
            var characterPosition = this.player.CharacterPosition;
            characterPosition.X = platform.Right; // Place player to the right of platform
            this.player.CharacterPosition = characterPosition;
        }

        private void ResolvePlatformLeftCollision(Rectangle platform)
        {
            var characterPosition = this.player.CharacterPosition;
            characterPosition.X = platform.Left - this.characterTextureWidth;  // Place player to the left of platform
            this.player.CharacterPosition = characterPosition;
        }

        private void ResolvePlatformTopCollision(Rectangle platform)
        {
            this.player.playerVelocity.Y = 0; // Stop upward movement (ceiling collision)

            var characterPosition = this.player.CharacterPosition;
            characterPosition.Y = platform.Bottom; // Prevent player from passing through platform
            this.player.CharacterPosition = characterPosition;
        }

        private void ResolvePlatformBottomCollision(Rectangle platform)
        {
            var characterPosition = this.player.CharacterPosition;
            characterPosition.Y = platform.Top - this.characterTextureHeight;  // Place player on top of platform
            this.player.CharacterPosition = characterPosition;
            this.player.playerVelocity.Y = 0;
            this.player.IsGrounded = true;
        }

        private void HandleObstacleCollision(Obstacle obstacle)
        {
            if (obstacle.CheckCollision(this.player.Bounds).Count > 0)
            {
                // Check if obstacle is already in an invulnerable state
                if (!obstacle.IsInvulnerable)
                {
                    this.healthManager.ReduceLife();
                    this.player.HandleObstacleBlinker();
                }

                // If player still has lives, activate temporary invulnerability
                if (this.healthManager.CurrentLives > 0)
                {
                    obstacle.IsInvulnerable = true; // Prevents multiple hits from same obstacle
                    obstacle.InvulnerabilityTimer = obstacle.InvulnerabilityDuration;
                }
                else
                {
                    this.player.GameOver();
                }
            }
        }

        private void HandleLavaCollision(Lava lava, GameTime gameTime)
        {
            if (lava.CheckCollision(this.player.Bounds).Count > 0)
            {
                if (this.healthManager.CurrentLives > 0)
                {
                    this.player.ReduceLife();
                    this.player.Respawn(this.player.RespawnPosition);
                    this.player.ResetPlayerVelocity();
                }
                else
                {
                    this.player.GameOver();
                }
            }
        }
    }
}
