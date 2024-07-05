using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ControleEstoque.Data;
using ControleEstoque.Models;
using Microsoft.Extensions.WebEncoders.Testing;

namespace ControleEstoque.Controllers
{
    public class MovimentacaoController : Controller
    {
        private readonly ControleEstoqueContext _context;

        public MovimentacaoController(ControleEstoqueContext context)
        {
            _context = context;
        }

        // GET: Movimentacao
        public async Task<IActionResult> Index()
        {
            var controleEstoqueContext = _context.Movimentacao.Include(m => m.Produto);
            return View(await controleEstoqueContext.ToListAsync());
        }

        // GET: Movimentacao/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimentacao = await _context.Movimentacao
                .Include(m => m.Produto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movimentacao == null)
            {
                return NotFound();
            }

            return View(movimentacao);
        }

        // GET: Movimentacao/Create
        public IActionResult Create()
        {
            // Produto
            var produtos = _context.Produto.Select(p => new { p.Id, p.Nome }).ToList();
            produtos.Insert(0, new { Id = Guid.Empty, Nome = "Selecione um Produto" });
            ViewData["ProdutoId"] = new SelectList(produtos, "Id", "Nome");

            // Fornecedor
            var fornecedores = _context.Fornecedor.Select(f => new { f.Id, f.Nome }).ToList();
            fornecedores.Insert(0, new { Id = Guid.Empty, Nome = "Selecione um Fornecedor" });
            ViewData["FornecedorId"] = new SelectList(fornecedores, "Id", "Nome");

            // Cliente
            var clientes = _context.Cliente.Select(c => new { c.Id, c.Nome }).ToList();
            clientes.Insert(0, new { Id = Guid.Empty, Nome = "Selecione um Cliente" });
            ViewData["ClienteId"] = new SelectList(clientes, "Id", "Nome");

            // TipoMovimentacao
            ViewData["TipoMovimentacao"] = new SelectList(new[] {
                new { Value = "entrada", Text = "Entrada" },
                new { Value = "saida", Text = "Saída" }
            }, "Value", "Text");

            return View();
        }



        // POST: Movimentacao/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProdutoId,FornecedorId,ClienteId,TipoMovimentacao,Quantidade,Data")] Movimentacao movimentacao)
        {
            try
            {
                movimentacao.Id = Guid.NewGuid();
                movimentacao = await parseProps(movimentacao);
                _context.Add(movimentacao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            } catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                // Produto select list
                ViewData["ProdutoId"] = new SelectList(_context.Produto, "Id", "Nome");

                // Fornecedor select list with empty option
                var fornecedores = _context.Fornecedor.Select(f => new { f.Id, f.Nome }).ToList();
                fornecedores.Insert(0, new { Id = Guid.Empty, Nome = "Selecione um Fornecedor" });
                ViewData["FornecedorId"] = new SelectList(fornecedores, "Id", "Nome");

                // Cliente select list with empty option
                var clientes = _context.Cliente.Select(c => new { c.Id, c.Nome }).ToList();
                clientes.Insert(0, new { Id = Guid.Empty, Nome = "Selecione um Cliente" });
                ViewData["ClienteId"] = new SelectList(clientes, "Id", "Nome");

                // TipoMovimentacao select list
                ViewData["TipoMovimentacao"] = new SelectList(new[] {
                    new { Value = "entrada", Text = "Entrada" },
                    new { Value = "saida", Text = "Saída" }
                }, "Value", "Text");

                return View();
            }
        }

        // GET: Movimentacao/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimentacao = await _context.Movimentacao.FindAsync(id);
            if (movimentacao == null)
            {
                return NotFound();
            }
            ViewData["ProdutoId"] = new SelectList(_context.Produto, "Id", "Nome", movimentacao.ProdutoId);
            return View(movimentacao);
        }

        // POST: Movimentacao/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,ProdutoId,FornecedorId,ClienteId,TipoMovimentacao,Quantidade,Data")] Movimentacao movimentacao)
        {
            if (id != movimentacao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movimentacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovimentacaoExists(movimentacao.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProdutoId"] = new SelectList(_context.Produto, "Id", "Nome", movimentacao.ProdutoId);
            return View(movimentacao);
        }

        // GET: Movimentacao/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimentacao = await _context.Movimentacao
                .Include(m => m.Produto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movimentacao == null)
            {
                return NotFound();
            }

            return View(movimentacao);
        }

        // POST: Movimentacao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var movimentacao = await _context.Movimentacao.FindAsync(id);
            if (movimentacao != null)
            {
                _context.Movimentacao.Remove(movimentacao);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovimentacaoExists(Guid id)
        {
            return _context.Movimentacao.Any(e => e.Id == id);
        }

        public async Task<Movimentacao> parseProps(Movimentacao movimentacao)
        {
            if ((movimentacao.ClienteId != Guid.Empty && movimentacao.ClienteId != Guid.Parse("00000000-0000-0000-0000-000000000000")) &&
                (movimentacao.FornecedorId != Guid.Empty && movimentacao.FornecedorId != Guid.Parse("00000000-0000-0000-0000-000000000000")))
            {
                throw new Exception("Não é possível cadastrar uma movimentação com cliente e um fornecedor no mesmo registro");
            }

            if (movimentacao.ClienteId == Guid.Empty && movimentacao.FornecedorId == Guid.Empty)
            {
                throw new Exception("Não é possível cadastrar uma movimentação sem pelos menos um cliente ou fornecedor");
            }

            if (movimentacao.ClienteId == Guid.Parse("00000000-0000-0000-0000-000000000000")) movimentacao.ClienteId = null;
            if (movimentacao.FornecedorId == Guid.Parse("00000000-0000-0000-0000-000000000000")) movimentacao.FornecedorId = null;

            if (movimentacao.Data.Kind == DateTimeKind.Unspecified)
            {
                movimentacao.Data = DateTime.SpecifyKind(movimentacao.Data, DateTimeKind.Utc);
            }
            else if (movimentacao.Data.Kind == DateTimeKind.Local)
            {
                movimentacao.Data = movimentacao.Data.ToUniversalTime();
            }

            return movimentacao;

        }
    }
}
