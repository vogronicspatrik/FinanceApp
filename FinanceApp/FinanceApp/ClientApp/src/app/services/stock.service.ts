import {Inject, Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Observable} from "rxjs";
import {Stock} from "../models/Stock";

const httpOptions = {
  headers: new HttpHeaders({'Content-type': 'application/json'})
}

@Injectable({
  providedIn: 'root'
})
export class StockService {

  baseUrl: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  getStocks(): Observable<Stock[]> {
    return this.http.get<Stock[]>(`${this.baseUrl}api/stocks`);
  }

  getStock(id: string): Observable<Stock> {
    return this.http.get<Stock>(`${this.baseUrl}api/stocks/${id}`);
  }

  addStock(stock: Stock): Observable<Stock> {
    const newStock = {
      Symbol: stock.symbol,
      PurchaseTime: stock.purchaseTime,
      CurrencyCode: stock.currencyCode,
      ValueAtPurchase: stock.valueAtPurchase,
      Amount: stock.amount
    }

    return this.http.post<Stock>(`${this.baseUrl}api/stocks`, newStock, httpOptions);
  }

  updateStock(stockId: string, stock: Stock): Observable<Stock> {
    const newStock = {
      Symbol: stock.symbol,
      PurchaseTime: stock.purchaseTime,
      CurrencyCode: stock.currencyCode,
      ValueAtPurchase: stock.valueAtPurchase,
      Amount: stock.amount
    }

    return this.http.put<Stock>(`${this.baseUrl}api/stocks/${stockId}`, newStock, httpOptions);
  }

  deleteStock(id: string): Observable<any> {
    return this.http.delete(`${this.baseUrl}api/stocks/${id}`);
  }

  getCurrentPrice(id: string): Observable<number> {
    return this.http.get<number>(`${this.baseUrl}api/stocks/currentPrice/${id}`);
  }
}
