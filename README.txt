Group/Project: 11c_vr_game-psb - Psyche VR Game

Team Members: Noah Pfeffer, Cameron Joseph Schmidt, Jack Brand, and Peyton O'Boyle

Title: Psyche Odyssey VR

Directions for deploying on a Meta Quest 2 device:

- If you plan on building the APK through the Unity Editor and also using the Unity Editor to deploy it onto the headset...
	- The project should be configured already if pulled in its entirety from GitHub
	- Open File->Build Settings in upper left when the project is open in the editor (Meta Quest 2 may require an Oculus developer account to proceed to the next step)
	- Then, turn on the Meta Quest 2 and connect it to the computer running the editor with a USB-C (accept the popup that appears in the headset upon connection)
	- In the Build Settings, click on Build and Run to build the APK and push it to the headset (make sure to keep the USB-C connected during this process)
	- If you only want the APK and do not want to push with Unity Editor, select Build instead.

- If you have the APK for the game...
	- You will likely need to sideload it to Meta Quest 2
	- SideQuest is a software that can facilitate this, although you will need an Oculus developer account to proceed.
	- Once in the software, connect the headset to the computer using a USB-C and accept the popup in the headset.
	- Then, click "Install APK file from folder on computer" on the top bar (it is a box with an arrow pointing down).
	- SideQuest will then sideload the APK onto the headset once the file is specified.

Controls (minigame specific controls also visually provided in accompanying slide presentation):

- Index triggers for ray selection (menuing with right ray and teleport with left teleport ray when enabled)
- Grip triggers to pick up grabable objects
- X button to open the pause menu when in a minigame
- Y button to exit minigame upon completion (prompt will be shown in front of player)
- Left joy stick to move (in building minigames)
- Right joy stick to turn (in all minigames)
- A button to select point of interest with camera in Imaging minigame.
- To use power glove in building minigame, pressing the index trigger to create the ray caster, then press the grip trigger to grab the object
	- Can use A and B to bring the objects closer or push it further along the ray.

Description: 

- Psyche Odyseey VR is a Unity VR game that is mostly meant to be played on the Meta Quest 2 in an event setting.
- It features a normal mode (play through all three minigames sequentially)
- and an event mode (select one minigame to play and then return to the minigame select screen once finished)
- Includes in-game timer functionality that can be configured by navigating to Start Game -> Timer Options
	- For this, enter the desired time (e.g. 2.5 for 2 minutes and 30 seconds) and then press Back to set it.
	- The timer will start at the beginning of a minigame and can be viewed through the pause menu.
- Includes accessibility options for movement and volume in the options menu on the main menu and on the pause menu.


