// <copyright file="ThirdLevel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Project1.Levels
{
    using System.Diagnostics;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using Project1;
    using Project1.Entities;

    /// <summary>
    /// Represents the first level of the game.
    /// </summary>
    public class ThirdLevel : ILevel
    {
        private readonly int doorXPosition = 1170;
        private readonly int doorYPosition = 532;
        private readonly int keyXCoordinate = 1050;
        private readonly int keyYCoordinate = 100;

        private readonly int firstPlatformXPosition = 0;
        private readonly int firstPlatformYPosition = 656;
        private readonly int secondPlatformXPosition = 250;
        private readonly int secondPlatformYPosition = 500;
        private readonly int thirdPlatformXPosition = 1152;
        private readonly int thirdPlatformYPosition = 656;
        private readonly int fourthPlatformXPosition = 550;
        private readonly int fourthPlatformYPosition = 400;
        private readonly int fifthPlatformXPosition = 800;
        private readonly int fifthPlatformYPosition = 300;

        private readonly int lavaXPosition = 128;
        private readonly int lavaYPosition = 656;

        private Vector2 doorPosition;
        private Vector2 keyPosition;
        private Vector2 firstPlatformPosition;
        private Vector2 secondPlatformPosition;
        private Vector2 thirdPlatformPosition;
        private Vector2 fourthPlatformPosition;
        private Vector2 fifthPlatformPosition;
        private Vector2 lavaPosition;

        private Door door;
        private Key key;
        private Player player;
        private Lava lava;
        private InputManager inputManager;
        private Platform platform;
        private Obstacle obstacle;
        private Heart heart;
        private Texture2D pauseButton;
        private LevelCompleteScreen levelCompleteScreen;

        /// <summary>
        /// Initializes a new instance of the <see cref="ThirdLevel"/> class.
        /// </summary>
        /// <param name="content">The content manager to load resources.</param>
        /// <param name="inputManager">The input manager to handle input.</param>
        /// <param name="player">The player object in the game.</param>
        /// <param name="obstacle">The obstacle object in the game.</param>
        /// <param name="levelCompleteScreen">The game over screen to display when the player dies.</param>
        public ThirdLevel(ContentManager content, InputManager inputManager, Player player, LevelCompleteScreen levelCompleteScreen, Obstacle obstacle)
        {
            this.player = player;
            this.obstacle = obstacle;

            this.doorPosition = new Vector2(this.doorXPosition, this.doorYPosition);
            this.keyPosition = new Vector2(this.keyXCoordinate, this.keyYCoordinate);
            this.firstPlatformPosition = new Vector2(this.firstPlatformXPosition, this.firstPlatformYPosition);
            this.secondPlatformPosition = new Vector2(this.secondPlatformXPosition, this.secondPlatformYPosition);
            this.thirdPlatformPosition = new Vector2(this.thirdPlatformXPosition, this.thirdPlatformYPosition);
            this.fourthPlatformPosition = new Vector2(this.fourthPlatformXPosition, this.fourthPlatformYPosition);
            this.fifthPlatformPosition = new Vector2(this.fifthPlatformXPosition, this.fifthPlatformYPosition);
            this.lavaPosition = new Vector2(this.lavaXPosition, this.lavaYPosition);
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
        public int NumberOfPlatforms { get; set; } = 2;

        /// <summary>
        /// Gets or sets the number of obstacles in the level.
        /// </summary>
        public int NumberOfObstacles { get; set; } = 2;

        /// <summary>
        /// Gets or sets the X position of the platform.
        /// </summary>
        public int EdgeLenght { get; set; } = 64;

        /// <summary>
        /// Gets or sets the X position of the platform.
        /// </summary>
        public int ObstacleXCoordinate { get; set; }

        /// <summary>
        /// Gets or sets the Y position of the platform.
        /// </summary>
        public int ObstacleYCoordinate { get; set; }

        /// <summary>
        /// Updates the game state for the third level.
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
                this.door.Draw(spriteBatch, this.doorPosition);
                this.heart.Draw(spriteBatch);
                this.lava.Draw(spriteBatch);
                this.player.Draw(spriteBatch);
                spriteBatch.Draw(this.pauseButton, new Vector2(1222, 10), Color.White);
            }
            else if (this.door.DoorAccessed)
            {
                this.levelCompleteScreen.Draw(spriteBatch);
            }
        }

        /// <summary>
        /// Initializes the level with the required entities.
        /// </summary>
        /// <param name="content">Content Manager to handle the loaded content.</param>
        /// <param name="inputManager">Input Manager to handle user input.</param>
        public void InitializeLevel(ContentManager content, InputManager inputManager)
        {
            this.player.ResetForNewLevel();
            this.key = new Key(content, this.keyPosition);
            this.door = new Door(content, this.key, this.player);
            this.inputManager = inputManager;
            this.platform = new Platform(content);
            this.heart = new Heart(content, this.player, this.player.HealthManager);
            this.lava = new Lava(content);

            this.obstacle.IsActive = false;

            this.platform.CreatePlatforms(this.NumberOfPlatforms, this.firstPlatformPosition);
            this.lava.CreateLava(16, this.lavaPosition);
            this.platform.CreatePlatforms(2, this.secondPlatformPosition);
            this.platform.CreatePlatforms(this.NumberOfPlatforms, this.thirdPlatformPosition);
            this.platform.CreatePlatforms(1, this.fourthPlatformPosition);
            this.platform.CreatePlatforms(1, this.fifthPlatformPosition);
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