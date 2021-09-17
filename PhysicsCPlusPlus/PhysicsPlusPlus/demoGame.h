#pragma once

#include "baseGame.h"

class demoGame : public baseGame {
	void onTick() override;
	void onDraw() const override;
};