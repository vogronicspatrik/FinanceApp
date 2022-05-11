import {Pipe, PipeTransform} from '@angular/core';

@Pipe({name: 'recurrencyPeriodDisplay'})
export class RecurrencyPeriodPipe implements PipeTransform {
  transform(value: RecurrencyPeriodType): string {
    switch (value) {
      case RecurrencyPeriodType.Month1:
        return 'month'
      case RecurrencyPeriodType.Month3:
        return '3 month'
      case RecurrencyPeriodType.Month6:
        return 'half year'
      case RecurrencyPeriodType.Month12:
        return 'year'
    }
  }
}

export enum RecurrencyPeriodType {
  Month1 = 'Month1',
  Month3 = 'Month3',
  Month6 = 'Month6',
  Month12 = 'Month12'
}
