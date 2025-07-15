using System;
using System.Linq;
using System.Reflection;

namespace Ua_Nodeset_Utils_Playground;

class Program
{
    static async Task Main(string[] args)
    {
        // Get the assembly that contains the scripts
        var assembly = Assembly.GetExecutingAssembly();

        // Find all public classes in the Scripts namespace with a RunAsync or Run method

        var scriptTypes = assembly.GetTypes()
            .Where(t => t.IsClass && t.IsPublic && t.Namespace == "Ua_Nodeset_Utils_Playground.Scripts")
            .ToList();

        if (scriptTypes.Count == 0)
        {
            Console.WriteLine("No scripts found in Ua_Nodeset_Utils_Playground.Scripts namespace.");
            return;
        }


        // Display menu
        Console.WriteLine("Available scripts:");
        for (int i = 0; i < scriptTypes.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {scriptTypes[i].Name}");
        }

        Console.Write("\nEnter the number of the script to run: ");
        var input = Console.ReadLine();

        if (!int.TryParse(input, out int choice) || choice < 1 || choice > scriptTypes.Count)
        {
            Console.WriteLine("Invalid choice.");
            return;
        }

        var selectedType = scriptTypes[choice - 1];
        Console.WriteLine($"\nRunning script: {selectedType.Name}\n");

        // Find RunAsync or Run method
        var method = selectedType.GetMethod("RunAsync", BindingFlags.Public | BindingFlags.Static)
                     ?? selectedType.GetMethod("Run", BindingFlags.Public | BindingFlags.Static);

        if (method == null)
        {
            Console.WriteLine($"Error: {selectedType.Name} does not have a Run or RunAsync method.");
            return;
        }

        try
        {

            // Invoke the method
            var result = method.Invoke(null, null);


            // If async, await the Task
            if (result is Task task)
            {
                await task;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error executing script: {ex.Message}");
        }
    }
}

