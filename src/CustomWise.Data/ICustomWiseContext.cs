namespace CustomWise.Data {
    using Sophcon.Data;

    /// <summary>
    /// Defines the CustomWise data context.
    /// </summary>
    /// <seealso cref="Sophcon.Data.ISophconContext" />
    /// <seealso cref="CustomWise.Data.ICustomWiseMigrationContext" />
    public interface ICustomWiseContext 
        : ISophconContext,
        ICustomWiseMigrationContext {
    }
}
