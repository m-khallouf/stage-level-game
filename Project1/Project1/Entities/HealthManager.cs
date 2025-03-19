// <copyright file="HealthManager.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Project1.Entities
{
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Manages the health of the player, including the current number of lives and maximum lives.
    /// </summary>
    public class HealthManager
    {
        private readonly Player player;
        private int currentLives;

        /// <summary>
        /// Initializes a new instance of the <see cref="HealthManager"/> class.
        /// </summary>
        /// <param name="player">The player whose health is managed.</param>
        public HealthManager(Player player)
        {
            this.player = player;
            this.currentLives = this.MaxLives;
        }

        /// <summary>
        /// Gets the maximum number of lives the player can have.
        /// </summary>
        public int MaxLives { get; } = 3;

        /// <summary>
        /// Gets or sets the current number of lives the player has.
        /// </summary>
        public int CurrentLives
        {
            get => this.currentLives;
            set => this.currentLives = MathHelper.Clamp(value, 0, this.MaxLives);
        }

        /// <summary>
        /// Resets the player's health to the maximum number of lives.
        /// </summary>
        public void ResetHealth() => this.currentLives = this.MaxLives;

        /// <summary>
        /// Reduces the player's current lives by one, if they have any remaining.
        /// </summary>
        public void ReduceLife()
        {
            if (this.currentLives > 0)
            {
                this.currentLives--;
            }
        }
    }
}
