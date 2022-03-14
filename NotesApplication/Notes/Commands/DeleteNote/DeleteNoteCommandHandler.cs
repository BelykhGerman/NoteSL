﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NotesApplication.Common.Exceptions;
using NotesApplication.Interfaces;
using NotesDomain;

namespace NotesApplication.Notes.Commands.UpdateNote {
    public class DeleteNoteCommandHandler : IRequestHandler<DeleteNoteCommand> {

        private readonly INotesDbContext _dbContext;

        public DeleteNoteCommandHandler ( INotesDbContext dbContext ) {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle ( DeleteNoteCommand request , CancellationToken cancellationToken ) {
            var entity =
                await _dbContext.Notes.FindAsync ( new object[] { request.Id } , cancellationToken );
            if ( entity == null || entity.UserId != request.UserId ) {
                throw new NotFoundException ( nameof ( Note ) , request.Id );
            }

            _dbContext.Notes.Remove ( entity );
            return Unit.Value;
        }
    }
}
