using FocoTotal.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using FocoTotal.Services;
using FocoTotal.Exceptions;


namespace FocoTotal.Models
{
    public class Tarefa
    {
        public EnumPrioridade TipoPrioridade { get; set; }
        public string TituloTarefa { get; set; }
        public string DescricaoTarefa { get; set; }
        public DateTime DataTarefa { get; set; }


        public Tarefa(string tituloTarefa, string descricaoTarefa, EnumPrioridade tipoPrioridade, DateTime dataTarefa)
        {
            TituloTarefa = tituloTarefa;
            DescricaoTarefa = descricaoTarefa;
            TipoPrioridade = tipoPrioridade;
            DataTarefa = dataTarefa;
        }
    }
}
