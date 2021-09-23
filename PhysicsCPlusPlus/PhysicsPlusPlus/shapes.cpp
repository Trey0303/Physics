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
	//clamp the center of the circle to the boundraries of the aabb
	//get center point of circle
	glm::vec2 center(posA + circleA.radius);
	//get center of aabb
	glm::vec2 aabb_half_extent(aabbB.width / 2.0f, aabbB.height / 2.0f);
	glm::vec2 aabb_center(posB.x + aabbB.width, posB.y + aabbB.height);

	//get difference vector between both centers
	glm::vec2 difference = center - aabb_center;
	//                              value    ,    min           ,   max
	glm::vec2 clamped = glm::clamp(difference, -aabb_half_extent, aabb_half_extent);

	//add clamped value to aabb_center and we get the value of box closest to circle
	glm::vec2 closest = aabb_center + clamped;

	//retrieve vector between center circle and closest point aabb and check if length <= radius
	//if clamped point is within radius of circle then there is collision
	difference = closest - center;
	return glm::length(difference) < circleA.radius;

}

bool checkCircleAABB(const glm::vec2& posA, const shape& shapeA, const glm::vec2& posB, const shape& shapeB) {
	return checkCircleAABB(posA, shapeA.circleData, posB, shapeB.aabbData);
}

glm::vec2 depenetrateCircleCircle(const glm::vec2& posA, const shape &shapeA, const glm::vec2& posB, const shape &shapeB, float& pen) {
	glm::vec2 offset = posA - posB;
	float radiiSum = shapeA.circleData.radius + shapeB.circleData.radius;
	float dist = glm::length(offset);

	//write the penetration depth
	pen = radiiSum - dist;

	//return collision normal
	return glm::normalize(offset);

}