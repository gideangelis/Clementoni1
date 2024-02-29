using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Clementoni1WebAPI.Models.DB;

public partial class FormazioneDBContext : DbContext
{
    public FormazioneDBContext()
    {
    }

    public FormazioneDBContext(DbContextOptions<FormazioneDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comune> Comune { get; set; }

    public virtual DbSet<Person> Person { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=AC-GDEANGELIS\\SQLEXPRESS;Initial Catalog=FormazioneDB;Integrated Security=True;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasOne(d => d.IdComuneNavigation).WithMany(p => p.Person)
                .HasForeignKey(d => d.IdComune)
                .HasConstraintName("FK_Person_Comune");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
