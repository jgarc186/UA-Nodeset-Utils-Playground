namespace UA_Nodeset_Utils_Playground.Scripts;

using CESMII.OpcUa.NodeSetModel;
using CESMII.OpcUa.NodeSetModel.Factory.Opc;
using Microsoft.Extensions.Logging.Abstractions;
using Opc.Ua;
using Opc.Ua.Export;

public class CreateRelationshipType : BaseScript
{
    public static async Task RunAsync()
    {
        var uaBaseModel = await LoadUaNodeSetAsync("opcfoundation.org.UA.NodeSet2.xml");

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
