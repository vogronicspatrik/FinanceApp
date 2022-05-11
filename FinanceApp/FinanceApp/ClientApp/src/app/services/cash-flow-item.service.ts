import {Inject, Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Observable} from "rxjs";
import {CashFlowItem} from "../models/CashFlowItem";
import {CurrencyType} from "../models/Currency";
import {BondReturnInterval} from "../models/Bond";
import {RecurrencyPeriodType} from "../models/RecurrencyPeriod";

const httpOptions = {
  headers: new HttpHeaders({'Content-type': 'application/json'})
}

@Injectable({
  providedIn: 'root'
})
export class CashFlowItemService {

  baseUrl: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  getCashFlowItem(): Observable<CashFlowItem[]> {
    return this.http.get<CashFlowItem[]>(`${this.baseUrl}api/cashflowitems`);
  }

  addCashFlowItem(cashFlowItem: CashFlowItem): Observable<CashFlowItem> {
    const newCashFlowItem = {
      name: cashFlowItem.name,
      type: cashFlowItem.type,
      otherParty: cashFlowItem.otherParty,
      amount: cashFlowItem.amount,
      currency: cashFlowItem.currency,
      recurrencyPeriod: cashFlowItem.recurrencyPeriod
    }

    return this.http.post<CashFlowItem>(`${this.baseUrl}api/cashflowitems`, newCashFlowItem, httpOptions);
  }

  deleteCashFlowItem(id: string): Observable<any> {
    return this.http.delete(`${this.baseUrl}api/cashflowitems/${id}`);
  }

  getSummary(): Observable<{ currency: CurrencyType, period: RecurrencyPeriodType, incomeSummary: number, expenseSummary: number }[]> {
    return this.http.get<{ currency: CurrencyType, period: RecurrencyPeriodType, incomeSummary: number, expenseSummary: number }[]>(`${this.baseUrl}api/cashflowitems/GetSummary`);
  }
}
