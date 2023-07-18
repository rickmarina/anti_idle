# anti_idle
Code repository that provides a solution to prevent your computer from going into idle mode and ensures that your Microsoft Teams status is always displayed as "Available" (green).

This program simulate user activity on your computer preventing it from enterin sleep or idle mode. 

Each 25 secs it checks if your mouse has been moved if not it generates a mouse movement and click in order to keep your pc with activity. 

## instructions

to compile: 
dotnet publish -c release 
