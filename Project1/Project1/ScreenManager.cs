// <copyright file="ScreenManager.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Project1
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
    using Microsoft.Xna.Framework.Input.Touch;
    using Project1.Entities;
    using Project1.Levels;

    /// <summary>
    /// Manages the different screens in the game, such as the main menu and level manager.
    /// </summary>
    public class ScreenManager
    {
        private readonly MainMenu mainMenu;
        private readonly LevelManager levelManager;
        private readonly InputManager inputManager;
        private readonly FirstLevel firstLevel;
        private readonly SecondLevel secondLevel;
        private readonly ThirdLevel thirdLevel;
        private readonly GameOverScreen gameOverScreen;
        private readonly LevelCompleteScreen levelCompleteScreen;
        private readonly PausedScreen pausedScreen;
        private readonly Player player;
        private readonly Skinshop skinshop;
        private readonly SpriteBatch spriteBatch;
        private readonly GraphicsDevice graphicsDevice;
        private readonly Obstacle obstacle;

        private object currentScreen;
        private object currentLevel;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScreenManager"/> class.
        /// </summary>
        /// <param name="graphic">The graphics device used to draw sprites.</param>
        /// <param name="content">The content manager used to load assets.</param>
        /// <param name="gameFont">The font used for game text.</param>
        /// <param name="inputManager">The input manager for handling user input.</param>
        /// <param name="platform">The platform object in the game.</param>
        /// <param name="player">The player object in the game.</param>
        /// <param name="obstacle">The obstacle object in the game.</param>
        public ScreenManager(GraphicsDevice graphic, ContentManager content, SpriteFont gameFont, InputManager inputManager, Platform platform, Player player, Obstacle obstacle)
        {
            this.graphicsDevice = graphic;
            this.spriteBatch = new SpriteBatch(this.graphicsDevice);
            this.obstacle = obstacle;

            this.player = player;

            this.mainMenu = new MainMenu(content, gameFont, inputManager);
            this.levelManager = new LevelManager(content, gameFont, inputManager);
            this.gameOverScreen = new GameOverScreen(content, gameFont, inputManager);
            this.levelCompleteScreen = new LevelCompleteScreen(content, gameFont, inputManager, player);
            this.pausedScreen = new PausedScreen(content, gameFont, inputManager, player);
            this.skinshop = new Skinshop(content, gameFont, inputManager, this.player);
            this.inputManager = inputManager;
            this.firstLevel = new FirstLevel(content, inputManager, this.player, this.levelCompleteScreen, this.obstacle);
            this.secondLevel = new SecondLevel(content, inputManager, this.player, this.levelCompleteScreen, this.obstacle);
            this.thirdLevel = new ThirdLevel(content, inputManager, this.player, this.levelCompleteScreen, this.obstacle);
            this.currentScreen = this.mainMenu;

            // Subscribe to MainMenu events
            this.SubscribeToEvents(content, inputManager);
        }

        /// <summary>
        /// Changes the current screen to the specified new screen.
        /// </summary>
        /// <param name="newScreen">The new screen to display.</param>
        /// <param name="inputManager">The input manager for handling user input.</param>
        public void ChangeScreen(object newScreen, InputManager inputManager)
        {
            this.currentScreen = newScreen;

            // Update currentLevel if the new screen is a level and reset player state
            switch (newScreen)
            {
                case FirstLevel:
                    this.obstacle.IsActive = false;
                    this.firstLevel.ResetForNewAttempt();
                    this.ResetLevel(newScreen);
                    break;
                case SecondLevel:
                    this.obstacle.IsActive = true;
                    this.secondLevel.ResetForNewAttempt();
                    this.ResetLevel(newScreen);
                    break;
                case ThirdLevel:
                    this.obstacle.IsActive = false;
                    this.thirdLevel.ResetForNewAttempt();
                    this.ResetLevel(newScreen);
                    break;
            }
        }

        /// <summary>
        /// Updates the current screen based on the game time, mouse state, screen width, and screen height.
        /// </summary>
        /// <param name="gameTime">The current game time.</param>
        /// <param name="mouseState">The current state of the mouse.</param>
        public void Update(GameTime gameTime, MouseState mouseState)
        {
            if (this.currentScreen is MainMenu mainMenu)
            {
                mainMenu.Update(mouseState);
            }
            else if (this.currentScreen is LevelManager levelManager)
            {
                levelManager.Update(mouseState);
            }
            else if (this.currentScreen is Skinshop skinshop)
            {
                skinshop.Update(mouseState, this.player);
            }
            else if (this.currentScreen is ILevel)
            {
                this.UpdateLevels(gameTime, mouseState);
            }
            else if (this.currentScreen is GameOverScreen gameOverScreen)
            {
                gameOverScreen.Update(mouseState);
            }
        }

        /// <summary>
        /// Updates the current level based on the game time and mouse state.
        /// </summary>
        /// <param name="gameTime">Time of the game to input levels.</param>
        /// <param name="mouseState">State of the mouse to track input.</param>
        public void UpdateLevels(GameTime gameTime, MouseState mouseState)
        {
            if (this.currentScreen is FirstLevel firstLevel)
            {
                firstLevel.Update(gameTime, mouseState);
                if (firstLevel.StageComplete)
                {
                    this.ChangeScreen(this.levelCompleteScreen, this.inputManager);
                    this.currentLevel = firstLevel;
                    firstLevel.StageComplete = false;
                }
            }
            else if (this.currentScreen is SecondLevel secondLevel)
            {
                secondLevel.Update(gameTime, mouseState);
                if (secondLevel.StageComplete)
                {
                    this.ChangeScreen(this.levelCompleteScreen, this.inputManager);
                    this.currentLevel = secondLevel;
                    secondLevel.StageComplete = false;
                }
            }
            else if (this.currentScreen is ThirdLevel thirdLevel)
            {
                thirdLevel.Update(gameTime, mouseState);
                if (thirdLevel.StageComplete)
                {
                    this.ChangeScreen(this.levelCompleteScreen, this.inputManager);
                    this.currentLevel = thirdLevel;
                    thirdLevel.StageComplete = false;
                }
            }
        }

        /// <summary>
        /// Draws the current screen.
        /// </summary>
        /// <param name="spriteBatch">Helper class for drawing text and sprites.</param>
        public void Draw()
        {
            this.graphicsDevice.Clear(Color.Black);

            this.spriteBatch.Begin();

            if (this.currentScreen is MainMenu mainMenu)
            {
                mainMenu.Draw(this.spriteBatch);
            }
            else if (this.currentScreen is LevelManager levelManager)
            {
                levelManager.Draw(this.spriteBatch);
            }
            else if (this.currentScreen is Skinshop skinshop)
            {
                skinshop.Draw(this.spriteBatch);
            }
            else if (this.currentScreen is ILevel)
            {
                this.DrawLevels(this.spriteBatch);
            }
            else if (this.currentScreen is GameOverScreen gameOverScreen)
            {
                gameOverScreen.Draw(this.spriteBatch);
            }
            else if (this.currentScreen is PausedScreen pausedScreen)
            {
                pausedScreen.Draw(this.spriteBatch);
            }
            else if (this.currentScreen is LevelCompleteScreen levelCompleteScreen)
            {
                levelCompleteScreen.Draw(this.spriteBatch);
            }

            this.spriteBatch.End();
        }

        private void DrawLevels(SpriteBatch spriteBatch)
        {
            if (this.currentScreen is FirstLevel firstLevel)
            {
                firstLevel.Draw(spriteBatch);
            }
            else if (this.currentScreen is SecondLevel secondLevel)
            {
                secondLevel.Draw(spriteBatch);
            }
            else if (this.currentScreen is ThirdLevel thirdLevel)
            {
                thirdLevel.Draw(spriteBatch);
            }
        }

        /// <summary>
        /// Continues the current level.
        /// </summary>
        private void ContinueLevel()
        {
            this.currentScreen = this.currentLevel;
        }

        /// <summary>
        /// Resets the current level.
        /// </summary>
        /// <param name="newScreen">The screen that's loaded onto the game.</param>
        private void ResetLevel(object newScreen)
        {
            this.currentLevel = newScreen;

            this.player.ResetForNewLevel();
        }

        private void SubscribeToEvents(ContentManager content, InputManager inputManager)
        {
            this.inputManager.OnPlayClicked += () => this.ChangeScreen(this.levelManager, inputManager);
            this.inputManager.OnLevelOneClicked += () => this.ChangeScreen(this.firstLevel, inputManager);
            this.inputManager.OnLevelTwoClicked += () => this.ChangeScreen(this.secondLevel, inputManager);
            this.inputManager.OnLevelThreeClicked += () => this.ChangeScreen(this.thirdLevel, inputManager);
            this.inputManager.OnRetryClicked += () => this.ChangeScreen(this.currentLevel, inputManager);
            this.inputManager.OnMainMenuClicked += () => this.ChangeScreen(this.mainMenu, inputManager);
            this.inputManager.OnSkinClicked += () => this.ChangeScreen(this.skinshop, inputManager);
            this.inputManager.OnPauseClicked += () => this.ChangeScreen(this.pausedScreen, inputManager);
            this.player.OnGameOver += () => this.ChangeScreen(this.gameOverScreen, inputManager);
            this.inputManager.OnContinueClicked += () => this.ContinueLevel();
            this.inputManager.OnNextLevelClicked += () => this.LoadNextLevel();
        }

        /// <summary>
        /// Loads the next level based on the current level.
        /// </summary>
        private void LoadNextLevel()
        {
            if (this.currentLevel == this.firstLevel && this.currentScreen == this.levelCompleteScreen)
            {
                this.ChangeScreen(this.secondLevel, this.inputManager);
                this.currentLevel = this.secondLevel;
            }
            else if (this.currentLevel == this.secondLevel && this.currentScreen == this.levelCompleteScreen)
            {
                this.ChangeScreen(this.thirdLevel, this.inputManager);
                this.currentLevel = this.thirdLevel;
            }
            else if (this.currentLevel == this.thirdLevel && this.currentScreen == this.levelCompleteScreen)
            {
                this.ChangeScreen(this.mainMenu, this.inputManager);
            }
        }
    }
}
