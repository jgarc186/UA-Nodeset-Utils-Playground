namespace UA_Nodeset_Utils_Playground.Scripts;

using CESMII.OpcUa.NodeSetModel;
using CESMII.OpcUa.NodeSetModel.Factory.Opc;
using Microsoft.Extensions.Logging.Abstractions;
using Opc.Ua;
using Opc.Ua.Export;

public class BaseScript
{
    protected static async Task<NodeSetModel> LoadUaNodeSetAsync(string fileName)
    {
        // Getting the NodeSet XML from the file system
        var nodesetPath = Path.Combine("nodesets", fileName);
        var nodesetXml = File.ReadAllText(nodesetPath);

        // Initializing the OpcUaContext and starting the import process
        var opcContext = new DefaultOpcUaContext(NullLogger.Instance);
        var importer = new UANodeSetModelImporter(opcContext);

        // Loading the Base UA NodeSet into the importer
        var baseNodeSetsList = await importer.ImportNodeSetModelAsync(nodesetXml);
        var baseNodeSetsDict = baseNodeSetsList.ToDictionary(n => n.ModelUri);
        var uaBaseModel = baseNodeSetsDict[Namespaces.OpcUa];

        return uaBaseModel;
    }
}
