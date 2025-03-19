// <copyright file="Heart.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Project1.Entities
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// MDisplays the player's health on the screen.
    /// </summary>
    internal class Heart
    {
        private readonly Texture2D heartFilled;
        private readonly Texture2D heartEmpty;

        private readonly Player player;
        private readonly HealthManager healthManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="Heart"/> class.
        /// </summary>
        /// <param name="content">The content manager used to load textures.</param>
        /// <param name="player">The player whose health is being displayed.</param>
        /// <param name="healthManager">The health manager used to manage the player's health.</param>
        public Heart(ContentManager content, Player player, HealthManager healthManager)
        {
            this.heartFilled = content.Load<Texture2D>("heartFilled");
            this.heartEmpty = content.Load<Texture2D>("heartEmpty");
            this.player = player;
            this.healthManager = healthManager;
        }

        /// <summary>
        /// Draws the player's lives on the screen.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch used for drawing.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            int currentLives = this.healthManager.CurrentLives;
            int maxLives = this.healthManager.MaxLives;

            for (int i = 0; i < maxLives; i++)
            {
                if (i < currentLives)
                {
                    spriteBatch.Draw(this.heartFilled, new Vector2(10 + (i * 80), 10), Color.White);
                }
                else
                {
                    spriteBatch.Draw(this.heartEmpty, new Vector2(10 + (i * 80), 10), Color.White);
                }
            }
        }
    }
}