namespace UA_Nodeset_Utils_Playground.Scripts;

using CESMII.OpcUa.NodeSetModel;
using CESMII.OpcUa.NodeSetModel.Factory.Opc;
using Microsoft.Extensions.Logging.Abstractions;
using Opc.Ua;
using Opc.Ua.Export;

public class CreateObjectType : BaseScript
{
    public static async Task RunAsync()
    {
        var uaBaseModel = await LoadUaNodeSetAsync("opcfoundation.org.UA.NodeSet2.xml");

        // Creating new NodeSetModel with a reference to the Base UA NodeSet
        var newNodeSetModel = await CreateNodeSetModelAsync("https://opcua.rocks/UA", uaBaseModel.ModelUri, uaBaseModel.PublicationDate, uaBaseModel.Version);

        var uaObjectTypes = uaBaseModel.ObjectTypes;
        var superType = uaObjectTypes.First(x => x.DisplayName.First().Text == "BaseObjectType");

        uint nextNodeId = 1000;
        var animalType = await CreateObjectTypeAsync(
            newNodeSetModel,
            "Animal Type",
            "animal_type",
            superType,
            nextNodeId,
            newNodeSetModel
        );

        newNodeSetModel.ObjectTypes.Add(animalType);

        Console.WriteLine($"Created new ObjectType: {animalType.BrowseName} with NodeId: {animalType.NodeId}");
    }
}
