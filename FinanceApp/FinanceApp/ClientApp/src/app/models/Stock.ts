import {CurrencyType} from "./Currency";

export class Stock {

  constructor(id: string, userId: string, symbol: string, purchaseTime: string, currency: CurrencyType, valueAtPurchase: number, amount: number, currentValue: number) {
    this.id = id;
    this.userId = userId;
    this.symbol = symbol;
    this.purchaseTime = purchaseTime;
    this.currencyCode = currency;
    this.valueAtPurchase = valueAtPurchase;
    this.amount = amount;
    this.currentValue = currentValue;
  }

  id: string;
  userId: string;
  symbol: string;
  purchaseTime: string;
  currencyCode: CurrencyType;
  valueAtPurchase: number;
  amount: number;
  currentValue: number;
}
