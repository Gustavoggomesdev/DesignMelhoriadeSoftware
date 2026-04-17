# Sistema de Processamento de Pedidos

## Regras de negocios implicitas

- Calculo de Juros;
- Calculo de Desconto;
- Calculo de Frete;
- Verificar se o frete é calculado com taxa nacional ou internacional de acordo com peso;
- Calculo de Juros;
- Geração Alertas:
- Se o pedido for maior que 1000;
- Recomendar o produto caso ele Não seja internacional e abaixo e de 100;
- Caso o Pedido seja acima de 5000 e o cliente for novo a compra precisará de validação.
- Se o total for negativo o valor é atribuído a 0;
- Quantidade de itens é válida;
- O cliente pode definir a forma de pagamentol
- Durante pagamento é validado se o limite do cartão de crédito está bloqueado ou não;
- É necessária a validação do E-mail do Cliente;
- Cada tipo de cliente está atrelado a um nivel de desconto;
- O valor total se altera de acordo com a categoria da oferta e produto;
- Limite de 3000 para Pagamento de no boleto;

---

### 1. Código Legado
Classe monolítica contendo:
- validações
- cálculos
- regras de negócio
- logs
- envio de email

Problemas identificados:
- Método extremamente grande
- Baixa coesão
- Alto acoplamento
- Uso de strings mágicas
- Difícil manutenção

---

### 2. Redocumentação
Foi realizada:
- melhoria de nomes de variáveis
- inclusão de comentários de regras de negócio
- organização lógica

---

### 3. Refatoração

separação em camadas:

- Domain → entidades do sistema
- Services → regras de negócio isoladas

Melhorias:
- Aplicação do princípio SRP (Single Responsibility)
- Redução de complexidade
- Código testável
- Clareza nas regras de negócio

---

## Engenharia Reversa

Durante a análise do código, foram identificadas entidades implícitas:

- Pedido
- Cliente
- ItemPedido
- Pagamento
- Entrega
- Cupom

Tais entidades foram extraídas e modeladas corretamente.

---
