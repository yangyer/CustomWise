
namespace CustomWise.Data.Entities {
    using Base;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Class that defines the detail metadata.
    /// </summary>
    /// <seealso cref="CustomWise.Data.Entities.Base.BaseEntity" />
    public class MetaDataDefinitionDetail 
        : BaseEntity {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Required, MaxLength(24)]
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the meta data definition identifier.
        /// </summary>
        /// <value>
        /// The meta data definition identifier.
        /// </value>
        [ForeignKey(nameof(MetaDataDefinition))]
        public int MetaDataDefinitionId { get; set; }
        /// <summary>
        /// Gets or sets the meta data definition.
        /// </summary>
        /// <value>
        /// The meta data definition.
        /// </value>
        public MetaDataDefinition MetaDataDefinition { get; set; }

        public MetaDataDefinitionDetail()
            : base() { }
    }
}
