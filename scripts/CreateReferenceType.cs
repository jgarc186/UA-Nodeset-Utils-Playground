namespace Ua_Nodeset_Utils_Playground.Scripts;

using CESMII.OpcUa.NodeSetModel;
using CESMII.OpcUa.NodeSetModel.Factory.Opc;
using Microsoft.Extensions.Logging.Abstractions;
using Opc.Ua;
using Opc.Ua.Export;

public class CreateReferenceType
{
    public static async Task RunAsync()
    {
        // Location of nodeset xml files
        var nodeSetDirectory = "nodesets";

        // Set up the importer
        var importer = new UANodeSetModelImporter(NullLogger.Instance);
        var nodeSetModels = new Dictionary<string, NodeSetModel>();
        var opcContext = new DefaultOpcUaContext(nodeSetModels, NullLogger.Instance);

        // Read and import the OPC UA nodeset
        var file = Path.Combine(nodeSetDirectory, "opcfoundation.org.UA.NodeSet2.xml");
        var nodeSet = UANodeSet.Read(new FileStream(file, FileMode.Open));
        await importer.LoadNodeSetModelAsync(opcContext, nodeSet);
        var uaModel = nodeSetModels.First();

    }
}
