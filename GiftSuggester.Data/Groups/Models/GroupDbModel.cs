using GiftSuggester.Data.Users.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GiftSuggester.Data.Groups.Models;

public class GroupDbModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public virtual UserDbModel Owner { get; set; } = new();
    public virtual List<UserDbModel> Admins { get; set; } = new();
    public virtual List<UserDbModel> Members { get; set; } = new();

    internal class Map : IEntityTypeConfiguration<GroupDbModel>
    {
        public void Configure(EntityTypeBuilder<GroupDbModel> builder)
        {
            builder
                .HasMany(dbModel => dbModel.Admins)
                .WithMany()
                .UsingEntity("groups_admins");

            builder
                .HasMany(dbModel => dbModel.Members)
                .WithMany()
                .UsingEntity("groups_members");

            builder
                .HasOne(dbModel => dbModel.Owner)
                .WithMany();
        }
    }
}