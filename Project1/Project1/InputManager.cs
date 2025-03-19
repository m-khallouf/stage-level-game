// <copyright file="InputManager.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Project1
{
    using System;
    using System.Diagnostics;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    /// <summary>
    /// Manages the input from the keyboard.
    /// </summary>
    public class InputManager
    {
        // private bool isFalling = false;
        private KeyboardState keyboardState;
        private Rectangle playButtonBounds;
        private Rectangle skinButtonBounds;
        private Rectangle retryButtonBounds;
        private Rectangle continueButtonBounds;
        private Rectangle returnButtonBounds;
        private Rectangle mainMenuButtonBounds;
        private Rectangle pauseButtonBounds;
        private Rectangle levelOneButtonBounds;
        private Rectangle levelTwoButtonBounds;
        private Rectangle levelThreeButtonBounds;
        private Rectangle nextLevelButtonBounds;

        /// <summary>
        /// Initializes a new instance of the <see cref="InputManager"/> class.
        /// </summary>
        public InputManager()
        {
            this.playButtonBounds = new Rectangle(350, 300, 155, 50);
            this.skinButtonBounds = new Rectangle(750, 300, 155, 50);
            this.retryButtonBounds = new Rectangle(550, 500, 155, 50);
            this.continueButtonBounds = new Rectangle(550, 400, 155, 50);
            this.mainMenuButtonBounds = new Rectangle(550, 600, 155, 50);
            this.nextLevelButtonBounds = new Rectangle(550, 300, 155, 50);
            this.pauseButtonBounds = new Rectangle(1220, 10, 50, 50);
            this.returnButtonBounds = new Rectangle(10, 10, 50, 50);
            this.levelOneButtonBounds = new Rectangle(50, 90, 239, 177);
            this.levelTwoButtonBounds = new Rectangle(500, 90, 239, 177);
            this.levelThreeButtonBounds = new Rectangle(950, 90, 239, 177);
        }

        /// <summary>
        /// Occurs when the play button is clicked.
        /// </summary>
        public event Action OnPlayClicked;

        /// <summary>
        /// Occurs when the retry button is clicked.
        /// </summary>
        public event Action OnRetryClicked;

        /// <summary>
        /// Occurs when the continue button is clicked.
        /// </summary>
        public event Action OnContinueClicked;

        /// <summary>
        /// Occurs when the main menu button is clicked.
        /// </summary>
        public event Action OnMainMenuClicked;

        /// <summary>
        /// Occurs when the skin button is clicked.
        /// </summary>
        public event Action OnSkinClicked;

        /// <summary>
        /// Occurs when the pause button is clicked.
        /// </summary>
        public event Action OnPauseClicked;

        /// <summary>
        /// Occurs when the level one button is clicked.
        /// </summary>
        public event Action OnLevelOneClicked;

        /// <summary>
        /// Occurs when the level two button is clicked.
        /// </summary>
        public event Action OnLevelTwoClicked;

        /// <summary>
        /// Occurs when the level three button is clicked.
        /// </summary>
        public event Action OnLevelThreeClicked;

        /// <summary>
        /// Occurs when the move left key is pressed.
        /// </summary>
        public event Action OnMoveLeftClicked;

        /// <summary>
        /// Occurs when the move right key is pressed.
        /// </summary>
        public event Action OnMoveRightClicked;

        /// <summary>
        /// Occurs when the space key is pressed.
        /// </summary>
        public event Action OnSpaceClicked;

        /// <summary>
        /// Occurs when the next level button is clicked.
        /// </summary>
        public event Action OnNextLevelClicked;

        /// <summary>
        /// Updates the input manager state.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        /// <param name="mouseState">The current state of the mouse.</param>
        public void Update(GameTime gameTime, MouseState mouseState)
        {
            this.keyboardState = Keyboard.GetState();
            this.HandleMouseInput(mouseState);
            this.HandleKeyboardInput();
        }

        /// <summary>
        /// Handles mouse input and triggers corresponding events when buttons are clicked.
        /// </summary>
        /// <param name="mouseState">The current state of the mouse.</param>
        public void HandleMouseInput(MouseState mouseState)
        {
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                switch (mouseState.Position)
                {
                    case var position when this.playButtonBounds.Contains(position):
                        this.OnPlayClicked?.Invoke();
                        break;
                    case var position when this.pauseButtonBounds.Contains(position):
                        this.OnPauseClicked?.Invoke();
                        break;
                    case var position when this.skinButtonBounds.Contains(position):
                        this.OnSkinClicked?.Invoke();
                        break;
                    case var position when this.retryButtonBounds.Contains(position):
                        this.OnRetryClicked?.Invoke();
                        break;
                    case var position when this.returnButtonBounds.Contains(position):
                        this.OnMainMenuClicked?.Invoke();
                        break;
                    case var position when this.continueButtonBounds.Contains(position):
                        this.OnContinueClicked?.Invoke();
                        break;
                    case var position when this.mainMenuButtonBounds.Contains(position):
                        this.OnMainMenuClicked?.Invoke();
                        break;
                    case var position when this.levelOneButtonBounds.Contains(position):
                        this.OnLevelOneClicked?.Invoke();
                        break;
                    case var position when this.levelTwoButtonBounds.Contains(position):
                        this.OnLevelTwoClicked?.Invoke();
                        break;
                    case var position when this.levelThreeButtonBounds.Contains(position):
                        this.OnLevelThreeClicked?.Invoke();
                        break;
                    case var position when this.nextLevelButtonBounds.Contains(position):
                        this.OnNextLevelClicked?.Invoke();
                        break;
                }
            }
        }

        /// <summary>
        /// Handles key input and triggers corresponding events when buttons are clicked.
        /// </summary>
        /// <param name="mouseState">The current state of the mouse.</param>
        public void HandleKeyboardInput()
        {
            var keyboardState = this.GetKeyboardState();

            if (keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left))
            {
                this.OnMoveLeftClicked?.Invoke();
            }

            if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right))
            {
                this.OnMoveRightClicked?.Invoke();
            }

            if (keyboardState.IsKeyDown(Keys.Space))
            {
                this.OnSpaceClicked?.Invoke();
            }
        }

        /// <summary>
        /// For testing.
        /// </summary>
        /// <returns> Keyboard State. </returns>
        public virtual KeyboardState GetKeyboardState()
        {
            return Keyboard.GetState();
        }
    }
}
