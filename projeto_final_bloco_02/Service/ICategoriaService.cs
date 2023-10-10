using projeto_final_bloco_02.Model;
namespace projeto_final_bloco_02.Service
{
    public interface ICategoriaService
    {
        Task<IEnumerable<Categoria>> GetAll();

        Task<Categoria?> GetById(long id);

        Task<IEnumerable<Categoria>> GetByDescricao(string descricao);

        Task<Categoria> Create(Categoria tema);

        Task<Categoria?> Update(Categoria tema);

        Task Delete(Categoria tema);
    }
}
