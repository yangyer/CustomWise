namespace CustomWise.Data.Entities.Versioning {
    using Sophcon;

    public interface IEntityVersion {
        int Id { get; set; }
        string VersionNumber { get; set; }
        string Action { get; set; }
    }
}
