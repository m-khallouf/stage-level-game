// <copyright file="Key.cs" company="PlaceholderCompany">
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
    /// Represents a key in the game that can be collected by the player.
    /// </summary>
    public class Key
    {
        private const int KeyPickupRadius = 70;
        private readonly Texture2D keyTexture;
        private Vector2 keyPosition;

        /// <summary>
        /// Initializes a new instance of the <see cref="Key"/> class.
        /// </summary>
        /// <param name="content">The content manager used to load the key texture.</param>
        /// <param name="keyPosition">The position of the key in the game world.</param>
        public Key(ContentManager content, Vector2 keyPosition)
        {
            this.keyTexture = content.Load<Texture2D>("key");
            this.KeyCollected = false;
            this.keyPosition = keyPosition;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the key has been collected.
        /// </summary>
        public bool KeyCollected { get; set; }

        /// <summary>
        /// Gets the key position.
        /// </summary>
        public Vector2 KeyPosition
        {
            get => this.keyPosition;
        }

        /// <summary>
        /// Updates the state of the key based on the player's current position.
        /// </summary>
        /// <param name="currentPlayerPosition">The current position of the player.</param>
        public void Update(Vector2 currentPlayerPosition)
        {
            if (Vector2.Distance(currentPlayerPosition, this.keyPosition) < KeyPickupRadius)
            {
                this.KeyCollected = true;
            }
        }

        /// <summary>
        /// Draws the key on the screen.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch used to draw the key.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (!this.KeyCollected)
            {
                spriteBatch.Draw(this.keyTexture, this.keyPosition, Color.White);
            }
        }
    }
}