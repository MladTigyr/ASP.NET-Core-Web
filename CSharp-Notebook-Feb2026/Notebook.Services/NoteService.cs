namespace Notebook.Services
{
    using Microsoft.EntityFrameworkCore;
    using Notebook.Data;
    using Notebook.Data.Models;
    using Notebook.ViewModels.Note;
    using Services.Interfaces;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class NoteService : INoteService
    {
        private readonly ApplicationDbContext dbContext;
        public NoteService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<AllNotesViewModel>> AllNotesViewModelOrderedByCreationAndNameAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return null;
            }

            IEnumerable<AllNotesViewModel> notes = await dbContext.Notes
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.CreatedOn)
                .ThenBy(n => n.Title)
                .Select(n => new AllNotesViewModel
                {
                    Id = n.Id,
                    Title = n.Title,
                    CreatedOn = n.CreatedOn
                })
                .ToListAsync();

            return notes;
        }

        public async Task CreateNoteToDbWithInputModelAndUserIdParamsAsync(CreateNoteInputModel model, string userId)
        {
            Note note = new Note
            {
                Title = model.Title,
                Content = model.Content,
                UserId = userId,
                CreatedOn = DateTime.UtcNow
            };

            dbContext.Notes.Add(note);
            await dbContext.SaveChangesAsync();
        }

        public async Task<EditNoteInputModel> GetEditNoteFromDbWithIdParamAsync(int id)
        {
            EditNoteInputModel? note = await dbContext.Notes
                .AsNoTracking()
                .Where(n => n.Id == id)
                .Select(n => new EditNoteInputModel
                {
                    Id = n.Id,
                    Title = n.Title,
                    Content = n.Content
                })
                .FirstOrDefaultAsync();

            return note;
        }

        public async Task<bool> CheckIfNoteExistWithIdParamAsync(int id)
        {
            Note? note = await dbContext.Notes
                .AsNoTracking()
                .FirstOrDefaultAsync(n => n.Id == id);

            bool isExist = false;

            if (note != null)
            {
                isExist = true;
            }

            return isExist;
        }

        public async Task EditNoteInDbWithIdAndEditNoteModelParamsAsync(int id, EditNoteInputModel model)
        {
            Note? note = await dbContext.Notes
                .AsNoTracking()
                .FirstOrDefaultAsync(n => n.Id == id);

            note.Title = model.Title;
            note.Content = model.Content;

            await dbContext.SaveChangesAsync();
        }

        public async Task<DeleteNoteInputModel> GetDeleteInputModelWithIdParamAsync(int id)
        {
            DeleteNoteInputModel? note = await dbContext.Notes
                .AsNoTracking()
                .Where(n => n.Id == id)
                .Select(n => new DeleteNoteInputModel
                {
                    Id = n.Id,
                    Title = n.Title,
                    Content = n.Content,
                    CreatedOn = n.CreatedOn
                })
                .FirstOrDefaultAsync();

            return note;
        }

        public async Task DeleteNoteFromDbWithIdParamAsync(int id)
        {
            Note? note = await dbContext.Notes
                .FirstOrDefaultAsync(n => n.Id == id);

            if (note != null)
            {
                dbContext.Notes.Remove(note);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
