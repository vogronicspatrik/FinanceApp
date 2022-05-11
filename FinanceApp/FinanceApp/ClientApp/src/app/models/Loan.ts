import {CurrencyType} from "./Currency";

export class Loan {
  constructor(id: string, name: string, otherParty: string, amount: number, currency: CurrencyType, termInMonths: number, interestRate: number) {
    this.id = id;
    this.name = name;
    this.otherParty = otherParty;
    this.amount = amount;
    this.currency = currency;
    this.interestRate = interestRate;
    this.termInMonths = termInMonths;
  }

  id: string;
  name: string;
  otherParty: string;
  amount: number;
  currency: CurrencyType;
  termInMonths: number;
  interestRate: number;
  monthlyInstallment: number = 0;
  remainingMonths: number = 0;
  remainingAmount: number = 0;
  loanSchedule: LoanSchedule[] = [];

}

export class LoanSchedule {
  index: number = 0;
  startingBalance: number = 0;
  interest: number = 0;
  principal: number = 0;
  endingBalance: number = 0;
}
