import {Inject, Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Observable} from "rxjs";
import {Wallet} from "../models/Wallet";
import {Transaction} from "../models/Transaction";

const httpOptions = {
  headers: new HttpHeaders({'Content-type': 'application/json'})
}

@Injectable({
  providedIn: 'root'
})
export class WalletService {

  baseUrl: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  getWallets(): Observable<Wallet[]> {
    return this.http.get<Wallet[]>(`${this.baseUrl}api/wallets`);
  }

  getWallet(id: string, from: string, to: string): Observable<Wallet> {
    return this.http.get<Wallet>(`${this.baseUrl}api/wallets/${id}/${from}/${to}`);
  }

  addWallet(wallet: Wallet): Observable<Wallet> {
    const newWallet = {
      Location: wallet.location,
      Type: wallet.type,
      Owner: wallet.owner,
      AccountNumber: wallet.accountNumber,
      Currency: wallet.currency
    }

    return this.http.post<Wallet>(`${this.baseUrl}api/wallets`, newWallet, httpOptions);
  }

  updateWallet(walletId: string, wallet: Wallet): Observable<Wallet> {
    const newWallet = {
      Location: wallet.location,
      Type: wallet.type,
      Owner: wallet.owner,
      AccountNumber: wallet.accountNumber,
      Currency: wallet.currency
    }

    return this.http.put<Wallet>(`${this.baseUrl}api/wallets/${walletId}`, newWallet, httpOptions);
  }

  deleteWallet(id: string): Observable<any> {
    return this.http.delete(`${this.baseUrl}api/wallets/${id}`);
  }

  createTransaction(transaction: Transaction, walletId: string, categoryId: string) {
    const newTransaction = {
      Type: transaction.type.valueOf(),
      WalletId: walletId,
      Notice: transaction.notice,
      Price: Number(transaction.price),
      CategoryId: categoryId,
      DateOfTransaction: transaction.dateOfTransaction
    }

    return this.http.post(`${this.baseUrl}api/transactions`, newTransaction, httpOptions);
  }

  deleteTransaction(id: string): Observable<any> {
    return this.http.delete(`${this.baseUrl}api/transactions/${id}`);
  }
}
