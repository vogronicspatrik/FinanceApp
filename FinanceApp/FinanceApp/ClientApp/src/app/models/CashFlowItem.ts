import {CurrencyType} from "./Currency";
import {RecurrencyPeriodType} from "./RecurrencyPeriod";

export class CashFlowItem {
  constructor(id: string, name: string, otherParty: string, type: CashFlowDirection, amount: number, currency: CurrencyType, recurrencyPeriod: RecurrencyPeriodType) {
    this.id = id;
    this.name = name;
    this.otherParty = otherParty;
    this.type = type;
    this.amount = amount;
    this.currency = currency;
    this.recurrencyPeriod = recurrencyPeriod;
  }

  id: string;
  name: string;
  otherParty: string;
  type: CashFlowDirection;
  amount: number;
  currency: CurrencyType;
  recurrencyPeriod: RecurrencyPeriodType;
}

export enum CashFlowDirection {
  Income = 'INCOME',
  Cost = 'COST'
}
