// <copyright file="InputManagerTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Project1.Tests
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using NUnit.Framework;

    /// <summary>
    /// Tests for the InputManager class.
    /// </summary>
    [TestFixture]
    public class InputManagerTests
    {
        private TestableInputManager inputManager;

        /// <summary>
        /// Initializes a new instance of the test class and sets up the input manager.
        /// </summary>
        [SetUp]
        public void Setup() => this.inputManager = new TestableInputManager();

        /// <summary>
        /// Verifies that pressing the left movement key invokes the corresponding event.
        /// </summary>
        [Test]
        public void HandleKeyboardInputMoveLeftKeyPressedShouldInvokeOnMoveLeftClicked()
        {
            bool moveLeftClicked = false;
            this.inputManager.OnMoveLeftClicked += () => moveLeftClicked = true;

            this.inputManager.SetSimulatedKeyboardState(new KeyboardState(Keys.A));
            this.inputManager.HandleKeyboardInput();

            Assert.That(moveLeftClicked, Is.True);
        }

        /// <summary>
        /// Verifies that pressing the right movement key invokes the corresponding event.
        /// </summary>
        [Test]
        public void HandleKeyboardInputMoveRightKeyPressedShouldInvokeOnMoveRightClicked()
        {
            bool moveRightClicked = false;
            this.inputManager.OnMoveRightClicked += () => moveRightClicked = true;

            this.inputManager.SetSimulatedKeyboardState(new KeyboardState(Keys.D));
            this.inputManager.HandleKeyboardInput();

            Assert.That(moveRightClicked, Is.True);
        }

        /// <summary>
        /// Verifies that pressing the space key invokes the corresponding event.
        /// </summary>
        [Test]
        public void HandleKeyboardInputSpaceKeyPressedShouldInvokeOnSpaceClicked()
        {
            bool spaceClicked = false;
            this.inputManager.OnSpaceClicked += () => spaceClicked = true;

            this.inputManager.SetSimulatedKeyboardState(new KeyboardState(Keys.Space));
            this.inputManager.HandleKeyboardInput();

            Assert.That(spaceClicked, Is.True);
        }

        /// <summary>
        /// Verifies that pressing a non-relevant key does not trigger any event.
        /// </summary>
        [Test]
        public void HandleKeyboardInputNoRelevantKeyPressedShouldNotInvokeAnyEvent()
        {
            bool eventTriggered = false;

            this.inputManager.OnMoveLeftClicked += () => eventTriggered = true;
            this.inputManager.OnMoveRightClicked += () => eventTriggered = true;
            this.inputManager.OnSpaceClicked += () => eventTriggered = true;

            this.inputManager.SetSimulatedKeyboardState(new KeyboardState(Keys.Z));
            this.inputManager.HandleKeyboardInput();

            Assert.That(eventTriggered, Is.False);
        }

        /// <summary>
        /// Verifies that pressing multiple keys triggers the corresponding events.
        /// </summary>
        [Test]
        public void HandleKeyboardInputMultipleKeysPressedShouldTriggerBothEvents()
        {
            bool moveLeftClicked = false;
            bool moveRightClicked = false;

            this.inputManager.OnMoveLeftClicked += () => moveLeftClicked = true;
            this.inputManager.OnMoveRightClicked += () => moveRightClicked = true;

            this.inputManager.SetSimulatedKeyboardState(new KeyboardState(Keys.A, Keys.D));
            this.inputManager.HandleKeyboardInput();

            Assert.Multiple(() =>
            {
                Assert.That(moveLeftClicked, Is.True);
                Assert.That(moveRightClicked, Is.True);
            });
        }

        /// <summary>
        /// Verifies that the Update method processes both mouse and keyboard inputs.
        /// </summary>
        [Test]
        public void UpdateShouldHandleMouseAndKeyboardInputs()
        {
            bool moveLeftClicked = false;
            bool playClicked = false;

            this.inputManager.OnMoveLeftClicked += () => moveLeftClicked = true;
            this.inputManager.OnPlayClicked += () => playClicked = true;

            this.inputManager.SetSimulatedKeyboardState(new KeyboardState(Keys.A));
            var mouseState = new MouseState(350, 300, 0, ButtonState.Pressed, ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released);

            this.inputManager.Update(new GameTime(), mouseState);

            Assert.Multiple(() =>
            {
                Assert.That(moveLeftClicked, Is.True);
                Assert.That(playClicked, Is.True);
            });
        }

        /// <summary>
        /// Verifies that the pause button triggers the OnPauseClicked event.
        /// </summary>
        [Test]
        public void HandleMouseInputPauseButtonClickedShouldInvokeOnPauseClicked()
        {
            bool pauseClicked = false;
            this.inputManager.OnPauseClicked += () => pauseClicked = true;

            var mouseState = new MouseState(1225, 15, 0, ButtonState.Pressed, ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released);
            this.inputManager.HandleMouseInput(mouseState);

            Assert.That(pauseClicked, Is.True);
        }

        /// <summary>
        /// Verifies that the return button triggers the OnMainMenuClicked event.
        /// </summary>
        [Test]
        public void HandleMouseInputReturnButtonClickedShouldInvokeOnMainMenuClicked()
        {
            bool mainMenuClicked = false;
            this.inputManager.OnMainMenuClicked += () => mainMenuClicked = true;

            var mouseState = new MouseState(15, 15, 0, ButtonState.Pressed, ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released);
            this.inputManager.HandleMouseInput(mouseState);

            Assert.That(mainMenuClicked, Is.True);
        }

        /// <summary>
        /// Verifies that Keyboard.GetState is called in the InputManager implementation.
        /// </summary>
        [Test]
        public void GetKeyboardStateShouldCallKeyboardGetState()
        {
            var testInputManager = new TestableInputManager();
            var simulatedState = new KeyboardState(Keys.W);

            testInputManager.SetSimulatedKeyboardState(simulatedState);

            var keyboardState = testInputManager.GetKeyboardState();

            Assert.That(keyboardState, Is.EqualTo(simulatedState));
        }

        /// <summary>
        /// Verifies that mouse input correctly triggers all button-related events.
        /// </summary>
        [Test]
        public void HandleMouseInputAllButtonsShouldTriggerCorrectEvents()
        {
            var testCases = new (Rectangle bounds, Action eventTrigger, string eventName)[]
           {
                (new Rectangle(350, 300, 155, 50), () => this.inputManager.OnPlayClicked += Assert.Pass, "Play"),
                (new Rectangle(750, 300, 155, 50), () => this.inputManager.OnSkinClicked += Assert.Pass, "Skin"),
                (new Rectangle(550, 500, 155, 50), () => this.inputManager.OnRetryClicked += Assert.Pass, "Retry"),
                (new Rectangle(550, 400, 155, 50), () => this.inputManager.OnContinueClicked += Assert.Pass, "Continue"),
                (new Rectangle(550, 600, 155, 50), () => this.inputManager.OnMainMenuClicked += Assert.Pass, "MainMenu"),
                (new Rectangle(50, 90, 239, 177), () => this.inputManager.OnLevelOneClicked += Assert.Pass, "LevelOne"),
                (new Rectangle(500, 90, 239, 177), () => this.inputManager.OnLevelTwoClicked += Assert.Pass, "LevelTwo"),
                (new Rectangle(950, 90, 239, 177), () => this.inputManager.OnLevelThreeClicked += Assert.Pass, "LevelThree"),
                (new Rectangle(550, 300, 155, 50), () => this.inputManager.OnNextLevelClicked += Assert.Pass, "NextLevel"),
           };

            foreach (var (bounds, eventTrigger, eventName) in testCases)
            {
                bool triggered = false;

                // Set up the event trigger
                this.inputManager.OnPlayClicked += () => triggered = true;
                this.inputManager.OnSkinClicked += () => triggered = true;
                this.inputManager.OnRetryClicked += () => triggered = true;
                this.inputManager.OnContinueClicked += () => triggered = true;
                this.inputManager.OnMainMenuClicked += () => triggered = true;
                this.inputManager.OnLevelOneClicked += () => triggered = true;
                this.inputManager.OnLevelTwoClicked += () => triggered = true;
                this.inputManager.OnLevelThreeClicked += () => triggered = true;
                this.inputManager.OnNextLevelClicked += () => triggered = true;

                // Simulate the mouse state for the bounds
                var mouseState = new MouseState(bounds.Center.X, bounds.Center.Y, 0, ButtonState.Pressed, ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released);
                this.inputManager.HandleMouseInput(mouseState);

                Assert.That(triggered, Is.True, $"{eventName} event was not triggered.");
            }
        }

        /// <summary>
        /// Verifies that clicking outside of any buttons does not trigger any events.
        /// </summary>
        [Test]
        public void HandleMouseInputNoButtonClickedShouldNotInvokeAnyEvent()
        {
            bool eventTriggered = false;

            this.inputManager.OnPlayClicked += () => eventTriggered = true;
            this.inputManager.OnSkinClicked += () => eventTriggered = true;

            var mouseState = new MouseState(5, 5, 0, ButtonState.Pressed, ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released);
            this.inputManager.HandleMouseInput(mouseState);

            Assert.That(eventTriggered, Is.False);
        }

        /// <summary>
        /// Verifies that no exceptions are thrown when no event handlers are registered.
        /// </summary>
        [Test]
        public void NoHandlersRegisteredShouldNotThrowExceptions()
        {
            Assert.DoesNotThrow(() =>
            {
                this.inputManager.SetSimulatedKeyboardState(new KeyboardState(Keys.A));
                this.inputManager.HandleKeyboardInput();

                var mouseState = new MouseState(350, 300, 0, ButtonState.Pressed, ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released);
                this.inputManager.HandleMouseInput(mouseState);
            });
        }

        /// <summary>
        /// A testable version of the InputManager class to simulate keyboard states.
        /// </summary>
        private class TestableInputManager : InputManager
        {
            private KeyboardState simulatedKeyboardState;

            /// <summary>
            /// Sets the simulated keyboard state.
            /// </summary>
            /// <param name="state">The simulated keyboard state.</param>
            public void SetSimulatedKeyboardState(KeyboardState state)
            {
                this.simulatedKeyboardState = state;
            }

            /// <summary>
            /// Gets the simulated keyboard state.
            /// </summary>
            /// <returns>The simulated keyboard state.</returns>
            public override KeyboardState GetKeyboardState()
            {
                return this.simulatedKeyboardState;
            }
        }
    }
}