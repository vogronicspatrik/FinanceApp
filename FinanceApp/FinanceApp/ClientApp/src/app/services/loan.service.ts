import {Inject, Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Observable} from "rxjs";
import {Loan} from "../models/Loan";

const httpOptions = {
  headers: new HttpHeaders({'Content-type': 'application/json'})
}

@Injectable({
  providedIn: 'root'
})
export class LoanService {

  baseUrl: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  getLoans(): Observable<Loan[]> {
    return this.http.get<Loan[]>(`${this.baseUrl}api/loans`);
  }

  addLoan(loan: Loan): Observable<Loan> {
    const newLoan = {
      name: loan.name,
      otherParty: loan.otherParty,
      amount: loan.amount,
      currency: loan.currency,
      termInMonths: loan.termInMonths,
      interestRate: loan.interestRate
    }

    return this.http.post<Loan>(`${this.baseUrl}api/loans`, newLoan, httpOptions);
  }

  deleteLoan(id: string): Observable<any> {
    return this.http.delete(`${this.baseUrl}api/loans/${id}`);
  }

  payInstallment(id: string): Observable<Loan> {
    return this.http.put<Loan>(`${this.baseUrl}api/loans/${id}`, undefined, httpOptions);
  }
}

