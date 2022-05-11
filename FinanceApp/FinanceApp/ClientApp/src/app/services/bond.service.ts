import {Inject, Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Observable} from "rxjs";
import {Bond, BondReturnInterval} from "../models/Bond";
import {CurrencyType} from "../models/Currency";

const httpOptions = {
  headers: new HttpHeaders({'Content-type': 'application/json'})
}

@Injectable({
  providedIn: 'root'
})
export class BondService {

  baseUrl: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  getBonds(): Observable<Bond[]> {
    return this.http.get<Bond[]>(`${this.baseUrl}api/bonds`);
  }

  addBond(bond: Bond): Observable<Bond> {
    const newBond = {
      bondName: bond.bondName,
      purchaseDate: bond.purchaseDate,
      faceValue: bond.faceValue,
      purchaseValue: bond.purchaseValue,
      currency: bond.currency,
      maturityDate: bond.maturityDate,
      count: bond.count,
      returnRate: bond.returnRate,
      returnInterval: bond.returnInterval
    }

    return this.http.post<Bond>(`${this.baseUrl}api/bonds`, newBond, httpOptions);
  }

  deleteBond(id: string): Observable<any> {
    return this.http.delete(`${this.baseUrl}api/bonds/${id}`);
  }

  getSummary(): Observable<{ currency: CurrencyType, interval: BondReturnInterval, summary: number }[]> {
    return this.http.get<{ currency: CurrencyType, interval: BondReturnInterval, summary: number }[]>(`${this.baseUrl}api/bonds/GetSummary`);
  }
}
