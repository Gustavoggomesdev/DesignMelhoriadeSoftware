class Customer:
    def __init__(self, name, email, customer_type, is_blocked=False):
        self.name = name
        self.email = email
        self.customer_type = customer_type
        self.is_blocked = is_blocked