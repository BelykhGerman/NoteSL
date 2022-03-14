using System.Collections.Generic;

namespace NotesApplication.Notes.Queries.GetNoteList {
    public class NoteListVm {
        public IList<NoteLookupDto> Notes { get; set; }
    }
}
