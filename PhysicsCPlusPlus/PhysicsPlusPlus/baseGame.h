#pragma once

#include "physObject.h"
#include <vector>

class baseGame
{
    
    
protected:
    //The amount of time since the last fixed tick
    float accumulatedFixedTime;

    // Called internally when game-specifc initialization occurs
    void virtual onInit() { }

    // Called internally when game-specifc tick occurs
    void virtual onTick() { }

    void virtual onTickFixed(){ }

    // Called internally when game-specifc drawing occurs
    void virtual onDraw() const { }

    // Called internally when game-specifc clean-up occurs
    void virtual onExit() { }

    std::vector<physObject> objects;
    //physObject object;

public:
    float targetFixedStep = 1 / 60.0f;

    // Trivial constructor
    baseGame();

    // Initializes the game window
    void init();

    // Poll for input, update timers, etc.
    void tick();

    //tick objects working on a fixed rate
    void tickFixed();

    // Draw the current world state
    void draw() const;

    // Close the game and shutdown
    void exit();

    // Returns true if the game is trying to shut down
    bool shouldClose() const;

    //returns true when enough time has passed for a fixed tick to occur
    bool shouldTickFixed() const;
};