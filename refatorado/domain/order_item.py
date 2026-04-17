class OrderItem:
    def __init__(self, name, category, quantity, unit_price):
        self.name = name
        self.category = category
        self.quantity = quantity
        self.unit_price = unit_price

    def calculate_total(self):
        return self.quantity * self.unit_price