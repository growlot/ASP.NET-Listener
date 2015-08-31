using WNP.Listener.MetadataService.Model;
using WNP.Listener.Persistence;

using static DBMetadata;

namespace WNP.Listener.MetadataService.Mapping
{
    public class WNPMetadataMapping : FluentMapper<WNPMetadataEntry>
    {
        public WNPMetadataMapping() : base(Metadata.ToString(), Metadata.Id)
        {
            this.Property(metadata => metadata.ColumnName, Metadata.ColumnName)
                .Property(metadata => metadata.CustomerLabel, Metadata.CustomerLabel)
                .Property(metadata => metadata.DataDescription, Metadata.DataDescription)
                .Property(metadata => metadata.DataExample, Metadata.DataExample)
                .Property(metadata => metadata.DataFormat, Metadata.DataFormat)
                .Property(metadata => metadata.DataLength, Metadata.DataLength)
                .Property(metadata => metadata.DataPrecision, Metadata.DataPrecision)
                .Property(metadata => metadata.DataRegex, Metadata.DataRegex)
                .Property(metadata => metadata.DataType, Metadata.DataType)
                .Property(metadata => metadata.Id, Metadata.Id)
                .Property(metadata => metadata.InitIfNull, Metadata.InitIfNull)
                .Property(metadata => metadata.IsPrimaryKey, Metadata.IsPrimaryKey)
                .Property(metadata => metadata.IsForeignKey, Metadata.IsForeignKey)
                .Property(metadata => metadata.IsIdentity, Metadata.IsIdentity)
                .Property(metadata => metadata.IsRequiredAlways, Metadata.IsRequiredAlways)
                .Property(metadata => metadata.IsRequiredOnNew, Metadata.IsRequiredOnNew)
                .Property(metadata => metadata.ValueIfNull, Metadata.ValueIfNull)
                .Property(metadata => metadata.IsUsed, Metadata.IsUsed)
                .Property(metadata => metadata.NullIfBlank, Metadata.NullIfBlank)
                .Property(metadata => metadata.Owner, Metadata.Owner)
                .Property(metadata => metadata.TableName, Metadata.TableName);
        }
    }
}