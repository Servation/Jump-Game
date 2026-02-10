# Jump-Game

A 2D platformer developed in Visual Basic .NET utilizing GDI+ for rendering. The game features collision physics, state management for entities, and sprite sheet animation.

![Gameplay Preview](https://raw.githubusercontent.com/Servation/Jump-Game/master/JumpGameSampleIMG.png)

## Technical Overview

The engine uses a double-buffered panel to reduce flicker during high-frequency screen updates. Game logic follows a standard update-draw loop implemented via Windows Forms timers.

### Features

* **Entity Logic**: Object-oriented design for characters, enemies, and interactive objects like keys, doors, and spikes.
* **Collision Detection**: Rectangular bounds checking for platform grounding and hazard interaction.
* **Asset Pipeline**: Sprite-based rendering with dedicated classes for platform geometry, including rounded corner calculations.
* **Game States**: Objective-based gameplay requiring key collection to trigger door state changes.

## Development Environment

* **Language**: Visual Basic .NET
* **Framework**: .NET Framework
* **IDE**: Visual Studio
* **Rendering**: System.Drawing (GDI+)

## Project Structure

* **Character.vb**: Core player physics and input handling.
* **Enemy.vb**: Slime AI patterns and collision hazards.
* **Platform.vb**: Static environment geometry with custom rendering logic.
* **PanelDoubleBuffer.vb**: Custom control to optimize graphics performance.
* **Form1.vb**: Main game loop and level initialization.

## Installation

1. Clone the repository:

```Bash
git clone https://github.com/Servation/Jump-Game.git
```
2. Open Jump Game.sln in Visual Studio.

3. Restore NuGet packages if prompted.

4. Build and Run the project.
