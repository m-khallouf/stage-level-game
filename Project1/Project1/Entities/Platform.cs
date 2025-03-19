// <copyright file="Platform.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Project1.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Represents a platform in the game.
    /// </summary>
    public class Platform
    {
        private readonly int horizontalSpacing = 64;
        private readonly Texture2D platformTexture;

        /// <summary>
        /// Gets the list of platform rectangles.
        /// </summary>
        private readonly List<Rectangle> platforms;

        /// <summary>
        /// Initializes a new instance of the <see cref="Platform"/> class.
        /// </summary>
        /// <param name="content">The content manager to load textures.</param>
        public Platform(ContentManager content)
        {
            this.platformTexture = content.Load<Texture2D>("platform");
            this.platforms = new List<Rectangle>();
        }

        /// <summary>
        /// Gets the list of platform rectangles.
        /// </summary>
        public Rectangle Bounds { get; private set; }

        /// <summary>
        /// Creates a specified number of platforms starting from a given position.
        /// </summary>
        /// <param name="numberOfPlatforms">The number of platforms to create.</param>
        /// <param name="startPosition">The starting position for the first platform.</param>
        public void CreatePlatforms(int numberOfPlatforms, Vector2 startPosition)
        {
            for (int i = 0; i < numberOfPlatforms; i++)
            {
                var position = new Vector2(startPosition.X + (i * this.horizontalSpacing), startPosition.Y);
                this.Bounds = new Rectangle((int)position.X, (int)position.Y, this.platformTexture.Width, this.platformTexture.Height);
                this.platforms.Add(this.Bounds);
            }
        }

        /// <summary>
        /// Gets the list of platform rectangles.
        /// </summary>
        /// <returns>A list of platform rectangles.</returns>
        public List<Rectangle> GetPlatformRectangles()
        {
            return this.platforms;
        }

        /// <summary>
        /// Checks if the platform collides with the player.
        /// </summary>
        /// <param name="bounds">The player to check collision with.</param>
        /// <returns>True if there is a collision, otherwise false.</returns>
        public List<Rectangle> CheckCollision(Rectangle bounds)
        {
            var result = new List<Rectangle>();

            foreach (var platform in this.platforms)
            {
                if (bounds.Intersects(platform))
                {
                    // Debug.WriteLine("Collision detected!");
                    result.Add(platform);
                }
            }

            return result;
        }

        /// <summary>
        /// Draws the platform.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch used for drawing.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var platform in this.platforms)
            {
                spriteBatch.Draw(this.platformTexture, platform, Color.White);
            }
        }
    }
}
