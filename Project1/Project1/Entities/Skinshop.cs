// <copyright file="Skinshop.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Project1.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection.Metadata;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    /// <summary>
    /// Represents a shop that sells skins.
    /// </summary>
    internal class Skinshop
    {
        // private readonly Texture2D skin1Button;
        // private readonly Texture2D skin2Button;
        // private readonly Texture2D skin3Button;
        private readonly Texture2D returnButton;
        private readonly Texture2D skinDefaultButton;
        private readonly SpriteFont gameFont;
        private readonly InputManager inputManager;
        private readonly Player player;

        private readonly int buttonYCoordinate = 400;
        private readonly int buttonTextYCoordinate = 415;
        private readonly int skinsFontYCoordinate = 150;
        private readonly int startX = 200;
        private readonly int xSpacing = 300;
        private readonly int iconYCoordinate = 250;
        private readonly int returnButtonXCoordinate = 10;
        private readonly int returnButtonYCoordinate = 10;

        /// <summary>
        /// Initializes a new instance of the <see cref="Skinshop"/> class.
        /// </summary>
        /// <param name="content">Content Manager for loading textures.</param>
        /// <param name="gameFont">The font used for buttons.</param>
        /// <param name="inputManager">To check whether buttons are clicked or not.</param>
        /// <param name="player">The player instance.</param>
        public Skinshop(ContentManager content, SpriteFont gameFont, InputManager inputManager, Player player)
        {
            // this.skin1Button = content.Load<Texture2D>("emptyButton");
            // this.skin2Button = content.Load<Texture2D>("emptyButton");
            // this.skin3Button = content.Load<Texture2D>("emptyButton");
            this.player = player;
            this.returnButton = content.Load<Texture2D>("returnButton");
            this.skinDefaultButton = content.Load<Texture2D>("emptyButton");
            this.gameFont = gameFont;
            this.inputManager = inputManager;

            this.AvailableSkins = new List<Skin>
            {
                new ("Default", content.Load<Texture2D>("character")),
                new ("Detective", content.Load<Texture2D>("skin1")),
                new ("Cyberpunk", content.Load<Texture2D>("skin2")),
                new ("Santa", content.Load<Texture2D>("skin3")),
            };
        }

        /// <summary>
        /// Gets or sets the list of available skins in the shop.
        /// </summary>
        public List<Skin> AvailableSkins { get; set; }

        /// <summary>
        /// Changes the current screen to the specified screen.
        /// </summary>
        /// <param name="player">The Player instance.</param>
        /// <param name="skinIndex">The skin to be changed into.</param>
        public void ApplySkinToPlayer(Player player, int skinIndex)
        {
            if (skinIndex >= 0 && skinIndex < this.AvailableSkins.Count)
            {
                player.ChangeSkin(this.AvailableSkins[skinIndex].Icon);
            }
        }

        /*
        /// <summary>
        /// Purchases a skin for the specified player.
        /// </summary>
        /// <param name="skin">The skin to be purchased.</param>
        /// <param name="player">The player who is purchasing the skin.</param>
        /// <returns>True if the purchase is successful, otherwise false.</returns>
        public static bool PurchaseSkin(Skin skin, Player player)
        {
            // Implementation here
            return false;
        }
        */

        /// <summary>
        /// Updates the skinshop based on the current mouse state.
        /// </summary>
        /// <param name="mouseState">The current state of the mouse.</param>
        /// <param name="player">The player instance.</param>
        public void Update(MouseState mouseState, Player player)
        {
            this.inputManager.HandleMouseInput(mouseState);

            int xSpacing = 300; // Adjust spacing to match UI layout
            int startX = 200;

            for (int i = 0; i < this.AvailableSkins.Count; i++)
            {
                int x = startX + ((i % 4) * xSpacing); // Calculate button position
                Rectangle buttonRect = new Rectangle(x - 50, this.buttonYCoordinate, this.skinDefaultButton.Width, this.skinDefaultButton.Height);
                if (mouseState.LeftButton == ButtonState.Pressed && buttonRect.Contains(mouseState.Position))
                {
                    this.ApplySkinToPlayer(player, i);
                }
            }
        }

        /// <summary>
        /// Updates the skin shop based on the current mouse state.
        /// </summary>
        /// <param name="spriteBatch">The sprites of the buttons and characters.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < this.AvailableSkins.Count; i++)
            {
                var skin = this.AvailableSkins[i];
                int x = this.startX + ((i % 4) * this.xSpacing); // 3 skins per row

                // Draw skin icon (The 2 values are the width and height of the sprites)
                spriteBatch.Draw(skin.Icon, new Rectangle(x, this.iconYCoordinate, 45, 100), Color.White);

                spriteBatch.Draw(this.skinDefaultButton, new Rectangle(x - 50, this.buttonYCoordinate, this.skinDefaultButton.Width, this.skinDefaultButton.Height), Color.White);
                spriteBatch.DrawString(this.gameFont, "Apply Skin", new Vector2(x - 15, this.buttonTextYCoordinate), Color.White);

                // Draw skin name
                spriteBatch.DrawString(this.gameFont, skin.Name, new Vector2(x, this.skinsFontYCoordinate), Color.White);
            }

            spriteBatch.Draw(this.returnButton, new Vector2(this.returnButtonXCoordinate, this.returnButtonYCoordinate), Color.White);
        }
    }
}
