class Order:
    def __init__(self, order_id, customer, items, delivery, payment, coupon=None):
        self.order_id = order_id
        self.customer = customer
        self.items = items
        self.delivery = delivery
        self.payment = payment
        self.coupon = coupon

        self.subtotal = 0
        self.total = 0