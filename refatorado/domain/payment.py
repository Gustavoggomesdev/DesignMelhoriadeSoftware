class Payment:
    def __init__(self, method, installments=1):
        self.method = method
        self.installments = installments