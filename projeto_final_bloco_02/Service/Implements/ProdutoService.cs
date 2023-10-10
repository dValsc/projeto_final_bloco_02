using projeto_final_bloco_02.Model;
using projeto_final_bloco_02.Data;
using Microsoft.EntityFrameworkCore;

namespace projeto_final_bloco_02.Service.Implements
{
    public class ProdutoService : IProdutoService
    {
        private readonly AppDbContext _context;
        public ProdutoService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Produto?> Create(Produto produto)
        {
            if (produto.Categoria is not null)
            {
                var BuscaCategoria = await _context.Categorias.FindAsync(produto.Categoria.Id);

            if (BuscaCategoria is null)
                return null;

            produto.Categoria = BuscaCategoria;

        }

        await _context.Produtos.AddAsync(produto);
        await _context.SaveChangesAsync();

            return produto;
        }

        public async Task Delete(Produto produto)
        {
            _context.Produtos.Remove(produto);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Produto>> GetAll()
        {
            return await _context.Produtos.Include(p => p.Categoria).ToListAsync();
        }

        public async Task<Produto?> GetById(long id)
        {
            try 
            {
                var Produto = await _context.Produtos.Include(p => p.Categoria).FirstAsync(p => p.Id == id);
             return Produto;
            } 
            catch 
            {
                return null;
            }
        }

        public async Task<IEnumerable<Produto>> GetByTitulo(string titulo)
        {
            var Produto = await _context.Produtos
                .Include(p => p.Categoria)
                .Where(p => p.Titulo.Contains(titulo))
                .ToListAsync();
            return Produto;
        }

        public async Task<Produto?> Update(Produto produto)
        {
            var produtoUpdate = await _context.Produtos.FindAsync(produto.Id);

            if (produtoUpdate == null)
                return null;

            if (produto.Categoria is not null)
            {
                var BuscaCategoria = await _context.Categorias.FindAsync(produto.Categoria.Id);

                if (BuscaCategoria is null)
                    return null;

                produto.Categoria = BuscaCategoria;

            }

            _context.Entry(produtoUpdate).State = EntityState.Detached;
            _context.Entry(produto).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return produto;
        }
    }
}
