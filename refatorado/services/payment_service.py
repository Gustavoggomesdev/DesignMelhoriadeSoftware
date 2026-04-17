from domain.enums import PaymentMethod


class PaymentService:

    def calculate(self, order):
        subtotal = order.subtotal
        interest = 0
        discount = 0

        if order.payment.method == PaymentMethod.CARD:
            if 1 < order.payment.installments <= 6:
                interest = subtotal * 0.02
            elif order.payment.installments > 6:
                interest = subtotal * 0.05

        elif order.payment.method == PaymentMethod.BOLETO:
            discount += 5

        elif order.payment.method == PaymentMethod.PIX:
            discount += 10

        return interest, discount
