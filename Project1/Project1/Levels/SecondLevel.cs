// <copyright file="SecondLevel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Project1.Levels
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
    using Project1;
    using Project1.Entities;

    /// <summary>
    /// Represents the second level of the game.
    /// </summary>
    public class SecondLevel : ILevel
    {
        private readonly int doorXCoordinate = 1170;
        private readonly int doorYCoordintae = 532;
        private readonly int keyXCoordinate = 536;
        private readonly int keyYCoordinate = 450;

        private readonly int firstPlatformXCoordinate = 0;
        private readonly int firstPlatformYCoordinate = 656;
        private readonly int secondPlatformXCoordinate = 450;
        private readonly int secondPlatformYCoordinate = 480;
        private readonly int shortSpikeXCoordinate = 300;
        private readonly int shortSpikeYCoordinate = 632;
        private readonly int longSpikeXCoordinate = 800;
        private readonly int longSpikeYCoordinate = 608;

        private Vector2 doorPosition;
        private Vector2 keyPosition;
        private Vector2 firstPlatformPosition;
        private Vector2 secondPlatformPosition;
        private Vector2 longSpikePosition;
        private Vector2 shortSpikePosition;

        private Door door;
        private Key key;
        private Player player;
        private InputManager inputManager;
        private Platform platform;
        private Obstacle obstacle;
        private Heart heart;
        private Lava lava;
        private Texture2D pauseButton;
        private LevelCompleteScreen levelCompleteScreen;

        /// <summary>
        /// Initializes a new instance of the <see cref="SecondLevel"/> class.
        /// </summary>
        /// <param name="content">The content manager to load resources.</param>
        /// <param name="inputManager">The input manager to handle input.</param>
        /// <param name="player">The player object in the game.</param>
        /// <param name="levelCompleteScreen">The game over screen to display when the player dies.</param>
        /// <param name="obstacle">The obstacle object in the game.</param>
        public SecondLevel(ContentManager content, InputManager inputManager, Player player, LevelCompleteScreen levelCompleteScreen, Obstacle obstacle)
        {
            this.obstacle = obstacle;
            this.player = player;
            this.inputManager = inputManager;
            this.doorPosition = new Vector2(this.doorXCoordinate, this.doorYCoordintae);
            this.keyPosition = new Vector2(this.keyXCoordinate, this.keyYCoordinate);
            this.firstPlatformPosition = new Vector2(this.firstPlatformXCoordinate, this.firstPlatformYCoordinate);
            this.secondPlatformPosition = new Vector2(this.secondPlatformXCoordinate, this.secondPlatformYCoordinate);
            this.longSpikePosition = new Vector2(this.longSpikeXCoordinate, this.longSpikeYCoordinate);
            this.shortSpikePosition = new Vector2(this.shortSpikeXCoordinate, this.shortSpikeYCoordinate);
            this.pauseButton = content.Load<Texture2D>("pauseButton");
            this.levelCompleteScreen = levelCompleteScreen;

            this.InitializeLevel(content, inputManager);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the stage is complete.
        /// </summary>
        public bool StageComplete { get; set; }

        /// <summary>
        /// Gets or sets the number of platforms in the level.
        /// </summary>
        public int NumberOfPlatforms { get; set; } = 20;

        /// <summary>
        /// Updates the game state for the second level.
        /// </summary>
        /// <param name="gameTime">The game time information.</param>
        /// <param name="mouseState">The current state of the mouse.</param>
        public void Update(GameTime gameTime, MouseState mouseState)
        {
            this.key.Update(this.player.CharacterPosition);
            this.door.Update(this.doorPosition);
            this.inputManager.Update(gameTime, mouseState);
            this.obstacle.Update(gameTime);

            this.player.Update(gameTime, this.platform, this.lava, this.obstacle);
            this.player.Clamp();

            if (this.door.DoorAccessed)
            {
                this.StageComplete = true;
            }
        }

        /// <summary>
        /// Draws the level elements.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch to draw textures.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (!this.door.DoorAccessed)
            {
                this.key.Draw(spriteBatch);
                this.platform.Draw(spriteBatch);
                this.obstacle.Draw(spriteBatch);
                this.door.Draw(spriteBatch, this.doorPosition);
                this.heart.Draw(spriteBatch);
                this.player.Draw(spriteBatch);
                spriteBatch.Draw(this.pauseButton, new Vector2(1222, 10), Color.White);
            }
            else if (this.door.DoorAccessed)
            {
                this.levelCompleteScreen.Draw(spriteBatch);
            }
        }

        /// <summary>
        /// Initializes the level elements.
        /// </summary>
        /// <param name="content">The content manager to load resources.</param>
        /// <param name="inputManager">The input manager to handle input.</param>
        public void InitializeLevel(ContentManager content, InputManager inputManager)
        {
            this.player.ResetForNewLevel();
            this.key = new Key(content, this.keyPosition);
            this.door = new Door(content, this.key, this.player);
            this.inputManager = inputManager;
            this.platform = new Platform(content);
            this.heart = new Heart(content, this.player, this.player.HealthManager);
            this.lava = new Lava(content);

            this.obstacle.CreateObstacle(this.shortSpikePosition, "spikeShort");
            this.obstacle.CreateObstacle(this.longSpikePosition, "spikeLong");

            this.platform.CreatePlatforms(this.NumberOfPlatforms, this.firstPlatformPosition);
            this.platform.CreatePlatforms(4, this.secondPlatformPosition);
        }

        /// <summary>
        /// Resets the level for a new attempt.
        /// </summary>
        public void ResetForNewAttempt()
        {
            this.key.KeyCollected = false;
            this.door.DoorAccessed = false;
        }
    }
}