#include "shapes.h"
#include <iostream>

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

	//pos.x = left side of aabb
	//pos.x + aabb.width = right side of aabb
	//pos.y = bottom side of aabb
	//pos.y + aadd.width = top side of aabb

	//      leftA       rightB               bottomA           topB
	return(posA.x < posB.x + aabbB.width && posA.y < posB.y + aabbB.height &&
		   posB.x < posA.x + aabbA.width && posB.y < posA.y + aabbA.height);
	//      leftB         rightA             bottomB           topA
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

//Depenetrating Objects
//To depenetrate objects, we need to determine two things :
//
//1.The direction to separate them along
//2.How far to move each object along that direction

glm::vec2 depenetrateCircleCircle(const glm::vec2& posA, const shape &shapeA, const glm::vec2& posB, const shape &shapeB, float& pen) {
	//The direction to separate them along
	// get the distance between the two circles
	glm::vec2 offset = posA - posB;
	// add up the sum of the two radii
	float radiiSum = shapeA.circleData.radius + shapeB.circleData.radius;
	float dist = glm::length(offset);

	//How far to move each object along that direction
	//write the penetration depth
	// find the difference and write it to the referenced variable
	pen = radiiSum - dist;

	//return collision normal
	// return the direction to correct along
	return glm::normalize(offset);

}

//There are two axes upon which AABBs can collide: horizontally (X-axis) and vertically (Y-axis). 
	//Assuming they are indeed colliding, you'll need to measure the amount of overlap on each axis 
	//and choose the one with the least amount of overlap.

//collision normal and penetration depth between AABBs
glm::vec2 depenetrateAABBAABB(const glm::vec2& posA, const shape& shapeA, const glm::vec2& posB, const shape& shapeB, float& pen)
{
	glm::vec2 normal;
	
	//measure gap between the two aabbs
	//x axis
	float boxOne_halfwidth = shapeA.aabbData.width / 2;
	float boxTwo_halfwidth = shapeB.aabbData.width / 2;
	
	float offsetX = (posB.x + boxTwo_halfwidth) - (posA.x + boxOne_halfwidth);

	//grabs the centers x position
	float overlapX =  boxOne_halfwidth + boxTwo_halfwidth - abs(offsetX);

	//y axis
	float boxOne_halfheight = shapeA.aabbData.height / 2;
	float boxTwo_halfheight = shapeB.aabbData.height / 2;

	//grabs the centers y position
	float offsetY = (posB.y + boxTwo_halfheight) - (posA.y + boxOne_halfheight);
	
	float overlapY = boxOne_halfheight + boxTwo_halfheight - abs(offsetY);

	if(overlapX > 0)
	{

		//std::cout << "boxes are penetrating each other" << " !" << std::endl;

		// separate on the axis that is less penetrated
		// if X is closer to zero, depen along X
		if (overlapX < overlapY)
		{
			normal = offsetX < 0 ? glm::vec2(1, 0) : glm::vec2(-1, 0);
			pen = overlapX;
		}
		// if Y is closer to zero, depen along Y
		else
		{
			normal = offsetY < 0 ? glm::vec2(0, 1) : glm::vec2(0, -1);
			pen = overlapY;
		}
	}
	
	return glm::normalize(normal);
}

//glm::vec2 depenetrateCircleAABB(const glm::vec2& posA, const shape& shapeA, const glm::vec2& posB, const shape& shapeB, float& pen) {
//	//float radius = shapeA.circleData.radius;
//	////clamp the center of the circle to the boundraries of the aabb
//	////get center point of circle
//	//glm::vec2 center(posA + radius);
//	////get center of aabb
//	//glm::vec2 aabb_half_extent(shapeB.aabbData.width / 2.0f, shapeB.aabbData.height / 2.0f);
//	//glm::vec2 aabb_center(posB.x + shapeB.aabbData.width, posB.y + shapeB.aabbData.height);
//
//	////get difference vector between both centers
//	//glm::vec2 difference = center - aabb_center;
//	////                              value    ,    min           ,   max
//	//glm::vec2 clamped = glm::clamp(difference, -aabb_half_extent, aabb_half_extent);
//
//	////add clamped value to aabb_center and we get the value of box closest to circle
//	//glm::vec2 closest = aabb_center + clamped;
//
//
//	////pen = circleRadius - (circleCenter - closestPointOnAABB);
//	//difference = closest - center;
//	//pen = radius - difference;
//
//	//
//	//return glm::normalize(difference);
//}
