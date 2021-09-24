Physics for Games Assessment
created by Trey Gleason
github source code:

https://github.com/Trey0303/Physics

Building:
Building this project requires Visual Studio 2019 or newer.

This is written against .NET Core 3.1 and primarily supports Windows. Adjustments may be needed for other platforms.

Clone the repository and open the solution in Visual Studio. Both the solution and project should already be configured and ready to start working with. To test this, build and run any of the provided projects.

About:
For this assessment you are required to create a custom physics simulation and demonstrate its successful implementation within a real-time application. You may not use any third-party physics libraries in the creation of this simulation. The simulation must demonstrate:

Static and Dynamic Rigid Bodies
Forces being applied to Dynamic Rigid Bodies
Static and Dynamic Rigid Bodies interacting with each other as expected
You must visually display the simulation in a meaningful way.

Once you have created your simulation you will need to create documentation that includes:

Class diagrams for your custom physics systems
List of references and research material used for creating the custom physics simulation
Any third-party non-physics libraries used
What improvements could be made to the simulation
Finally, you are tasked with demonstrating advanced physics interactions using a third-party physics system within a second non-trivial, real-time application or game. This application must demonstrate:

Joints and Ragdoll Physics
Trigger systems that use call-backs to influence the simulation when collisions occur
Use of a complex Character Controller that uses Dynamic and Kinematic systems
Demonstration of ray-casting in the simulation environment

Controls:
	
	unity project:
		-tab: switch camera
		(while using player camera) :
			-w,a,s,d: moves player
			-spacebar: jump
			
		(while using overview camera):
			-w,a,s,d moves camera
			-right mouse click: explodes slimes
			-left mouse click(on gate): open/close slime gate
			(slime teleport):
				-left mouse click(on slime): select slime
				(while a slime is selected):
					-left mouse click(on ground): teleports slime to selected spot
			(slime launcher):
				-middle mouse click(on slime): select slime
				(while a slime is selected):
					-middle mouse click(on ground): launches slime towards selected spot
				

License:
MIT License - Copyright (c) 2020 Academy of Interactive Entertainment

For more information, see the license file.

Third party works are attributed under thirdparty.md.