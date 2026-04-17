using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaPedidos
{
    public class ProcessadorPedidos
    {
        private List<string> _logs = new List<string>();
//classe para processar os pedidos, contém os métodos para processar o pedido e obter os logs
        public string ProcessarPedido(
            int pedidoId,
            string nomeCliente,
            string emailCliente,
            string tipoCliente,
            List<ItemPedido> itens,
            string cupom,
            string formaPagamento,
            string enderecoEntrega,
            double pesoTotal,
            bool entregaExpressa,
            bool clienteBloqueado,
            bool enviarEmail,
            bool salvarLog,
            string pais,
            int parcelas)
        {

            string returnMessage = "";
            double subtotalValue = 0;
            double discountValue = 0;
            double shippingValue = 0;
            double interestValue = 0;
            double totalValue = 0;
            bool hasError = false;


// Validação de dados, verifica se os dados do pedido sao validos, se nao forem, adiciona mensagem de erro
            if (pedidoId <= 0)
            {
                returnMessage += "Pedido inválido\n";
                hasError = true;
            }//se pedidoId for menor ou igual a 0, é inválido

            if (nomeCliente == null || nomeCliente == "")
            {
                returnMessage += "Nome do cliente não informado\n";
                hasError = true;
            }//se nomeCliente for nulo ou vazio, é inválido

            if (emailCliente == null || emailCliente == "")
            {
                returnMessage += "Email do cliente não informado\n";
                hasError = true;
            }//se emailCliente for nulo ou vazio, é inválido

            if (clienteBloqueado == true)
            {
                returnMessage += "Cliente bloqueado\n";
                hasError = true;
            }//se clienteBloqueado for true, o cliente está bloqueado

            if (itens == null)
            {
                returnMessage += "Lista de itens nula\n";
                hasError = true;
            }//se itens for nulo, é inválido
            else
            {
                if (itens.Count == 0)
                {
                    returnMessage += "Pedido sem itens\n";
                    hasError = true;
                }//se itens estiver vazio, é inválido
                else
                {
                    for (int i = 0; i < itens.Count; i++)//para cada item na lista de itens
                    {
                        if (itens[i].Quantidade <= 0)//se a quantidade do item for menor ou igual a 0, é inválido
                        {
                            returnMessage += "Item com quantidade inválida: " + itens[i].Nome + "\n";
                            hasError = true;
                        }//se a quantidade do item for menor ou igual a 0, é inválido

                        if (itens[i].PrecoUnitario < 0)
                        {
                            returnMessage += "Item com preço inválido: " + itens[i].Nome + "\n";
                            hasError = true;
                        }//se o preço unitário do item for menor que 0, é inválido

                        subtotalValue = subtotalValue + (itens[i].PrecoUnitario * itens[i].Quantidade);

                        if (itens[i].Categoria == "ALIMENTO")
                        {
                            subtotalValue = subtotalValue + 2;
                        }//se a categoria do item for ALIMENTO, adiciona 2 ao subtotal

                        if (itens[i].Categoria == "IMPORTADO")
                        {
                            subtotalValue = subtotalValue + 5;
                        }//se a categoria do item for IMPORTADO, adiciona 5 ao subtotal
                    }
                }
            }



//descontos [pr cliente, cupom]
            if (hasError == false)//se não tem erro, processa o pedido
            {
                if (tipoCliente == "VIP")
                {
                    discountValue = subtotalValue * 0.15;
                }//se o tipo de cliente for VIP, o desconto é 15% do subtotal
                else if (tipoCliente == "PREMIUM")
                {
                    discountValue = subtotalValue * 0.10;
                }//se o tipo de cliente for PREMIUM, o desconto é 10% do subtotal
                else if (tipoCliente == "NORMAL")
                {
                    discountValue = subtotalValue * 0.02;
                }//se o tipo de cliente for NORMAL, o desconto é 2% do subtotal
                else if (tipoCliente == "NOVO")
                {
                    discountValue = 0;
                }//se o tipo de cliente for NOVO, não tem desconto
                else
                {
                    discountValue = 1;
                }//se o tipo de cliente for diferente dos tipos válidos, o desconto é 1



//validacao de cupom
                if (cupom != null && cupom != "")
                {
                    if (cupom == "DESC10")
                    {
                        discountValue = discountValue + (subtotalValue * 0.10);
                    }
                    else if (cupom == "DESC20")
                    {
                        discountValue = discountValue + (subtotalValue * 0.20);
                    }
                    else if (cupom == "FRETEGRATIS")
                    {
                        shippingValue = 0;
                    }
                    else if (cupom == "VIP50" && tipoCliente == "VIP")
                    {
                        discountValue = discountValue + 50;
                    }
                    else
                    {
                        returnMessage += "Cupom inválido ou não aplicável\n";
                    }
                }//se o cupom for diferente dos cupons válidos, é inválido ou não aplicável





//Validacao de entrega
                if (enderecoEntrega == null || enderecoEntrega == "")
                {
                    returnMessage += "Endereço de entrega não informado\n";
                    hasError = true;
                }//se o endereço de entrega for nulo ou vazio, é inválido

                if (pais == "BR")
                {
                    if (pesoTotal <= 1)
                    {
                        shippingValue = 10;
                    }
                    else if (pesoTotal <= 5)
                    {
                        shippingValue = 25;
                    }
                    else if (pesoTotal <= 10)
                    {
                        shippingValue = 40;
                    }
                    else
                    {
                        shippingValue = 70;
                    }

                    if (entregaExpressa == true)
                    {
                        shippingValue = shippingValue + 30;
                    }
                }//se o país for BR, calcula o frete de acordo com o peso total e se a entrega é expressa
                else
                {
                    if (pesoTotal <= 1)
                    {
                        shippingValue = 50;
                    }
                    else if (pesoTotal <= 5)
                    {
                        shippingValue = 80;
                    }
                    else
                    {
                        shippingValue = 120;
                    }

                    if (entregaExpressa == true)
                    {
                        shippingValue = shippingValue + 70;
                    }
                }//se o país for diferente de BR, calcula o frete de acordo com o peso total e se a entrega é expressa




//Validacao de pagamento
                if (formaPagamento == "CARTAO")
                {
                    if (parcelas > 1 && parcelas <= 6)
                    {
                        interestValue = subtotalValue * 0.02;
                    }
                    else if (parcelas > 6)
                    {
                        interestValue = subtotalValue * 0.05;
                    }
                }//se a forma de pagamento for CARTAO, calcula os juros de acordo com o numero de parcelas

                else if (formaPagamento == "BOLETO")
                {
                    discountValue = discountValue + 5;
                }//se a forma de pagamento for BOLETO, adiciona 5 ao desconto
                else if (formaPagamento == "PIX")
                {
                    discountValue = discountValue + 10;
                }//se a forma de pagamento for PIX, adiciona 10 ao desconto
                else if (formaPagamento == "DINHEIRO")
                {
                    discountValue = discountValue + 0;
                }//se a forma de pagamento for DINHEIRO, não tem desconto
                else
                {
                    returnMessage += "Forma de pagamento inválida\n";
                    hasError = true;
                }//se a forma de pagamento for diferente das formas válidas, é inválida

                totalValue = subtotalValue - discountValue + shippingValue + interestValue;

                if (totalValue < 0)
                {
                    totalValue = 0;
                }//se o total for menor que 0, o total é 0

                if (subtotalValue > 1000)
                {
                    returnMessage += "Pedido de alto valor\n";
                }//se o subtotal for maior que 1000, é um pedido de alto valor

                if (subtotalValue > 5000 && tipoCliente == "NOVO")
                {
                    returnMessage += "Pedido suspeito para cliente novo\n";
                }//se o subtotal for maior que 5000 e o tipo de cliente for NOVO, é um pedido suspeito para cliente novo

                if (formaPagamento == "BOLETO" && subtotalValue > 3000)
                {
                    returnMessage += "Pedido com boleto acima do limite recomendado\n";
                }//se a forma de pagamento for BOLETO e o subtotal for maior que 3000, é um pedido com boleto acima do limite recomendado

                if (pais != "BR" && subtotalValue < 100)
                {
                    returnMessage += "Pedido internacional abaixo do valor mínimo recomendado\n";
                }//se o país for diferente de BR e o subtotal for menor que 100, é um pedido internacional abaixo do valor mínimo recomendado




//Salvar logs e enviar email
                if (salvarLog == true)
                {
                    _logs.Add("Pedido: " + pedidoId);
                    _logs.Add("Cliente: " + nomeCliente);
                    _logs.Add("Subtotal: " + subtotalValue);
                    _logs.Add("Desconto: " + discountValue);
                    _logs.Add("Frete: " + shippingValue);
                    _logs.Add("Juros: " + interestValue);
                    _logs.Add("Total: " + totalValue);
                    _logs.Add("Data: " + DateTime.Now.ToString());
                }//se salvarLog for true, salva os logs do pedido

                if (enviarEmail == true)//se enviarEmail for true, envia o email para o cliente, se o cliente tiver email, caso contrário, informa que o email não foi enviado
                {
                    if (emailCliente != null && emailCliente != "")
                    {
                        returnMessage += "Email enviado para " + emailCliente + "\n";
                    }
                    else
                    {
                        returnMessage += "Email não enviado: cliente sem email\n";
                    }
                }//se enviarEmail for true, envia o email para o cliente, se o cliente tiver email, caso contrário, informa que o email não foi enviado

                returnMessage += "TOTAL_FINAL=" + totalValue + "\n";
            }//se tem erro for false, processa o pedido e calcula o total final

            return returnMessage;
        }//método para processar o pedido, recebe os dados do pedido e retorna o resultado do processamento

        public List<string> ObterLogs()
        {
            return _logs;
        }//método para obter os logs do processamento dos pedidos
    }//classe para processar os pedidos, contém os métodos para processar o pedido e obter os logs


//classe para representar um item do pedido, contém as propriedades do item
    public class ItemPedido
    {
        public string Nome;
        public string Categoria;
        public int Quantidade;
        public double PrecoUnitario;
    }//classe para representar um item do pedido, contém as propriedades do item
}//namespace para organizar as classes relacionadas ao sistema legado de pedidos