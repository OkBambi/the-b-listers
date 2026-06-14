# PRIMARY
An FPS game in a whitespace arena where the player fights a constant onslaught of enemies of colors RED, YELLOW, and BLUE, in order to try and fight for the highest score possible. They do this throughout all 5 stages of the game, using their color swapping ability and shooting daggers to eliminate enemies.

## Technology
This project uses Unity3D and GitHub as it's source control. During active development we also used Trello to organise and delegate tasks between our 6 members.

## Practices
Our team used an AGILE workflow for the development of PRIMARY and performed scrum/stand-up meetings. This allows us to make flexible decisions and keep everyone in check, as in the past we had to modify the games direction quite substantially, so AGILE lets that happen.

## Features
PRIMARY is built around a core mechanic; Colors.

The player can switch colors on command which allow them to hit enemies of the same colour with their daggers.
For example, a red dagger can kill red enemies but passes through yellow and blue enemies.

In addition, each color has an abilty. A Schmove if you will.
Schmoves have cooldowns and are unique to each color, but can hit all enemies regardless of color.

## Contents and Structure
The game has 5 stages, each with different layouts and enemy combinations.

There are 5 types of enemies:
- Boid
- Monolith
- Monk
- Stopwatch
- Snake

Each enemy derrives from the same base class but have independant AI and actions.

There are 3 difficulties in the game, Easy, Medium and Hard. Each difficulty has its own modifiers that change up the game.
Currently each difficulty has 3 modifiers.

The main menu UI and the gameplay UI are similar but separated.

## Goals


## Future

