# Introduction
Welcome to RovingRobot a simple .NET 6 console application that takes in a set of commands to move Rover the roving robot around a 5x5 table.
This project is unit tested with Xunit.

# Commands 
The list of commands that can be contained within the .txt file are:
- `PLACE X,Y,FACING_DIRECTION` - This command places Rover at a given valid location on the board. (For example `PLACE 1,2,NORTH` places Rover at `x: 1`, `y: 2` facing `northward`.
- `MOVE` - This command will move Rover forward 1 space in the direction they are facing, providing it won't make them fall off the table
- `LEFT` - This command will rotate ROVE 90 degress anti-clockwise. (For example, `NORTH` -> `WEST`)
- `RIGHT` - This command will rotate ROVE 90 degress clockwise. (For example, `NORTH` -> `EAST`)
- `REPORT` - This command will print out the information for the journey Rover was on for the given command set. It will also output their final X,Y position and facing direction.

# Running the application
To run RovingRobot, it is fairly simple, following the below steps should allow you to run the application successfully. 
```
If you are running this using the dotnet run command:
Ensure you have the latest .NET 6 SDK installed, which can be found here: (https://dotnet.microsoft.com/en-us/download)
```
* Firstly, download the latest release from here [Latest Release](https://github.com/David-Mimnagh/RovingRobot/releases/latest).
* Once the file has been downloaded, extract it to a suitable location.
* Open your terminal within the `RovingRobot-X.X.X` folder where `X.X.X` is the release version. 
* Run the following command: `cd RovingRobot && dotnet run`
* That's it!

If you would like to Create your own command set and put Rover to the test, you can do so by creating a `.txt` file within the RovingRobot/Files/ subdirectory and then re-run the application.

If you have any trouble with this, you should be able to open the .sln file in Visual studio and run the code with RovingRobot as the startup project.
