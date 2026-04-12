namespace Notebook.Services.Interfaces
{
    using Notebook.ViewModels.Note;

    public interface INoteService 
    {
        public Task<IEnumerable<AllNotesViewModel>> AllNotesViewModelOrderedByCreationAndNameAsync(string userId);

        public Task CreateNoteToDbWithInputModelAndUserIdParamsAsync(CreateNoteInputModel model, string userId);

        public Task<EditNoteInputModel> GetEditNoteFromDbWithIdParamAsync(int id);

        public Task EditNoteInDbWithIdAndEditNoteModelParamsAsync(int id, EditNoteInputModel model);

        public Task<bool> CheckIfNoteExistWithIdParamAsync(int id);

        public Task<DeleteNoteInputModel> GetDeleteInputModelWithIdParamAsync(int id);

        public Task DeleteNoteFromDbWithIdParamAsync(int id);
    }
}
