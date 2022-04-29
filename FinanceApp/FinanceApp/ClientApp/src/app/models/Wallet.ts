import {CurrencyType} from "./Currency";
import {Transaction} from "./Transaction";

export class Wallet {

  constructor(id: string, location: string, type: string, owner: string, accountNumber: string, balance: number, currency: CurrencyType, synced: boolean) {
    this.id = id;
    this.location = location;
    this.type = type
    this.owner = owner;
    this.accountNumber = accountNumber;
    this.balance = balance;
    this.currency = currency;
    this.synced = synced;
  }

  id: string;
  location: string;
  type: string;
  owner: string;
  accountNumber: string;
  balance: number;
  currency: CurrencyType;
  synced: boolean;
  transactions: Transaction[] = [];
}
