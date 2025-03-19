// <copyright file="MainMenu.cs" company="PlaceholderCompany">
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

    /// <summary>
    /// Represents the main menu of the game.
    /// </summary>
    public class MainMenu
    {
        private readonly Texture2D playButton;
        private readonly Texture2D skinButton;
        private readonly SpriteFont gameFont;
        private readonly InputManager inputManager;

        private readonly int playButtonXCoordinate = 350;
        private readonly int skinButtonXCoordinate = 750;
        private readonly int buttonYCoordinate = 300;

        private readonly int playFontXCoordinate = 400;
        private readonly int skinFontXCoordinate = 800;
        private readonly int fontYCoordinate = 315;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainMenu"/> class.
        /// </summary>
        /// <param name="content">The content manager used to load assets.</param>
        /// <param name="gameFont">The font used for the game text.</param>
        /// <param name="inputManager">The input manager for handling input.</param>
        public MainMenu(ContentManager content, SpriteFont gameFont, InputManager inputManager)
        {
            this.playButton = content.Load<Texture2D>("emptyButton");
            this.skinButton = content.Load<Texture2D>("emptyButton");
            this.gameFont = gameFont;
            this.inputManager = inputManager;
        }

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
            spriteBatch.Draw(this.playButton, new Vector2(this.playButtonXCoordinate, this.buttonYCoordinate), Color.White);
            spriteBatch.DrawString(this.gameFont, "Play", new Vector2(this.playFontXCoordinate, this.fontYCoordinate), Color.White);

            spriteBatch.Draw(this.skinButton, new Vector2(this.skinButtonXCoordinate, this.buttonYCoordinate), Color.White);
            spriteBatch.DrawString(this.gameFont, "Skins", new Vector2(this.skinFontXCoordinate, this.fontYCoordinate), Color.White);
        }
    }
}
