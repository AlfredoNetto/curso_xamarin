using System;


namespace PodePedir.Model
{
    public class ItemPedido
    {
        public long? IdPedido { get; set; }
        public long? IdProduto { get; set; }
        public int Quantidade { get; set; }
        public string Nome { get; set; }
        public Decimal ValorTotal { get; set; }
        public string Descricao { get; set; }
        public Byte[] Foto { get; set; }
    }
}
