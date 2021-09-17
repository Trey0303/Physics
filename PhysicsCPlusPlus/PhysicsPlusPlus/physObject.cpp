#include "physObject.h"
#include <raylib\raylib.h>

//Initializes our pos and vel to good default (zero'd out)
physObject::physObject() {
	pos = glm::vec2(0, 0);
    vel = glm::vec2(0, 0);
	totalForces = glm::vec2(0, 0);

	mass = 2.0f;
}

//Integrates velocity into position with the provided time step (delta)
void physObject::tickPhys(float delta) {
	vel += totalForces * delta;
	totalForces = glm::vec2(0, 0);
	
	pos += vel * delta;
}

//constaint
void physObject::addForce(glm::vec2 force) {
	//a = m/f
	//acceleration = mass / force;

	totalForces += force / mass;

}

//instant
void physObject::addImpulse(glm::vec2 force)
{
	vel += force / mass;

}

//instant
void physObject::addVelocity(glm::vec2 force)
{
	//same as addImpulse execpt without mass
	vel += force;

}

//constaint
void physObject::addAccel(glm::vec2 force)
{
	//same as addForce execpt without mass
	totalForces += force;
}

void physObject::useGravity(float forceOfGravity, bool gravity) {
	if (gravity) {
		vel.y += forceOfGravity;
	}
}

void physObject::draw() {
	switch(collider.type){
		case shapeType::NONE:
			DrawPixel(pos.x, pos.y, RED);
			break;
		case shapeType::CIRCLE:
			DrawCircle(pos.x, pos.y, collider.circleData.radius, GREEN);
			break;
		case shapeType::AABB:
			DrawRectangle(pos.x, pos.y, collider.aabbData.width, collider.aabbData.height, YELLOW);
			break;
		//case shapeType::MESH:
		//	break;
		default:
			break;

	}
}
