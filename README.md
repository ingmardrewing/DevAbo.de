# DevAbo.de
3D Dungeon Crawler. Game mechanics are inspired by NetHack, contents are derived from the http://DevAbo.de webcomic.

- requires Unity3D 5

# Installing
1. Clone this git repository
2. Open unity and navigate to the repository via "Open Other"
3. Even though unity opens an externally existing project, it puts some useless game objects into the stage (directional light, main camera) - remove them, so that the stage is completely empty
4. create an empty game object.
5. drag the C#-Script „GameControllerScript.cs“ onto the empty GameObject (you find the script in the folder „Code“ within the Assets folder).

# Implemented features so far
- generates a simple structure of connected rooms, whose creation is based on prefabbed 3D-Meshes created with Cheetah3D (the files ending with .jas)
- saves a 2D-Map which is used for creating the 3D-Dungeons (saves it as Maze.png)
- when the player character touches the staircase a new level is generated and the character is immediately placed into the new level
- in level 1 sewers appear, shedding light into the dungeon 
- no enemies or loot implemented yet

# Caution
The code is very creatively structured, i.e. it needs refactoring and an architecture to begin with ...
