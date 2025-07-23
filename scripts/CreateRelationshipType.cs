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
        var newNodeSetModel = await CreateNodeSetModelAsync("https://opcua.rocks/UA", uaBaseModel.ModelUri, uaBaseModel.PublicationDate, uaBaseModel.Version);

        var uaObjectTypes = uaBaseModel.ObjectTypes;
        var superType = uaObjectTypes.First(x => x.DisplayName.First().Text == "BaseObjectType");

        uint nextNodeId = 1000;
        var newNodeId = new ExpandedNodeId(nextNodeId++, newNodeSetModel.ModelUri);
        // var animalType = new ObjectTypeModel
        // {
        //     DisplayName = new List<NodeModel.LocalizedText> { "Animal Type" },
        //     BrowseName = "Animal Type",
        //     SymbolicName = "animal_type",
        //     SuperType = superType,
        //     NodeSet = newNodeSetModel,
        //     NodeId = newNodeId.ToString(),
        //     Properties = new List<VariableModel>(),
        //     DataVariables = new List<DataVariableModel>(),
        // };

        var animalType = new ReferenceTypeModel
        {
            DisplayName = new List<NodeModel.LocalizedText> { "Animal Relationship" },
            BrowseName = "AnimalRelationship",
            SymbolicName = "animal_relationship",
            SuperType = uaBaseModel.ReferenceTypes.First(x => x.DisplayName.First().Text == "NonHierarchicalReferences"),
            NodeSet = newNodeSetModel,
            NodeId = new ExpandedNodeId(nextNodeId++, newNodeSetModel.ModelUri).ToString(),
            // InverseName = "isRelatedTo",
        };

        newNodeSetModel.ObjectTypes.Add(animalType);

        Console.WriteLine($"Created new ObjectType: {animalType.BrowseName} with NodeId: {animalType.NodeId}");
    }
}
