#include "baseGame.h"

#include "raylib/raylib.h"

#include <iostream>

#include "physObject.h"

#include "shapes.h"
#include "enumUtils.h"

baseGame::baseGame() {
	//registers what happens when a circle-circle pairing happens
	//auto pair = shapeType::CIRCLE | shapeType::CIRCLE;
	collMap[static_cast<collisionPair>(shapeType::CIRCLE | shapeType::CIRCLE)] = checkCircleCircle;

	collMap[static_cast<collisionPair>(shapeType::AABB | shapeType::AABB)] = checkAABBAABB;

	collMap[static_cast<collisionPair>(shapeType::CIRCLE | shapeType::AABB)] = checkCircleAABB;

	depenMap[static_cast<collisionPair>(shapeType::CIRCLE | shapeType::CIRCLE)] = depenetrateCircleCircle;
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


	for (size_t i = 0; i < objects.size(); ++i)
	{
		for (size_t j = 0; j < objects.size(); ++j)
		{
			// TODO: check each object against every other object

			//skip self collision
			if (i == j) { continue; }

			//skip collision on things that dont have a collider
			if(objects[i].collider.type == shapeType::NONE || objects[j].collider.type == shapeType::NONE) {
				continue;
			}

			int lhs = i;
			int rhs = j;

			//swaps so that the smaller number shape always goes first when checking for collision
			if ((uint8_t)objects[i].collider.type > (uint8_t)objects[j].collider.type) {
				
				//swap
				lhs = j;
				rhs = i;
			}

			collisionPair pairing = (collisionPair)(objects[lhs].collider.type | objects[rhs].collider.type);
			auto result = collMap[pairing];
			bool collision = collMap[pairing](objects[lhs].pos, objects[lhs].collider, 
											  objects[rhs].pos, objects[rhs].collider);
			
			if (collision) {
				std::cout << "COLLISION OCCURRED AT " << GetTime() << " !" << std::endl;
				
				float pen = 0.0f;
				glm::vec2 normal = depenMap[pairing](objects[lhs].pos,      //lhs position
													objects[lhs].collider,  //lhs collider
													objects[rhs].pos,       //rhs position
													objects[rhs].collider,  //rhs collision
													pen);                   //assign pen

				resolvePhysBodies(objects[lhs], objects[rhs], 1.0f, normal, pen);
			}
		}
	}

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
		objects[i].draw();
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