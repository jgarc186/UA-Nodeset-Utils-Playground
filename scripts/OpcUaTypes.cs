namespace UA_Nodeset_Utils_Playground.Scripts;

using CESMII.OpcUa.NodeSetModel;
using CESMII.OpcUa.NodeSetModel.Factory.Opc;
using Microsoft.Extensions.Logging.Abstractions;
using Opc.Ua;
using Opc.Ua.Export;

public class OpcUaTypes
{
    public static async Task RunAsync()
    {
        // Location of nodeset xml files
        var nodeSetDirectory = "nodesets";

        // Set up the importer
        var nodeSetModels = new Dictionary<string, NodeSetModel>();
        var opcContext = new DefaultOpcUaContext(nodeSetModels, NullLogger.Instance);

        // Read and import the OPC UA nodeset
        var file = Path.Combine(nodeSetDirectory, "opcfoundation.org.UA.NodeSet2.xml");
        var nodeSet = UANodeSet.Read(new FileStream(file, FileMode.Open));
        var importer = new UANodeSetModelImporter(NullLogger.Instance);
        await importer.LoadNodeSetModelAsync(opcContext, nodeSet);
        var uaModel = nodeSetModels.First();

        var uaObjectType = uaModel.Value.ObjectTypes;
        var uaVariableType = uaModel.Value.VariableTypes;
        var uaDataType = uaModel.Value.DataTypes;
        var uaReferenceType = uaModel.Value.ReferenceTypes;

        // Display the names of the object types, variable types, data types, and reference types
        // for the OPC UA base nodeset
        uaObjectType.ForEach(aObjectTypeModel =>
        {
            Console.WriteLine($"Object Type: {aObjectTypeModel.DisplayName.First().Text}");
        });

        uaVariableType.ForEach(aVariableTypeModel =>
        {
            Console.WriteLine($"Variable Type: {aVariableTypeModel.DisplayName.First().Text}");
        });

        uaDataType.ForEach(aDataTypeModel =>
        {
            Console.WriteLine($"Data Type: {aDataTypeModel.DisplayName.First().Text}");
        });

        uaReferenceType.ForEach(aReferenceModel =>
        {
            Console.WriteLine($"Reference Type: {aReferenceModel.DisplayName.First().Text}");
        });
    }
}
