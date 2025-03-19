// <copyright file="LevelManager.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Project1
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
    using Project1.Entities;

    /// <summary>
    /// Manages the levels in the game, including drawing level buttons and labels.
    /// </summary>
    public class LevelManager
    {
        private readonly Texture2D levelButton;
        private readonly Texture2D mainMenuButton;
        private readonly SpriteFont gameFont;
        private readonly InputManager inputManager;

        // level button Coordinates
        private readonly int levelOneButtonXCoordinate = 50;
        private readonly int levelTwoButtonXCoordinate = 500;
        private readonly int levelThreeButtonXCoordinate = 950;
        private readonly int levelButtonYCoordinate = 90;
        private readonly int mainMenuButtonXCoordinate = 10;
        private readonly int mainMenubuttonYCoordinate = 10;

        // font Coordinates
        private readonly int levelOneFontXCoordinate = 140;
        private readonly int levelTwoFontXCoordinate = 590;
        private readonly int levelThreeFontXCoordinate = 1040;
        private readonly int levelFontYCoordinate = 160;

        /// <summary>
        /// Initializes a new instance of the <see cref="LevelManager"/> class.
        /// </summary>
        /// <param name="content">The content manager used to load assets.</param>
        /// <param name="gameFont">The font used for drawing text.</param>
        /// <param name="inputManager">The input manager for handling user input.</param>
        /// <param name="screenWidth">The width of the screen.</param>
        /// <param name="screenHeight">The height of the screen.</param>
        /// <param name="platform">The platform object used in the game.</param>
        /// <param name="gameOverScreen">The game over screen object used in the game.</param>
        public LevelManager(ContentManager content, SpriteFont gameFont, InputManager inputManager)
        {
            this.levelButton = content.Load<Texture2D>("levelButton");
            this.mainMenuButton = content.Load<Texture2D>("returnButton");
            this.gameFont = gameFont;
            this.inputManager = inputManager;
        }

        /// <summary>
        /// Draws the level buttons and labels.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch used for drawing.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.mainMenuButton, new Vector2(this.mainMenuButtonXCoordinate, this.mainMenubuttonYCoordinate), Color.White);

            spriteBatch.Draw(this.levelButton, new Vector2(this.levelOneButtonXCoordinate, this.levelButtonYCoordinate), Color.White);
            spriteBatch.DrawString(this.gameFont, "Level 1", new Vector2(this.levelOneFontXCoordinate, this.levelFontYCoordinate), Color.White);

            spriteBatch.Draw(this.levelButton, new Vector2(this.levelTwoButtonXCoordinate, this.levelButtonYCoordinate), Color.White);
            spriteBatch.DrawString(this.gameFont, "Level 2", new Vector2(this.levelTwoFontXCoordinate, this.levelFontYCoordinate), Color.White);

            spriteBatch.Draw(this.levelButton, new Vector2(this.levelThreeButtonXCoordinate, this.levelButtonYCoordinate), Color.White);
            spriteBatch.DrawString(this.gameFont, "Level 3", new Vector2(this.levelThreeFontXCoordinate, this.levelFontYCoordinate), Color.White);
        }

        /// <summary>
        /// Updates the level manager state based on the game time and mouse state.
        /// </summary>
        /// <param name="mouseState">The current state of the mouse.</param>
        internal void Update(MouseState mouseState)
        {
            this.inputManager.HandleMouseInput(mouseState);
        }
    }
}