# DevAbo.de
3D Dungeon Crawler. Game mechanics are inspired by NetHack, contents are derived from the http://DevAbo.de webcomic.

- requires Unity3D 5

Implemented features as of today:
- generates a maze based on prefabbed 3D-Meshes created with Cheetah3D (the files ending with .jas)
- a 2D-Map which is used for creating the 3D-Dungeons is saved as Maze.png
- when the player character touches the staircase a new level is generated and the character is immediately placed into the new level
- in level 1 sewers appear, shedding light into the dungeon 

# Caution
The code is very creatively structured, i.e. it needs refactoring and an architecture to begin with ...
