using System.Collections.Generic;

using Telerik.OpenAccess.Metadata.Fluent;

namespace DataAccessSelfReference
{
    using Telerik.OpenAccess;
    using Telerik.OpenAccess.Metadata;

    public partial class DbMetadataSource : FluentMetadataSource
    {
        protected override IList<MappingConfiguration> PrepareMapping()
        {
            List<MappingConfiguration> mappingConfigurations = new List<MappingConfiguration>();

            MappingConfiguration<Code> codeConfiguration = this.GetCodeMappingConfiguration();
            mappingConfigurations.Add(codeConfiguration);

            return mappingConfigurations;
        }

        public MappingConfiguration<Code> GetCodeMappingConfiguration()
        {
            MappingConfiguration<Code> configuration = this.GetCodeClassConfiguration();
            this.PrepareCodePropertyConfigurations(configuration);
            this.PrepareCodeAssociationConfigurations(configuration);

            return configuration;
        }

        public MappingConfiguration<Code> GetCodeClassConfiguration()
        {
            MappingConfiguration<Code> configuration = new MappingConfiguration<Code>();
            configuration.MapType().WithConcurencyControl(OptimisticConcurrencyControlStrategy.Changed).ToTable("Code");

            return configuration;
        }

        public void PrepareCodePropertyConfigurations(MappingConfiguration<Code> configuration)
        {
            configuration.HasProperty(x => x.Id).IsIdentity(KeyGenerator.Default).WithDataAccessKind(DataAccessKind.ReadWrite).ToColumn("Id").IsNotNullable().HasColumnType("int").HasPrecision(0).HasScale(0);
            configuration.HasProperty(x => x.Name).WithDataAccessKind(DataAccessKind.ReadWrite).ToColumn("Name").IsNotNullable().HasColumnType("nvarchar").HasLength(255);
        }

        public void PrepareCodeAssociationConfigurations(MappingConfiguration<Code> configuration)
        {
            configuration.HasAssociation(x => x.RestrictedFor).IsManaged().WithOpposite(x => x.Restricts).WithDataAccessKind(DataAccessKind.ReadWrite).MapJoinTable("CodeScope", (x, y) => new { Code_Id = x.Id, Scope_Id = y.Id }); 
            //configuration.HasAssociation(x => x.Restricts).IsManaged().WithOpposite(x => x.RestrictedFor).WithDataAccessKind(DataAccessKind.ReadWrite).MapJoinTable("CodeScope", (x, y) => new { Scope_Id = x.Id, Code_Id = y.Id }); 
        }
    }
}