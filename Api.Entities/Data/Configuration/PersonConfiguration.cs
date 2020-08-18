using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Api.Entities.Configuration
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasData
            (
                new Person
                {
                    Id = new Guid("0102F709-1DD7-40DE-AF3D-23598C6BBD1F"),
                    FullName = "Muhammed Cindioğlu",
                    DateOfBirth = new DateTime(1976, 08, 20),
                    Job = "Developer",
                },
                new Person
                {
                    Id = new Guid("63be0594-1b47-4a4f-be42-b608c75c1453"),
                    FullName = "Mathilda Gates",
                    DateOfBirth = new DateTime(1971, 06, 12),
                    Job = "Accountant",
                },
                new Person
                {
                    Id = new Guid("86de0227-1f7c-4d43-937c-09e69b7ed770"),
                    FullName = "Alanis Davey",
                    DateOfBirth = new DateTime(1982, 12, 24),
                    Job = "Artist",
                },
                new Person
                {
                    Id = new Guid("3116e6a1-98f0-4380-80bc-de067137e3fa"),
                    FullName = "Cade Vargas",
                    DateOfBirth = new DateTime(1985, 03, 05),
                    Job = "Desginer",
                }
            );
        }
    }
}
