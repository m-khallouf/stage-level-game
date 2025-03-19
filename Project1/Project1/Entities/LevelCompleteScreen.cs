// <copyright file="LevelCompleteScreen.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Project1.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using Project1;

    /// <summary>
    /// Represents the main menu of the game.
    /// </summary>
    public class LevelCompleteScreen
    {
        private readonly Texture2D retryButton;
        private readonly Texture2D mainMenuButton;
        private readonly Texture2D nextLevelButton;
        private readonly SpriteFont gameFont;
        private readonly InputManager inputManager;
        private readonly Player player;

        private readonly int buttonXCoordinate = 550;
        private readonly int fontXCoordinate = 590;

        private readonly int nextLevelButtonYCoordinate = 300;
        private readonly int nextLevelFontYCoordinate = 315;

        private readonly int retryButtonYCoordinate = 500;
        private readonly int retryfontYCoordinate = 515;

        private readonly int mainMenuButtonYCoordinate = 600;
        private readonly int mainMenuFontYCoordinate = 615;

        private readonly int gameOverFontXCoordinate = 590;
        private readonly int gameOverFontYCoordinate = 50;

        /// <summary>
        /// Initializes a new instance of the <see cref="LevelCompleteScreen"/> class.
        /// </summary>
        /// <param name="content">The content manager used to load assets.</param>
        /// <param name="gameFont">The font used for the game text.</param>
        /// <param name="inputManager">The input manager for handling input.</param>
        /// <param name="player">The player object in the game.</param>
        public LevelCompleteScreen(ContentManager content, SpriteFont gameFont, InputManager inputManager, Player player)
        {
            this.retryButton = content.Load<Texture2D>("emptyButton");
            this.mainMenuButton = content.Load<Texture2D>("emptyButton");
            this.nextLevelButton = content.Load<Texture2D>("emptyButton");
            this.gameFont = gameFont;
            this.inputManager = inputManager;
            this.player = player;

            player.OnGameOver += () =>
            {
                this.PlayerIsDead = true;
            };
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is dead.
        /// </summary>
        public bool PlayerIsDead { get; set; } = false;

        /// <summary>
        /// Updates the main menu based on the current mouse state.
        /// </summary>
        /// <param name="mouseState">The current state of the mouse.</param>
        public void Update(MouseState mouseState)
        {
            // Handle the mouse input using the input manager
            this.inputManager.HandleMouseInput(mouseState);
        }

        /// <summary>
        /// Draws the main menu.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch used for drawing.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(this.gameFont, "Level Complete!", new Vector2(this.gameOverFontXCoordinate, this.gameOverFontYCoordinate), Color.White);

            spriteBatch.Draw(this.nextLevelButton, new Vector2(this.buttonXCoordinate, this.nextLevelButtonYCoordinate), Color.White);
            spriteBatch.DrawString(this.gameFont, "Next Level", new Vector2(this.fontXCoordinate, this.nextLevelFontYCoordinate), Color.White);

            spriteBatch.Draw(this.retryButton, new Vector2(this.buttonXCoordinate, this.retryButtonYCoordinate), Color.White);
            spriteBatch.DrawString(this.gameFont, "Retry", new Vector2(this.fontXCoordinate, this.retryfontYCoordinate), Color.White);

            spriteBatch.Draw(this.mainMenuButton, new Vector2(this.buttonXCoordinate, this.mainMenuButtonYCoordinate), Color.White);
            spriteBatch.DrawString(this.gameFont, "Main Menu", new Vector2(this.fontXCoordinate, this.mainMenuFontYCoordinate), Color.White);
        }
    }
}
