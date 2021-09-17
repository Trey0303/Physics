#include "shapes.h"

//circle-circle
//uses shapes radius and distance between the two shapes to know if they are colliding or not
bool checkCircleCircle(glm::vec2 posA, circle circleA, glm::vec2 posB, circle circleB) {
	//get the distance between the two circles
	glm::vec2 offset = posA - posB;
	float distance = glm::length(offset);

	//add the circles radius together
	float radiiSum = circleA.radius + circleB.radius;

	//check if the distance is less than the radiiSum to know if its colliding or not
	return distance < radiiSum;

}

bool checkCircleCircle(const glm::vec2& posA, const shape& shapeA, const glm::vec2& posB, const shape& shapeB) {
	return checkCircleCircle(posA, shapeA.circleData, posB, shapeB.circleData);
}


//aabb - aabb
bool checkAABBAABB(glm::vec2 posA, aabb aabbA, glm::vec2 posB, aabb aabbB) {
	
	//is there collision on the x and y axis
	return(posA.x < posB.x + aabbB.width && posA.y < posB.y + aabbB.height &&
		   posB.x < posA.x + aabbA.width && posB.y < posA.y + aabbA.height);
}

bool checkAABBAABB(const glm::vec2& posA, const shape& shapeA, const glm::vec2& posB, const shape& shapeB) {
	return checkAABBAABB(posA, shapeA.aabbData, posB, shapeB.aabbData);
}

//circle-aabb
bool checkCircleAABB(glm::vec2 posA, circle circleA, glm::vec2 posB, aabb aabbB) {
	
	return 0;
}

bool checkCircleAABB(const glm::vec2& posA, const shape& shapeA, const glm::vec2& posB, const shape& shapeB) {
	return checkCircleAABB(posA, shapeA.circleData, posB, shapeB.aabbData);
}