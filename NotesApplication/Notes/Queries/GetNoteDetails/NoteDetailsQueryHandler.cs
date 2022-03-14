using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NotesApplication.Common.Exceptions;
using NotesApplication.Interfaces;
using NotesDomain;
using System.Threading;
using System.Threading.Tasks;

namespace NotesApplication.Notes.Queries.GetNoteDetails {
    public class NoteDetailsQueryHandler : IRequestHandler<GetNoteDetailsQuery , NoteDetailsVm> {
        private readonly INotesDbContext _dbContext;
        private readonly IMapper _mapper;

        public NoteDetailsQueryHandler ( INotesDbContext dbContext , IMapper mapper ) {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<NoteDetailsVm> Handle ( GetNoteDetailsQuery request , CancellationToken cancellationToken ) {
            var entity =
                await _dbContext.Notes.FirstOrDefaultAsync ( note => note.Id == request.Id , cancellationToken );

            if ( entity is null || entity.UserId != request.UserId ) {
                throw new NotFoundException ( nameof ( Note ) , request.Id );
            }

            return _mapper.Map<NoteDetailsVm> ( entity );
        }
    }
}
