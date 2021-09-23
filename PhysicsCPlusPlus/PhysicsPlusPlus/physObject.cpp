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

void physObject::draw() const{
	switch(collider.type){
		case shapeType::NONE:
			DrawPixel(pos.x, pos.y, RED);
			break;
		case shapeType::CIRCLE:
			DrawCircle(pos.x, pos.y, collider.circleData.radius, BLUE);
			break;
		case shapeType::AABB:
			DrawRectangle(pos.x, pos.y, collider.aabbData.width, collider.aabbData.height, ORANGE);
			break;
		//case shapeType::MESH:
		//	break;
		default:
			break;

	}
}

float resolveCollision(glm::vec2 posA, glm::vec2 velA, float massA, glm::vec2 posB, glm::vec2 velB, float massB, float elasticity, glm::vec2 normal)
{
	//calculate the relative velocity
	glm::vec2 relVel = velA - velB;

	//calculate the magnitude of the impulse to apply
	float impulseMag = glm::dot(-(1.0f + elasticity) * relVel, normal) / 
					   glm::dot(normal, normal * (1 / massA + 1 / massB));

	//return the impulse
	return impulseMag;
}

void resolvePhysBodies(physObject& lhs, physObject& rhs, float elasticity, const glm::vec2& normal, float pen)
{
	// both values to be assigned by 'resolveCollision'
	//float pen = 0.0f;

	// calculate resolution impulse
	//   normal and pen are passed by reference and will be updated
	float impulseMag = resolveCollision(lhs.pos, lhs.vel, lhs.mass, rhs.pos, rhs.vel, rhs.mass,
										elasticity, normal);

	glm::vec2 impulse = impulseMag * normal;

	if (lhs.collider.type == shapeType::AABB ) {//check if lhs is a aabb
		if (lhs.collider.aabbData.isStatic == false) {//check if NOT static
			// depenetrate (aka separate) the two objects
			pen *= .51f;
			glm::vec2 correction = normal * pen;
			lhs.pos += correction;
			// apply resolution forces to both objects
			lhs.addImpulse(correction);
			
		}
	}
	else if (rhs.collider.type == shapeType::AABB) {//check if rhs is a aabb
		if (rhs.collider.aabbData.isStatic == false) {//check if NOT static
			glm::vec2 correction = normal * pen;
			rhs.pos -= correction;

			rhs.addImpulse(-correction); // remember: this gets an equal but opposite force

		}
	}
	else if(lhs.collider.type != shapeType::AABB && rhs.collider.type != shapeType::AABB){
		// depenetrate (aka separate) the two objects
		pen *= .51f;
		glm::vec2 correction = normal * pen;
		lhs.pos += correction;
		rhs.pos -= correction;

		// apply resolution forces to both objects
		lhs.addImpulse(correction);
		rhs.addImpulse(-correction); // remember: this gets an equal but opposite force
	}
	
}
