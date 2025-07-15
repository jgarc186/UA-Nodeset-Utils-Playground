# UA Nodeset Utils Playground
This is a playground for testing and exploring with the [CESMII NodeSet Utilities](https://github.com/cesmii/CESMII-NodeSet-Utilities) library.

The purpose of this project is to provide a simple way to run various scripts that utilize the NodeSet Utilities library, allowing for quick experimentation and testing of different functionalities.

My hope is that this will be a useful tool for developers working with OPC UA nodesets, especially those who are new to the CESMII NodeSet Utilities library.

## Installation
First, make sure you have dotnet 8 installed. You can check this by running:
```bash
dotnet --version
```
If you don't have it installed, you can download it from the [.NET website](https://dotnet.microsoft.com/download).

Next, clone this repository and navigate to the project directory:
```bash
git clone git@github.com:ThinkIQ-Labs/UA-Nodeset-Utils-Playground.git 
cd UA-Nodeset-Utils-Playground
```

Then, restore the project dependencies:
```bash
dotnet restore
```

## Running the project
To run the project, use the following command:
```bash
dotnet run
```
This will execute the `Main` method in the `Program.cs` file, which is the entry point of the application.
The way that it works is that it will read all the scripts in the `scripts` directory and give you a prompt to run them
based on the numeric value assigned to each script.

## Adding new scripts
To add a new script, create a new `.cs` file in the `scripts` directory. Here's an example of how to create a new script:
```csharp
namespace UA_Nodeset_Utils_Playground.Scripts;

public class MyNewScript
{

    public void Run()
    {
        // Your script logic here
        Console.WriteLine("Hello from My New Script!");
    }
}
```
Make sure to implement the `Run` or `RunAsync` method, which will contain the logic of your script. The name of the class will be used to identify the script in the prompt.

## Contributing
If you want to contribute to this project, feel free to open a pull request with your changes. Make sure to follow the coding style of the existing scripts and add any necessary documentation.

## License
This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more details.
