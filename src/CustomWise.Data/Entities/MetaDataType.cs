namespace CustomWise.Data.Entities {
    using Base;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// Contains data that describes a <see cref="MetaData"/> instance.
    /// </summary>
    /// <seealso cref="CustomWise.Data.Entities.Base.BaseEntity" />
    [Table("MetaDataTypes")]
    public class MetaDataType 
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
        public ICollection<MetaData> MetaData { get; set; } = new HashSet<MetaData>();

        /// <summary>
        /// Initializes a new instance of the <see cref="MetaDataType"/> class.
        /// </summary>
        public MetaDataType() 
            : base() { }
    }
}
