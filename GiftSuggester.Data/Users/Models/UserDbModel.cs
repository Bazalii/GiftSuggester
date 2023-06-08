using System.ComponentModel.DataAnnotations;
using GiftSuggester.Data.Groups.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GiftSuggester.Data.Users.Models;

public class UserDbModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Login { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }

    internal class Map : IEntityTypeConfiguration<UserDbModel>
    {
        public void Configure(EntityTypeBuilder<UserDbModel> builder)
        {
            builder
                .HasIndex(dbModel => dbModel.Login)
                .IsUnique();
            builder
                .HasIndex(dbModel => dbModel.Email)
                .IsUnique();
        }
    }
}