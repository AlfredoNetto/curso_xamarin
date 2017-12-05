using System;

namespace PodePedir.Model
{
    public class Pedido
    {
        public int id { get; set; }
        public long? IdCliente { get; set; }
        public string Data { get; set; }
        public string Hora { get; set; }
        public decimal ValorTotalPedido { get; set; }
        public string Descricao { get; set; }
        public string NomeCliente { get; set; }

    }
}
