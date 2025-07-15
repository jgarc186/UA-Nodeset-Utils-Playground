using CESMII.OpcUa.NodeSetModel;
using CESMII.OpcUa.NodeSetModel.Factory.Opc;
using Microsoft.Extensions.Logging.Abstractions;
using Opc.Ua;
using Opc.Ua.Export;

// Location of nodeset xml files
var nodeSetDirectory = "NodeSets";
var baseNodeSetsDict = new Dictionary<string, NodeSetModel>();


// Set up the importer
var importer = new UANodeSetModelImporter(NullLogger.Instance);
var nodeSetModels = new Dictionary<string, NodeSetModel>();
var opcContext = new DefaultOpcUaContext(nodeSetModels, NullLogger.Instance);

// Read and import the OPC UA nodeset
var file = Path.Combine(nodeSetDirectory, "opcfoundation.org.UA.NodeSet2.xml");
var nodeSet = UANodeSet.Read(new FileStream(file, FileMode.Open));
await importer.LoadNodeSetModelAsync(opcContext, nodeSet);
var uaBaseModel = baseNodeSetsDict[Namespaces.OpcUa];

// Read and import the OPC UA DI nodeset
file = Path.Combine(nodeSetDirectory, "opcfoundation.org.UA.DI.NodeSet2.xml");
nodeSet = UANodeSet.Read(new FileStream(file, FileMode.Open));
await importer.LoadNodeSetModelAsync(opcContext, nodeSet);
var uaDiModel = nodeSetModels.Last();

// Output Object Types of the DI nodeset
Console.WriteLine($"DI Object Types:");
uaDiModel.Value.ObjectTypes.ForEach(aObjectTypeModel =>
{
    Console.WriteLine(aObjectTypeModel.DisplayName.First().Text);
});

