using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Api.Entities.Configuration
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasData
            (
                new Address
                {
                    Id = new Guid("89e99b0f-e230-4036-84af-f5d5f702e8bc"),
                    Title = "Home",
                    Country = "Turkey",
                    City = "Istanbul",
                    Details = "Ordu Mah., 124 Nolu Cad., No:5/12",
                    PersonId = new Guid("0102F709-1DD7-40DE-AF3D-23598C6BBD1F"),
                },
                new Address
                {
                    Id = new Guid("15242470-59df-4728-b00a-3c255a7d1452"),
                    Title = "Work",
                    Country = "Turkey",
                    City = "Istanbul",
                    Details = "Kavaklik Mah., Ordu Cad., No:24/A",
                    PersonId = new Guid("0102F709-1DD7-40DE-AF3D-23598C6BBD1F"),
                },
                new Address
                {
                    Id = new Guid("4b76cd99-2473-4bca-8c9d-a2104238c10c"),
                    Title = "Home",
                    Country = "Turkey",
                    City = "Bursa",
                    Details = "Sahinbey Mah., 124356 Nolu Sok., No:5A/16",
                    PersonId = new Guid("63be0594-1b47-4a4f-be42-b608c75c1453"),
                },
                new Address
                {
                    Id = new Guid("d4461e68-8790-4836-bfe4-d8e1cb007b59"),
                    Title = "School",
                    Country = "Turkey",
                    City = "Ankara",
                    Details = "GuneyKent Mah., Mehmet akif Sok., No:18/4",
                    PersonId = new Guid("86de0227-1f7c-4d43-937c-09e69b7ed770"),
                }
            );
        }
    }
}
