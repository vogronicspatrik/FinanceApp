import {Category} from "./Category";

export class Transaction {
  constructor(id: string, type: TransactionType, notice: string, price: number, category: Category, dateOfTransaction: string) {
    this.id = id;
    this.type = type;
    this.notice = notice;
    this.price = price;
    this.category = category;
    this.dateOfTransaction = dateOfTransaction;
  }

  id: string;
  type: TransactionType;
  notice: string;
  price: number;
  category: Category;
  dateOfTransaction: string;
}

export enum TransactionType {
  Expense = "EXPENSE",
  Income = "INCOME"
}
