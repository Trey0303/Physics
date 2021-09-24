#pragma once
#include "glm/glm.hpp"

#include <cstdint>
struct circle {
	float radius;
};

struct aabb {
	float width;//x
	float height;//y
};

//struct mesh {
//
//};

enum class shapeType : uint8_t {
	NONE = 0,
	CIRCLE = 1 << 0,
	AABB = 1 << 1,
	/*MESH = 1 << 2*/
};

struct shape
{
	//an enum identifing
	shapeType type;

	//add new types of shapes to this anonymous union
	union {//colliderData
		circle circleData;
		aabb aabbData;
		//mesh meshData;
	};
};

bool checkCircleCircle(glm::vec2 posA, circle circleA, glm::vec2 posB, circle circleB);

//references(&) cost less than copying original
bool checkCircleCircle(const glm::vec2& posA, const shape& shapeA, const glm::vec2& posB, const shape& shapeB);


bool checkAABBAABB(glm::vec2 posA, aabb aabbA, glm::vec2 posB, aabb aabbB);

bool checkAABBAABB(const glm::vec2& posA, const shape& shapeA, const glm::vec2& posB, const shape& shapeB);


bool checkCircleAABB(glm::vec2 posA, circle circleA, glm::vec2 posB, aabb aabbB);

bool checkCircleAABB(const glm::vec2& posA, const shape& shapeA, const glm::vec2& posB, const shape& shapeB);

glm::vec2 depenetrateCircleCircle(const glm::vec2& posA, const shape &shapeA, const glm::vec2& posB, const shape &shapeB, float &pen);

glm::vec2 depenetrateAABBAABB(const glm::vec2& posA, const shape& shapeA, const glm::vec2& posB, const shape& shapeB, float& pen);

glm::vec2 depenetrateCircleAABB(const glm::vec2& posA, const shape& shapeA, const glm::vec2& posB, const shape& shapeB, float& pen);