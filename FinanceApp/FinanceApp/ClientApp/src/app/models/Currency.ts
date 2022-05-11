import {Wallet} from "./Wallet";
import {Transaction, TransactionType} from "./Transaction";

export class Currency {
  public static getBalance(wallet: Wallet): string {
    switch (wallet.currency) {
      case CurrencyType.EUR:
        return "\u20AC " + wallet.balance;
      case CurrencyType.HUF:
        return wallet.balance + " HUF";
    }
  }

  public static getPrice(transaction: Transaction, currency: CurrencyType): string {
    let output;
    switch (currency) {
      case CurrencyType.EUR:
        output = "\u20AC " + transaction.price;
        break;
      case CurrencyType.HUF:
        output = transaction.price + " HUF";
        break;
    }

    return transaction.type == TransactionType.Income ? "+ " + output : "- " + output;
  }

  public static getCurrency(value: number, currency: CurrencyType): string {
    let output;
    switch (currency) {
      case CurrencyType.EUR:
        output = "\u20AC " + value;
        break;
      case CurrencyType.HUF:
        output = value + " HUF";
        break;
    }

    return output;
  }
}

export enum CurrencyType {
  EUR = "EUR",
  HUF = "HUF"
}
