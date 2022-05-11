import {CurrencyType} from "./Currency";
import {Pipe, PipeTransform} from "@angular/core";
import {RecurrencyPeriodType} from "./RecurrencyPeriod";

export class Bond {
  constructor(id: string, bondName: string, purchaseDate: string, faceValue: number, purchaseValue: number, currency: CurrencyType, maturityDate: string, count: number, returnRate: number, returnInterval: BondReturnInterval) {
    this.id = id;
    this.bondName = bondName;
    this.purchaseDate = purchaseDate;
    this.faceValue = faceValue;
    this.purchaseValue = purchaseValue;
    this.currency = currency;
    this.maturityDate = maturityDate;
    this.count = count;
    this.returnRate = returnRate;
    this.returnInterval = returnInterval;
  }

  id: string;
  bondName: string;
  purchaseDate: string;
  faceValue: number;
  purchaseValue: number;
  currency: CurrencyType;
  maturityDate: string;
  count: number;
  returnRate: number;
  returnInterval: BondReturnInterval;
  yield: number = 0;
}

@Pipe({name: 'returnIntervalDisplay'})
export class ReturnIntervalPipe implements PipeTransform {
  transform(value: BondReturnInterval): string {
    switch (value) {
      case BondReturnInterval.HalfYear:
        return "semiannual";
      case BondReturnInterval.Year:
        return "annual";
    }
  }
}

export enum BondReturnInterval {
  Year = 'YEAR',
  HalfYear = 'HALF_YEAR'
}
