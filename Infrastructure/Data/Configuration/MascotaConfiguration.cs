using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class MascotaConfiguration : IEntityTypeConfiguration<Mascota>
    {
        public void Configure(EntityTypeBuilder<Mascota> builder)
        {
            builder.ToTable("mascota");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);

            builder.Property(p => p.Nombre)
            .IsRequired()
            .HasMaxLength(50);

            builder.Property(p => p.FechaNacimiento)
            .HasColumnType("datetime");

            builder.HasOne(p => p.Raza)
            .WithMany(p => p.Mascotas)
            .HasForeignKey(p => p.IdRaza);
            
            builder.HasOne(p => p.Clientes)
            .WithMany(p => p.Mascotas)
            .HasForeignKey(p => p.IdCliente);
            
        }
    }
}