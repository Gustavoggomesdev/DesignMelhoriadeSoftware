from enum import Enum

class CustomerType(Enum):
    VIP = "VIP"
    PREMIUM = "PREMIUM"
    NORMAL = "NORMAL"
    NEW = "NOVO"

class PaymentMethod(Enum):
    CARD = "CARTAO"
    BOLETO = "BOLETO"
    PIX = "PIX"
    CASH = "DINHEIRO"

class ItemCategory(Enum):
    FOOD = "ALIMENTO"
    IMPORTED = "IMPORTADO"