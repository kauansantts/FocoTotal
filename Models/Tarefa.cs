using FocoTotal.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using FocoTotal.Services;
using FocoTotal.Exceptions;
using FocoTotal.Enums;


namespace FocoTotal.Models
{
    public class Tarefa
    {
        public EnumPrioridade TipoPrioridade { get; set; }
        public string TituloTarefa { get; set; }
        public string DescricaoTarefa { get; set; }
        public DateTime DataTarefa { get; set; }
        public int IdTarefa { get; set; }


        public Tarefa(string tituloTarefa, string descricaoTarefa, EnumPrioridade tipoPrioridade, DateTime dataTarefa, int idTarefa)
        {
            TituloTarefa = tituloTarefa;
            DescricaoTarefa = descricaoTarefa;
            TipoPrioridade = tipoPrioridade;
            DataTarefa = dataTarefa;
            IdTarefa = idTarefa;
        }
    }
}
