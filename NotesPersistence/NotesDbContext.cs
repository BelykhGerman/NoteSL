using Microsoft.EntityFrameworkCore;
using NotesApplication.Interfaces;
using NotesDomain;
using NotesPersistence.EntityTypeConfigurations;

namespace NotesPersistence {
    public class NotesDbContext : DbContext, INotesDbContext {
        public DbSet<Note> Notes { get; set; }
        public NotesDbContext(DbContextOptions<NotesDbContext> options ) : base ( options ) { }

        protected override void OnModelCreating ( ModelBuilder modelBuilder ) {
            modelBuilder.ApplyConfiguration ( new NoteConfiguration () );
            base.OnModelCreating ( modelBuilder );
        }
    }
}
