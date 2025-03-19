# Overview

**Project1** is a simple 2D game built using **MonoGame**, where the player must collect a key to open a door and progress through different stages. The game features a player character that can move, jump, and interact with objects like keys and doors.

# Game Features
- **Player Movement:** The player can move left and right, and jump using keyboard input.
- **Key Collection:** The player can collect a key, which allows them to open the door.
- **Door Mechanism:** A door that opens once the player collects the key.
- **Stages:** The game can load different stages (currently only "Stage1").
- **Input Handling:** Custom input manager for handling player actions like movement and jumping.

# Project Structure

## GameManager.cs
#### Overview
The GameManager class is the core component of the game, managing the game's main logic, input, screens, and levels. It extends the Game class from the Microsoft.Xna.Framework library and acts as the central point for initializing, updating, and drawing the game components. The class handles screen resolution, input management, and controls the flow between different levels and screens (e.g., Main Menu, levels, etc.).

## InputManager.cs
#### Overview
The InputManager class is designed to manage user input through both the mouse and keyboard in a game environment. It handles events for mouse clicks on buttons (such as Play, Create, and Level Selection) and keyboard key presses (such as movement and jumping). The class is event-driven, allowing external components to subscribe to input events and handle them accordingly.
#### Features
- Mouse Input: Detects mouse clicks on specific UI buttons and triggers corresponding events.
- Keyboard Input: Handles movement and jump actions using specific keys (A/Left Arrow for left movement, D/Right Arrow for right movement, and Space for jumping).
- Event-Driven: External components can subscribe to events such as button clicks and key presses.
#### Events
- OnPlayClicked: Triggered when the Play button is clicked.
- OnCreateClicked: Triggered when the Create button is clicked.
- OnLevelOneClicked: Triggered when the Level One button is clicked.
- OnLevelTwoClicked: Triggered when the Level Two button is clicked.
- OnLevelThreeClicked: Triggered when the Level Three button is clicked.
- OnLevelFourClicked: Triggered when the Level Four button is clicked.
- OnMoveLeftClicked: Triggered when the user presses the "A" or "Left Arrow" key.
- OnMoveRightClicked: Triggered when the user presses the "D" or "Right Arrow" key.
- OnSpaceClicked: Triggered when the user presses the "Space" key.
#### Methods
- **Update(GameTime gameTime, MouseState mouseState)**
Updates the input manager by checking the keyboard state and mouse state.
- **HandleMouseInput(MouseState mouseState)**
Handles mouse input to detect button clicks and trigger corresponding events.
- **HandleKeyboardInput()**
Handles keyboard input for movement and actions (e.g., moving left, moving right, jumping) and triggers the associated events.

## ScreenManager.cs
#### Overview
The ScreenManager class is responsible for managing the different screens in the game, such as the main menu and various levels. It allows for smooth transitions between screens and centralizes the logic for updating and drawing each screen. It integrates with the InputManager to handle user inputs and change the active screen accordingly.
#### Features
- Screen Transitions: Facilitates switching between different screens like the main menu and the levels.
- Dynamic Screen Updates: Based on user input, the class updates the currently active screen (e.g., MainMenu, LevelManager, FirstLevel, etc.).
- Drawing Logic: Draws the active screen to the game window using the SpriteBatch.
- Event-Driven: Responds to user input events (e.g., button clicks) to change screens.
#### Properties
currentScreen: Holds the current screen that is being displayed. It can be any of the following: MainMenu, LevelManager, FirstLevel, SecondLevel, or ThirdLevel.
#### Event Handling
The ScreenManager subscribes to several events in the InputManager to handle user input:
- OnPlayClicked: Changes the screen to the LevelManager.
- OnLevelOneClicked: Switches the screen to the FirstLevel.
- OnLevelTwoClicked: Switches the screen to the SecondLevel.
- OnLevelThreeClicked: Switches the screen to the ThirdLevel.

## LevelManager.cs
#### Overview
The LevelManager class is responsible for managing the levels in the game, particularly by displaying level selection buttons and handling user input to select levels. It renders the level buttons and associated labels on the screen, allowing the player to choose a level to play.
#### Features
- Level Button Display: Draws buttons on the screen for each available level (e.g., Level 1, Level 2, Level 3, Level 4).
- User Input Handling: Tracks mouse input to detect which level button the player clicks, allowing them to choose a level.
- Level Button Coordinates: Each level button is positioned on the screen using defined coordinates for better control over the layout.

## Entities

### Player.cs
#### Overview
- Represents the player character.
- Handles movement and jumping logic.

### GameOverScreen.cs
#### Overview
- Controls the Game Over logic and displays the end screen with appropriate buttons. 
#### Features
- Player Integration: Subscribes to Player's death action, which triggers this screen
- 2 Buttons: The player can choose to either restart the stage or return to main menu.

### Skin.cs
#### Overview
- Loads all the skins, they will then replace the Player's sprite in Skinshop class.
#### Features
- Skin Attributes: Every skin a specific name and also an icon. Icon is the sprite of the skin.
- Skin selection: The selected skin is sent as an object to Skinshop, where it replaces the Player's sprite.

### Skinshop.cs
#### Overview
- Allows the user to change from one skin to another, also keeping it consistent between levels until it is changed again.
#### Features
- 4 Different Skins: The default skin, Detective, Cyberpunk and Santa.
- Applies the Skins: Replaces the player's sprite with the selected skin sprite.

### Lava.cs
#### Overview
- Represents lava platforms in the game. It is responsible for creating and rendering a series of lava platforms and detecting collisions with the player or other entities.
#### Features
- Platform Generation: Dynamically creates multiple lava platforms based on a starting position and specified count.
- Collision Detection: Checks for collisions between lava platforms and player boundaries.
- Dynamic Rendering: Efficiently renders all lava platforms using a texture

### Key.cs
#### Overview
- Represents a collectible key in the game. Players can collect the key by moving within a specific proximity, after which the key is marked as collected and no longer appears on the screen.
#### Features
- Collectible Object: Implements logic for a key that players can collect during gameplay.
- Pickup Radius: The player must be within a defined radius (50 units) to collect the key.
- Dynamic Rendering: Only draws the key when it has not been collected.
- Player Interaction: Updates its state based on the player's position.

### Heart.cs
#### Overview
- Displays the player's health as a series of filled or empty heart icons on the screen. It visually represents the player's current lives compared to their maximum lives.
#### Features
- Health Visualization: Dynamically renders filled and empty hearts based on the player's current and maximum lives.
- Integration with Player Class: Reads the player's health values from the Player class.
- Customizable Positioning: Hearts are displayed horizontally, with spacing between them.

### Obstacle.cs
#### Overview
- Represents an obstacle in the game. It manages obstacle properties, interactions with the player, and drawing obstacles on the screen.
#### Features
- Collision Detection: Checks if the obstacle collides with the player based on position and distance thresholds.
- Obstacle Creation: Supports creating different types of obstacles (e.g., "spikeLong" and "spikeShort") with distinct thresholds.
- Dynamic Positioning: Allows the obstacle's position to be updated dynamically, which also updates its bounding box.
- Rendering: Renders all created obstacles on the screen using appropriate textures.

### Door.cs
#### Overview
- Represents a door object in the game, which can either be open or closed based on whether the player has collected the required key. The door uses two textures: one for the closed state and one for the open state. 
- Handles the door's visual state and updates it based on the player's actions (such as collecting a key).
#### Features
- State Management: The door toggles between open and closed states based on whether the key has been collected.
- Drawing: The door is drawn at a specified position using the appropriate texture (either closed or open).
- Integration with Key: The door depends on the collection of a key to transition between states.
#### Properties
- keyCollected: A boolean flag indicating whether the player has collected the key (used to toggle between open and closed states).
- closedDoorTexture: The texture used to display the door when it is closed.
- openDoorTexture: The texture used to display the door when it is open.
- key: The Key object associated with the door, which tracks whether the key has been collected

### Platform.cs
#### Overview
- Represents a platform in the game, which can be used by the player as a surface to interact with. The platform's functionality includes generating multiple platforms, checking collisions between the player and platforms, and rendering the platform texture in the game world.
#### Features
- Platform Creation: Can generate a series of platforms with specified starting positions and spacing.
- Collision Detection: Checks if a player’s rectangle intersects with any of the platforms, helping to manage platform-based interactions (e.g., jumping or landing).
- Rendering: Draws the platforms using a texture loaded from the content pipeline.

## Levels

# Controls

- **Left Arrow / A**: Move left.
- **Right Arrow / D**: Move right.
- **Spacebar**: Jump.