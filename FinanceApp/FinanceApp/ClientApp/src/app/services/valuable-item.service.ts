import {Inject, Injectable} from '@angular/core';
import {Observable} from "rxjs";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {ValuableItem} from "../models/ValuableItem";
import {CurrencyType} from "../models/Currency";

const httpOptions = {
  headers: new HttpHeaders({'Content-type': 'application/json'})
}

@Injectable({
  providedIn: 'root'
})
export class ValuableItemService {

  baseUrl: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  getValuableItems(): Observable<ValuableItem[]> {
    return this.http.get<ValuableItem[]>(`${this.baseUrl}api/valuableassets`);
  }

  addValuableItem(valuableItem: ValuableItem): Observable<ValuableItem> {
    const newValuableItem = {
      name: valuableItem.name,
      originalValue: valuableItem.originalValue,
      currency: valuableItem.currency,
      dateOfPurchasing: valuableItem.dateOfPurchasing,
      amortizationRatePerYear: valuableItem.amortizationRatePerYear
    }

    return this.http.post<ValuableItem>(`${this.baseUrl}api/valuableassets`, newValuableItem, httpOptions);
  }

  deleteValuableItem(id: string): Observable<any> {
    return this.http.delete(`${this.baseUrl}api/valuableassets/${id}`);
  }

  getSummary(): Observable<{currency: CurrencyType, withAmortization: number, withoutAmortization: number}[]> {
    return this.http.get<{currency: CurrencyType, withAmortization: number, withoutAmortization: number}[]>(`${this.baseUrl}api/valuableassets/GetSummary`);
  }
}
