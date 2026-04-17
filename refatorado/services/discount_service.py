from domain.enums import CustomerType

class DiscountService:

    def calculate(self, order):
        subtotal = order.subtotal
        discount = 0


        if order.customer.customer_type == CustomerType.VIP:
            discount = subtotal * 0.15
        elif order.customer.customer_type == CustomerType.PREMIUM:
            discount = subtotal * 0.10
        elif order.customer.customer_type == CustomerType.NORMAL:
            discount = subtotal * 0.02

        if order.coupon:
            if order.coupon.code == "DESC10":
                discount += subtotal * 0.10
            elif order.coupon.code == "DESC20":
                discount += subtotal * 0.20
            elif order.coupon.code == "VIP50" and order.customer.customer_type == CustomerType.VIP:
                discount += 50

        return discount