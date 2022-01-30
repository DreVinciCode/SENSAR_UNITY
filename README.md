# SENSAR

Welcome to SENSAR! (Seeing Everything iN Situ with Augmented Reality)
In this project, we created a visualization tool that displays various robotoic information in context of the environment with the use of augmented reality technology.
For anyone who may encounter a new robot for the first time it is natural that they may be hesitent to be near a robot. People will wonder what is the robot doing? Can I walk near or pass the robot? Is the robot broken? Should I be doing something? With SENSAR, we hope to build a person's willingness to interact with the robot by making the robot transparent with its intentions and observations. By gazing at the robot, a person will be able to obtain the information they might seek as it is visualized over the real world. 
SENSAR was developed with Unity and utilizes Vuforia Engine to track the robot's pose. The robot runs specific scripts to publish filited and transformed data to the AR device found in [SENSAR_ROS](https://github.com/DreVinciCode/SENSAR_ROS).

Robotic data that SENSAR can currently display include:
- Navigation Planner
- Person Detector
- Lidar laserscan
- OccupancyGrid
- Safefy Clearance
- Localization Points
- Loaded Map of Environment
- Diagnostic Information

![Alt text](Demo/demo.gif)
<br/> A Short Demo of Current Progress


## Helpful Links
Youtube tutorial link to connect ROS + Unity : https://www.youtube.com/watch?v=OZiAJuWh6w8&t=594s&ab_channel=TheRealFran

Download ROS-Sharp repo, unzip, and add RosSharp folder into Assets folder: https://github.com/siemens/ros-sharp 

Vuforia Engine 10.4.4 https://developer.vuforia.com/downloads/sdk 

Android SDK using sdkmanager : https://www.youtube.com/watch?v=wvi03sOBKWQ&ab_channel=ODK-XTeam 
