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
}
