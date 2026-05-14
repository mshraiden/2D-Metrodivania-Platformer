# 2D Metroidvania Platformer

This project is a 2D action-platformer / Metroidvania game built in Unity using C#. It was inspired by games like Hollow Knight, with a focus on smooth player movement, responsive combat, and real-time physics interactions.

The main goal of this project was to build a playable 2D game system from the ground up while learning how Unity handles physics, colliders, animations, player input, and combat mechanics. I focused on making the controls feel responsive and making the gameplay systems work together cleanly.

---

## Project Overview

In this project, I designed and programmed the core systems for a 2D Metroidvania-style game. The game includes movement, jumping, dashing, enemy interaction, hit detection, and combat mechanics. Most of the gameplay logic was written using custom C# scripts in Unity.

This project helped me understand how real-time games are structured, especially how player input, physics, collision detection, and animation timing all connect together.

---

## Key Features

- **Responsive Player Movement:** Built a movement system that supports walking, jumping, double jumping, and quick directional control.
- **Dash Mechanic:** Added a dash ability to make movement faster and more fluid.
- **Combat System:** Implemented directional attacks such as side attacks, upward attacks, and downward attacks.
- **Hit Detection:** Used Unity colliders and physics checks to detect when attacks hit enemies.
- **Enemy Damage System:** Created enemy health behavior so enemies can take damage from player attacks.
- **Knockback and Recoil:** Added feedback when enemies or the player are hit to make combat feel more realistic.
- **Pogo Bounce Mechanic:** Added a downward attack bounce mechanic, similar to Metroidvania-style platforming.
- **State Management:** Managed different player states such as idle, moving, jumping, attacking, and dashing.
- **Physics-Based Interaction:** Used Rigidbody2D and Collider2D components to handle real-time movement and collisions.

---

## Technologies Used

- Unity
- C#
- Unity 2D Physics
- Rigidbody2D
- Collider2D
- Animator
- Tilemaps
- Custom C# scripts

---

## What I Built

### Player Controller

The player controller handles the main movement of the character. This includes walking, jumping, falling, double jumping, and dashing. I focused on making the movement feel smooth and responsive so that the player can control the character accurately.

The movement system uses Unity physics and custom C# logic to update the player's position based on input and current state.

### Jumping and Movement Feel

I worked on improving the platforming feel by adding mechanics such as double jump, jump timing, and responsive input. These mechanics are important in a Metroidvania game because the player needs to move through platforms, avoid enemies, and react quickly during combat.

### Dash System

The dash system gives the player a short burst of speed. This was added to make movement more dynamic and to allow the player to dodge attacks or move quickly across gaps.

The dash mechanic also required state handling so that the player does not accidentally dash, attack, or jump in a way that breaks the movement system.

### Combat System

The combat system allows the player to attack in multiple directions. I implemented attacks that can hit enemies depending on where the player is aiming or moving.

The attacks use hitboxes and Unity collision detection to check whether an enemy is inside the attack range. When an enemy is hit, the enemy health system responds by applying damage.

### Enemy Interaction

Enemies can be hit by the player’s attacks and respond through health reduction and knockback. This made the combat feel more interactive instead of just being a visual animation.

This part helped me learn how to connect multiple systems together, such as player attacks, enemy colliders, damage values, and movement reactions.

### Pogo Bounce

I added a pogo bounce mechanic where the player can bounce upward after hitting an enemy or object with a downward attack. This mechanic is useful in Metroidvania games because it adds more depth to both combat and platforming.

This feature required checking attack direction, collision timing, and player velocity.

---

## Key Learning Goals

Through this project, I learned more about:

- Writing modular C# scripts in Unity
- Using Rigidbody2D for player movement
- Using Collider2D for hit detection
- Handling real-time player input
- Managing player states
- Connecting combat logic with enemy behavior
- Designing responsive game mechanics
- Debugging physics-based movement issues
- Building a project that combines software logic with user experience

---

## Why This Project Matters

This project helped me understand how software systems work in real-time interactive environments. Unlike a normal program that only gives output after running, a game has to constantly respond to player input, collisions, animations, and physics updates.

Because of this, the project improved my understanding of object-oriented programming, event-based behavior, and real-time system design. It also gave me more experience writing C# code that has to be organized, reusable, and easy to debug.

---

## How to Run This Project

### 1. Prerequisites

Make sure you have the following installed:

- Unity Hub
- Unity Editor
- Git

### 2. Clone the Repository

```bash
git clone https://github.com/mshraiden/2D-Metroidvania-Platformer.git
cd 2D-Metroidvania-Platformer
```

### 3. Open in Unity

1. Open Unity Hub.
2. Click **Add** or **Open**.
3. Select the cloned project folder.
4. Open the project using the correct Unity version.
5. Press the **Play** button in the Unity Editor to test the game.

---

## Project Structure

```text
Assets/
├── Scripts/
│   ├── Player/
│   ├── Enemy/
│   ├── Combat/
│   └── Systems/
├── Scenes/
├── Prefabs/
├── Sprites/
├── Animations/
└── Tilemaps/
```

---

## Skills Demonstrated

- Game development with Unity
- C# programming
- Object-oriented programming
- Physics-based movement
- Collision detection
- Gameplay systems design
- Debugging and testing
- Player experience design
- Real-time interactive programming

---

## Future Improvements

Some possible future improvements for this project include:

- Adding more enemy types
- Creating boss fights
- Adding a health UI
- Improving animations
- Adding sound effects and music
- Expanding the map using more rooms
- Adding checkpoints and save points
- Creating ability unlocks for Metroidvania progression

---

## License

This project is licensed under the MIT License. See the LICENSE file for details.

---

## Academic Integrity & Attribution

This project was completed for learning, portfolio, and demonstration purposes. Please do not submit this work as your own for academic credit. If you use or adapt this code, please provide proper attribution.

---

## Author

Created by Amogh.

GitHub: [mshraiden](https://github.com/mshraiden)
