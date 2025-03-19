// <copyright file="Door.cs" company="PlaceholderCompany">
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

    /// <summary>
    /// Represents a door in the game.
    /// </summary>
    public class Door
    {
        private readonly Texture2D closedDoorTexture;
        private readonly Texture2D openDoorTexture;
        private readonly Player player;
        private readonly Key key;
        private bool keyCollected;

        /// <summary>
        /// Initializes a new instance of the <see cref="Door"/> class.
        /// </summary>
        /// <param name="content">The content manager used to load textures.</param>
        /// <param name="key">The key required to open the door.</param>
        /// <param name="player">The player object in the game.</param>
        public Door(ContentManager content, Key key, Player player)
        {
            // this.position = new Vector2(screenWidth - openDoor.Width, screenHeight - openDoor.Height);
            this.closedDoorTexture = content.Load<Texture2D>("closedDoor");
            this.openDoorTexture = content.Load<Texture2D>("openDoor");
            this.key = key;
            this.player = player;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the key has been collected.
        /// </summary>
        public bool KeyCollected { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the level is complete.
        /// </summary>
        public bool DoorAccessed { get; set; }

        /// <summary>
        /// Updates the door state based on whether the key has been collected.
        /// </summary>
        /// <param name="doorPosition">Indicates the position of the current door.</param>
        public void Update(Vector2 doorPosition)
        {
            this.keyCollected = this.key.KeyCollected;

            if (this.keyCollected && this.CheckCollision(doorPosition))
            {
                this.DoorAccessed = true;
            }
        }

        /// <summary>
        /// Draws the door at the specified position.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch used to draw the door.</param>
        /// <param name="doorPosition">The position where the door should be drawn.</param>
        public void Draw(SpriteBatch spriteBatch, Vector2 doorPosition)
        {
            Texture2D currentTexture = this.keyCollected ? this.openDoorTexture : this.closedDoorTexture;
            spriteBatch.Draw(currentTexture, doorPosition, Color.White);
        }

        /// <summary>
        /// Gets the bounding rectangle of the door.
        /// </summary>
        /// <param name="doorPosition">The position of the door.</param>
        /// <returns>The bounding rectangle of the door.</returns>
        public Rectangle GetBounds(Vector2 doorPosition)
        {
            Texture2D currentTexture = this.keyCollected ? this.openDoorTexture : this.closedDoorTexture;
            return new Rectangle((int)doorPosition.X, (int)doorPosition.Y, currentTexture.Width, currentTexture.Height);
        }

        private bool CheckCollision(Vector2 doorPosition)
        {
            Rectangle doorBounds = this.GetBounds(doorPosition);
            Rectangle playerBounds = this.player.Bounds;
            return doorBounds.Intersects(playerBounds);
        }
    }
}
