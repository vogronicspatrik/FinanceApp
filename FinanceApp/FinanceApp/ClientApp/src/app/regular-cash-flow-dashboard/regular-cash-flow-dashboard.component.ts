import {Component, OnInit} from '@angular/core';
import {CashFlowItemService} from "../services/cash-flow-item.service";
import {CashFlowDirection, CashFlowItem} from "../models/CashFlowItem";
import {LoanService} from "../services/loan.service";
import {Loan} from "../models/Loan";
import {CurrencyType} from "../models/Currency";
import {RecurrencyPeriodType} from "../models/RecurrencyPeriod";

@Component({
  selector: 'app-regular-cash-flow-dashboard',
  templateUrl: './regular-cash-flow-dashboard.component.html',
  styleUrls: ['./regular-cash-flow-dashboard.component.css']
})
export class RegularCashFlowDashboardComponent implements OnInit {
  incomeItems: CashFlowItem[] = [];
  costItems: CashFlowItem[] = [];
  loans: Loan[] = [];

  summary: { currency: CurrencyType; periods: { period: RecurrencyPeriodType, incomeSummary: number, expenseSummary: number }[] }[] = [];
  openSummary: boolean[] = [];

  constructor(private cashFlowItemService: CashFlowItemService,
              private loanService: LoanService) {
  }

  ngOnInit(): void {
    this.cashFlowItemService.getCashFlowItem().subscribe(cashFlowItems => {
      this.incomeItems = cashFlowItems.filter(item => item.type == CashFlowDirection.Income);
      this.costItems = cashFlowItems.filter(item => item.type == CashFlowDirection.Cost);
    });

    this.loanService.getLoans().subscribe(loans => {
      this.loans = loans;
    });

    this.loadSummary();
  }

  loadSummary() {
    this.cashFlowItemService.getSummary().subscribe(summary => {
      this.summary = [];
      summary.forEach(item => {
        if (this.summary.length == 0 || this.summary[this.summary.length - 1].currency != item.currency) {
          this.summary.push({currency: item.currency, periods: []});
        }
        this.summary[this.summary.length - 1].periods.push({
          period: item.period,
          incomeSummary: item.incomeSummary,
          expenseSummary: item.expenseSummary
        });
      });
    });
  }

  updateItems($event: any) {
    this.cashFlowItemService.getCashFlowItem().subscribe(cashFlowItems => {
      this.incomeItems = cashFlowItems.filter(item => item.type == CashFlowDirection.Income);
      this.costItems = cashFlowItems.filter(item => item.type == CashFlowDirection.Cost);
    });

    this.loadSummary();
  }

  deleteItem($event: string) {
    this.incomeItems = this.incomeItems.filter(item => item.id != $event);
    this.costItems = this.costItems.filter(item => item.id != $event);
    this.loadSummary();
  }

  updateLoans($event: any) {
    this.loanService.getLoans().subscribe(loans => {
      this.loans = loans;
    })
  }

  deleteLoan($event: string) {
    this.loans = this.loans.filter(loan => loan.id != $event);
  }
}
