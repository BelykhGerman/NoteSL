using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NotesDomain;

namespace NotesApplication.Interfaces {
    public interface INotesDbContext {
        DbSet<Note> Notes { get; set; }
        Task<int> SaveChangesAsync ( CancellationToken cancellationToken );
    }
}
