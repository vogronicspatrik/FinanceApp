import {CurrencyType} from "./Currency";

export class ValuableItem {
  constructor(id: string, name: string, originalValue: number, currency: CurrencyType, dateOfPurchasing: string, amortizationRatePerYear: number) {
    this.id = id;
    this.name = name;
    this.originalValue = originalValue;
    this.currency = currency;
    this.dateOfPurchasing = dateOfPurchasing;
    this.amortizationRatePerYear = amortizationRatePerYear;
  }

  id: string;
  name: string;
  originalValue: number;
  currency: CurrencyType;
  dateOfPurchasing: string;
  amortizationRatePerYear: number;
  currentValue: number = 0;
}
