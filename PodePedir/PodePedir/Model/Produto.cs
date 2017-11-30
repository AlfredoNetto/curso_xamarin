using SQLite.Net.Attributes;
using System;

namespace PodePedir.Model
{
    public class Produto
    {
        [PrimaryKey, AutoIncrement]
        public long? IdProduto { get; set; }
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public decimal Preco { get; set; }
        public string Descricao { get; set; }
        public Byte[] Foto { get; set; }
    }
}
