﻿using System.ComponentModel.DataAnnotations;

namespace VIXTEAM.Models
{
    public class PessoaModel
    {
        [Key]
        public int Codigo { get; set; }
        public string Nome { get; set; }
        [Display(Name = "E-mail")]
        public string Email { get; set; }
        [Display(Name = "Data de nascimento")]
        public DateTime DataNascimento { get; set; }
        [Display(Name = "Quantidade de filhos")]
        public int QuantidadeFilhos { get; set; }
        [Display(Name = "Salário")]
        public decimal Salario { get; set; }
        [Display(Name = "Situação")]
        public string Situacao { get; set; }
    }
}
