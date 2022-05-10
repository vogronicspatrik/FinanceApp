import {CurrencyType} from "./Currency";

export class Stock {

  constructor(id: string, userId: string, symbol: string, purchaseTime: string, currency: CurrencyType, valueAtPurchase: number, amount: number) {
    this.id = id;
    this.userId = userId;
    this.symbol = symbol;
    this.purchaseTime = purchaseTime;
    this.currency = currency;
    this.valueAtPurchase = valueAtPurchase;
    this.amount = amount;
  }

  id: string;
  userId: string;
  symbol: string;
  purchaseTime: string;
  currency: CurrencyType;
  valueAtPurchase: number;
  amount: number;
}