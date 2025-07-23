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

    /// <summary>
    /// Creates a new NodeSetModel with a required model.
    /// </summary>
    /// <param name="modelUri">The URI of the new NodeSetModel.</param>
    /// <param name="requiredModelUri">The URI of the required model.</param>
    /// <param name="publicationDate">The publication date of the required model.</param>
    /// <param name="version">The version of the required model.</param>
    /// <returns>A Task that represents the asynchronous operation, containing the created NodeSetModel.</returns>
    protected static async Task<NodeSetModel> CreateNodeSetModelAsync(string modelUri, string requiredModelUri, DateTime? publicationDate, string version)
    {
        var nodeSetModel = new NodeSetModel
        {
            ModelUri = modelUri,
            RequiredModels = new List<RequiredModelInfo>
            {
                new RequiredModelInfo
                {
                    ModelUri = requiredModelUri,
                    PublicationDate = publicationDate,
                    Version = version
                }
            },
        };

        return await Task.FromResult(nodeSetModel);
    }

    protected static async Task<ObjectTypeModel> CreateObjectTypeAsync(
        NodeSetModel nodeSetModel,
        string browseName,
        string symbolicName,
        ObjectTypeModel superType,
        uint nextNodeId,
        NodeSetModel newNodeSetModel
    )
    {
        var newNodeId = new ExpandedNodeId(nextNodeId++, nodeSetModel.ModelUri);

        var objectType = new ObjectTypeModel
        {
            DisplayName = new List<NodeModel.LocalizedText> { "Animal Type" },
            BrowseName = browseName,
            SymbolicName = symbolicName,
            SuperType = superType,
            NodeSet = newNodeSetModel,
            NodeId = newNodeId.ToString(),
            Properties = new List<VariableModel>(),
            DataVariables = new List<DataVariableModel>(),
        };

        return await Task.FromResult(objectType);
    }

    /// <summary>
    /// Creates a new ReferenceTypeModel with the specified parameters.
    /// </summary>
    /// <param name="nodeSetModel">The NodeSetModel to which the new ReferenceType will belong.</param>
    /// <param name="browseName">The browse name of the new ReferenceType.</param>
    /// <param name="symbolicName">The symbolic name of the new ReferenceType.</param>
    /// <param name="superType">The super type of the new ReferenceType.</param>
    /// <param name="nextNodeId">The next available NodeId for the new ReferenceType.</param>
    /// <param name="isAbstract">Indicates whether the ReferenceType is abstract.</param>
    /// <param name="inverseName">The inverse name of the ReferenceType, if applicable.</param>
    /// <param name="symmetric">Indicates whether the ReferenceType is symmetric.</param>
    /// <returns>A Task that represents the asynchronous operation, containing the created ReferenceTypeModel.</returns>
    protected static async Task<ReferenceTypeModel> CreateReferenceTypeAsync(
        NodeSetModel nodeSetModel,
        string browseName,
        string symbolicName,
        ObjectTypeModel superType,
        uint nextNodeId,
        bool isAbstract = false,
        string? inverseName = null,
        bool symmetric = false
    )
    {
        var newNodeId = new ExpandedNodeId(nextNodeId++, nodeSetModel.ModelUri);

        var referenceType = new ReferenceTypeModel
        {
            DisplayName = new List<NodeModel.LocalizedText>
            {
                new NodeModel.LocalizedText { Locale = "en-US", Text = browseName }
            },
            BrowseName = browseName,
            SymbolicName = symbolicName,
            SuperType = superType,
            NodeSet = nodeSetModel,
            NodeId = newNodeId.ToString(),
            IsAbstract = isAbstract,
            InverseName = inverseName != null ? new List<NodeModel.LocalizedText>
            {
                new NodeModel.LocalizedText { Locale = "en-US", Text = inverseName }
            } : null,
            Symmetric = symmetric
        };

        return await Task.FromResult(referenceType);
    }
}
