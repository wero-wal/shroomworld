# Shroomworld
**My A-Level Project**

## Projects
- [ ] Quest prototype
- Demonstrates:
  1. how users will acquire quests from NPCs
  2. how the quest menu will be accessed
  3. what the quest menu will display.


- [ ] Terrain generation prototype
1. User enters world size and number of biomes.
2. A world made up of hills and caves will be created using the Perlin noise algorithm.
3. A number of biomes will be chosen at random, each with its own start point.
4. Different tile types will be applied to the terrain depending on the biome and the distance from the surface.


- [x] Movement prototype
- I used this to get comfortable with using MonoGame and familiarising myself with the scale of the coordinates
1. Use WASD keys to move a player around the screen


- [ ] Physics prototype
1. Use WAS keys to move a player around the screen. Simplistic rigid body physics apply.
2. Player should not be able to pass through solid objects.


- [ ] Shroomworld
The final project.

# Design
## General design principles
1. I have tried to separate data from interfaces as much as possible.
2. In general, each game object contains information about itself relating to its context (i.e. the world it resides in), as well as a reference to its type, which contains all the data that remains the same for all objects of that type in all worlds.
3. I have tried as much as possible to store all data in files. The only exception to this is file paths.
4. I have gone with an OOP design, with some ECS-style elements (such as using component classes to store data about game objects).
5. I have mostly opted for composition and interfaces over inheritance, except for a few small exceptions (see `QuestItem` classes).
6. I have used standard `C#` naming conventions.

## File Design
1. I use the CSV file format to store data. Sometimes I have a variable which consists of multiple components, so I have designated two other symbols to work as variable-separators, as follows:

  |Nesting Level|Symbol|Symbol as word|
  |---|---|---|
  |1|,|comma|
  |2|;|semi-colon|
  |3|:|colon|
2. I have chosen not to encrypt the data because I want to be able to edit it with ease and it's not confidential.