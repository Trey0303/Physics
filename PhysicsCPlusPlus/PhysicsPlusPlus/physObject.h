#pragma once

#include "glm/glm.hpp"

#include "shapes.h"

class physObject {
	//sum if all continuous forces being applied
	glm::vec2 totalForces;
public:
	//The position of the object
	glm::vec2 pos;
	//The velocity of the object
	glm::vec2 vel;


	float mass;
	/*float acceleration;
	float force;*/

	shape collider;

	//Initializes our pos and vel to good default (zero'd out)
	physObject();

	//Integrates velocity into position with the provided time step (delta)
	void tickPhys(float delta);

	//This should accept one parameter: a 2-dimentional vector describing the direction
	//and the length of the force applied.
	void addForce(glm::vec2 force);

	void addImpulse(glm::vec2 force);

	void addVelocity(glm::vec2 force);

	void addAccel(glm::vec2 force);

	void useGravity(float forceOfGravity, bool gravity);

	void draw() const;
};
