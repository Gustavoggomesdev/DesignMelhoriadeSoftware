class ShippingService:

    def calculate(self, order):
        weight = order.delivery.weight
        country = order.delivery.country
        shipping_cost = 0

        if country == "BR":
            if weight <= 1:
                shipping_cost = 10
            elif weight <= 5:
                shipping_cost = 25
            elif weight <= 10:
                shipping_cost = 40
            else:
                shipping_cost = 70

            if order.delivery.is_express:
                shipping_cost += 30
        else:
            if weight <= 1:
                shipping_cost = 50
            elif weight <= 5:
                shipping_cost = 80
            else:
                shipping_cost = 120

            if order.delivery.is_express:
                shipping_cost += 70

        return shipping_cost