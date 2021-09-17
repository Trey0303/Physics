#pragma once

#include <cstdint>
struct circle {
	float radius;
};

struct aabb {
	float length;
	float width;
};

struct mesh {

};

enum class shapeType : uint8_t {
	NONE = 0,
	CIRCLE = 1 << 0,
	AABB = 2 << 1,
	MESH = 3 << 2
};

struct shape
{
	//an enum identifing
	shapeType type;

	//add new types of shapes to this anonymous union
	union {
		circle circleData;
		aabb aabbData;
		mesh meshData;
	};
};