namespace Ua_Nodeset_Utils_Playground.Scripts;

using CESMII.OpcUa.NodeSetModel;
using CESMII.OpcUa.NodeSetModel.Factory.Opc;
using Microsoft.Extensions.Logging.Abstractions;
using Opc.Ua;
using Opc.Ua.Export;

public class CreateObjectType
{
    public static async Task RunAsync()
    {
        // Getting the NodeSet XML from the file system
        var nodesetPath = Path.Combine("nodesets", "opcfoundation.org.UA.NodeSet2.xml");
        var nodesetXml = File.ReadAllText(nodesetPath);

        // Initializing the OpcUaContext and starting the import process
        var opcContext = new DefaultOpcUaContext(NullLogger.Instance);
        var importer = new UANodeSetModelImporter(opcContext);

        // Loading the Base UA NodeSet into the importer
        var baseNodeSetsList = await importer.ImportNodeSetModelAsync(nodesetXml);
        var baseNodeSetsDict = baseNodeSetsList.ToDictionary(n => n.ModelUri);
        var uaBaseModel = baseNodeSetsDict[Namespaces.OpcUa];

        // Creating new NodeSetModel with a reference to the Base UA NodeSet
        var newNodeSetModel = new NodeSetModel
        {
            ModelUri = "https://opcua.rocks/UA",
            RequiredModels = new List<RequiredModelInfo>
            {
                new RequiredModelInfo
                {
                    ModelUri = uaBaseModel.ModelUri,
                    PublicationDate = uaBaseModel.PublicationDate,
                    Version = uaBaseModel.Version
                }
            },
        };

        var uaObjectTypes = uaBaseModel.ObjectTypes;
        var superType = uaObjectTypes.First(x => x.DisplayName.First().Text == "BaseObjectType");

        uint nextNodeId = 1000;
        var newNodeId = new ExpandedNodeId(nextNodeId++, newNodeSetModel.ModelUri);
        var animalType = new ObjectTypeModel
        {
            DisplayName = new List<NodeModel.LocalizedText> { "Animal Type" },
            BrowseName = "Animal Type",
            SymbolicName = "animal_type",
            SuperType = superType,
            NodeSet = newNodeSetModel,
            NodeId = newNodeId.ToString(),
            Properties = new List<VariableModel>(),
            DataVariables = new List<DataVariableModel>(),
        };

        newNodeSetModel.ObjectTypes.Add(animalType);

        Console.WriteLine($"Created new ObjectType: {animalType.BrowseName} with NodeId: {animalType.NodeId}");
    }
}
