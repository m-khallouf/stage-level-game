// <copyright file="ILevel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace Project1.Levels
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    /// <summary>
    /// Represents a game level interface with methods to draw and update the level.
    /// </summary>
    internal interface ILevel
    {
        /// <summary>
        /// Draws the level using the specified sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch used to draw the level.</param>
        void Draw(SpriteBatch spriteBatch);

        /// <summary>
        /// Updates the level based on the game time and mouse state.
        /// </summary>
        /// <param name="gameTime">The game time information.</param>
        /// <param name="mouseState">The current state of the mouse.</param>
        void Update(GameTime gameTime, MouseState mouseState);
    }
}