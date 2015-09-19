using AMSLLC.Listener.MetadataService.Model;
using AMSLLC.Listener.Persistence;
using AMSLLC.Listener.Persistence.Metadata;

namespace AMSLLC.Listener.MetadataService.Mapping
{
    public class WNPMetadataMapping : FluentMapper<WNPMetadataEntry>
    {
        public WNPMetadataMapping() : base(DBMetadata.Metadata.ToString(), DBMetadata.Metadata.Id)
        {
            this.Property(metadata => metadata.ColumnName, DBMetadata.Metadata.ColumnName)
                .Property(metadata => metadata.CustomerLabel, DBMetadata.Metadata.CustomerLabel)
                .Property(metadata => metadata.DataDescription, DBMetadata.Metadata.DataDescription)
                .Property(metadata => metadata.DataExample, DBMetadata.Metadata.DataExample)
                .Property(metadata => metadata.DataFormat, DBMetadata.Metadata.DataFormat)
                .Property(metadata => metadata.DataLength, DBMetadata.Metadata.DataLength)
                .Property(metadata => metadata.DataPrecision, DBMetadata.Metadata.DataPrecision)
                .Property(metadata => metadata.DataRegex, DBMetadata.Metadata.DataRegex)
                .Property(metadata => metadata.DataType, DBMetadata.Metadata.DataType)
                .Property(metadata => metadata.Id, DBMetadata.Metadata.Id)
                .Property(metadata => metadata.InitIfNull, DBMetadata.Metadata.InitIfNull)
                .Property(metadata => metadata.IsPrimaryKey, DBMetadata.Metadata.IsPrimaryKey)
                .Property(metadata => metadata.IsForeignKey, DBMetadata.Metadata.IsForeignKey)
                .Property(metadata => metadata.IsIdentity, DBMetadata.Metadata.IsIdentity)
                .Property(metadata => metadata.IsRequiredAlways, DBMetadata.Metadata.IsRequiredAlways)
                .Property(metadata => metadata.IsRequiredOnNew, DBMetadata.Metadata.IsRequiredOnNew)
                .Property(metadata => metadata.ValueIfNull, DBMetadata.Metadata.ValueIfNull)
                .Property(metadata => metadata.IsUsed, DBMetadata.Metadata.IsUsed)
                .Property(metadata => metadata.NullIfBlank, DBMetadata.Metadata.NullIfBlank)
                .Property(metadata => metadata.Owner, DBMetadata.Metadata.Owner)
                .Property(metadata => metadata.TableName, DBMetadata.Metadata.TableName);
        }
    }
}