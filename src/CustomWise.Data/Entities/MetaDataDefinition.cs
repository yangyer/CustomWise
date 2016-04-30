namespace CustomWise.Data.Entities {
    using Sophcon;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Contains data that describes a <see cref="MetaData"/> instance.
    /// </summary>
    /// <seealso cref="CustomWise.Data.Entities.Base.BaseEntity" />
    [Table("MetaDataDefinitions")]
    public class MetaDataDefinition 
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
        [Required, MaxLength(64)]
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the meta data.
        /// </summary>
        /// <value>
        /// The meta data.
        /// </value>
        public virtual ICollection<MetaData> MetaData { get; set; } = new HashSet<MetaData>();
        /// <summary>
        /// Gets or sets the meta data definition detail.
        /// </summary>
        /// <value>
        /// The meta data definition detail.
        /// </value>
        public virtual ICollection<MetaDataDefinitionDetail> MetaDataDefinitionDetails { get; set; } = new HashSet<MetaDataDefinitionDetail>();

        /// <summary>
        /// Initializes a new instance of the <see cref="MetaDataDefinition"/> class.
        /// </summary>
        public MetaDataDefinition() 
            : base() { }
    }
}
