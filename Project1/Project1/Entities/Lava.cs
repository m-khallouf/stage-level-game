// <copyright file="Lava.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Project1.Entities
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Represents a platform in the game.
    /// </summary>
    public class Lava
    {
        private readonly int horizontalSpacing = 64;
        private readonly Texture2D lavaTexture;

        // private readonly Player player;
        private readonly List<Rectangle> lavas;
        private Vector2 platformPosition;

        /// <summary>
        /// Initializes a new instance of the <see cref="Lava"/> class.
        /// </summary>
        /// <param name="content">The content manager to load textures.</param>
        public Lava(ContentManager content)
        {
            this.lavaTexture = content.Load<Texture2D>("lava");
            this.lavas = new List<Rectangle>();
            this.platformPosition = Vector2.Zero;
        }

        /// <summary>
        /// Creates a specified number of lava platforms starting from a given position.
        /// </summary>
        /// <param name="numberOfLavas">The number of lava platforms to create.</param>
        /// <param name="startPosition">The starting position for the first lava platform.</param>
        public void CreateLava(int numberOfLavas, Vector2 startPosition)
        {
            for (int i = 0; i < numberOfLavas; i++)
            {
                var position = new Vector2(startPosition.X + (i * this.horizontalSpacing), startPosition.Y);
                var lavaRectangle = new Rectangle((int)position.X, (int)position.Y, this.lavaTexture.Width, this.lavaTexture.Height);
                this.lavas.Add(lavaRectangle);
            }
        }

        /// <summary>
        /// Gets the list of platform rectangles.
        /// </summary>
        /// <returns>A list of platform rectangles.</returns>
        public List<Rectangle> GetLavaRectangles()
        {
            return this.lavas;
        }

        /// <summary>
        /// Checks if the platform collides with the player.
        /// </summary>
        /// <param name="bounds">The player to check collision with.</param>
        /// <returns>True if there is a collision, otherwise false.</returns>
        public List<Rectangle> CheckCollision(Rectangle bounds)
        {
            var result = new List<Rectangle>();

            foreach (var lava in this.lavas)
            {
                if (bounds.Intersects(lava))
                {
                    // Debug.WriteLine("Collision detected!");
                    result.Add(lava);
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
            foreach (var platform in this.lavas)
            {
                spriteBatch.Draw(this.lavaTexture, platform, Color.White);
            }
        }
    }
}