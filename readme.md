# UA Nodeset Utils Playground
This is a playground for testing and exploring with the [CESMII NodeSet Utilities](https://github.com/cesmii/CESMII-NodeSet-Utilities) library.

The purpose of this project is to provide a simple way to run various scripts that utilize the NodeSet Utilities library, allowing for quick experimentation and testing of different functionalities.

My hope is that this will be a useful tool for developers working with OPC UA nodesets, especially those who are new to the CESMII NodeSet Utilities library and want to get a feel for how it works.

Also, this project serves as a perfect starting point to implement new features or test out new ideas for the [OPC UA NodeSet WebAPI](https://github.com/ThinkIQ-Labs/OPC-UA-Nodeset-WebAPI), as it provides a simple and isolated environment to work with.

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

using CESMII.OpcUa.NodeSetModel;
using CESMII.OpcUa.NodeSetModel.Factory.Opc;
using Microsoft.Extensions.Logging.Abstractions;
using Opc.Ua;
using Opc.Ua.Export;

public class MyNewScript : BaseScript
{
    public static async Task RunAsync()
    {
        // Your script logic here
        Console.WriteLine("Hello from My New Script!");
    }
}
```
Make sure to implement the `Run` or `RunAsync` method, which will contain the logic of your script. The name of the class will be used to identify the script in the prompt.

## Walkthrough
If you would like a walkthrough of how to implement a new script, [@gregorvilkner](https://www.linkedin.com/in/gregorvilkner/) has created an [article](https://www.linkedin.com/pulse/creating-opc-ua-information-models-using-cesmiis-net-vilkner-ph-d-/) that explains how to do this step by step.
He isn't using this project, but the concepts are the same and it should be easy to adapt the code to work with this playground.

## Contributing
If you want to contribute to this project, feel free to open a pull request with your changes. Make sure to follow the coding style of the existing scripts and add any necessary documentation.

## License
This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more details.
