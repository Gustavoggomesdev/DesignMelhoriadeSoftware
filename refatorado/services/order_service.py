from services.desconto_service import DescontoService
from services.frete_service import FreteService
from services.pagamento_service import PagamentoService

class PedidoService:

    def __init__(self):
        self.desconto_service = DescontoService()
        self.frete_service = FreteService()
        self.pagamento_service = PagamentoService()

    def processar(self, pedido):

        self.validar(pedido)

        pedido.subtotal = sum(item.calcular_total() for item in pedido.itens)

        for item in pedido.itens:
            if item.categoria == "ALIMENTO":
                pedido.subtotal += 2
            elif item.categoria == "IMPORTADO":
                pedido.subtotal += 5

        desconto = self.desconto_service.calcular(pedido)
        frete = self.frete_service.calcular(pedido)
        juros, desconto_pagamento = self.pagamento_service.calcular(pedido)

        total = pedido.subtotal - (desconto + desconto_pagamento) + frete + juros

        pedido.total = max(total, 0)

        return pedido

    def validar(self, pedido):
        if pedido.id <= 0:
            raise Exception("Pedido inválido")

        if not pedido.cliente.nome:
            raise Exception("Cliente sem nome")

        if pedido.cliente.bloqueado:
            raise Exception("Cliente bloqueado")

        if not pedido.itens:
            raise Exception("Pedido sem itens")