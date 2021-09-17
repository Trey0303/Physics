#include "baseGame.h"

#include "raylib/raylib.h"
#include <iostream>
#include "physObject.h"

baseGame::baseGame() {

}

void baseGame::init() {
	//std::cout << "init";
	int screenWidth = 800;
	int screenHeight = 450;

	InitWindow(screenWidth, screenHeight, "raylib [core] example - basic window");

	SetTargetFPS(60);

	onInit();
}

void baseGame::tick() {
	//std::cout << "tick";
	onTick();
	//count up by time.deltaTime
	accumulatedFixedTime = accumulatedFixedTime + GetFrameTime();
}

void baseGame::tickFixed() {
	//std::cout << " tickFixed";
	accumulatedFixedTime = 0;

	//object.addForce(glm::vec2(15.0f, 4.0f));

	//update all physics bodies in the project
	for (int i = 0; i < objects.size(); i++) {
		objects[i].tickPhys(targetFixedStep);
	}

	onTickFixed();
}

void baseGame::draw() const {
	//std::cout << " draw";
	BeginDrawing();

	//DrawCircle(object.pos.x, object.pos.y, 15, BLUE);
	for (int i = 0; i < objects.size(); i++) {
		DrawCircle(objects[i].pos.x, objects[i].pos.y, 15, BLUE);
	}

	onDraw();
	
	EndDrawing();
	
}

void baseGame::exit() {
	std::cout << " exit";
	onExit();
	
	CloseWindow();
}

bool baseGame::shouldClose() const {
	return WindowShouldClose();
}

bool baseGame::shouldTickFixed() const {
	if (accumulatedFixedTime > targetFixedStep) {
		//std::cout << " tickFixed";
		return true;
	}
	return false;
}