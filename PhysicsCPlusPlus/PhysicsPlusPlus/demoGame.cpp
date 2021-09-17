#include "demoGame.h"

#include "raylib/raylib.h"
#include "baseGame.h"

#include "physObject.h"

void demoGame::onDraw() const {
	ClearBackground(RAYWHITE);

	DrawText("Congrats! You created your first window!", 190, 200, 20, LIGHTGRAY);
}

void demoGame::onTick() {
	//if (IsMouseButtonPressed(0)) {
	//	physObject newObject;
	//	Vector2 cursorPos = GetMousePosition();
	//	newObject.pos.x = cursorPos.x;
	//	newObject.pos.y = cursorPos.y;
	//	newObject.collider.type = shapeType::CIRCLE;
	//	newObject.collider.circleData = circle{ 25 };
	//	//newObject.addForce(glm::vec2(100, 100));
	//	//newObject.addImpulse(glm::vec2(100, 100));
	//	//newObject.useGravity(100.0f, true);
	//	newObject.addAccel(glm::vec2(100, 100));
	//	objects.push_back(newObject);
	//}

	if (IsMouseButtonPressed(1)) {
		physObject newObject;
		Vector2 cursorPos = GetMousePosition();
		newObject.pos.x = cursorPos.x;
		newObject.pos.y = cursorPos.y;
		newObject.collider.type = shapeType::AABB;
		newObject.collider.aabbData = aabb{ 25,25 };
		//newObject.addForce(glm::vec2(100, 100));
		//newObject.addImpulse(glm::vec2(100, 100));
		//newObject.useGravity(100.0f, true);
		newObject.addAccel(glm::vec2(100, 100));
		objects.push_back(newObject);
	}
}