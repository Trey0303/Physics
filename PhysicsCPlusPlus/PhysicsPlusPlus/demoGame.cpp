#include "demoGame.h"

#include "raylib/raylib.h"
#include "baseGame.h"

#include "physObject.h"

void demoGame::onDraw() const {
	ClearBackground(RAYWHITE);

	DrawText("Congrats! You created your first window!", 190, 200, 20, LIGHTGRAY);
}

void demoGame::onTick() {
	if (IsMouseButtonPressed(0)) {
		physObject newObject;
		Vector2 cursorPos = GetMousePosition();
		newObject.pos.x = cursorPos.x;
		newObject.pos.y = cursorPos.y;
		newObject.isStatic = false;
		newObject.collider.type = shapeType::CIRCLE;
		newObject.collider.circleData = circle{ 15 };
		//newObject.addForce(glm::vec2(rand() % 100 - 100, rand() % 100 - 180));
		//newObject.addVelocity(glm::vec2(rand() % 100, rand() % 100));
		//newObject.addImpulse(glm::vec2(100, 100));
		//newObject.addAccel(glm::vec2(rand() % 100 + -100, rand() % 100 + -100));
		newObject.useGravity(100.0f, false);
		objects.push_back(newObject);
	}

	if (IsMouseButtonPressed(1)) {
		physObject newObject;
		Vector2 cursorPos = GetMousePosition();
		newObject.pos.x = cursorPos.x;
		newObject.pos.y = cursorPos.y;
		newObject.isStatic = false;
		newObject.collider.type = shapeType::AABB;
		newObject.collider.aabbData = aabb{ 25,25 };
		//newObject.addForce(glm::vec2(100, 100));
		//newObject.addImpulse(glm::vec2(rand() % 100, rand() % 100));
		//newObject.addAccel(glm::vec2(rand() % 100 + -100, rand() % 100 + -100));
		newObject.useGravity(1.0f, true);
		//newObject.collider.aabbData.isStatic = false;
		objects.push_back(newObject);
	}
	//static aabb
	if (IsMouseButtonPressed(2)) {
		physObject newObject;
		Vector2 cursorPos = GetMousePosition();
		newObject.pos.x = cursorPos.x;
		newObject.pos.y = cursorPos.y;
		newObject.isStatic = true;
		newObject.collider.type = shapeType::AABB;
		newObject.collider.aabbData = aabb{ 100,100 };
		//newObject.collider.aabbData.isStatic = true;

		objects.push_back(newObject);
	}
}