using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DELBONI.Data;
using VIXTEAM.Models;

namespace DELBONI.Models
{
    public class PessoasController : Controller
    {
        private readonly DELBONIContext _context;

        public PessoasController(DELBONIContext context)
        {
            _context = context;
        }

        // GET: Pessoas
        public async Task<IActionResult> Index()
        {
            return View(await _context.PessoaModel.ToListAsync());
        }

        // GET: Pessoas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoaModel = await _context.PessoaModel
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (pessoaModel == null)
            {
                return NotFound();
            }

            return View(pessoaModel);
        }

        // GET: Pessoas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pessoas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo,Nome,Email,DataNascimento,QuantidadeFilhos,Salario")] PessoaModel pessoaModel)
        {
            pessoaModel.Situacao = "Ativo";
            if (ModelState.IsValid)
            {
                if (pessoaModel.DataNascimento < new DateTime(1990, 1, 1))
                {
                    ModelState.AddModelError("","Data de nascimento inválida");
                    return View(pessoaModel);
                }
                if (pessoaModel.QuantidadeFilhos < 0)
                {
                    ModelState.AddModelError("","Quantidade de filhos inválida");
                    return View(pessoaModel);
                }
                if (pessoaModel.Salario < 1200 && pessoaModel.Salario > 13000)
                {
                    ModelState.AddModelError("","Salário inválido");
                    return View(pessoaModel);
                }
                var pessoasEmail = _context.PessoaModel.Where(x => x.Email.Equals(pessoaModel.Email));
                if (pessoasEmail.Count() > 0)
                {
                    ModelState.AddModelError("","E-mail já cadastrado");
                    return View(pessoaModel);
                }
                _context.Add(pessoaModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pessoaModel);
        }

        // GET: Pessoas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoaModel = await _context.PessoaModel.FindAsync(id);
            if (pessoaModel == null)
            {
                return NotFound();
            }
            return View(pessoaModel);
        }

        // POST: Pessoas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Codigo,Nome,Email,DataNascimento,QuantidadeFilhos,Salario,Situacao")] PessoaModel pessoaModel)
        {
            if (id != pessoaModel.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (pessoaModel.DataNascimento < new DateTime(1990, 1, 1))
                {
                    ModelState.AddModelError("","Data de nascimento inválida");
                    return View(pessoaModel);
                }
                if (pessoaModel.QuantidadeFilhos < 0)
                {
                    ModelState.AddModelError("","Quantidade de filhos inválida");
                    return View(pessoaModel);
                }
                if (pessoaModel.Salario < 1200 && pessoaModel.Salario > 13000)
                {
                    ModelState.AddModelError("","Salário inválido");
                    return View(pessoaModel);
                }
                if (pessoaModel.Situacao.Equals("Inativo"))
                {
                    ModelState.AddModelError("","Não é possível editar pessoas com situação inativa");
                    return View(pessoaModel);
                }
                var pessoasEmail = _context.PessoaModel.Where(x => x.Email.Equals(pessoaModel.Email) && x.Codigo != pessoaModel.Codigo);
                if(pessoasEmail.Count()>0)
                {
                    ModelState.AddModelError("","E-mail já cadastrado");
                    return View(pessoaModel);
                }
                try
                {
                    _context.Update(pessoaModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PessoaModelExists(pessoaModel.Codigo))
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
            return View(pessoaModel);
        }

        // GET: Pessoas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoaModel = await _context.PessoaModel
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (pessoaModel == null)
            {
                return NotFound();
            }

            return View(pessoaModel);
        }

        // POST: Pessoas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pessoaModel = await _context.PessoaModel.FindAsync(id);
            if (pessoaModel.Situacao.Equals("Ativo"))
            {
                ModelState.AddModelError("", "Não é possível deletar pessoas com situação ativa");
                return View(pessoaModel);
            }
            _context.PessoaModel.Remove(pessoaModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // GET: PessoaModel/Alterar/5
        [HttpGet]
        public async Task<IActionResult> Alterar(int id)
        {
            var pessoaModel = await _context.PessoaModel.FindAsync(id);
            if (pessoaModel.Situacao.Equals("Ativo"))
            {
                pessoaModel.Situacao = "Inativo";
            }
            else
            {
                pessoaModel.Situacao = "Ativo";
            }
            _context.PessoaModel.Remove(pessoaModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool PessoaModelExists(int id)
        {
            return _context.PessoaModel.Any(e => e.Codigo == id);
        }
    }
}
